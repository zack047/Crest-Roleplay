<script>

  import '@/views/player/hudevo/main.sass';
  import './assets/css/misc.css';
  import './assets/css/style.css';
  export let visible;

  import { addListernEvent } from 'api/functions';

  window.vehicleState = {}

  import Chat from './components/chat.svelte'
  import Quest from './components/quest.svelte';
  import Weapon from './components/weapon.svelte';
  import Gift from './components/gift.svelte';
  import Playerinfo from './components/playerinfo.svelte';
  import Score from './components/score.svelte';
  import Notify from './components/notify.svelte';
  import Map from './components/map.svelte';
  import Speedometer from './components/speedometer.svelte';
  import Enter from './components/enter.svelte'
  import Restart from './components/restart.svelte' 
  import UseButton from './components/usebutton.svelte';
  import DropItem from './components/dropitem.svelte';
  import Killist from './components/killlist.svelte'
  import Walkietalkie from './walkietalkie/index.svelte'
  import Phone from './phonenew/index.svelte';
  import PhoneNotify from './phonenew/indexNotify.svelte';
  import Notification from './components/notification/index.svelte';

  window.hudStore.isHint = (value) => console.log('isHint', value);
  window.hudStore.HideHelp = (value) => console.log('HideHelp', value);
  
  let show = true,
      phoneNotification = false,
      isWalkietalkie = false,
      isHudNewPhone = false;
  
  const onPhoneNotification = (json) => {
    if (json)
      json = JSON.parse(json);

    phoneNotification = json;
  }

  addListernEvent ("phone.notify", onPhoneNotification)


  window.hudStore.isWalkietalkie = (value) => isWalkietalkie = value;
  window.hudStore.isHudNewPhone = (value) => isHudNewPhone = value;  
  window.hudStore.isHudVisible = (value) => show = value;

</script>
<Notification />
<div class="hud" class:hud__hide={!(visible && show)} >
  <div class="hud__top">
      <Playerinfo />
      <Gift />
      <Weapon/>
      <Quest />
      <Killist />

  </div>

  <Speedometer />

  <div class="hud__center_bottom">
    <Enter />
    <UseButton />
    <DropItem />
    <Restart/>
  </div>
  
  <Score />
  <Map/>
  <Chat/>
  <Notify/>

</div>


{#if show}
    {#if isHudNewPhone}
        <div id="hudevo" style="position: absolute;top: 50%;transform: translate(-50%, -50%);left: 50%;">
            <Phone {phoneNotification} />
        </div>
    {:else if phoneNotification}
        <div id="hudevo" style="position: absolute;top: 50%;transform: translate(-50%, -50%);left: 50%;">
            <PhoneNotify {isHudNewPhone} {phoneNotification} />
        </div>
    {/if}
{/if}
<div id="hudevo" class:hudevo__hide={!(show) || !isWalkietalkie} style="position: absolute;top: 50%;transform: translate(-50%, -50%);left: 50%;">
    <Walkietalkie />
</div>