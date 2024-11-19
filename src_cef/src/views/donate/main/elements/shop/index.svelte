<script>
    import { translateText } from 'lang'
    import { format } from 'api/formatter'
    import { serverDonatMultiplier } from 'store/server'
    import { validate } from 'api/validation';
    let selectIndex = 0;

    export let SetPopup;
    const getDonate = (text) => {
        if (text < 0) text = 0;
        else if (text > 999999) text = 999999;
        let tallage = 0;
        if ($serverDonatMultiplier > 1) {
            text = text * $serverDonatMultiplier;
        } else {
            if (text >= 20000) {
                tallage = 50;
            } else if (text >= 10000) {
                tallage = 30;
            } else if (text >= 3000) {
                tallage = 20;
            } else if (text >= 1000) {
                tallage = 10;
            }
        }

        return `${Math.round(text) + Math.round(text / 100 * tallage)}`;
    }

    import ImgMenuTop_up from './img/top_up.png';
    import ImgMenuChar from './img/char.png';

    import ImgBuy from './img/buy.svg';
    import ImgConversion from './img/conversion.svg';
    import ImgSapper from './img/sapper.png';

    import ImgP_1 from './img/p_1.svg';
    import ImgP_2 from './img/p_2.svg';
    import ImgP_3 from './img/p_3.svg';
    import SimPhoto from './img/sim.png';
    import NumberPhoto from './img/number.png';
    
    /*import ImgClM_1 from './img/clothes/male/1.png';
    import ImgClM_2 from './img/clothes/male/2.png';
    import ImgClM_3 from './img/clothes/male/3.png';
    import ImgClM_4 from './img/clothes/male/4.png';
    import ImgClM_5 from './img/clothes/male/5.png';
    import ImgClM_6 from './img/clothes/male/6.png';
    import ImgClM_7 from './img/clothes/male/7.png';
    import ImgClM_8 from './img/clothes/male/8.png';
    import ImgClM_9 from './img/clothes/male/9.png';
    import ImgClM_10 from './img/clothes/male/10.png';
    import ImgClM_11 from './img/clothes/male/11.png';
    import ImgClM_12 from './img/clothes/male/12.png';
    import ImgClM_13 from './img/clothes/male/13.png';
    import ImgClM_14 from './img/clothes/male/14.png';
    import ImgClM_15 from './img/clothes/male/15.png';
    import ImgClM_16 from './img/clothes/male/16.png';
    import ImgClM_17 from './img/clothes/male/17.png';
    import ImgClM_18 from './img/clothes/male/18.png';
    import ImgClM_19 from './img/clothes/male/19.png';
    import ImgClM_20 from './img/clothes/male/20.png';
    import ImgClM_21 from './img/clothes/male/21.png';
    import ImgClM_22 from './img/clothes/male/22.png';
    import ImgClM_23 from './img/clothes/male/23.png';

    import ImgClF_1 from './img/clothes/female/1.png';
    import ImgClF_2 from './img/clothes/female/2.png';
    import ImgClF_3 from './img/clothes/female/3.png';
    import ImgClF_4 from './img/clothes/female/4.png';
    import ImgClF_5 from './img/clothes/female/5.png';
    import ImgClF_6 from './img/clothes/female/6.png';
    import ImgClF_7 from './img/clothes/female/7.png';
    import ImgClF_8 from './img/clothes/female/8.png';
    import ImgClF_9 from './img/clothes/female/9.png';
    import ImgClF_10 from './img/clothes/female/10.png';
    import ImgClF_17 from './img/clothes/female/17.png';
    import ImgClF_18 from './img/clothes/female/18.png';
    import ImgClF_19 from './img/clothes/female/19.png';
    import ImgClF_20 from './img/clothes/female/20.png';
    import ImgClF_21 from './img/clothes/female/21.png';
    import ImgClF_22 from './img/clothes/female/22.png';
    import ImgClF_23 from './img/clothes/female/23.png';*/

    const shopList = [
        {
            title: "Basic",
            desc: "",
            function: "onSelectPrice",
            img: ImgBuy,
            list: [
                /*{
                    name: "Top up",
                    desc: "",
                    btnName: "Buy",
                    img: ImgBuy,
                },*/
                {
                    name: "Conversion",
                    btnName: "Convert",
                    desc: "",
                    img: ImgConversion,
                },
                {
                    name: "Minesweeper Game",
                    btnName: "Play",
                    desc: "",
                    img: ImgSapper,
                },
            ]
        },
        {
            title: "Character",
            desc: "",
            function: "onSelectP",
            img: ImgMenuChar,
            list: [
                {
                    id: 0,
                    isName: true,
                    name: "Change Name",
                    desc: "",
                    text: `Allows you to change your name once
                            After changing your name,the character will forget about the handshakes made earlier,
                            inventory and statistics of the character will not change.`,
                    img: ImgP_1,
                    price: 800,
                },
                {
                    id: 1,
                    name: "Change Appearance",
                    desc: "",
                    text: `After paying for this feature,
                            your character will be sent to the appearance editor (as when creating a character),
                            where you can re-customise their appearance.
                            Inventory and tattoos will remain.`,
                    img: ImgP_2,
                    price: 1000,
                },
                {
                    id: 2,
                    name: "Remove Warn",
                    desc: "",
                    text: `Warn - A warning from the administration. This function removes only 1 warn.
                        If you have 2 warnings, you will need to use this function twice for "Withdrawal of Warn".
                        If you receive 3 warnings at the same time, you will be banned for 30 days.`,
                    img: ImgP_3,
                    price: 1000,
                },
                {
                    id: 3,
                    isNumber: true,
                    name: "Custom License Plate",
                    desc: "",
                    text: `Purchase a custom plate for your car!
                        Collect your unique license plate and let everyone around you envy...
                        The plate on the car is issued as an item in the inventory.`,
                    img: NumberPhoto,
                    btnName: "Buy"
                },
                {
                    id: 4,
                    isSim: true,
                    name: "Custom Sim",
                    desc: "",
                    text: `Purchase a custom Sim Number!
                        Collect your unique Sim Number and let everyone around you envy...
                        The sim is issued as an item in the inventory.`,
                    img: SimPhoto,
                    btnName: "Buy"
                },
            ]
        },
        /*{
            title: "Mens Clothing",
            desc: "",
            function: "onSelectC",
            img: ImgClM_11,
            list: [
                {
                    id: 0,
                    name: "Beard",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_2,
                    price: 30000,
                },
                {
                    id: 1,
                    name: "Rat Mask",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_17,
                    price: 1000,
                },
                {
                    id: 2,
                    name: "Neon Mask",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_18,
                    price: 2000,
                },
                {
                    id: 3,
                    name: "Mask Marshmallow",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_5,
                    price: 35000,
                },
                {
                    id: 4,
                    name: "Neon Glasses",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_19,
                    price: 10000,
                },
                {
                    id: 5,
                    name: "Neon Helmet",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_20,
                    price: 30000,
                },
                {
                    id: 6,
                    name: "Admirals Cap",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_21,
                    price: 1500,
                },
                {
                    id: 7,
                    name: "Headband",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_6,
                    price: 4200,
                },
                {
                    id: 8,
                    name: "Neon pants",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_23,
                    price: 25000,
                },
                {
                    id: 9,
                    name: "Bright Pants",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_13,
                    price: 20000,
                },
                {
                    id: 10,
                    name: "Pants with Backlight",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_16,
                    price: 20000,
                },
                {
                    id: 11,
                    name: "Supreme Shorts",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_1,
                    price: 5000,
                },
                {
                    id: 12,
                    name: "GUCCI Pants",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_12,
                    price: 30000,
                },
                {
                    id: 13,
                    name: "Neon Shoes",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_22,
                    price: 25000,
                },
                {
                    id: 14,
                    name: "Fins",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_4,
                    price: 3000,
                },
                {
                    id: 15,
                    name: "Neon top",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_10,
                    price: 15000,
                },
                {
                    id: 16,
                    name: "Bright Top",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_14,
                    price: 10000,
                },
                {
                    id: 17,
                    name: "Illuminated Top",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_15,
                    price: 10000,
                },
                {
                    id: 18,
                    name: "Gucci Sweatshirt",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_11,
                    price: 30000,
                },
                {
                    id: 19,
                    name: "Fashionable T-Shirt",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClM_7,
                    price: 5000,
                },
            ]
        },
        {
            title: "Womens Clothing",
            desc: "",
            function: "onSelectC",
            img: ImgClF_1,
            list: [
                {
                    id: 20,
                    name: "Rat Mask",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_17,
                    price: 1000,
                },
                {
                    id: 21,
                    name: "Neon Mask",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_18,
                    price: 2000,
                },
                {
                    id: 22,
                    name: "Mask Marshmallow",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_7,
                    price: 35000,
                },
                {
                    id: 23,
                    name: "Neon Glasses",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_19,
                    price: 10000,
                },
                {
                    id: 24,
                    name: "Neon Helmet",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_20,
                    price: 30000,
                },
                {
                    id: 25,
                    name: "Admirals Cap",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_21,
                    price: 1500,
                },
                {
                    id: 26,
                    name: "Neon pants",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_2,
                    price: 25000,
                },
                {
                    id: 27,
                    name: "Bright Pants",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_5,
                    price: 20000,
                },
                {
                    id: 28,
                    name: "Illuminated Pants",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_10,
                    price: 20000,
                },
                {
                    id: 29,
                    name: "Neon Shoes",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_22,
                    price: 10000,
                },
                {
                    id: 30,
                    name: "Fins",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_8,
                    price: 3000,
                },
                {
                    id: 31,
                    name: "Neon Top",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_3,
                    price: 15000,
                },
                {
                    id: 32,
                    name: "Bright Top",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_6,
                    price: 10000,
                },
                {
                    id: 33,
                    name: "Illuminated Top",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_9,
                    price: 10000,
                },
                {
                    id: 34,
                    name: "Fashionable T-Shirt",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_1,
                    price: 2000,
                },
                {
                    id: 35,
                    name: "Vulgar T-Shirt",
                    desc: "",
                    text: `Clothing`,
                    img: ImgClF_4,
                    price: 4000,
                },
            ]
        }*/
    ];

    let
        FirstName = "",
        LastName = "";
    const onToServer = (item) => {
        switch (shopList[selectIndex].function) {
            case "onSelectPrice":
                if (item.btnName === "Buy") SetPopup ("PopupPayment", 0);
                else if (item.btnName === "Convert") SetPopup ("PopupChange");
                else window.router.setView('DonateSapper');
                break;
            case "onSelectP":
                if (item.isNumber) {
                    SetPopup ("PopupNomer");
                    return;
                }
                if (item.isSim) {
                    SetPopup ("PopupSim");
                    return;
                }

                if (item.isName) {
                    let check;

                    check = validate("name", FirstName);
                    if(!check.valid) {
                        window.notificationAdd(4, 9, check.text, 3000);
                        return;
                    }

                    check = validate("surname", LastName);
                    if(!check.valid) {
                        window.notificationAdd(4, 9, check.text, 3000);
                        return;
                    }
                    item.isName = `${FirstName}_${LastName}`
                    item.text = `Do you really want to change your name to ${item.isName}`
                }
                SetPopup ("PopupPPopup", item);
                break;
            case "onSelectC":
                SetPopup ("PopupCPopup", item);
                break;
        }
    }
    const onSelectPrice = (item) => {
        if (!item.price) SetPopup ("PopupPayment", 0);
        else SetPopup ("PopupPayment", item.priceReal);
    }
