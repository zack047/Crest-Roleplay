<script>
    import image from '../../images/burger.jpg'
	import image2 from '../../images/burger2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">Dozens of fast food establishments are located throughout the state, where everyone can buy a juicy burger or snack! Below is the energy value of the products in relation to your health:</span></p>
    <p><span style="font-weight: 400;"><strong>Burger</strong> - replenishes 30% of health.<br /><strong>Hot Dog</strong> - replenishes 30% of health.<br /><strong>Sandwich</strong> - replenishes 30% of health.<br />
	    <strong>Cola</strong> - replenishes 10% of health.<br /><strong>Sprunk</strong> - replenishes 10% of health.</span></p>
    <p><span style="font-weight: 400;"><strong>Cost</strong> - the price of drinks starts from $3 and is limited to $5, the cost of food starts from $10 and is limited to $24.</span></p>
    
	<div style="display: flex; flex-direction: column;"><img src={image} alt="Restaurant" class="contentImg"/><img src={image2} alt="Restaurant" class="contentImg"/></div>
	
    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show Nearest<span class="icon map"/></div>
    </div>
</div>