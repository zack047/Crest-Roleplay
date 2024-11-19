<script>
    import { TimeFormat } from 'api/moment'
    import { serverDateTime } from 'store/server'
    import { isInputToggled, isWaterMark, isPlayer, isHelp, isPhone } from 'store/hud'
    import keys from 'store/keys'
    import keysName from 'json/keys.js'
    import { charFractionID, charOrganizationID, charUUID, charWanted, charMoney, charBankMoney } from 'store/chars'
    import { format } from 'api/formatter'
    import CountUp from 'api/countup';

    // export let SafeSone;

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

    let isWorld = true;
    window.hudStore.isWorld = (value) => isWorld = value;

    let food = 100;
    window.hudStore.food = (value) => food = value;

    let drink = 100;
    window.hudStore.drink = (value) => drink = value;

    let direction = "NE";
    window.hudStore.direction = (value) => direction = value;

    let street = "";
    window.hudStore.street = (value) => street = value;

    let area = "";
    window.hudStore.area = (value) => area = value;

    let microphone = 0;
    window.hudStore.microphone = (value) => microphone = value

    let isMute = false;
    window.hudStore.isMute = (value) => {
        isMute = value;
    }
    
    let polygon = 0;
    window.hudStore.polygon = (value) => polygon = value;

    let radio = 0;
    window.hudStore.radio = (value) => radio = value;

    
    let serverPlayerId = 0;
    window.serverStore.serverPlayerId = (value) => serverPlayerId = value;

    let serverOnline = 0;
    window.serverStore.serverOnline = (value) => serverOnline = value;
