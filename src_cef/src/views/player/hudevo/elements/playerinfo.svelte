<script>
    import { itemsInfo } from 'json/itemsInfo.js'
    import { serverDonatMultiplier } from 'store/server'
    import { charUUID, charWanted, charMoney, charBankMoney } from 'store/chars'
    import { isWaterMark, isPlayer } from 'store/hud'
    import { otherStatsData } from 'store/account'
    import { fly } from 'svelte/transition';
    import { format } from 'api/formatter'
    import CountUp from 'api/countup';
    
    let userData = {
        UUID:0,
        targetMoney: 0,
        changeMoney: 0,
        timerIdMoney: 0,
        Money: 0,
        targetBank: 0,
        changeBank: 0,
        timerIdBank: 0,
        Bank: 0,
    };

    import { onMount } from 'svelte';
    onMount(async () => {
        //bar.animate(1.0);

        charMoney.subscribe(value => {
            if (userData.Money !== value) {
                CounterUpdate ("Money", value);
            }
        });
        charBankMoney.subscribe(value => {
            if (userData.Bank !== value) {
                CounterUpdate ("Bank", value);
            }
        });
        charUUID.subscribe(value => {
            if (userData.UUID !== value) {
                CounterUpdate ("UUID", value);
            }
        });
    });

    const CounterUpdate = (args, value) => {
        if (userData["timerId" + args])
            clearTimeout (userData["timerId" + args]);
        userData["change" + args] = userData[args] > value ? (0 - (userData[args] - value)) : (value - userData[args]);
        userData[args] = value;
        userData["timerId" + args] = setTimeout (() => {
            userData["timerId" + args] = 0;
            userData["change" + args] = 0;
            if (!userData["target" + args]) {
                userData["target" + args] = new CountUp("target" + args, value);
                //userData["target" + args].start();
                //userData["target" + args].update(value);
            }
            else
                userData["target" + args].update(value);
        }, !userData["target" + args] ? 0 : 5000)
    }

    let serverName = "";
    window.setServerName = (name) => serverName = name;

    let isRotate = false;
    
    const secretFunction = () => {
        isRotate = !isRotate;

    }

    let greenZone = false;
    window.hudStore.greenZone = (value) => greenZone = value;

    let serverPlayerId = 0;
    window.serverStore.serverPlayerId = (value) => serverPlayerId = value;

    let serverOnline = 0; 
    window.serverStore.serverOnline = (value) => serverOnline = value;

    let weaponItemId = 0;
    window.hudStore.weaponItemId = (value) => weaponItemId = value;

    let clipSize = 0;
    window.hudStore.clipSize = (value) => clipSize = value;

    let ammo = 0;
    window.hudStore.ammo = (value) => ammo = value;

    let isShow = false;

    serverDonatMultiplier.subscribe(value => {
        if (value > 1) {
            isShow = true;

            setTimeout(() => {
                isShow = false;
            }, 1000 * 30);
        }
    });

