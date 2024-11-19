<script>
    export let viewData;
    export let onBuy;
    export let onExit;
    import { format } from 'api/formatter'
        
    const descToClass = [

    ]
</script>

<div class="house__buy">
    <div class="house__header">House Number:{viewData.id}</div>
    <div class="house__info">
        {descToClass [viewData.class]} in <span>Rockford Hills</span>
    </div>
    <div class="house__buy_flex">
        <div class="box-column">
            <div class="house__stat mint">${format("money", viewData.gosPrice)}</div>
            <div class="house__menu">State Value:</div>
        </div>
        {#if viewData.price}
        <div class="house__line"></div>
        <div class="box-column">
            <div class="house__stat orange">${format("money", viewData.price)}</div>
            <div class="house__menu">Suggested Value:</div>
        </div>
        {/if}
    </div>
    {#if viewData.price}
    <div class="house__buy_center">
        <div class="house__menu">Улучшения:</div>
        <div class="box-column">
            <div class="house__upgrade_element">Alarm System</div>
            <div class="house__upgrade_element">Burglar Proof Safe</div>
            <div class="house__upgrade_element">Wardrobe [x3]</div>
        </div>
    </div>
    {/if}
    <div class="house_bottom_buttons back" on:click={onBuy}>
        <div>Purchase</div>
        <div class="house_bottom_button">ENTER</div>
    </div>
    <div class="house_bottom_buttons esc" on:click={onExit}>
        <div>Exit</div>
        <div class="house_bottom_button">ESC</div>
    </div>
    <div class="houseicon-house house__background"></div>
</div>