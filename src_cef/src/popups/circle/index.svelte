<script>
    import  './assets/css/iconscircle.css';
    import  './assets/css/circle.sass';
    import  './assets/css/main.css';
    import { executeClient } from 'api/rage'
    import keys from 'store/keys'
    export let popupData;
    import { getIcon } from './assets/GetImgs.js';
    
    $: if (popupData && typeof popupData === "string") 
        popupData = JSON.parse (popupData)
    

    const updateCategory = (json) => {
        popupData = JSON.parse (json)
    }
    window.events.addEvent("cef.circle.updateCategory", updateCategory);

    import { onDestroy } from 'svelte'

    onDestroy(() => {
        window.events.removeEvent("cef.circle.updateCategory", updateCategory);
    });


    const    
        prefix = "circle-";


    let drawname = "Назад"
    const OnHovered = (name, isBack = false) => {
        drawname = name;
        executeClient ("client.circle.isBack", isBack);
    }

    const onCircleClick = (func, index = 0) => {
        executeClient ("client.circle.select", func, index);
    } 

    const ontest = (index, max) => {
        switch (max) {
            case 1:
                return 1;
            case 2:
                switch (index) {
                    case 0:
                        return 1;
                    case 1:
                        return 5;
                }
                return;
            case 3:
                switch (index) {
                    case 0:
                        return 1;
                    case 1:
                        return 3;
                    case 2:
                        return 5;
                }
                return;
            case 4:
                switch (index) {
                    case 0:
                        return 1;
                    case 1:
                        return 3;
                    case 2:
                        return 5;
                    case 3:
                        return 7;
                }
                return;
            case 5:
                switch (index) {
                    case 0:
                        return 1;
                    case 1:
                        return 2;
                    case 2:
                        return 4;
                    case 3:
                        return 6;
                    case 4:
                        return 8;
                }
                return;
            case 6:
                switch (index) {
                    case 0:
                        return 1;
                    case 1:
                        return 2;
                    case 2:
                        return 4;
                    case 3:
                        return 5;
                    case 4:
                        return 6;
                    case 5:
                        return 8;
                }
                return;
        
        }
        return index + 1;
    }

    const defaultCircle__closeWidth = 280;
    const defaultCircle__closeHeight = 280;

    const initCircle = (node) => {
        node = node.getBoundingClientRect();
        if (node) {
            const percentWidth = (node.width * 100 / defaultCircle__closeWidth) / 100;
            const percentHeight = (node.height * 100 / defaultCircle__closeHeight) / 100;
            executeClient ("client.circle.initCircle", percentWidth, percentHeight);
        }
    }



    const handleKeyUp = (event) => {
        const { keyCode } = event;

        for(let i = 0; i < 8; i++) {
            if (49 + i == keyCode) {
                onCircleClick (popupData [i].index)
                return;
            }
        }
    }


    const handleKeyDown = (event) => {
        const { keyCode } = event;

        if (keyCode === $keys[31])
            return onCircleClick ("back");
    }
    
    const handleMouseUp = (event) => {
        const { which } = event;

        if (which === 3)
            onCircleClick ("back");
    }
 
</script>

<svelte:window on:keydown={handleKeyDown} on:keyup={handleKeyUp} on:mouseup={handleMouseUp} />

<div class="gta5devgmenu">
    {#if popupData.length === 2 || popupData.length === 8}
        <div class="list1">
            {#each popupData as data, index}
                <div class="block" on:keypress={() => {}} on:click={() => onCircleClick (data.func, data.index)} on:mouseenter={() => OnHovered (data.name)} on:mouseleave={() => OnHovered ("Back")}>
                    <p>{index + 1}</p>
                    <img src="{getIcon(data.name)}" alt=""> 
                     
                    <span>{data.name}</span>
                </div>
            {/each}
        </div>
    {/if}
    {#if popupData.length === 1 || popupData.length === 3 || popupData.length === 5 || popupData.length === 7}
        <div class="list2">
            {#each popupData as data, index}
                <div class="block" on:keypress={() => {}} on:click={() => onCircleClick (data.func, data.index)} on:mouseenter={() => OnHovered (data.name)} on:mouseleave={() => OnHovered ("Back")}>
                    <p>{index + 1}</p>
                    <img src="{getIcon(data.name)}" alt="">
                    <span>{data.name}</span>
                </div>
            {/each}
        </div>
    {/if}
    {#if popupData.length === 4 || popupData.length === 6}
        <div class="list3">
            {#each popupData as data, index}
                <div class="block" on:keypress={() => {}} on:click={() => onCircleClick (data.func, data.index)} on:mouseenter={() => OnHovered (data.name)} on:mouseleave={() => OnHovered ("Back")}>
                    <p>{index + 1}</p>
                    <img src="{getIcon(data.name)}" alt="">
                    <span>{data.name}</span>
                </div>
            {/each}
        </div>
    {/if}
</div>

<div class="circle" style="display: none;">
    <div class="circle__close"  use:initCircle on:mouseenter={() => OnHovered ('Back', true)} on:mouseleave={() => OnHovered ('Back')} on:keypress={() => {}} on:click={() => onCircleClick ("back")}>
        <div class="box-column">
            <div class="circle__image" class:active={drawname !== "Back"}></div>
            <div class="circle__text">{drawname}</div>
        </div>
    </div>
    <div class="center">
        {#each popupData as data, index}
        <li on:keypress={() => {}} on:click={() => onCircleClick (data.func, data.index)} on:mouseenter={() => OnHovered (data.name)} on:mouseleave={() => OnHovered ("Back")} class="contents child{ontest (index, popupData.length)}">
            <span class="icons-circle {prefix}{data.func}" />
            <div>{data.name}</div>
            <div class="contents__index">{index + 1}</div>
        </li>
    {/each}
    </div>
</div>