</script>


<div id="newdonate__shop">
    <div class="shop-elements">

        {#each shopList[selectIndex].list as item, index}
        <div class="shop-element">
            {#if item.icon}
            <div class="shop-element__icon">
                <span class="{item.icon} element__icon" />
            </div>
            {:else}
            <div class="star-img" style="background-image: url({item.img})" />
            {/if}
            <div class="shop-element__info">
                <!--<div class="shop-element__condition">Up to level 3</div>-->
                <div class="shop-element__title">{item.name}</div>
                {#if item.isName}
                <input class="shop-element__input" placeholder="Name" type="text" bind:value={FirstName} >
                <input class="shop-element__input" placeholder="Last Name" type="text" bind:value={LastName}>
                {:else}
                <div class="shop-element__paragraph">{item.desc}</div>
                {/if}
                <div class="shop-element__button-box">
                    <div class="newdonate__button_small shop-element__button" on:click={() => onToServer (item)}>
                        {#if item.btnName}
                        <div class="newdonate__button-text">{item.btnName}</div>
                        {:else}
                        <div class="newdonate__button-text">Buy For {format("money", item.price)} CC</div>
                        {/if}
                    </div>
                    
                </div>
            </div>
        </div>
        {/each}
    </div>
    <div class="shop-categorie">
        {#each shopList as item, index}        
        <div class="shop-categorie__element" class:active={selectIndex === index} on:click={() => selectIndex = index}>
            <div class="shop-categorie__info">
                <div class="shop-categorie__checkbox">
                    <div class="shop-categorie__checkbox_active"/>
                </div>
                <div class="shop-element__title">{item.title}</div>
                <div class="shop-element__paragraph">{item.desc}</div>
            </div>
            <div class="star-img" style="background-image: url({item.img})"/>
        </div>
        {/each}
    </div>
</div>