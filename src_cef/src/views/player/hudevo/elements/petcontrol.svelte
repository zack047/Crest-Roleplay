<script>
    import { executeClient } from 'api/rage'
    import { charIsPet } from 'store/chars'
    import keysName from 'json/keys.js'
    import keys from 'store/keys'
    import { isInputToggled } from 'store/hud'
    import CustomKey from './Key.svelte'
	import router from 'router';
    import { fly } from 'svelte/transition';

    let commandsArray = []
    const pet = 'cat';

    const keyToBind = {
		48: 9,
		49: 0,
		50: 1,
		51: 2,
		52: 3,
		53: 4,
		54: 5,
		55: 6,
		56: 7,
		57: 8,
	}

    let playerData = [];
    let playerEvent = "";

    let isAnimal = false;
    window.hudStore.isAnimal = (value) => isAnimal = value;
	let animalName = "Pet";
	window.hudStore.animalName = (value) => animalName = value;

    const handleKeydown = (event) => {
        if (!$router.PlayerHud)
            return;
        else if ($isInputToggled)
            return;
        else if (!isAnimal)
            return;
        
        const { keyCode } = event;

        for (let i = 0; i < 11; i++) {
            if (keyCode !== (48 + i))
                continue;
            else if (keyToBind [48 + i] === undefined)
                continue;
            const command = playerData.length ? playerData[ keyToBind [48 + i] ] : commandsArray[ keyToBind [48 + i] ];

            if (command) {
                if (playerData.length) {
                    if (!command.isEnd) 
                        executeClient (playerEvent, command.pId)
                    playerData = []
                    playerEvent = "";
                }
                else {
                    executeClient (command.event);
                }
                return
            }
        }
	}

    const SetPlayers = (players, event) => {
        playerData = JSON.parse (players);
        playerEvent = event;
    }

    const SetMenu = (menu) => {
        commandsArray = JSON.parse (menu);
    }
        
    import { onMount, onDestroy } from 'svelte'

    let health = 100;
    const SetHealth = (value) => {
        health = value;
    }

    onMount(() => {
        window.events.addEvent("cef.pet.menu", SetMenu);
        window.events.addEvent("cef.pet.player", SetPlayers);
        window.events.addEvent("cef.pet.health", SetHealth);
    });

    onDestroy(() => {
        window.events.removeEvent("cef.pet.menu", SetMenu);
        window.events.removeEvent("cef.pet.player", SetPlayers);
        window.events.removeEvent("cef.pet.health", SetHealth);
    });
</script>
<svelte:window on:keydown={handleKeydown}/>


{#if $charIsPet}
<div class="petinfopika">
    <div class="infopet">
        <div class="icon">
            <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                <g clip-path="url(#clip0_0_411)">
                <path d="M4.30925 3.46922C5.14122 3.46922 5.81806 2.79238 5.81806 1.96041V1.50881C5.81806 0.676844 5.14122 0 4.30925 0C3.47728 0 2.80044 0.676844 2.80044 1.50881V1.96041C2.80044 2.79234 3.47728 3.46922 4.30925 3.46922ZM9.56019 5.42972C9.56019 6.26169 10.237 6.93853 11.069 6.93853C11.901 6.93853 12.5778 6.26169 12.5778 5.42972V4.73634C12.5778 3.90438 11.901 3.22753 11.069 3.22753C10.237 3.22753 9.56019 3.90438 9.56019 4.73634V5.42972ZM8.26856 3.46922C9.10053 3.46922 9.77738 2.79238 9.77738 1.96041V1.50881C9.77738 0.676844 9.10053 0 8.26856 0C7.43659 0 6.75975 0.676844 6.75975 1.50881V1.96041C6.75975 2.79234 7.43659 3.46922 8.26856 3.46922ZM9.86684 9.26341C9.72309 9.12478 9.55122 8.95772 9.47922 8.89028C8.96462 8.40825 8.6695 7.72741 8.6695 7.02228C8.6695 5.64937 7.55256 4.53241 6.17966 4.53241C4.80672 4.53241 3.68978 5.64937 3.68978 7.02228C3.68978 7.72738 3.39466 8.40825 2.88006 8.89028C2.69619 9.0625 2.38672 9.35784 1.98509 9.74444C1.61734 10.0984 1.40647 10.5941 1.40647 11.1045C1.40647 12.2913 2.37197 13.2567 3.55869 13.2567C4.74541 13.2567 5.71091 12.2913 5.71091 11.1045H6.64841C6.64841 12.2608 7.56506 13.2066 8.70978 13.2544C8.45747 12.7802 8.32034 12.2463 8.32034 11.7032C8.32031 10.6272 8.95359 9.69647 9.86684 9.26341ZM3.01763 5.42972V4.73634C3.01763 3.90438 2.34078 3.22753 1.50881 3.22753C0.676844 3.22753 0 3.90438 0 4.73634V5.42972C0 6.26169 0.676844 6.93853 1.50881 6.93853C2.34078 6.93856 3.01763 6.26172 3.01763 5.42972Z" fill="white" fill-opacity="0.5"/>
                <path d="M14.2074 9.91066C13.514 9.91066 12.9113 10.3064 12.6133 10.8839C12.3153 10.3064 11.7126 9.91066 11.0192 9.91066C10.0307 9.91066 9.22659 10.7148 9.22659 11.7032C9.22659 12.3463 9.49134 12.9719 9.95297 13.4196L12.6133 16L15.2736 13.4197C15.7352 12.9719 16 12.3463 16 11.7033C16 10.7148 15.1958 9.91066 14.2074 9.91066Z" fill="white" fill-opacity="0.5"/>
                </g>
                <defs>
                <clipPath id="clip0_0_411">
                <rect width="16" height="16" fill="white"/>
                </clipPath>
                </defs>
            </svg>                    
        </div>
        <div class="namepet">
            <h1>{animalName}</h1>
            <div class="healbar">
                <div class="healprogres"></div>   
            </div>  
        </div>  
    </div>
    <p>Действие [ {keysName[$keys[55]]} ]</p>
    <div class="btninfo" style="opacity: {isAnimal ? 1 : 0};">
        {#each (playerData.length ? playerData : commandsArray) as command, index}
            <div class="petdev">
                <p>
                    {#if index == 9}
                        <b>{keysName[48]}</b>
                    {:else}
                        <b>{keysName[49 + index]}</b>
                    {/if}
                    {command.name}
                </p>
            </div>
        {/each}
    </div>
</div>
{/if}