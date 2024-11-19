<script>
    import { translateText } from 'lang'
    import { fly } from 'svelte/transition';
    import keys from 'store/keys'
    import keysName from 'json/keys.js'
    import './main.css';

    const InfoData = {
        Warehouse: translateText('player2', 'Открыть склад'),
        DriveSchool: translateText('player2', 'Открыть меню автошколы'),
        RentCar: translateText('player2', 'Открыть меню аренды'),
        Festive: translateText('player2', 'Открыть обмен Coin'),
        SaluteShop: translateText('player2', 'Открыть магазин фейерверков'),
        BlackMarket: translateText('player2', 'Открыть чёрный рынок'),
        TankRoyale: translateText('player2', 'Регистрация Tank Royale'),
        Casino: translateText('player2', 'двери в казино'),
        Atm: translateText('player2', 'Открыть меню банкомата'),
        Electrician: translateText('player2', 'Переодеться'),
        Repair: translateText('player2', 'Ремонтировать транспорт'),
        SeatingArrangements: translateText('player2', 'Взаимодействовать'),
        BuyPoints: translateText('player2', 'Купить алкоголь'),
        EnterGarage: translateText('player2', 'Войти в гараж'),
        ExitGarage: translateText('player2', 'Выйти из гаража'),
        EnterHouse: [
            {
                text: translateText('player2', 'Войти в дом'),
                key: 9
            },
            {
                text: translateText('player2', 'Открыть/закрыть дом'),
                key: 39
            }
        ],
        ExitHouse: translateText('player2', 'Выйти из дома'),
        HealkitHouse: translateText('player2', 'Использовать аптечку'),
        EnterHotel: translateText('player2', 'Войти в отель'),
        ExitHotel: translateText('player2', 'Выйти из отеля'),
        CarRentHotel: translateText('player2', 'Арендовать скутер'),
        FractionCityhallBeginWorkDay: translateText('player2', 'Раздевалка CityHall'),
        FractionCityhallGunMenu: translateText('player2', 'Оружейный склад CityHall'),
        JobGoPosta: translateText('player2', 'Начать работу почтальона'),
        JobGoPostaCar: translateText('player2', 'Взять рабочий транспорт'),
        TakeMoney: translateText('player2', 'Взять мешок с деньгами'),
        ChangeAutoNumber: translateText('player2', 'Открыть меню смены номеров'),
        ElectionPoint: translateText('player2', 'Открыть меню выборов'),
        Bear: translateText('player2', 'Нажмите чтобы поднять мишку'),

        BoatFix: translateText('player2', 'Нажмите чтобы починить лодку'),

        buyMetro: translateText('player2', 'Купить билет'),
        exitMetro: translateText('player2', 'Для выхода'),
        Seating: translateText('player2', 'Чтобы сесть'),
        SeatingUp: translateText('player2', 'Встать'),
    }

    let visible = false;
    window.hudEnter = (text) => {
        if (text === -1)
            visible = false;
        else
            visible = InfoData[text] ? InfoData[text] : translateText('player2', 'Нажмите для взаимодействия');
    }
