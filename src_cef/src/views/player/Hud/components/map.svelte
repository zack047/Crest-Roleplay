<script>

  import { onMount } from 'svelte';

  import hudConfig from '../hud.config.js';
  const gc = ( id ) => { return hudConfig.colors[id] || '#ffffff' };

  import { charMoney, charBankMoney } from 'store/chars'

  let microphone = false;
  window.hudStore.microphone = (value) => microphone = value;

  let userData = {
    Money: 0,
    Bank: 0,
  };

  let greenZone = false;
  window.hudStore.greenZone = (value) => greenZone = value;
  
  onMount(async () => {
    charMoney.subscribe(value => {
      if (userData.Money !== value)
        userData.Money = value;
    });
    charBankMoney.subscribe(value => {
      if (userData.Bank !== value) 
        userData.Bank = value;
    });
  });

  function format( summ ) {
    const formatter = new Intl.NumberFormat('en-US', {
        style: 'currency',
        currency: 'USD',
        maximumFractionDigits: 0,
    });
    return formatter.format(summ)
  }
</script>

<div class="hud__map">
  {#if greenZone}
  <div class="hud__map_money cash">
    <div class="hud__map_money-text">
        <h2 style="color: green;">Greenzone</h2>
    </div>
</div>
{/if}
  <div class="hud__map_voice" class:active="{microphone}">
      <img src="./assets/img/icons/microphone.svg" alt="">
  </div>
  <div class="hud__map_money cash">
      <img src="./assets/img/icons/cash.svg" alt="">
      <div class="hud__map_money-text">
          <h1>Cash</h1>
          <h2 style="color: {gc('mainColor')}, --shadow: {gc('mainShadow')}">{format(userData.Money)}</h2>
      </div>
  </div>
  <div class="hud__map_money bank">
      <img src="./assets/img/icons/bank.svg" alt="">
      <div class="hud__map_money-text">
          <h1>Bank</h1>
          <h2 style="color: {gc('mainColor')}, --shadow: {gc('mainShadow')}">{format(userData.Bank)}</h2>
      </div>
  </div>
</div>