<script>
  import hudConfig from '../hud.config.js';
  const gc = ( id ) => { return hudConfig.colors[id] || '#ffffff' };

  let show = false,
    inVeh = false,
    gear = 0,
    rpm = 0,
    speed = '000',
    fuel = {
      value: 0,
      max: 80
    },
    states = {
      doors: true,
      engine: false,
      light: false
    }

    
  let direction = "N",
    area = "",
    street = "";

  window.hudStore.direction = ( value ) => direction = value;
  window.hudStore.area = ( value ) => area = value;
  window.hudStore.street = ( value ) => street = value;

  window.vehicleState.fuel = ( value ) => fuel.value = value;
  window.vehicleState.engine = (value) => states.engine = value;

  window.vehicleState.speed = ( value ) => speed = String(value).padStart(3, '0');
  window.vehicleState.gear = (value) => gear = value;
  window.vehicleState.rpm = (value) => rpm = value;

  //TODO: ClientSide
  window.vehicleState.setLight = (value, value2) => states.light = value ? true:value2;

  window.vehicleState.doors = (value) => states.doors = value;

  window.vehicleState.isToggledVehicleHud = (value) => show = value;
  window.hudStore.inVehicle = ( value ) => inVeh = value;

</script>


{#if show && inVeh}
<div class="hud__speedometer">
    <div class="hud__speedometer_other">
        <div class="hud__speedometer_other-oth">
            <div class="hud__speedometer_other-oth_misc">
                <div class="hud__speedometer_other-oth_misc-spgr">
                    <h1 style="color: {gc('mainColor')}; --shadow: {gc('mainShadow')}">{speed}</h1>
                    <div class="gear">
                        <h2>{gear}</h2>
                        <img src="./assets/img/icons/gear.svg" alt="">
                    </div>
                </div>
                <div class="hud__speedometer_other-oth_misc-rpm">
                  {#each Array(9) as _, k (k)}
                    <hr class:active="{(1 / 9) * k <= rpm}">
                  {/each}
                </div>
            </div>
            <div class="hud__speedometer_other-oth_red">
                <h1>KM/H</h1>
                <div class="hud__speedometer_other-oth_red-flex">
                    <hr>
                    <hr>
                </div>
            </div>

        </div>
        <div class="hud__speedometer_other-fuel">
            <h1>{fuel.value} liter</h1>
            <div class="dots">
              {#each Array(8) as _, k (k)}
                <div class="dot" class:active="{((1 / fuel.max) * k) <= fuel.value}" style="--color: {gc('mainColor')}"></div>
              {/each}
            </div>
        </div>
    </div>
    <div class="hud__speedometer_states">
        <div class="hud__speedometer_states-state engine" style="--color: {gc('mainColor')}" class:active="{states.engine}"><img src="./assets/img/icons/engine.svg" alt=""></div>
        <div class="hud__speedometer_states-state doors" style="--color: {gc('mainColor')}" class:active="{states.doors}"><img src="./assets/img/icons/doors.svg" alt=""></div>
        <div class="hud__speedometer_states-state light" style="--color: {gc('mainColor')}" class:active="{states.light}"><img src="./assets/img/icons/light.svg" alt=""></div>
    </div>
</div>
{/if}

<div class="hud__gps" class:abs="{show && inVeh}">
  <div class="hud__gps_direction">
      <h1>{direction}</h1>
  </div>
  <div class="hud__gps_text">
      <h1>{area}</h1>
      <h2 style="color: {gc('mainColor')}; --shadow: {gc('mainShadow')}">{street}</h2>
  </div>
</div>