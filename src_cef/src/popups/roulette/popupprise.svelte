<script>
    import { translateText } from 'lang'
    export let SetPopup;
    export let popupData;
    import { format } from 'api/formatter'
    import {executeClient, executeClientAsync} from 'api/rage'

    let caseData = {};
    let isLoad = false;
    executeClientAsync("donate.roulette.getCaseOne").then((result) => {
        if (result && typeof result === "string") {
            caseData = JSON.parse(result);
            isLoad = true;
        }
    });

    let selectWin = 0;
    
    const Take = (index, IndexList) => {
        if (IndexList !== -1) {
            popupData[index].Done = true;
            ClosePopup ();
        } else 
            SetPopup ()
        executeClient ("client.roullete.confirm", false, IndexList);
    }

    const Sell = (index, IndexList) => {
        if (IndexList !== -1) {
            popupData[index].Done = true;
            ClosePopup ();
        } else 
            SetPopup ()
        executeClient ("client.roullete.confirm", true, IndexList);
    }

    const ClosePopup = () => {
        let isDone = true;
        
        popupData.forEach(item => {
            if (!item.Done && item.winBlock && caseData.items[item.winBlock.ItemIndex])
                isDone = false;
        });
        
        if (isDone)
            SetPopup ()
    }

    const onAllWinBlockPrice = (data) => {
        let price = 0;
        data.forEach(item => {
            if (!item.Done && item.winBlock && item.winBlock.Price)
                price += item.winBlock.Price;
        });
        return price;
    }
</script>

{#if isLoad}
<div class="newdonate__prise">
    <div class="newdonate__prise-block" on:mouseenter on:mouseleave>
        <div class="newdonate__prise-title">{translateText('popups', 'You knocked it out of the case')}</div>
        <div class="newdonate__prise-elements">
            {#each popupData as item, index}
                {#if !item.Done && item.winBlock && caseData.items[item.winBlock.ItemIndex]}
                <div class="newdonate__prise-main-element" on:mouseenter={() => selectWin = index}>
                    <div class="prise-main-buttons">
                        {#if item.winBlock.Price}
                            <div class="prise-main-button-img sell-item-img" tooltip={`Sell for: ${format("money", item.winBlock.Price)} RB`} on:click={() => Sell(index, item.winBlock.IndexList)} />
                        {/if}
                        <div class="prise-main-button-img take-item-img" tooltip="Leaving the subjectт" on:click={() => Take(index, item.winBlock.IndexList)}/>
                    </div>
                    <div class="newdonate__prise-element {caseData.items[item.winBlock.ItemIndex].color}">
                        <div class="prise-img" style={`background-image: url(${document.cloud + `img/roulette/${caseData.items[item.winBlock.ItemIndex].image}.png`}`}/>
                    </div>
                </div>
                {/if}
            {/each}
        </div>
        <div class="newdonate__prise-rarity">{@html caseData.items[popupData[selectWin].winBlock.ItemIndex].title}</div>
        <div class="newdonate__prise-name">{@html popupData[selectWin].winBlock.Text}</div>
        <div class="newdonate__prise-buttons">
            {#if onAllWinBlockPrice(popupData)}
            <div class="newdonate__button_small" on:click={() => Sell(0, -1)}>
                <div class="newdonate__button-text">{translateText('popups', 'Sell everything for')} {format("money", onAllWinBlockPrice(popupData))} RB</div>
            </div>
            {/if}
            <div class="newdonate__button_small yellow" on:click={() => Take(0, -1)}>
                <div class="newdonate__button-text">{translateText('popups', 'Take it all away')}</div>
            </div>
        </div>
    </div>
    <div class="newdonate__escape">
        <div class="box-flex">
            <span class="donateicons-esc"/>
            <div class="newdonate__escape-title">ESC</div>
        </div>
        <div class="newdonate__escape-text">
            {translateText('popups', 'Click to close')}
        </div>
    </div>
</div>
{/if}