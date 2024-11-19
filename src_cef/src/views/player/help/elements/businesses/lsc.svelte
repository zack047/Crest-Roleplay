<script>
    import image from '../../images/lsc1.jpg'
	import image2 from '../../images/lsc.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">Workshop for external and internal tuning.</span></p>
    <p><span style="font-weight: 400;"><strong>To improve your transport</strong> you should hit the marker near "<strong>LSC</strong>" and press the "<strong>E</strong>" key, after that, you will be taken to the tuning purchase interface.</span></p>
    <p><span style="font-weight: 400;"><strong>Take a look around with the help of a convenient camera</strong> and make your car a truly unique beauty!</span></p>
	<p><span style="font-weight: 400;"><strong>Each car has its own price for improvements</strong>, it depends on the cost of the car (the cheaper the car, the cheaper the tuning and vice versa).</span></p>
    <p><span style="font-weight: 400;"><strong>Among the available modifications</strong>, you can find more than twenty different options for improvements: bumpers, wheels, hoods, engines and other improvements, as well as neon tubes, headlights, gradients and many, many more!</span></p>
    <p><span style="font-weight: 400;"><strong>Some owners</strong> workshops give Cash Back to their visitors. You can learn more directly from the business owner, do not hesitate to contact them!</span></p>
    
	
	<div style="display: flex; flex-direction: column;"><img src={image} alt="LSC" class="contentImg"/><img src={image2} alt="LSC" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show Nearest<span class="icon map"/></div>
    </div>
</div>