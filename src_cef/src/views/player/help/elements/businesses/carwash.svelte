<script>
    import image from '../../images/lawn.jpg'
	import image2 from '../../images/lawn2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;"><strong>Has your car completely sunk into the mud?</strong> Then go to one of the nearest car washes fast!</span></p>
    <p><span style="font-weight: 400;">To use the services of a car wash, you need to drive your vehicle to "<strong>the marker</strong>", which is located in the car wash. <strong>Shut down your transport</strong> and press "<strong>E</strong>", then confirm that you want to use this service. After confirmation, your vehicle will be clean again.</span></p>
    <p><span style="font-weight: 400;"><strong>The cost of</strong> one car wash is from $95 to $175.</span></p>
    
		<div style="display: flex; flex-direction: column;"><img src={image} alt="Car Wash" class="contentImg"/><img src={image2} alt="Car Wash" class="contentImg"/></div>
	
    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show Nearest<span class="icon map"/></div>
    </div>
</div>