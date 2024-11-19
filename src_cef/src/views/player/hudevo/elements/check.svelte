<script>
    import { translateText } from 'lang'
    import { TimeFormat, GetTime } from 'api/moment'
    import { serverDateTime } from 'store/server'
    import { charFractionID } from 'store/chars'
    import { fly } from 'svelte/transition';
    import { format } from 'api/formatter'

    let cashValue = 0;
    window.PayDay = async (cash) => {
        //if (window.closeTip ())
        //    await window.wait(250);

        cashValue = cash;
        if (cashValue > 0) {
            window.hudStore.HideHelp (true);            
            await window.wait(5000);
            window.hudStore.HideHelp (false);
            cashValue = 0;
        } else {
            window.hudStore.HideHelp (false);
        }
    }


    const FractionTypes = {
        0: -1,
        1: 1, // The Families
        2: 1, // The Ballas Gang
        3: 1,  // Los Santos Vagos
        4: 1, // Marabunta Grande
        5: 1, // Blood Street
        6: 2, // Cityhall
        7: 2, // LSPD police
        8: 2, // Emergency care
        9: 2, // FBI 
        10: 0, // La Cosa Nostra 
        11: 0, // Russian Mafia
        12: 0, // Yakuza 
        13: 0, // Armenian Mafia 
        14: 2, // Army
        15: 2, // News
        16: 4, // The Lost
        17: 3, // Merryweather
        18: 2, // Sheriff
    };

</script>
{#if cashValue > 0}
    <div class="hud_check">
        <div class="check_head">RECEIPT OF PAYMENT</div>
        <div class="check_info"> 
            <div class="chech_if">
                <p>Тип</p>
                <b>{!(FractionTypes [$charFractionID] === 2 || FractionTypes [$charFractionID] === 3) ? 'Пособие по безработице' : 'Зарплата'}</b>
            </div><div class="chech_if">
                <p>Дата</p>
                <b>{TimeFormat ($serverDateTime, "H:mm")}</b>
            </div><div class="chech_if">
                <p>Идентификатор заказа</p>
                <b>{Math.round (GetTime ().unix() / 1000)}</b>
            </div><div class="chech_if">
                <p>Сумма</p>
                <b style="color: #FF3535;">{format("money", cashValue)}</b>
            </div>
            <div class="check_bg">
                <svg xmlns="http://www.w3.org/2000/svg" viewBox="3 2 146 141" fill="none">
                    <g filter="url(#filter0_d_132_2206)">
                        <path d="M7 117L4 120V0H154V120L151 117L147.5 120.5L144 117L140.5 120.5L136.5 117L133 120.5L129.5 117L126 120.5L122 117L118.5 120.5L115 117L111.5 120.5L108 117L104 120.5L100.5 117L97 120.5L93.5 117L90 120.5L86 117L82.5 120.5L79 117L75.2841 120.5L72 117L68 120.5L64.5 117L61 120.5L57.5 117L53.5 120.5L50 117L46.5 120.5L43 117L39.5 120.5L35.5 117L32 120.5L28.5 117L25 120.5L21.5 117L18 120.5L14 117L10.5 120.5L7 117Z" fill="#101518"></path>
                    </g>
                    <defs>
                        <filter id="filter0_d_132_2206" x="0" y="0" width="150" height="128.5" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
                        <feFlood flood-opacity="0" result="BackgroundImageFix"></feFlood>
                        <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha"></feColorMatrix>
                        <feOffset dy="4"></feOffset>
                        <feGaussianBlur stdDeviation="2"></feGaussianBlur>
                        <feComposite in2="hardAlpha" operator="out"></feComposite>
                        <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.5 0"></feColorMatrix>
                        <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_132_2206"></feBlend>
                        <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_132_2206" result="shape"></feBlend>
                        </filter>
                    </defs>
                </svg>
            </div>            
        </div>
    </div>
{/if}