</script>


    <div class="hud_micro">
        <div class="micro">
            <div class="microbg {microphone ? 'act' : 'none'}">
                <svg width="42" height="42" viewBox="0 0 42 42" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <rect x="-2" y="20.6275" width="32" height="32" rx="5" transform="rotate(-45 -2 20.6275)"></rect>
                </svg>
                <div class="microico">
                    <svg width="10" height="16" viewBox="0 0 10 16" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M9.46429 6.92194C9.7355 6.92194 9.95964 7.13359 9.99514 7.40809L10 7.48444V7.85944C10 10.529 8.03214 12.716 5.53643 12.9082L5.53571 14.6094C5.53571 14.9201 5.29586 15.1719 5 15.1719C4.72879 15.1719 4.50464 14.9603 4.46914 14.6858L4.46429 14.6094V12.9083C2.02374 12.7206 0.0877356 10.6254 0.00289985 8.03666L0 7.85944V7.48444C0 7.17379 0.23985 6.92194 0.535714 6.92194C0.806929 6.92194 1.03106 7.13359 1.06654 7.40809L1.07143 7.48444V7.85944C1.07143 9.97969 2.66746 11.7085 4.66686 11.7936L4.82143 11.7969H5.17857C7.19786 11.7969 8.84436 10.1211 8.92543 8.02174L8.92857 7.85944V7.48444C8.92857 7.17379 9.16843 6.92194 9.46429 6.92194ZM5 0.171936C6.57793 0.171936 7.85714 1.51508 7.85714 3.17194V7.67194C7.85714 9.32876 6.57793 10.6719 5 10.6719C3.42204 10.6719 2.14286 9.32876 2.14286 7.67194V3.17194C2.14286 1.51508 3.42204 0.171936 5 0.171936ZM5 1.29694C4.01379 1.29694 3.21429 2.1364 3.21429 3.17194V7.67194C3.21429 8.70746 4.01379 9.54694 5 9.54694C5.98621 9.54694 6.78571 8.70746 6.78571 7.67194V3.17194C6.78571 2.1364 5.98621 1.29694 5 1.29694Z"></path>
                    </svg>
                </div>
            </div>
            <div class="text">{keysName[$keys[36]]}</div>
        </div>
        {#if $charFractionID > 0 || $charOrganizationID > 0}
            <div class="micro">
                <div class="microbg {radio ? 'act' : 'none'}">
                    <svg width="42" height="42" viewBox="0 0 42 42" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <rect x="-2" y="20.6275" width="32" height="32" rx="5" transform="rotate(-45 -2 20.6275)"></rect>
                    </svg>
                    <div class="microico">
                        <svg width="18" height="18" viewBox="0 0 18 18" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <path d="M6.9834 6.91876H11.0165V9.11251H6.9834V6.91876Z"></path>
                            <path d="M12.0431 5.20877C11.9474 5.18065 11.8462 5.16659 11.7421 5.16659H11.4806V4.14846H10.859V5.16659H10.2965V4.14846H9.67494V5.16659H6.98338V1.49908C6.98338 1.3444 6.85682 1.21783 6.70213 1.21783C6.54744 1.21783 6.42088 1.3444 6.42088 1.49908V5.16659H6.25775C5.65588 5.16659 5.1665 5.65596 5.1665 6.25784V15.691C5.1665 16.2928 5.65588 16.7822 6.25775 16.7822H11.7421C12.344 16.7822 12.8334 16.2928 12.8334 15.691V6.25784C12.8329 6.02099 12.7557 5.79068 12.6132 5.60151C12.4706 5.41234 12.2706 5.27453 12.0431 5.20877ZM6.42088 6.63752C6.42088 6.48284 6.54744 6.35627 6.70213 6.35627H11.2978C11.4524 6.35627 11.579 6.48284 11.579 6.63752V9.39377C11.579 9.55127 11.4524 9.67502 11.2978 9.67502H6.70213C6.54744 9.67502 6.42088 9.55127 6.42088 9.39377V6.63752ZM9.99557 12.0431H8.00432C7.84963 12.0431 7.72307 11.9194 7.72307 11.7619C7.72307 11.6072 7.84963 11.4806 8.00432 11.4806H9.99557C10.1503 11.4806 10.2768 11.6072 10.2768 11.7619C10.2768 11.9194 10.1503 12.0431 9.99557 12.0431ZM10.2768 12.5522C10.2768 12.7069 10.1503 12.8335 9.99557 12.8335H8.00432C7.84963 12.8335 7.72307 12.7069 7.72307 12.5522C7.72307 12.3975 7.84963 12.271 8.00432 12.271H9.99557C10.1503 12.271 10.2768 12.3975 10.2768 12.5522ZM6.84838 16.2197C6.78088 16.0875 6.74432 15.9356 6.74432 15.7781V12.5522C6.74432 11.9335 6.30838 11.416 5.729 11.2866V10.7156C6.62338 10.8506 7.30682 11.6213 7.30682 12.5522V15.7781C7.30682 16.02 7.50369 16.2197 7.74838 16.2197H6.84838ZM9.99557 13.6238H8.00432C7.84963 13.6238 7.72307 13.4972 7.72307 13.3425C7.72307 13.1878 7.84963 13.0613 8.00432 13.0613H9.99557C10.1503 13.0613 10.2768 13.1878 10.2768 13.3425C10.2768 13.4972 10.1503 13.6238 9.99557 13.6238ZM12.2709 11.2866C11.9832 11.351 11.7259 11.5114 11.5414 11.7414C11.3569 11.9714 11.2561 12.2574 11.2556 12.5522V15.6347C11.2556 15.8485 11.1965 16.0481 11.0924 16.2197H10.1081C10.4287 16.2197 10.6931 15.9553 10.6931 15.6347V12.5522C10.6931 11.6213 11.3765 10.8506 12.2709 10.7156V11.2866Z"></path>
                        </svg>
                    </div>
                </div>
                <div class="text">{keysName[$keys[50]]}</div>
            </div>
        {/if}
    </div>
    <div class="data_info">
        <p><svg width="18" height="18" viewBox="0 0 18 18" fill="none" xmlns="http://www.w3.org/2000/svg">
                <g clip-path="url(#clip0_79_173)">
                <path d="M9 -0.000488281C4.03725 -0.000488281 0 4.03676 0 8.99951C0 13.9623 4.03725 17.9995 9 17.9995C13.9628 17.9995 18 13.9623 18 8.99951C18 4.03676 13.9628 -0.000488281 9 -0.000488281ZM13.2803 13.6548C13.134 13.801 12.942 13.8745 12.75 13.8745C12.558 13.8745 12.366 13.801 12.2197 13.6548L8.46975 9.90476C8.32875 9.76451 8.25 9.57401 8.25 9.37451V4.49951C8.25 4.08476 8.586 3.74951 9 3.74951C9.414 3.74951 9.75 4.08476 9.75 4.49951V9.06401L13.2803 12.5943C13.5735 12.8875 13.5735 13.3615 13.2803 13.6548Z" fill="white"/>
                </g>
                <defs>
                <clipPath id="clip0_79_173">
                <rect width="18" height="18" fill="white" transform="translate(0 -0.000488281)"/>
                </clipPath>
                </defs>
            </svg>
            {TimeFormat ($serverDateTime, "H:mm")}
            <b>/{TimeFormat ($serverDateTime, "DD.MM.YYYY")}</b>
        </p>
    </div>
    <div class="gps_info">
        <svg width="18" height="18" viewBox="0 0 18 18" fill="none" xmlns="http://www.w3.org/2000/svg">
            <g clip-path="url(#clip0_79_160)">
            <path d="M17.781 0.219426C17.5673 0.00493655 17.2463 -0.0580637 16.9665 0.055175L0.466453 6.8052C0.158202 6.93194 -0.030061 7.2462 0.00520085 7.57769C0.0396893 7.90918 0.288702 8.17771 0.616466 8.23768L8.35576 9.64468L9.76353 17.384C9.82277 17.7117 10.0913 17.9607 10.4228 17.996C10.449 17.9982 10.4753 17.9997 10.5008 17.9997C10.8023 17.9997 11.0791 17.8175 11.1953 17.5332L17.9453 1.03319C18.0593 0.754154 17.9948 0.433177 17.781 0.219426Z" fill="white"/>
            </g>
            <defs>
            <clipPath id="clip0_79_160">
            <rect width="18" height="18" fill="white" transform="translate(0 -0.000488281)"/>
            </clipPath>
            </defs>
        </svg>            
        <div class="gps_left">
            <h1>{street}</h1>
            <p>{area}</p>
        </div>
    </div>