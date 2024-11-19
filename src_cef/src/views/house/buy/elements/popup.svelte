<script>
    export let viewData;
    export let onBuy;
    export let onExit;
    import { format } from 'api/formatter'
</script>
<div id="house__popup">
    <div class="house__popup_block">
        <div class="houseicon-garage house__popup_image"></div>
        <div class="house__popup_header">House</div>
        <div class="house__gray">Buying a House</div>
        <div class="house__white">Value</div>
        <div class="box-flex">
            <div class="box-column">
                <div class="house__popup_top">${format("money", viewData.gosPrice)}</div>
                <div class="house__gray">Currencies</div>
            </div>
        </div>
    </div>
    <div class="box-flex">
        <div class="house_bottom_buttons back" on:click={onBuy}>
            <div>Buy</div>
            <div class="house_bottom_button">Enter</div>
        </div>
        <div class="house_bottom_buttons back ml-20" on:click={onExit}>
            <div>Exit</div>
            <div class="house_bottom_button">ESC</div>
        </div>
    </div>
</div>