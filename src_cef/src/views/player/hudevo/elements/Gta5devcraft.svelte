<script>
    import { ItemType, itemsInfo, ItemId } from 'json/itemsInfo.js'
    let infocraftmenu = false;
    let itemname;
    let gotovoitem = 0;
    let vsegoitem = 0;

    window.gta5devcrafthud5 = function (isCrafting, itemid, value) {
        itemname = itemsInfo[itemid].Name;
        vsegoitem = value;
        gotovoitem = 0;
        infocraftmenu = isCrafting;
    };

    window.gta5devcrafthudvalue = function (value) {
        gotovoitem = value;
    };

    window.gta5devcrafthudstop = function () {
        infocraftmenu = false;
    };


</script>

{#if infocraftmenu}
    <div class="infocraftmenu">
        <h1>{itemname}</h1>
        <p>{gotovoitem}<b>/ {vsegoitem}</b></p>
    </div>
{/if}
