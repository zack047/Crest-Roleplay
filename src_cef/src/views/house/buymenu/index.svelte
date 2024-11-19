<script>
    import { executeClient } from 'api/rage'
    import './main.sass'
    import './fonts/style.css'
    import { format } from 'api/formatter'
    import { houseType } from 'json/realEstate.js'
    import keys from 'store/keys'
    import keysName from 'json/keys.js'


    export let viewData;

    $: if (viewData && typeof viewData === "string") {
        viewData = JSON.parse (viewData)
    }

    const handleKeyUp = (event) => {
        const { keyCode } = event;
        
        if (keyCode === 27) 
            onEsc ();
    }

    const handleKeyDown = (event) => {
        const { keyCode } = event;
        
        if (keyCode === 13)
            onBuy ();
        else if (keyCode === 69)
            onInt ();
    }

    const onEsc = () => {        
        executeClient ("client.houseinfo.close");
    }

    const onBuy = () => {
        executeClient ("client.houseinfo.action", "buy");
    }

    const onInt = () => {
        executeClient ("client.houseinfo.action", "int");
    }
</script>

<svelte:window on:keyup={handleKeyUp} on:keydown={handleKeyDown} />

<div id="rielt">
    <div class="rielt__panel">
        <div class="rielt__header">Information about the house</div>
        <div class="rielt__mainmenu">
            <div class="rielt__mainmenu_block">
        </div>
        <div class="rielt__mainmenu_center">
            <div class="rielt__rielt_info">
                <div class="houseicon-house rielt__rielt_house-icon"></div>
                <div class="rielt__rielt_header">House Number:{viewData.id}</div>
                <div class="rielt__gray">{viewData.owner ? "" : "A wonderful house at an affordable price!"}</div>
                <div class="rielt__rielt_stat">
                    {#if viewData.tax != undefined}
                    <div class="rielt__rielt_element">
                        <div class="rielt__gray">Taxes:</div>
                        <div>${format("money", viewData.tax)}</div>
                    </div>
                    {/if}
                    <div class="rielt__rielt_element">
                        <div class="rielt__gray">Class:</div>
                        <div>{houseType[viewData.type]}</div>
                    </div>
                    {#if viewData.cars != undefined}
                    <div class="rielt__rielt_element">
                        <div class="rielt__gray">Garage Spaces:</div>
                        <div>{viewData.cars}</div>
                    </div>
                    {/if}
                    {#if viewData.owner != undefined}
                    <div class="rielt__rielt_element">
                        <div class="rielt__gray">Owner:</div>
                        <div>{viewData.owner.replace(/_/g, ' ')}</div>
                    </div>
                    {/if}
                    {#if viewData.door != undefined}
                    <div class="rielt__rielt_element">
                        <div class="rielt__gray">Condition of the doors:</div>
                        <div>{!viewData.door ? "Open" : "Closed"}</div>
                    </div>
                    {/if}
                </div>
                {#if viewData.price}
                <div class="box-column">
                    <div class="rielt__rielt_price">${format("money", viewData.price)}</div>
                    <div class="rielt__gray">State Value:</div>
                </div>
                {/if}
            </div>
        </div>
        <div class="rielt__mainmenu_block">
         
        </div>
    </div>
    <div class="box-between">
        {#if viewData.owner == undefined}
        <div class="house__bottom_left">
            <div class="house_bottom_buttons" on:click={onInt}>
                <div>View the interior</div>
                <div class="house_bottom_button">E</div>
            </div>
        </div>
        <div class="house_bottom_buttons back" on:click={onBuy}>
            <div>Buy House</div>
            <div class="house_bottom_button">ENTER</div>
        </div>
        {/if}
        <div class="house_bottom_buttons esc" on:click={onEsc}>
            <div>Exit</div>
            <div class="house_bottom_button">ESC</div>
        </div>
    </div>
    <div class="houseicon-house rielt__background"></div>
    </div>
</div>