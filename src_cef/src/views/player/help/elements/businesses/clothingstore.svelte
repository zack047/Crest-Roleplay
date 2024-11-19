<script>
    import image from '../../images/clot.png'
	import image2 from '../../images/clot2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">The <strong>clothing store</strong> offers hundreds of different variations of clothing, starting with "fashionable" collections from the best couturiers of Los Santos and ending with our own designs that you will definitely like!</span></p>
    <p><span style="font-weight: 400;"><strong>After entering</strong> the clothing store, you should go to the checkout and stand on "<strong>the marker</strong>", then click "<strong>E</strong>".</span></p>
    <p><span style="font-weight: 400;"><strong>Clothing stores</strong> are scattered throughout the state and in each of them, the owner personally sets the price of the goods. <strong>Take a closer look</strong> at the assortment, choose <strong>the best</strong> items and be <strong>the most fashionable citizen in the state!</strong>.</span></p>
    
	<div style="display: flex; flex-direction: column;"><img src={image} alt="Clothing Store" class="contentImg"/><img src={image2} alt="Clothing Store" class="contentImg"/></div>
	
    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show Nearest<span class="icon map"/></div>
    </div>
</div>