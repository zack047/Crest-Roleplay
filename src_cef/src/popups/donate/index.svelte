<script>
    import axios from 'axios';
    import qs from 'querystring';
    import { executeClient } from 'api/rage'
    import { accountLogin } from 'store/account'
    import { translateText } from 'lang'
    import { serverDonatMultiplier, serverId } from 'store/server'
    import './main.sass'

    export let popupData;

    if (!popupData) popupData = {
        id: 0,
        title: "Purchase of a new product",
        data: [
            1000,
            100
        ]
    }


    let selectType = 0;


    const config = {
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        }
    }

    const onDonate = () => {
        //if (!isLogin(player.login))
        //    return window.showTooltip("#donateInput", 2, "Что пошло не так...");
        //else if (!isNumeric(textValue))
        //    return window.showTooltip("#donateInput", 2, "Неверный формат");

        if (!selectType) {
            executeClient("client.donatepack.rb", popupData.id);
            executeClient ("client.donatepack.close");
        } else {
            
            window.notificationAdd(4, 9, `You have begun the process of purchasing a Battle Pass. Make a donation through your personal account (lk.redage.net) in the amount of 1999 rubles, and premium access automatically becomes available.`, 25000);
            executeClient ("client.donatepack.donate", popupData.id);
            executeClient ("client.donatepack.close");

            /*axios.post('https://pay.redage.net/', qs.stringify({
                name: $accountLogin,
                sum: Math.round(popupData.data[selectType]),
                srv: Math.round($serverId)
            }), config)
                .then(function (response) {
                    response = response.data;
                    if (response.status === 'success') {
                        executeClient ("client.opendonatesite", response.url);
                        executeClient ("client.donatepack.donate", popupData.id);
                        executeClient ("client.donatepack.close");
                    } else if (response.status == 'error') {
                        window.notificationAdd(4, 9, response.msg, 3000);
                        executeClient ("client.donatepack.close");
                    }
                });*/
        }
    }

    const getPrice = (price, type) => {
        if (type === 1)
            return price;

        return Math.floor (price / $serverDonatMultiplier);
    }
    
    const onKeyDown = (event) => {
        if (event.which === 27) {
            window.router.setPopUp ("");
        }
    }
</script>

<svelte:window on:keydown={onKeyDown} />

<div id="donatepopup">
    <div class="donatepopup__back"></div>

    <div class="donatepopup__payments">
        <div class="box-between">
            <div class="box-column">
                <div class="donatepopup__nickname">{popupData.title}</div>
                <div class="donatepopup__small">{!selectType ? "Buy for RedBucks" : "Buy for rubles"}</div>
                <div class="donatepopup__count">{popupData.data[selectType]}<span class="red"> {!selectType ? "RB" : "USD"}</span></div>
                <div class="donatepopup__button"><span class="gray">{!selectType ? "Equivalent to." : "Payable to"}: </span>{getPrice (popupData.data[selectType], selectType)} {translateText('popups', 'руб')}.</div>
            </div>
            <div class="donatepopup__logo"></div>
        </div>
        <div class="donatepopup__payments_title">{translateText('popups', 'Choose a payment method')}</div>
        <div class="donatepopup__grid">
            <div class="donatepopup__element" class:active={selectType === 0} on:click={() => selectType = 0}>
                <div class="donatepopup__element_img" style="background-image: url('{document.cloud}img/roulette/items_5.png')"></div>
            </div>
            <div class="donatepopup__element" class:active={selectType === 1} on:click={() => selectType = 1}>
                <div class="donatepopup__element_label">{translateText('popups', 'Скидка')} {100-Math.round(popupData.data[1] / getPrice (popupData.data[0], 0) * 100)}%</div>
                <div class="donatepopup__element_img" style="background-image: url('{document.cloud}img/roulette/items_0.png')"></div>
            </div>
        </div>
        <div class="box-between popup__button_box">
            <div class="popup__button big orange" on:click={onDonate}>{translateText('popups', 'Confirm')}</div>
            <div class="popup__button" on:click={() => window.router.setPopUp ("")}>{translateText('popups', 'Назад')}</div>
        </div>
        <div class="donatepopup__donate_info">
        {#if selectType === 1}
            {translateText('popups', 'After pressing the "Confirm" button" you have 5 minutes to make the payment.')}<br/><br/>{translateText('popups', 'If you quit before the transaction is completed, the account will be credited with RedBucks at the current rate.')}
        {/if}
        </div>
    </div>

</div>