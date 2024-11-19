<!-- newauth.svelte -->
<script>
    
    import { translateText } from 'lang';
    import { accountIsSession } from 'store/account';
    import { executeClient } from 'api/rage';
    import { accountLogin } from 'store/account';
    import { onMount } from 'svelte';
    import { TweenLite, Linear, Back, Power1, gsap } from 'gsap';
    import { CSSPlugin } from 'gsap/CSSPlugin';
    import InputCustom from 'components/input/index.svelte';
    export let OnSelectViews;

	import iMonkey from '../assets/img/monkey.png';

	import iUser from '../assets/img/icons/user.svg';
	import iPassword from '../assets/img/icons/password.svg';
	import iPromo from '../assets/img/icons/promo.svg';
	import iEmail from '../assets/img/icons/email.svg';

	import '../assets/fonts/Gilroy/font.css';
	import '../assets/scss/style.scss';


    
    let loginInput = "";
    let passwordInput = "";
    let loginIsFocus = false;
    let passwordIsFocus = false;

    let sparksContainer;
    let density = 120;
    let speed = 0.5;
    let winHeight = window.innerHeight;
    let winWidth = window.innerWidth;
    let start = {
      yMin: 0,
      yMax: winHeight,
      xMin: winWidth,
      xMax: winWidth,
      scaleMin: 0.25,
      scaleMax: 0.5,
      scaleXMin: 0.5,
      scaleXMax: 1.5,
      scaleYMin: 1.5,
      scaleYMax: 2.5,
      opacityMin: 0.4,
      opacityMax: 0.7
    };
    let mid = {
      yMin: winHeight * 0.4,
      yMax: winHeight * 0.9,
      xMin: winWidth * 0.1,
      xMax: winWidth * 0.9,
      scaleMin: 0.2,
      scaleMax: 0.8,
      opacityMin: 0.5,
      opacityMax: 1
    };
    let end = {
      yMin: -180,
      yMax: -180,
      xMin: -100,
      xMax: winWidth + 180,
      scaleMin: 0.1,
      scaleMax: 1,
      opacityMin: 0.4,
      opacityMax: 0.7
    };

    function range(map, prop) {
      var min = map[prop + 'Min'];
      var max = map[prop + 'Max'];
      return min + (max - min) * Math.random();
    }
  
    function sign() {
      return Math.random() < 0.5 ? -1 : 1;
    }
  
    function randomEase(easeThis, easeThat) {
      if (Math.random() < 0.5) {
        return easeThat;
      }
      return easeThis;
    }
  
    function spawn(particle) {
      var wholeDuration = (10 / speed) * (0.7 + Math.random() * 0.4);
      var delay = wholeDuration * Math.random();
      var partialDuration = (wholeDuration + 1) * (0.2 + Math.random() * 0.3);
      TweenLite.set(particle, {
        y: range(start, 'y'),
        x: range(start, 'x'),
        scaleX: range(start, 'scaleX'),
        scaleY: range(start, 'scaleY'),
        scale: range(start, 'scale'),
        opacity: range(start, 'opacity'),
        visibility: 'hidden'
      });
      TweenLite.to(particle, partialDuration, {
        delay: delay,
        y: range(mid, 'y'),
        ease: randomEase(Linear.easeOut, Back.easeInOut)
      });
      TweenLite.to(particle, wholeDuration - partialDuration, {
        delay: partialDuration + delay,
        y: range(end, 'y'),
        ease: Back.easeIn
      });
      TweenLite.to(particle, partialDuration, {
        delay: delay,
        x: range(mid, 'x'),
        ease: Power1.easeOut
      });
      TweenLite.to(particle, wholeDuration - partialDuration, {
        delay: partialDuration + delay,
        x: range(end, 'x'),
        ease: Power1.easeIn
      });
      partialDuration = wholeDuration * (0.5 + Math.random() * 0.3);
      TweenLite.to(particle, partialDuration, {
        delay: delay,
        scale: range(mid, 'scale'),
        autoAlpha: range(mid, 'opacity'),
        ease: Linear.easeNone
      });
      TweenLite.to(particle, wholeDuration - partialDuration, {
        delay: partialDuration + delay,
        scale: range(end, 'scale'),
        autoAlpha: range(end, 'opacity'),
        ease: Linear.easeNone,
        onComplete: spawn,
        onCompleteParams: [particle]
      });
    }
  



    onMount(() => {
        accountLogin.subscribe((value) => {
            if (value == -99) return;
            if (value == -1 || value == "-1") loginIsFocus = true;
            else {
                loginInput = value;
                passwordIsFocus = true;
            }
        });


        let i, particleSpark;
      for (i = 0; i < density; i += 1) {
        particleSpark = document.createElement('div');
        particleSpark.classList.add('auth__sparks2_spark');
        sparksContainer.appendChild(particleSpark);
        spawn(particleSpark);
      }
    });

    const onLogin = () => {
        //console.log(loginInput + passwordInput);
        if (loginInput && passwordInput && !$accountIsSession && loginInput.length && passwordInput.length && $accountLogin !== -99) {
            executeClient("client:OnSignInv2", loginInput, passwordInput);
        }
    };

    let language = "?";

    const onKeyUp = (event) => {
        const { keyCode } = event;
        if (keyCode === 13) onLogin();
    };
</script>

<svelte:window on:keyup={onKeyUp} />


<div class="auth">
    <img src={iMonkey} alt="" class="auth__monkey">
    <div class="auth__sparks2" bind:this={sparksContainer}></div>

    <div class="auth__menu">
        <h1>Welcome to</h1>
        <h2>MetaverseRP</h2>

        <div class="auth__menu_buttons">
            <button>Authorization</button>
        </div>

      
            <div class="auth__menu_input">
                <img src={iUser} alt="">
                <input bind:value={loginInput} type="text" required>
                <span>Username</span>
            </div>
            <!-- Rest of the authorization form fields -->


        <!-- Common form fields for both registration and authorization -->
        <div class="auth__menu_input">
            <img src={iPassword} alt="">
            <input bind:value={passwordInput} type="password" required>
            <span>Password</span>
        </div>


        <!-- Done button for both registration and authorization -->
        <button class="auth__menu_done" on:click={onLogin}>Done</button>


    </div>
</div>
