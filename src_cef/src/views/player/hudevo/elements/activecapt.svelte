<script>
    import { fly } from 'svelte/transition';

    import { addListernEvent } from "api/functions";
    
    let 
        visible = false,
        warType,
        warGripType,
        weaponType,
        title, 
        time, 
        attackName, 
        protectingName, 
        attackingCount, 
        protectingCount,
        attackingPlayersInZone, 
        protectingPlayersInZone;

    addListernEvent ("hud.war.open", (_warType, _warGripType, _weaponType, _title, _attackName, _protectingName) => {
        weaponType = _weaponType;
        warType = _warType;
        warGripType = _warGripType;
        title = _title;
        attackName = _attackName;
        protectingName = _protectingName;
        visible = true;
    });

    addListernEvent ("hud.war.update", (type, value1, value2) => {
        if (type === "count") {
            attackingCount = value1;
            protectingCount = value2;
        }

        if (type === "playersCount") {
            attackingPlayersInZone = value1;
            protectingPlayersInZone = value2;
        }

        if (type === "time") {
            time = value1;
        }
    });

    addListernEvent ("hud.war.close", () => {
        visible = false;
        attackingPlayersInZone = 0;
        protectingPlayersInZone = 0;
        attackingCount = 0;
        protectingCount = 0;
    });

    import { typeBattle, weaponsCategory } from '@/popups/war/data'

    const getProgress = (value, max) => {
        if (!max)
            return 50;
            
        return 100 / max * value;
    }

</script>

