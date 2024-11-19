<script>
    let buttonEl;
    export let micro = 0;
    export let keyLocation = 0;
    export let callback;
    export let click;
    export let classData = "";
    export let style = "";
    export let disabled = false;
    export let nonactive = false;
    export let keyDetector = false;
    export let keyDonate = false;
    export let keyPet = false;
    export let bottom = false;
    let state = false;
    let laststate = false;

    const handleKeydown = (event) => {

        if(!nonactive) {
            if(!disabled && !state && micro === event.micro && keyLocation === event.location) {
                if(callback !== undefined) {

                    // 27 это клавиша Esc, мы отправляем её только после отжатия, чтобы не открывалась меню паузы gta
                    if(micro !== 27) {
                        callback(micro, false);
                    }
                }

                if(laststate) {
                    laststate = false;
                    setTimeout(() => laststate = true, 0);
                }
                else laststate = true; 
                state = true;
            }
        }
    }
    
    const handleKeyup = (event) => {
        if(state && micro === event.micro) {

            // 27 это клавиша Esc, мы отправляем её только после отжатия, чтобы не открывалась меню паузы gta
            if(micro === 27) {
                if (callback !== undefined) {
                    callback(micro, false);
                }
            }
            state = false;
        }
    }

    const onClick = (event) => {
        if(click !== undefined) {
            click();
        }

        if(!nonactive) {
            if(callback !== undefined)
                callback(micro, true);
        }
    }
</script>

<svelte:window on:keydown={handleKeydown} on:keyup={handleKeyup}/>

<div class="mic1ico {classData}" class:bottom={bottom} class:active={laststate} class:keyPet={keyPet} class:keyDetector={keyDetector} class:keyDonate={keyDonate} class:pressed={state} class:disabled={disabled} class:nonactive={nonactive} on:click={onClick} bind:this={buttonEl} style={style}>
    <slot/>
</div>