</script> 
{#if visible}
    {#if typeof visible == "string"}
    <div class="hudhaxzer_use">
        <div class="usetitle">
            <div class="titleico">
                <svg width="55" height="55" viewBox="0 0 55 55" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <g filter="url(#filter0_d_35_390)">
                        <path d="M26.8805 7.59034C22.9898 7.72621 19.2234 8.99463 16.0431 11.24C12.8629 13.4854 10.4071 16.6102 8.97693 20.231C7.54678 23.8519 7.20448 27.8114 7.99201 31.624C8.77953 35.4365 10.6626 38.9364 13.4104 41.6942C16.1581 44.4521 19.651 46.3481 23.4606 47.1496C27.2703 47.9512 31.231 47.6235 34.8571 46.2067C38.4832 44.7899 41.617 42.3456 43.8741 39.1736C46.1312 36.0017 47.4134 32.2399 47.5636 28.3498L45.5651 28.2726C45.4299 31.7737 44.2759 35.1593 42.2445 38.0141C40.2131 40.8689 37.3927 43.0687 34.1293 44.3438C30.8658 45.619 27.3011 45.9139 23.8724 45.1925C20.4437 44.4711 17.3001 42.7647 14.8272 40.2826C12.3542 37.8005 10.6594 34.6507 9.95066 31.2194C9.24189 27.7881 9.54996 24.2245 10.8371 20.9657C12.1242 17.707 14.3344 14.8947 17.1967 12.8738C20.0589 10.853 23.4487 9.7114 26.9503 9.58913L26.8805 7.59034Z" fill="#FFFFFF"></path>
                    </g>
                    <defs>
                        <filter id="filter0_d_35_390" x="0.578613" y="0.589844" width="53.9849" height="53.9883" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
                            <feFlood flood-opacity="0" result="BackgroundImageFix"></feFlood>
                            <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha"></feColorMatrix>
                            <feOffset></feOffset>
                            <feGaussianBlur stdDeviation="3.5"></feGaussianBlur>
                            <feComposite in2="hardAlpha" operator="out"></feComposite>
                            <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0.568627 0 0 0 0 1 0 0 0 0.5 0"></feColorMatrix>
                            <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_35_390"></feBlend>
                            <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_35_390" result="shape"></feBlend>
                        </filter>
                    </defs>
                </svg>
            </div>
            <p>{keysName[$keys[9]]}</p>
        </div>
        <div class="usetext">{visible}</div>
    </div>
    {:else}
        {#each visible as item}   
            <div class="hudhaxzer_use">
                <div class="usetitle">
                    <div class="titleico">
                        <svg width="55" height="55" viewBox="0 0 55 55" fill="none" xmlns="http://www.w3.org/2000/svg">
                            <g filter="url(#filter0_d_35_390)">
                                <path d="M26.8805 7.59034C22.9898 7.72621 19.2234 8.99463 16.0431 11.24C12.8629 13.4854 10.4071 16.6102 8.97693 20.231C7.54678 23.8519 7.20448 27.8114 7.99201 31.624C8.77953 35.4365 10.6626 38.9364 13.4104 41.6942C16.1581 44.4521 19.651 46.3481 23.4606 47.1496C27.2703 47.9512 31.231 47.6235 34.8571 46.2067C38.4832 44.7899 41.617 42.3456 43.8741 39.1736C46.1312 36.0017 47.4134 32.2399 47.5636 28.3498L45.5651 28.2726C45.4299 31.7737 44.2759 35.1593 42.2445 38.0141C40.2131 40.8689 37.3927 43.0687 34.1293 44.3438C30.8658 45.619 27.3011 45.9139 23.8724 45.1925C20.4437 44.4711 17.3001 42.7647 14.8272 40.2826C12.3542 37.8005 10.6594 34.6507 9.95066 31.2194C9.24189 27.7881 9.54996 24.2245 10.8371 20.9657C12.1242 17.707 14.3344 14.8947 17.1967 12.8738C20.0589 10.853 23.4487 9.7114 26.9503 9.58913L26.8805 7.59034Z" fill="#FFFFFF"></path>
                            </g>
                            <defs>
                                <filter id="filter0_d_35_390" x="0.578613" y="0.589844" width="53.9849" height="53.9883" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
                                    <feFlood flood-opacity="0" result="BackgroundImageFix"></feFlood>
                                    <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha"></feColorMatrix>
                                    <feOffset></feOffset>
                                    <feGaussianBlur stdDeviation="3.5"></feGaussianBlur>
                                    <feComposite in2="hardAlpha" operator="out"></feComposite>
                                    <feColorMatrix type="matrix" values="0 0 0 0 0 0 0 0 0 0.568627 0 0 0 0 1 0 0 0 0.5 0"></feColorMatrix>
                                    <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_35_390"></feBlend>
                                    <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_35_390" result="shape"></feBlend>
                                </filter>
                            </defs>
                        </svg>
                    </div>
                    <p>{keysName[$keys[item.key]]}</p>
                </div>
                <div class="usetext">{item.text}</div>
            </div> 
        {/each} 
    {/if}
{/if}

<!-- {#if visible}
    {#if typeof visible == "string"}
    <div class="box-flex box-center">
        <div class="buttonsinfo__button mr-6">{keysName[$keys[9]]}</div>
        <div class="hudevo-usebutton__text">{visible}</div>
    </div>
    {:else}    
        <div class="box-column box-center hudevo__enters">
            {#each visible as item}            
                <div class="hudevo__enter mb-5">
                    <div class="buttonsinfo__button mr-6">{keysName[$keys[item.key]]}</div>
                    <div class="hudevo-usebutton__text">{item.text}</div>
                </div>
            {/each}
        </div>
    {/if}
{/if} -->