<script>
    import { translateText } from 'lang'
    import './main.sass'    
    export let popupData;

    const statuses = [
        {
            code: "kick",
            status: "You have been thrown",
            text: "You were kicked by Administrator : Dmitriy Jaguar you can appeal the kick in the forum of the project https://forum.redage.net/",
            button: "Обжаловать"
        },
        {
            code: "ban",
            status: "You are banned",
            text: "Вace banned Admin : Dmitrio Jaguarchik until 21.22.2019 reason: Poured jaguar https://forum.redage.net/",
            button: "Обжаловать"
        },
        {
            code: "restart",
            status: "Server on restart",
            text: "At the moment the server is on restart, expect within 5 minutes the server will start",
            button: "Встать в очередь"
        }
    ]

    let currentStatus = 0;
</script>

<div id="mainpopup" class="{popupData.Type}">
    <div class="mainpopup__back hud__icon-warning"></div>
    <div class="mainpopup__main {popupData.Type}">
        <div class="mainpopup__smallback hud__icon-warning"></div>
        <div class="popup__title {popupData.Type}">{popupData.Title}</div>
        <div class="popup__subtitle">{popupData.Text}</div>
        {#if false}
        <div class="box-between">
            <div class="popup__button big {popupData.Type}">{statuses[currentStatus].button}</div>
            <div class="popup__button red">{translateText('popups', 'Back')}</div>
        </div>
        {/if}
    </div>
</div>