</script>
<div class="playerinfo">
    <div class="topinfo">
        <div class="leftti">
            {#if greenZone}
                <p><b style="color: #CFF80B;">GREEN</b>ZONE</p>
            {/if}
            <p>ID: {userData.UUID}</p><!-- {serverPlayerId} / -->
            <p>
                <svg width="20.000000" height="20.000000" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink">
                    <defs>
                        <filter id="filter_0_162_dd" x="0.000000" y="0.000000" width="20.000000" height="20.000000" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
                            <feFlood flood-opacity="0" result="BackgroundImageFix"/>
                            <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha"/>
                            <feOffset dx="0" dy="0"/>
                            <feGaussianBlur stdDeviation="1.33333"/>
                            <feComposite in2="hardAlpha" operator="out" k2="-1" k3="1"/>
                            <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0"/>
                            <feBlend mode="normal" in2="BackgroundImageFix" result="effect_dropShadow_1"/>
                            <feBlend mode="normal" in="SourceGraphic" in2="effect_dropShadow_1" result="shape"/>
                        </filter>
                        <filter id="filter_0_164_dd" x="0.726562" y="4.000000" width="18.546875" height="20.000000" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
                            <feFlood flood-opacity="0" result="BackgroundImageFix"/>
                            <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha"/>
                            <feOffset dx="0" dy="4"/>
                            <feGaussianBlur stdDeviation="1.33333"/>
                            <feComposite in2="hardAlpha" operator="out" k2="-1" k3="1"/>
                            <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0"/>
                            <feBlend mode="normal" in2="BackgroundImageFix" result="effect_dropShadow_1"/>
                            <feBlend mode="normal" in="SourceGraphic" in2="effect_dropShadow_1" result="shape"/>
                        </filter>
                        <clipPath id="clip0_162">
                            <rect id="Frame" width="12.000000" height="12.000000" transform="translate(4.000000 4.000000)" fill="white" fill-opacity="0"/>
                        </clipPath>
                    </defs>
                    <g filter="url(#filter_0_162_dd)">
                        <rect id="Frame" width="12.000000" height="12.000000" transform="translate(4.000000 4.000000)" fill="#FFFFFF" fill-opacity="0"/>
                        <g clip-path="url(#clip0_162)">
                            <g filter="url(#filter_0_164_dd)">
                                <path id="Vector" d="M10 4C8.25 4 6.83 5.41 6.83 7.16C6.83 8.9 8.25 10.32 10 10.32C11.74 10.32 13.16 8.9 13.16 7.16C13.16 5.41 11.74 4 10 4ZM13.93 12.39C13.07 11.51 11.92 11.03 10.7 11.03L9.29 11.03C8.07 11.03 6.92 11.51 6.06 12.39C5.2 13.27 4.72 14.42 4.72 15.64C4.72 15.84 4.88 16 5.07 16L14.92 16C15.11 16 15.27 15.84 15.27 15.64C15.27 14.42 14.79 13.27 13.93 12.39Z" fill="#FFFFFF" fill-opacity="1.000000" fill-rule="nonzero"/>
                            </g>
                        </g>
                    </g>
                </svg>  
                {serverOnline}
            </p>
        </div>
        <div class="rightti">
            <div class="logo">
                <img src="logohere" alt="">
            </div>
            <div class="dopinfo">
            </div>
        </div>
    </div>
    <div class="bottominfo">
        <div class="money">
            <div class="nalmoney" class:hudevo__playerinfo_hide={userData["changeMoney"] !== 0}>
                <svg width="18" height="18" viewBox="0 0 18 18" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <g clip-path="url(#clip0_79_490)">
                    <path d="M9 0C7.21997 0 5.47991 0.527841 3.99987 1.51677C2.51983 2.50571 1.36628 3.91131 0.685088 5.55585C0.00389956 7.20038 -0.17433 9.00998 0.172936 10.7558C0.520203 12.5016 1.37737 14.1053 2.63604 15.364C3.89471 16.6226 5.49836 17.4798 7.24419 17.8271C8.99002 18.1743 10.7996 17.9961 12.4442 17.3149C14.0887 16.6337 15.4943 15.4802 16.4832 14.0001C17.4722 12.5201 18 10.78 18 9C17.9954 6.61446 17.0457 4.32793 15.3589 2.64109C13.6721 0.954255 11.3855 0.0045744 9 0ZM10.0385 13.1538H9.69231V13.8462C9.69231 14.0298 9.61937 14.2059 9.48954 14.3357C9.3597 14.4655 9.18361 14.5385 9 14.5385C8.81639 14.5385 8.6403 14.4655 8.51047 14.3357C8.38063 14.2059 8.30769 14.0298 8.30769 13.8462V13.1538H6.92308C6.73947 13.1538 6.56338 13.0809 6.43354 12.9511C6.30371 12.8212 6.23077 12.6451 6.23077 12.4615C6.23077 12.2779 6.30371 12.1018 6.43354 11.972C6.56338 11.8422 6.73947 11.7692 6.92308 11.7692H10.0385C10.3139 11.7692 10.578 11.6598 10.7728 11.4651C10.9675 11.2703 11.0769 11.0062 11.0769 10.7308C11.0769 10.4553 10.9675 10.1912 10.7728 9.99646C10.578 9.80171 10.3139 9.69231 10.0385 9.69231H7.96154C7.3189 9.69231 6.70258 9.43702 6.24817 8.9826C5.79375 8.52819 5.53846 7.91187 5.53846 7.26923C5.53846 6.62659 5.79375 6.01027 6.24817 5.55586C6.70258 5.10144 7.3189 4.84615 7.96154 4.84615H8.30769V4.15385C8.30769 3.97023 8.38063 3.79414 8.51047 3.66431C8.6403 3.53448 8.81639 3.46154 9 3.46154C9.18361 3.46154 9.3597 3.53448 9.48954 3.66431C9.61937 3.79414 9.69231 3.97023 9.69231 4.15385V4.84615H11.0769C11.2605 4.84615 11.4366 4.91909 11.5665 5.04892C11.6963 5.17876 11.7692 5.35485 11.7692 5.53846C11.7692 5.72207 11.6963 5.89816 11.5665 6.028C11.4366 6.15783 11.2605 6.23077 11.0769 6.23077H7.96154C7.68612 6.23077 7.42199 6.34018 7.22724 6.53493C7.03249 6.72968 6.92308 6.99381 6.92308 7.26923C6.92308 7.54465 7.03249 7.80878 7.22724 8.00353C7.42199 8.19828 7.68612 8.30769 7.96154 8.30769H10.0385C10.6811 8.30769 11.2974 8.56298 11.7518 9.01739C12.2063 9.47181 12.4615 10.0881 12.4615 10.7308C12.4615 11.3734 12.2063 11.9897 11.7518 12.4441C11.2974 12.8986 10.6811 13.1538 10.0385 13.1538Z" fill="white"/>
                    </g>
                    <defs>
                    <clipPath id="clip0_79_490">
                    <rect width="18" height="18" fill="white"/>
                    </clipPath>
                    </defs>
                </svg>   
                <p id="targetMoney"> 0</p>         
                {#if userData["changeMoney"] !== 0}
                    <p class="moneypush" style={`color:${userData["changeMoney"] > 0 ? "#c1ff3d" : "#fe5b3b"}`}>{userData["changeMoney"] > 0 ? "+" : "-"}{format("money", userData["changeMoney"])}</p>
                {/if}
            </div>
            <div class="nalmoney" class:hudevo__playerinfo_hide={userData["changeBank"] !== 0}>
                <svg width="18" height="18" viewBox="0 0 18 18" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <g clip-path="url(#clip0_13_12)">
                    <path d="M0 4.04995C0 3.69191 0.142232 3.34853 0.395406 3.09536C0.64858 2.84218 0.991958 2.69995 1.35 2.69995H16.65C17.008 2.69995 17.3514 2.84218 17.6046 3.09536C17.8578 3.34853 18 3.69191 18 4.04995V5.39995H0V4.04995Z" fill="white"/>
                    <path fill-rule="evenodd" clip-rule="evenodd" d="M0 7.19995V13.95C0 14.308 0.142232 14.6514 0.395406 14.9045C0.64858 15.1577 0.991958 15.3 1.35 15.3H16.65C17.008 15.3 17.3514 15.1577 17.6046 14.9045C17.8578 14.6514 18 14.308 18 13.95V7.19995H0ZM6.3 10.8H1.8V8.99995H6.3V10.8Z" fill="white"/>
                    </g>
                    <defs>
                    <clipPath id="clip0_13_12">
                    <rect width="18" height="18" fill="white"/>
                    </clipPath>
                    </defs>
                </svg>                      
                <p id="targetBank"> 0</p>         
                {#if userData["changeBank"] !== 0}
                    <p class="moneypush" style={`color:${userData["changeBank"] > 0 ? "#c1ff3d" : "#fe5b3b"}`}>{userData["changeBank"] > 0 ? "+" : "-"}{format("money", userData["changeBank"])}</p>
                {/if}
            </div>
        </div>
        <div class="stars" class:newhud__hide={!$isPlayer}>
            {#if $charWanted > 0}
                {#each new Array(5) as e, i}
                    <div class="star" in:fly={{ y: 10, duration: 50 * i }} class:active={i < $charWanted}>
                        <svg width="26" height="24" viewBox="0 0 26 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M13 0L16.7442 7.84656L25.3637 8.98278L19.0582 14.9684L20.6412 23.5172L13 19.37L5.35879 23.5172L6.94177 14.9684L0.636266 8.98278L9.25581 7.84656L13 0Z"></path>
                        </svg>
                    </div>
                {/each}
            {/if}
        </div>
        {#if ammo > 0}
            <div class="weapons">
                <img src="{document.cloud}inventoryItems/items/{weaponItemId}.png" alt=""/>
                <h1>{itemsInfo[weaponItemId].Name}</h1>
                {#if ammo > 0}
                    <p><svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <g clip-path="url(#clip0_0_142)">
                        <g filter="url(#filter0_d_0_142)">
                        <path d="M11.6667 18.3333H8.33337V17.5H11.6667V18.3333ZM10.8334 8.33333V5.83333H9.16671V8.33333L8.33337 9.58333V16.6667H11.6667V9.58333L10.8334 8.33333ZM10 1.66666C10 1.66666 9.16671 2.5 9.16671 4.16666V5H10.8334V4.16666C10.8334 4.16666 10.8334 2.5 10 1.66666ZM6.66671 18.3333H3.33337V17.5H6.66671V18.3333ZM5.83337 8.33333V5.83333H4.16671V8.33333L3.33337 9.58333V16.6667H6.66671V9.58333L5.83337 8.33333ZM5.00004 1.66666C5.00004 1.66666 4.16671 2.5 4.16671 4.16666V5H5.83337V4.16666C5.83337 4.16666 5.83337 2.5 5.00004 1.66666ZM16.6667 18.3333H13.3334V17.5H16.6667V18.3333ZM15.8334 8.33333V5.83333H14.1667V8.33333L13.3334 9.58333V16.6667H16.6667V9.58333L15.8334 8.33333ZM15 1.66666C15 1.66666 14.1667 2.5 14.1667 4.16666V5H15.8334V4.16666C15.8334 4.16666 15.8334 2.5 15 1.66666Z" fill="white"/>
                        </g>
                        </g>
                        <defs>
                        <filter id="filter0_d_0_142" x="-0.666626" y="-2.33334" width="21.3334" height="24.6667" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
                        <feFlood flood-opacity="0" result="BackgroundImageFix"/>
                        <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha"/>
                        <feOffset/>
                        <feGaussianBlur stdDeviation="2"/>
                        <feComposite in2="hardAlpha" operator="out"/>
                        <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0"/>
                        <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_0_142"/>
                        <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_0_142" result="shape"/>
                        </filter>
                        <clipPath id="clip0_0_142">
                        <rect width="20" height="20" fill="white"/>
                        </clipPath>
                        </defs>
                        </svg>
                        {ammo}{#if clipSize > 0 && clipSize < 1000}<b>/{clipSize}</b>{/if}</p>
                {/if}
            </div>
        {/if}
    </div>
</div>