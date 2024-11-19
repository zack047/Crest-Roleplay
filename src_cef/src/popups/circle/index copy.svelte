<script>
    import { translateText } from 'lang'
    import Animations from 'json/animations.js'
    import { storeAnimFavorites, storeAnimBind } from 'store/animation'
    import { spring } from 'svelte/motion';
    import { executeClient } from 'api/rage'
    export let viewData;
    
    let animMenuList = [
        {
            id: 0,
            title: translateText('player', 'Избранное'),
            count: 0
        },
        {
            id: 1,
            title: translateText('player', 'Сесть/Лечь'),
            count: 0
        },
        {
            id: 2,
            title: translateText('player', 'Социальные'),
            count: 0
        },
        {
            id: 3,
            title: translateText('player', 'Позы'),
            count: 0
        },
        {
            id: 4,
            title: translateText('player', 'Неприличное'),
            count: 0
        },
        {
            id: 5,
            title: translateText('player', 'Физ. упражнения'),
            count: 0
        },
        {
            id: 6,
            title: translateText('player', 'Танцы'),
            count: 0
        },
        {
            id: 7,
            title: translateText('player', 'Прочее'),
            count: 0
        }
    ];
    let selectMenu = animMenuList [0];

    Object.values(Animations).forEach(animation => {
        animMenuList.forEach((item, index) => {
            if (animation[0] === item.title) {
                animMenuList[index].count++;
            }
        });
    });
    
    let favoritesAnim = [];
    storeAnimFavorites.subscribe((value) => {
        favoritesAnim = value;
        animMenuList[0].count = favoritesAnim.length;
    });

    const onSelectMenu = (index) => {
        selectMenu = animMenuList [index];
    }
    let enterAnim = "";

    function handleSlotMouseEnter (index) {
        if (DragonDropData != "")
            return;
        enterAnim = `${selectMenu.id}_${index}`;
    }
	
	// Когда выходим из зоны ячейки
	function handleSlotMouseLeave() {
        enterAnim = "";
    }

    let dubleClickData = ''
    let dubleClickTime = 0;
    const onPlayAnimation = (item) => {
        if (dubleClickData === item && dubleClickTime > new Date().getTime()) {
            executeClient ("client.animation.play", item);
            //window.events.callEvent("hud.enter", 'SPACE', 'Нажмите чтобы отменить анимацию');
        } else {
            dubleClickTime = new Date().getTime() + 1000;
            dubleClickData = item
        }
    }

    let DragonDropData = "";
    let offsetInElementX = 0;
    let offsetInElementY = 0;
    let clientX = 0;
    let clientY = 0;

    /* Functions */
    let coords = spring({ x: 0, y: 0 }, {
        stiffness: 1.0,
        damping: 1.0
    });

    const handleMouseDown = (event, item) => {
        const target = event.target.getBoundingClientRect();

        offsetInElementX = (target.width - (target.right - event.clientX)) * 0.7222;
        offsetInElementY = (target.height - (target.bottom - event.clientY)) * 0.7222;
        DragonDropData = item;
        coords.set({ x: event.clientX, y: event.clientY });
        clientX = event.clientX;
        clientY = event.clientY;
    }
    
    let favoriteIndex = -1;
    function handleFavoriteSlotMouseEnter (index) {
        favoriteIndex = index;
    }
	
	// Когда выходим из зоны ячейки
	function handleFavoriteSlotMouseLeave() {
        favoriteIndex = -1;
    }

    let fastSlotIndex = -1;
    let fastSlotAnim = true;

    function handleFastSlotMouseEnter (index) {
        fastSlotIndex = index;
    }
	
	// Когда выходим из зоны ячейки
	function handleFastSlotMouseLeave() {
        fastSlotIndex = -1;
    }

    const handleGlobalMouseUp = () => {
        if (fastSlotIndex !== -1 && DragonDropData != "" && DragonDropData.split("_") && DragonDropData.split("_").length) {
            window.animationStore.addAnimBind(fastSlotIndex, DragonDropData);
        }
        DragonDropData = "";
        fastSlotAnim = false;
    }


    const onDell = (item) => {
        window.animationStore.dellAnimBind(item);
    }
    // Глобальные эвенты    
    const handleGlobalMouseMove = (event) => {
        if (DragonDropData != "" && DragonDropData.split("_") && DragonDropData.split("_").length) {
            if (clientX !== event.clientX || clientY !== event.clientY) {
                dubleClickData = ''
                coords.set({ x: event.clientX, y: event.clientY });
                fastSlotAnim = true;
            }
        }
    }

    const IsFavorite = (index, AnimListFavorites) => {
        let success = false;
        if (AnimListFavorites) {
            if (AnimListFavorites.findIndex(a => a == `${selectMenu.id}_${index}`) !== -1) success = true;
        }
        return success;
    }

    const AddFavorite = (event, item) => {
        event.stopPropagation();
        window.animationStore.addAnimFavorite(item);
    }

    const DellFavorite = (event, item) => {
        event.stopPropagation();
        window.animationStore.dellAnimFavorite(item);
    }

    const StopAnim = () => {
        viewData = false;
        executeClient ("client.animation.stop");
    }

    const OnClose = () => {
        executeClient ("escape");
    }
  
    let hovename = 0;

    function handleFocus(event) {
        // Обработка фокуса
    }

    function handleBlur() {
        // Обработка потери фокуса
    }
  
    function handleMousemove(label) {
      hovename = label;
    }

    const startanim = (item) => {
      executeClient ("client.animation.play", item)
    }

    const stopanim = () => {
      executeClient ("client.animation.stop");
    }

    let showNameAnim = true;

  </script>
  
  <style>
   .circle-menu {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      width: 77.7778vh;
      height: 77.7778vh;
      border-radius: 50%;
      display: flex;
      justify-content: center;
      align-items: center;
    }
  
   .menu-item {
        position: absolute;
        width: 17.4074vh;
        height: 17.4074vh;
        color: #fff;
        text-align: center;
        line-height: 7.4074vh;
        cursor: pointer;
        transform-origin: 50% 50%;
        transition: background-color 0.2s ease;
    }
    
    .blockbg {
        background: url(https://imgur.com/hyOOgT1.png);
        background-size: cover;
        width: 28.4259vh;
        height: 17.6852vh;
        rotate: 90deg;
    }

    .menu-item:hover .blockbg {
        background: url(https://imgur.com/OW9fm7Y.png);
        background-size: cover;
    }

    .menu-item:hover .blockbg::after {
        content: "";
        background: url(https://imgur.com/RNOSWS1.png);
        background-size: cover;
        position: absolute;
        display: block;
        margin-top: -1.7593vh;
        margin-left: -0.3704vh;
        width: 29.2593vh;
        height: 3.7963vh;
    }

    .menu-item:nth-child(2) img {
        position: absolute;
        width: 7.963vh;
        height: 7.963vh;
        z-index: 1;
        rotate: 90deg;
        margin-left: 2.5vh;
        margin-top: 5vh;
    }

    .menu-item:nth-child(3) img {
        position: absolute;
        width: 7.963vh;
        height: 7.963vh;
        z-index: 1;
        rotate: 45deg;
        margin-left: 2.5vh;
        margin-top: 5vh;
    }

    .menu-item:nth-child(4) img {
        position: absolute;
        width: 7.963vh;
        height: 7.963vh;
        z-index: 1;
        rotate: 0deg;
        margin-left: 2.5vh;
        margin-top: 5vh;
    }

    .menu-item:nth-child(5) img {
        position: absolute;
        width: 7.963vh;
        height: 7.963vh;
        z-index: 1;
        rotate: 315deg;
        margin-left: 2.5vh;
        margin-top: 5vh;
    }

    .menu-item:nth-child(6) img {
        position: absolute;
        width: 7.963vh;
        height: 7.963vh;
        z-index: 1;
        rotate: 270deg;
        margin-left: 2.5vh;
        margin-top: 5vh;
    }

    .menu-item:nth-child(7) img {
        position: absolute;
        width: 7.963vh;
        height: 7.963vh;
        z-index: 1;
        rotate: 225deg;
        margin-left: 2.5vh;
        margin-top: 5vh;
    }

    .menu-item:nth-child(8) img {
        position: absolute;
        width: 7.963vh;
        height: 7.963vh;
        z-index: 1;
        rotate: 180deg;
        margin-left: 2.5vh;
        margin-top: 5vh;
    }

    .menu-item:nth-child(9) img {
        position: absolute;
        width: 7.963vh;
        height: 7.963vh;
        z-index: 1;
        rotate: 135deg;
        margin-left: 2.5vh;
        margin-top: 5vh;
    }

    .stopanim {
        height: 42.5556vh;
        width: 42.5556vh;
        position: absolute;
        border-radius: 50%;
        color: #fff;
        display: flex;
        align-items: center;
        justify-content: center;
        background: rgb(18, 18, 18, 0.4);
        text-shadow: 0vh 0vh 0.37vh rgba(0, 0, 0, 0.4);
        font-family: SF Pro Display;
        font-size: 1.8519vh;
        font-style: normal;
        font-weight: 500;
        line-height: normal;
        transition: opacity 0.2s ease;
        flex-direction: column;
    }

    .stopanim svg {
        width: 8.7037vh;
        height: 8.7037vh;
        margin-bottom: 4.0741vh;
    }

    .nameanim {
        position: absolute;
        color: #fff;
        height: 5.3704vh;
        display: flex;
        align-items: center;
        justify-content: center;
        background: #111111;
        padding: 0 2.8148vh;
        min-width: 12.2222vh;
        text-shadow: 0vh 0vh 0.37vh rgba(0, 0, 0, 0.60);
        font-family: SF Pro Display;
        font-size: 1.8519vh;
        font-style: normal;
        font-weight: 500;
        line-height: normal;
        transition: opacity 0.2s ease;
        opacity: 0;
    }

    .circle-info {
        position: absolute;
        bottom: 0vh;
        width: 64.4444vh;
        height: 6.6667vh;
        background: rgb(18, 18, 18, 0.7);
        display: flex;
        align-items: center;
        justify-content: center;
        left: 50%;
        transform: translate(-50%, 0vh);
    }

    .circle-info p {
        font-family: SF Pro Display;
        color: rgb(255,255,255, 0.7);
        font-size: 1.6667vh;
        font-style: normal;
        font-weight: 400;
        line-height: normal;
    }
  </style>
  
  <div class="circle-menu">
    <div style="display: flex;align-items: center;justify-content: center;">
      <div class="stopanim" style={showNameAnim? '' : 'opacity: 0'} on:keypress on:click={stopanim}>
        <svg width="94" height="94" viewBox="0 0 94 94" fill="none" xmlns="http://www.w3.org/2000/svg">
          <g clip-path="url(#clip0_1072_93)">
          <path d="M76.9333 41.2461H74.9572V45.3426L76.9333 45.3417C78.0941 45.3417 79.0751 44.4039 79.0751 43.2939C79.0751 42.1839 78.0941 41.2461 76.9333 41.2461ZM54.8124 41.2461C51.6397 41.2461 49.0586 43.8272 49.0586 46.9997C49.0586 50.1722 51.6397 52.7534 54.8124 52.7534C57.9849 52.7534 60.5661 50.1722 60.5661 46.9997C60.5661 43.8272 57.9849 41.2461 54.8124 41.2461Z" fill="white" fill-opacity="0.5"/>
          <path d="M66.4679 0H27.5321L0 27.5319V66.4681L27.5321 94H66.4679L94 66.4681V27.5319L66.4679 0ZM25.4623 52.454C24.8612 55.9243 21.5879 58.4432 17.6795 58.4432C14.588 58.4432 11.683 57.1942 9.70936 55.0163L13.7906 51.3178C14.7084 52.3306 16.1623 52.9354 17.6795 52.9354C19.1185 52.9354 19.9357 52.087 20.035 51.5138C20.0789 51.2605 20.1955 50.5876 18.7152 50.0313C17.7563 49.6709 17.0478 49.4974 16.3626 49.3296C15.3136 49.0728 14.2287 48.8073 12.9839 47.998C10.7764 46.5634 10.0306 44.131 10.0688 42.333C10.1263 39.616 11.9918 37.1085 14.7106 36.094C19.3064 34.3788 23.0019 37.1239 25.21 38.764L21.9261 43.1857C18.6768 40.7727 17.6845 40.863 16.6362 41.2542C15.8757 41.5381 15.582 42.1383 15.5755 42.45C15.5687 42.7674 15.7099 43.2011 15.9853 43.3799C16.4243 43.6652 16.7617 43.7568 17.6724 43.9799C18.4356 44.1668 19.3857 44.3994 20.6525 44.8755C24.1446 46.1876 26.0325 49.1624 25.4623 52.454ZM42.5042 41.2464H37.7869V58.2616H32.2791V41.2464H27.5996V35.7385H42.5043V41.2464H42.5042ZM54.8115 58.2615C48.6019 58.2615 43.5499 53.2095 43.5499 47C43.5499 40.7905 48.6019 35.7385 54.8115 35.7385C61.0211 35.7385 66.073 40.7905 66.073 47C66.073 53.2095 61.0211 58.2615 54.8115 58.2615ZM76.9324 50.8498L74.9485 50.8505V58.2615H69.4407V35.7385H76.9324C81.1505 35.7385 84.582 39.1281 84.582 43.2942C84.582 47.4603 81.1505 50.8498 76.9324 50.8498Z" fill="white" fill-opacity="0.5"/>
          </g>
          <defs>
          <clipPath id="clip0_1072_93">
          <rect width="94" height="94" fill="white"/>
          </clipPath>
          </defs>
        </svg>        
        <p>Stop animation</p>
      </div>
      {#if hovename != 0 }
        <div class="nameanim" style={showNameAnim? '' : 'opacity: 1'}> 
          <p>{hovename}</p>
        </div>
      {/if}
    </div>
    {#each $storeAnimBind.slice(0, 8) as item, i}
      <div class="menu-item" on:focus={handleFocus} on:blur={handleBlur} on:keypress on:mouseover={(event) => handleMousemove(Animations [item][2])} on:mouseover={() => showNameAnim = false} on:mouseout={() => showNameAnim = true} style={`transform: rotate(${i * 45 - 90}deg) translate(23.5vh);`} on:click={startanim(item)}>
        {#if $storeAnimBind && Animations [item]}
            <img src="" alt="">
        {/if}
        <div class="blockbg">
        </div>
      </div>
    {/each}
  </div>

  <div class="circle-info">
    <p>Get involved and click the animation to reproduce it</p>
  </div>