<script>
    import image from '../../images/tatoo.jpg'
	import image2 from '../../images/tatoo2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">If you want to stand out from the crowd, then you can decorate your body with a lot of interesting drawings and patterns. There are <strong>more than 500 different tattoos available to choose from</strong>, which can be applied to the body from head to toe.</span></p>
    <p><span style="font-weight: 400;"><strong>In order to get a tattoo</strong>, visit a tattoo parlor and stand on the special "<strong>Marker</strong>", then press "<strong>E</strong>" and you will get a menu with a choice of location and tattoo.</span></p>
    <p><span style="font-weight: 400;">If you decide <strong>to remove the tattoo</strong>, we suggest you visit the central hospital of Los Santos and find an NPC in one of the rooms. With their help, you can <strong>remove tattoos</strong> from a certain area on the body, at a cost of $300</span></p>
    
	<div style="display: flex; flex-direction: column;"><img src={image} alt="Tattoo Shop" class="contentImg"/><img src={image2} alt="Tattoo Shop" class="contentImg"/></div>
	
    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show Nearest<span class="icon map"/></div>
    </div>
</div>