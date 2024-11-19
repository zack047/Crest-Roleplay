<script>
    import image from '../../images/azs.jpg'
	import image2 from '../../images/azs2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">Not enough gas to drive around the state? Did you forget to buy a canister? Hurry up and go to the nearest gas station!</span></p>
    <p><span style="font-weight: 400;"><strong>At the entrance</strong> to the gas station, you MUST <strong>turn off the vehicle</strong> and press the "<strong>E</strong>" key, after which you need <strong>to enter the required</strong> amount of fuel.</span></p>
    <p><span style="font-weight: 400;">You can also <strong>use the following keys</strong>:&nbsp;</span></p>
    <p><span style="font-weight: 400;"><strong>FULL</strong> - Refuel your car to a full tank<br /><strong>GOV</strong> - Fill up your car for free (this key is available only to employees of government organizations)</span></p>
    <p></p>
    
	<div style="display: flex; flex-direction: column;"><img src={image} alt="Gas Station" class="contentImg"/><img src={image2} alt="Gas Station" class="contentImg"/></div>
	
    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show Nearest<span class="icon map"/></div>
    </div>
</div>