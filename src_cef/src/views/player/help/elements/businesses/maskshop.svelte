<script>
    import image from '../../images/mask.jpg'
	import image2 from '../../images/mask2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">There are <strong>mask shops</strong> scattered throughout the state. You can buy a couple, gather friends up and have a fun carnival!</span></p>
    <p><span style="font-weight: 400;">A mask will hide the name of the person wearing it from everyone except members of the same faction. The assortment includes hundreds of colorful options.</span></p>
    <p><span style="font-weight: 400;"><strong>Criminal organizations</strong> use masks for crimes and robberies. If a person is wearing a mask when robbing a house, and the alarm goes off, they will not be issued a wanted level.</span></p>
	<p><span style="font-weight: 400;"><strong>In some cases</strong> masks are used as a regular accessory to help complement a style.</span></p>
	<p><span style="font-weight: 400;"><strong>FIB employees use masks</strong>, to hide their identity, because their work is full of secrets.</span></p>
	<p><span style="font-weight: 400;"><strong>Don't forget that</strong> the police can tear off your mask and arrest you if they suspect anything wrong.</span></p>

<div style="display: flex; flex-direction: column;"><img src={image} alt="Mask Shop" class="contentImg"/><img src={image2} alt="Mask Shop" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show Nearest<span class="icon map"/></div>
    </div>
</div>