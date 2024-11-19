<script>
  import hudConfig from '../hud.config.js';
  import { onMount } from 'svelte/internal';
  import { onDestroy, afterUpdate } from 'svelte';
  import { charUUID } from 'store/chars';

  const gc = ( id ) => { return hudConfig.colors[id] || '#ffffff' };
  const logo = hudConfig.logo;

  let online = 0,
      maxonline = 1000,
      id = 0;

  let currentDate = new Date();
  let currentTime = getCurrentTime();

  function getCurrentTime() {
    const date = new Date();
    const hours = String(date.getHours()).padStart(2, '0');
    const minutes = String(date.getMinutes()).padStart(2, '0');
    return `${hours}:${minutes}`;
  }

  onMount(() => {
    setInterval(() => {
      currentTime = getCurrentTime();
    }, 1000);
  });

  window.serverStore.serverPlayerId = (value) => id = value;
  window.serverStore.serverOnline = (value) => online = value;

  onDestroy(() => {
    online = 0,
    maxonline = 1000,
    id = 0;
    currentTime = getCurrentTime();
  });

  afterUpdate(() => {});

</script>

<div class="hud__top_line">
  <div class="hud__top_line-info id">
      <div class="hud__top_line-info_icon">
        <img src="./assets/img/icons/id.svg" alt="">
      </div>
      <div class="hud__top_line-info_text">
        <h1>ID</h1>
        <h2 style="color: {gc('mainColor')}; --shadow: {gc('mainShadow')}">{$charUUID}</h2>
      </div>
  </div>
  <div class="hud__top_line-info online">
      <div class="hud__top_line-info_icon">
          <img src="./assets/img/icons/user.svg" alt="">
      </div>
      <div class="hud__top_line-info_text">
          <h1>Online</h1>
          <h2 style="color: {gc('mainColor')}; --shadow: {gc('mainShadow')}">{online} / {maxonline}</h2>
      </div>
  </div>
  <div class="hud__top_line-info time">
      <div class="hud__top_line-info_icon">
          <img src="./assets/img/icons/clock.svg" alt="">
      </div>
      <div class="hud__top_line-info_text">
          <h1>{currentDate.toLocaleDateString()}</h1>
          <h2 style="color: {gc('mainColor')};, --shadow: {gc('mainShadow')}">{currentTime}</h2>
      </div>
  </div>
  <img src="{logo}" alt="" class="hud__top_line-logo">
</div>