{#if visible}
<div class="hud_capt">
    <div class="catp_timer">
        <b>{time}</b>
        <p>TIME</p>
    </div>
    <div class="capt_barinfo">
        <div class="leftcapt">
            <div class="capt_info">
                <span>
                    <svg width="21" height="21" viewBox="0 0 21 21" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <g clip-path="url(#clip0_0_312)">
                        <g filter="url(#filter0_d_0_312)">
                        <path d="M2.19537 5.2885L7.83562 10.5822L6.97462 11.501L9.49899 14.0254L10.5 13.0856L11.501 14.0254L14.0254 11.501L5.28849 2.19537C5.21735 2.11968 5.12075 2.07294 5.01724 2.06412L2.23474 1.7535C2.16956 1.74514 2.10332 1.75184 2.04113 1.77307C1.97893 1.7943 1.92243 1.8295 1.87596 1.87597C1.82949 1.92244 1.79429 1.97894 1.77306 2.04114C1.75183 2.10333 1.74513 2.16956 1.75349 2.23475L2.06412 5.01725C2.07293 5.12076 2.11967 5.21736 2.19537 5.2885ZM19.2356 17.4125C19.2083 17.2404 19.1401 17.0774 19.0365 16.9372C18.9329 16.7971 18.7971 16.684 18.6406 16.6075L18.4831 16.5331C18.3547 16.4659 18.2368 16.3804 18.1331 16.2794L16.1643 14.3106L14.3093 16.1656L16.2737 18.13C16.3733 18.2307 16.4574 18.3457 16.5231 18.4713L16.6106 18.6463C16.6865 18.8019 16.799 18.937 16.9384 19.0398C17.0778 19.1426 17.24 19.2103 17.4112 19.2369C17.4691 19.2456 17.5276 19.25 17.5862 19.25C17.8764 19.2503 18.1549 19.1354 18.3606 18.9306L18.9293 18.3619C19.052 18.2392 19.1437 18.0892 19.197 17.9241C19.2502 17.7591 19.2634 17.5837 19.2356 17.4125ZM15.8567 11.5255C15.8161 11.4848 15.7679 11.4526 15.7147 11.4305C15.6616 11.4085 15.6047 11.3972 15.5472 11.3972C15.4897 11.3972 15.4328 11.4085 15.3797 11.4305C15.3266 11.4526 15.2783 11.4848 15.2377 11.5255L11.5255 15.2377C11.4848 15.2783 11.4525 15.3266 11.4305 15.3797C11.4085 15.4328 11.3972 15.4897 11.3972 15.5472C11.3972 15.6047 11.4085 15.6616 11.4305 15.7148C11.4525 15.7679 11.4848 15.8161 11.5255 15.8567C11.6474 15.9787 11.7921 16.0753 11.9513 16.1413C12.1106 16.2073 12.2813 16.2413 12.4536 16.2413C12.626 16.2413 12.7967 16.2073 12.956 16.1413C13.1152 16.0753 13.2599 15.9787 13.3818 15.8567L15.8567 13.3818C15.9786 13.2599 16.0753 13.1152 16.1413 12.956C16.2073 12.7967 16.2412 12.626 16.2412 12.4537C16.2412 12.2813 16.2073 12.1106 16.1413 11.9513C16.0753 11.7921 15.9786 11.6474 15.8567 11.5255ZM18.8046 5.2885C18.8803 5.21736 18.9271 5.12076 18.9359 5.01725L19.2465 2.23475C19.2548 2.16956 19.2482 2.10333 19.2269 2.04114C19.2057 1.97894 19.1705 1.92244 19.124 1.87597C19.0775 1.8295 19.021 1.7943 18.9589 1.77307C18.8967 1.75184 18.8304 1.74514 18.7652 1.7535L15.9827 2.06412C15.8792 2.07294 15.7826 2.11968 15.7115 2.19537L11.1002 7.10675L13.8022 9.98506L18.8046 5.2885ZM2.86693 16.2794C2.76315 16.3804 2.64525 16.4659 2.51693 16.5331L2.35943 16.6075C2.2036 16.6845 2.06835 16.7974 1.9649 16.9371C1.86145 17.0768 1.79276 17.239 1.76454 17.4105C1.73631 17.582 1.74936 17.7578 1.8026 17.9232C1.85584 18.0887 1.94773 18.239 2.07068 18.3619L2.63943 18.9306C2.84476 19.1351 3.12271 19.2499 3.41249 19.25C3.47107 19.25 3.52956 19.2456 3.58749 19.2369C3.75867 19.2103 3.92087 19.1426 4.06028 19.0398C4.19969 18.937 4.31216 18.8019 4.38812 18.6463L4.47562 18.4713C4.54127 18.3457 4.62532 18.2307 4.72499 18.13L6.68937 16.1656L4.83437 14.3106L2.86693 16.2794ZM5.7623 11.5255C5.72167 11.4848 5.67342 11.4526 5.62031 11.4305C5.5672 11.4085 5.51027 11.3972 5.45277 11.3972C5.39528 11.3972 5.33835 11.4085 5.28524 11.4305C5.23212 11.4526 5.18387 11.4848 5.14324 11.5255C5.02134 11.6474 4.92464 11.7921 4.85867 11.9513C4.7927 12.1106 4.75874 12.2813 4.75874 12.4537C4.75874 12.626 4.7927 12.7967 4.85867 12.956C4.92464 13.1152 5.02134 13.2599 5.14324 13.3818L7.61818 15.8567C7.74006 15.9787 7.88476 16.0753 8.04401 16.1413C8.20326 16.2073 8.37396 16.2413 8.54633 16.2413C8.71871 16.2413 8.8894 16.2073 9.04866 16.1413C9.20791 16.0753 9.35261 15.9787 9.47449 15.8567C9.51517 15.8161 9.54744 15.7679 9.56945 15.7148C9.59147 15.6616 9.6028 15.6047 9.6028 15.5472C9.6028 15.4897 9.59147 15.4328 9.56945 15.3797C9.54744 15.3266 9.51517 15.2783 9.47449 15.2377L5.7623 11.5255Z" fill="white"/>
                        </g>
                        </g>
                        <defs>
                        <filter id="filter0_d_0_312" x="-2.25" y="-2.24999" width="25.5" height="25.5" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
                        <feFlood flood-opacity="0" result="BackgroundImageFix"/>
                        <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha"/>
                        <feOffset/>
                        <feGaussianBlur stdDeviation="2"/>
                        <feComposite in2="hardAlpha" operator="out"/>
                        <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0"/>
                        <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_0_312"/>
                        <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_0_312" result="shape"/>
                        </filter>
                        <clipPath id="clip0_0_312">
                        <rect width="21" height="21" fill="white"/>
                        </clipPath>
                        </defs>
                    </svg>
                    {attackingPlayersInZone}</span>
                {#if attackName === "Marabunta Grande"}
                    <h1 class="blue">{attackName}</h1>
                {/if}
                {#if attackName === "Vagos"}
                    <h1 class="yellow">{attackName}</h1>
                {/if}
                {#if attackName === "Ballas"}
                    <h1 class="violet">{attackName}</h1>
                {/if}
                {#if attackName === "The Families"}
                    <h1 class="green">{attackName}</h1>
                {/if}
                {#if attackName === "Bloods Street"}
                    <h1 class="red">{attackName}</h1>
                {/if}
            </div>
            <div class="capt_bar">
                <div class="bar_bg" style="width: {getProgress (attackingCount, (attackingCount + protectingCount))}%"></div>
            </div>
        </div>
        <div class="centercapt">
        </div>
        <div class="rightcapt">
            <div class="capt_info">
                {#if protectingName === "Marabunta Grande"}
                    <h1 class="blue">{protectingName}</h1>
                {/if}
                {#if protectingName === "Vagos"}
                    <h1 class="yellow">{protectingName}</h1>
                {/if}
                {#if protectingName === "Ballas"}
                    <h1 class="violet">{protectingName}</h1>
                {/if}
                {#if protectingName === "The Families"}
                    <h1 class="green">{protectingName}</h1>
                {/if}
                {#if protectingName === "Bloods Street"}
                    <h1 class="red">{protectingName}</h1>
                {/if}
                <span>
                    {protectingPlayersInZone}
                    <svg width="21" height="21" viewBox="0 0 21 21" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <g clip-path="url(#clip0_0_306)">
                        <g filter="url(#filter0_d_0_306)">
                        <path d="M17.1774 3.57984L11.1694 1.43062C10.737 1.27313 10.263 1.27313 9.83063 1.43062L3.82266 3.57984C3.43676 3.71851 3.10303 3.97283 2.86697 4.30813C2.63091 4.64343 2.50402 5.04338 2.5036 5.45344V10.1587C2.5036 10.2244 2.74969 16.6622 9.73548 19.5366C10.2253 19.7378 10.7747 19.7378 11.2645 19.5366C18.2503 16.6622 18.4964 10.2244 18.4964 10.1489V5.45344C18.496 5.04338 18.3691 4.64343 18.133 4.30813C17.897 3.97283 17.5633 3.71851 17.1774 3.57984ZM11.1202 17.5809C10.7226 17.7428 10.2774 17.7428 9.87985 17.5809C4.41329 15.3333 4.21969 10.2867 4.21969 10.2375V6.58219C4.22031 6.24889 4.32366 5.92388 4.51566 5.65144C4.70767 5.37899 4.97898 5.17236 5.29266 5.05969L9.95532 3.38953C10.3074 3.26266 10.6927 3.26266 11.0447 3.38953L15.7074 5.05969C16.021 5.17236 16.2923 5.37899 16.4844 5.65144C16.6764 5.92388 16.7797 6.24889 16.7803 6.58219V10.2277C16.7803 10.2867 16.5867 15.3333 11.1202 17.5809Z" fill="white"/>
                        </g>
                        <g filter="url(#filter1_d_0_306)">
                        <path d="M15.4875 5.67984L10.8248 4.00641C10.6143 3.93423 10.3857 3.93423 10.1751 4.00641L5.51248 5.67984C5.32635 5.74636 5.16531 5.86874 5.05137 6.03025C4.93742 6.19177 4.87615 6.38453 4.87592 6.58219V10.2277C4.88248 10.4081 5.06951 14.8936 10.1292 16.9739C10.2467 17.0226 10.3727 17.0477 10.5 17.0477C10.6272 17.0477 10.7532 17.0226 10.8708 16.9739C15.9304 14.8936 16.1175 10.4081 16.124 10.2211V6.58219C16.1238 6.38453 16.0625 6.19177 15.9486 6.03025C15.8347 5.86874 15.6736 5.74636 15.4875 5.67984Z" fill="white"/>
                        </g>
                        </g>
                        <defs>
                        <filter id="filter0_d_0_306" x="-1.4964" y="-2.6875" width="23.9928" height="26.375" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
                        <feFlood flood-opacity="0" result="BackgroundImageFix"/>
                        <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha"/>
                        <feOffset/>
                        <feGaussianBlur stdDeviation="2"/>
                        <feComposite in2="hardAlpha" operator="out"/>
                        <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0"/>
                        <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_0_306"/>
                        <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_0_306" result="shape"/>
                        </filter>
                        <filter id="filter1_d_0_306" x="0.875916" y="-0.047725" width="19.2481" height="21.0955" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
                        <feFlood flood-opacity="0" result="BackgroundImageFix"/>
                        <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha"/>
                        <feOffset/>
                        <feGaussianBlur stdDeviation="2"/>
                        <feComposite in2="hardAlpha" operator="out"/>
                        <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0.25 0"/>
                        <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_0_306"/>
                        <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_0_306" result="shape"/>
                        </filter>
                        <clipPath id="clip0_0_306">
                        <rect width="21" height="21" fill="white"/>
                        </clipPath>
                        </defs>
                    </svg>                        
                </span>
            </div>
            <div class="capt_bar">
                <div class="bar_bg" style="width: {getProgress (protectingCount, (attackingCount + protectingCount))}%"></div>
            </div>
        </div>
    </div>
</div>
{/if}
<!--<div class="hudevo__activecapt" in:fly={{ y: -50, duration: 500 }} out:fly={{ y: -50, duration: 250 }}>
    <div class="hudevo__activecapt_image">{title}</div>
    <div class="box-between">
        <div class="box-flex">
            {#if warGripType === 0 || warGripType === 3}
                <div class="hudevoicon-skull mr"></div>
            {:else}
                <div class="hudevoicon-star mr"></div>
            {/if}
            <div>{protectingCount}</div>
        </div>
        <div class="box-flex">
            {time}
        </div>
        <div class="box-flex">
            <div>{attackingCount}</div>
            {#if warGripType === 0 || warGripType === 3}
                <div class="hudevoicon-skull ml"></div>
            {:else}
                <div class="hudevoicon-star ml"></div>
            {/if}
        </div>
    </div>
    <div class="box-between mt-14">
        <div class="hudevo__activecapt_square green"></div>
        <div class="hudevo__activecapt_line">
            <div class="hudevo__activecapt_left" style="width: {getProgress (protectingCount, (attackingCount + protectingCount))}%"></div>
            <div class="hudevo__activecapt_center"></div>
            <div class="hudevo__activecapt_right" style="width: {getProgress (attackingCount, (attackingCount + protectingCount))}%"></div>
        </div>
        <div class="hudevo__activecapt_square red"></div>
    </div>
    <div class="box-between mt-14">
        <div class="activewidth l">{protectingName}</div>
        <div class="fs-10">{typeBattle[warGripType]}, {weaponsCategory[weaponType]}</div>
        <div class="activewidth r">{attackName}</div>
    </div>
    <div class="box-between mt-5">
        <div class="box-flex">
            <div class="hudevoicon-war-person mr"></div>
            <div class="activewidth l">{protectingPlayersInZone}</div>
        </div>
        <div class="box-flex">
            <div class="hudevoicon-war-person mr"></div>
            <div>23</div>
        </div>
        <div class="box-flex">
            <div class="activewidth r">{attackingPlayersInZone}</div>
            <div class="hudevoicon-war-person ml"></div>
        </div>
    </div>
</div>-->