<script>
    import eat from '../images/eatico.svg'
    let img = [
        {src: eat, class: "eat"}
    ]
    
    window.hudStore.food = (meat) => {
        console.log(meat)
        let foods = document.getElementById("foodid");
        foods.style.transform = 'translate(0' + ',' + (100 - meat) + '%)';
        //  if (meat == 100) {
        //      console.log("full")
        //  }
    };

</script>
<style>

.box {
    height: 4.259vh;
    width: 4.259vh;
    display: flex;
    margin-right: 1.019vh;
    background: radial-gradient(circle, rgba(255, 255, 255, 0.30) 0%, rgba(255, 255, 255, 0.00) 100%);
    border-radius: 100%;
    overflow: hidden;
    justify-content: center;
    align-items: center;
}
.box .percent {
    z-index: 3;
    display: flex;
    align-items: center;
    justify-content: center;
    position: absolute;
}
.box .eatclass {
    z-index: 2;
    width: 100%;
    height: 100%;
    transform: translate(0, 100%);
    background: #FF6E41;
    transition: all 0.3s;
}
.box .water_wave {
    width: 200%;
    position: absolute;
    bottom: 100%;
}
.box .water_wave_back {
    right: 0;
    fill: #c53d13;
    animation: wave-back 1.4s infinite linear;
}
.box .water_wave_front {
    left: 0;
    fill: #ffa98f;
    margin-bottom: -.1vh;
    animation: wave-front 0.7s infinite linear;
}

.box .eat {
    width: 1.852vh;
    height: 1.852vh;
}
@-webkit-keyframes wave-front {
    100% {
        transform: translate(-50%, 0);
    }
}

@keyframes wave-front {
    100% {
        transform: translate(-50%, 0);
    }
}

@-webkit-keyframes wave-back {
    100% {
        transform: translate(50%, 0);
    }
}

@keyframes wave-back {
    100% {
        transform: translate(50%, 0);
    }
}

</style>

<svg version="1.1" x="0" y="0" style="display: none;">
    <symbol id="wave">
      <path d="M420,20c21.5-0.4,38.8-2.5,51.1-4.5c13.4-2.2,26.5-5.2,27.3-5.4C514,6.5,518,4.7,528.5,2.7c7.1-1.3,17.9-2.8,31.5-2.7c0,0,0,0,0,0v20H420z"></path>
      <path d="M420,20c-21.5-0.4-38.8-2.5-51.1-4.5c-13.4-2.2-26.5-5.2-27.3-5.4C326,6.5,322,4.7,311.5,2.7C304.3,1.4,293.6-0.1,280,0c0,0,0,0,0,0v20H420z"></path>
      <path d="M140,20c21.5-0.4,38.8-2.5,51.1-4.5c13.4-2.2,26.5-5.2,27.3-5.4C234,6.5,238,4.7,248.5,2.7c7.1-1.3,17.9-2.8,31.5-2.7c0,0,0,0,0,0v20H140z"></path>
      <path d="M140,20c-21.5-0.4-38.8-2.5-51.1-4.5c-13.4-2.2-26.5-5.2-27.3-5.4C46,6.5,42,4.7,31.5,2.7C24.3,1.4,13.6-0.1,0,0c0,0,0,0,0,0l0,20H140z"></path>
    </symbol>
</svg>
<div class="box">
    <div class="percent">
        <div class="percentNum" id="count">
            <img src={img[0].src} class={img[0].class} alt="">
        </div>
    </div>
    <div id="foodid" class="eatclass">
        <svg viewBox="0 0 560 20" class="water_wave water_wave_back">
            <use xlink:href="#wave"></use>
        </svg>
        <svg viewBox="0 0 560 20" class="water_wave water_wave_front">
            <use xlink:href="#wave"></use>
        </svg>
    </div>
</div>
