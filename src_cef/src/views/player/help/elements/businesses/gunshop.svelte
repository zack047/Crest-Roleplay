<script>
    import image from '../../images/gun.png'
	import image2 from '../../images/gun2.png'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;"><strong>The Gun Shop</strong> offers a huge assortment of weapons from revolvers to sniper rifles. If you feel the need, come to the local Gun Shop and buy yourself a weapon; there are plenty to choose from.</span></p>
    <p><span style="font-weight: 400;">To buy a gun in a store, you must <strong>have a license to carry a gun, which is obtained from the police department</strong>.</span></p>
    <p><span style="font-weight: 400;"><strong>The store sells</strong>:<br />Pistols<br />Shotguns<br />Submachine Guns<br />Assault Rifles<br />Sniper Rifles</span></p>
    <p><span style="font-weight: 400;">And if you are a fan of quiet, bright or colorful weapons, you can <strong>buy modifications</strong> for it. Install a flashlight, a silencer, a flame thrower or cover your gun with some bright coloring so that the enemies are afraid!</span></p>
    <p><span style="font-weight: 400;"><strong>All modifications</strong> can be purchased in the weapons store.</span></p>

<div style="display: flex; flex-direction: column;"><img src={image} alt="Gun Shop" class="contentImg"/><img src={image2} alt="Gun Shop" class="contentImg"/></div>
<br>
    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show Nearest<span class="icon map"/></div>
    </div>
</div>