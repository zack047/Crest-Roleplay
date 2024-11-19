<script> 
  const weapon = {
    icon: "",
    ammo: [0,0]
  }
  window.hudStore.weaponItemId = (value) => weapon.icon = value;
  window.hudStore.clipSize = (value) => weapon.ammo[1] = value;
  window.hudStore.ammo = (value) => weapon.ammo[0] = value;
</script>

{#if weapon.ammo[0] > 0}
<div class="hud__top_weapon">
    <img src="{document.cloud}inventoryItems/items/{weapon.icon}.png" alt="" class="hud__top_weapon-icon" class:class="{weapon.icon}">
    <div class="hud__top_weapon-ammo">
        <img src="./assets/img/icons/ammo.svg" alt="">
        <h1>{weapon.ammo[0]}/{weapon.ammo[1]}</h1>
    </div>
</div>
{/if}