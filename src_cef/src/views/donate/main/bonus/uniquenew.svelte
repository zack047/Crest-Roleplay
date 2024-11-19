<script>
    import { translateText } from 'lang'
    import { executeClient } from 'api/rage';
    export let SetPopup;

    let selectListId = {
        id: 0,
        name: "Battlepass Season 1",
        class: "reborn", //ХЗ ЧЕ ЭТО)))
        price: 7777,
        img: document.cloud + 'inventoryItems/items/bp.png',
        list: [
            "More than 300 Unique tasks",
            "A huge number of Unique Clothes",
            "Two unique cars that are already waiting for you",
        ]
    };

    const onSelectMenu = () => {
        executeClient ("client.donate.close");
        executeClient ('client.battlepass.open');
    }


</script>


<div class="main-menu__header-element">
    <div class="main-menu__star-block">
        <div class="star-img" style="background-image: url({selectListId.img})"/>
    </div>
    <div class="main-menu__element-info">
        <div class="main-menu__timer-box">
            <div class="main-menu__timer box-center">'New'</div>
        </div>
        <div class="main-menu__title">{selectListId.name}</div>
        {#if selectListId.list}
        <div class="main-menu__paragraph">
        {#each selectListId.list as text, index}
            <b>- </b>{text}<br />
        {/each} 
        </div>
        {:else if selectListId.desc}
        <div class="main-menu__paragraph">{selectListId.desc}</div>
        {/if}
        <div class="newdonate__button_small" on:click={onSelectMenu}>
            <div class="newdonate__button-text">Go to</div>
        </div>
    </div>
</div>