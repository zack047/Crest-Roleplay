<script>
  import hudConfig from '../hud.config.js';
  import { onMount, onDestroy, afterUpdate } from 'svelte';
  import { fade, slide } from 'svelte/transition';
  import { addListernEvent } from "api/functions";

  let notifys = [],
      action = null;

  const gc = ( id ) => { return hudConfig.colors[id] || '#ffffff' };
  const noty = (id) => { return hudConfig.notifications[id] || hudConfig.notifications['default'] }
  import { executeClient } from 'api/rage';
  let 
        visible = false;

    addListernEvent ("hud.event", (_visible, _subTitle, _title, _desc, _image) => {
        if (_visible) {
            action = {
                title: `${_subTitle} | ${_title}`, text: _desc
            };
        } 
        visible = _visible;
    });

  
  

  function drawNotify( type, title, elements, timeout ) {
    let block = `block${Math.random() * Math.random()}`;

    let dat = {
      title: title,
      type: type,
      text: elements,
      time: timeout,
      block: block
    };
    notifys = [...notifys, dat];

    setTimeout( function() {
      notifys = notifys.filter(item => item !== dat);
    }, timeout )
  }

  window.notification = (type, title, elements, timeout) => {
    drawNotify(type, title, elements, timeout)
  }

  window.notificationAdd = (type, _, msg, time) => {
    executeClient ("notify", type, _, msg, time);
  }

  onDestroy(() => {
    notifys = [];
  });

  afterUpdate(() => {});
</script>

<div class="hud__notifys">
  <div class="notify-block">
    {#each notifys as i, k (k)}
      <div class="hud__notifys_notify" id={i.block} key="{k}" transition:slide>
        <div class="hud__notifys_notify-bg"></div>
        <div class="hud__notifys_notify-icon">
          <!-- <img src="./assets/img/notify/{noty(i.type).icon}" alt=""> -->
          <img src="./assets/img/action.svg" alt="">
        </div>
        <div class="hud__notifys_notify-text">
          <h1 style="color: {noty(i.type).textColor}, --shadow: {noty(i.type).textShadow}">{i.title}</h1>
          <h2>{i.text}</h2>
          <!-- <div class="line">
            <div class="innerLine" id="line_{i.block}" style="--color: {noty(i.type).lineColor}, --shadow: {noty(i.type).lineShadow}"></div>
          </div> -->
        </div>
      </div>
    {/each}
  </div>
  {#if visible}
  <div class="hud__notifys_action">
      <div class="hud__notifys_action-bg"></div>
      <div class="hud__notifys_action-icon">
          <img src="./assets/img/action.svg" alt="">
      </div>
      <div class="hud__notifys_action-text">
          <h1 style="color: {gc('actionText')}, --shadow: {gc('actionShadow')}">{action.title}</h1>
          <h2>{action.text}</h2>
          <!--<div class="sofly_buttons_container">
              <div class="sofly_Y"></div>
              <div class="sofly_N"></div>
          </div>-->
      </div>
  </div>
  {/if}
</div>