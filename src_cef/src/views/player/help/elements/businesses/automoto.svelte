<script>
    import image from '../../images/ar.jpg'
    import image2 from '../../images/ar2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;"><strong>Economy Car Dealership</strong> - This range includes the <strong>cheapest cars</strong>, great for people who have recently arrived in the state.<br /><strong>The cost of cars</strong> in this showroom ranges from $700 to $25,000.</span></p>
    <p><span style="font-weight: 400;"><strong>Premium Car Dealership</strong> - In this salon, cars in the mid-range are available and there are a lot of interesting options.<br /><strong>The cost of cars</strong> in this showroom ranges from $6,000 to $160,000.</span></p>
    <p><span style="font-weight: 400;"><strong>Luxury Car Dealership</strong> - Once a luxury salon was the most desirable and prestigious in our state, until the next one appeared... The most prestigious cars of domestic production are available here.<br /></span><span style="font-weight: 400;"><strong>The cost of cars</strong> in this showroom ranges from $38,500 to $1,000,000.</span></p>
    <p><span style="font-weight: 400;"><strong>Elite Car Dealership</strong> - One day, a container with foreign cars arrived in the state. The prices are high, but you'll be tearing up the streets in style!<br /><strong>The cost of cars</strong> in this showroom ranges from $250,000 to $3,000,000.</span></p>
    <p><span style="font-weight: 400;"><strong>Motorcycle Dealership</strong> - If you have a desire to curb your iron horse, you should look here. Bikers rule!<br /><strong>The cost of bikes</strong> in this showroom ranges from $250 to $500,000.</span><span style="font-weight: 400;"><br /></span></p>
    <p><span style="font-weight: 400;"><strong>When Buying a Transport</strong> - It is possible to <strong>choose a color</strong> and <strong>view the characteristics</strong> at the time of purchase. It is important to note that EACH vehicle can be tested by going on a <strong>test drive</strong>. Try it!</span></p>
    
	<div style="display: flex; flex-direction: column;"><img src={image} alt="State Factions" class="contentImg"/><img src={image2} alt="State Factions" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show Nearest<span class="icon map"/></div>
    </div>
</div>