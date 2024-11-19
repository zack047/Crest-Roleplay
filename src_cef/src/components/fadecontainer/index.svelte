<script>
	import { fade } from 'svelte/transition';
    import './main.sass'
    let inDuration = 0;
    let outDuration = 0;
    let visible = true;
    let textInfo = true;

    window.FadeScreen = (toggled, speed) => {
        visible = toggled;
        if (toggled) inDuration = speed / 3;
        else outDuration = speed / 3;
        
        if (!visible && textInfo) {
            textInfo = false;
        }
    }
</script>
{#if visible}
<div id="fadecontainer" in:fade={{duration: inDuration}} out:fade={{duration: outDuration}}>
    <div class="box-logo" />
    <div class="pointBlock__circle" />
    {#if textInfo}
    <div class="text-info" out:fade={{duration: outDuration}}>Hey you...<br/>
        When you see this message<br/>
        the server is loading. You are almost there!</div>
    {/if}
</div>
{/if}