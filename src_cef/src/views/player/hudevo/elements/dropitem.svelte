<script>
    import { translateText } from 'lang'
    import { fly } from 'svelte/transition';
    import keys from 'store/keys'
    import keysName from 'json/keys.js'
    import { isInputToggled } from 'store/hud'
    import { getPng } from '@/views/player/menu/elements/inventory/getPng.js'

    let visible = false;

    let ItemData = {};
    let ItemInfo = 0;
    let Name = "";
    let Count = 0;
    let Text = "";
    let isAdd = "";

    let timerId = 0;
    window.hudItem = {
        drop: (itemId, amount, Data, _isAdd = false) => {
            const ItemId = itemId;

            ItemData = {
                ItemId: ItemId,
                Data: Data,
            }

            if (timerId)
                clearTimeout (timerId);

            Count = amount;
            ItemInfo = window.getItem (ItemId);
            
            Name = ItemInfo.Name;

			if (ItemInfo.functionType == 1 && ItemId != -5 && ItemId != -9 && ItemId != -1 && Data.split("_").length >= 2) Name += Bool(Data.split("_")[2]) ? " (М)" : " (Ж)";
			else if (ItemInfo.functionType == 2) Name += ` (${Data})`;
			else if (ItemId == -9) Name += ` (x${Data})`;
			else if (ItemId == 19 && Data.split('_').length > 0) Name += ` (${Data.split('_')[0]})`;
			Text = ItemInfo.Description;
            isAdd = _isAdd;

            visible = true;

            timerId = setTimeout(ClearData, 5000);
        },
        dropFocus: () => {
            if (timerId)
                clearTimeout (timerId);

            timerId = setTimeout(ClearData, 500);
        }
    }

    const ClearData = () => {
        timerId = 0;
        visible = false;
    }
    
    const Bool = (text) => {
        return String(text).toLowerCase() === "true";
    }
</script>
{#if visible}
<div class="iteminfopik">
    <div class="imgitem">
        <img src="{getPng(ItemData, ItemInfo)}" alt=""/>
    </div>
    <div class="infoitem">
        <h1>{Name} {#if Count > 1}{Count} {translateText('player2', 'шт')}.{/if}</h1>
        <p>{Text}</p>
    </div>
</div>
{/if}