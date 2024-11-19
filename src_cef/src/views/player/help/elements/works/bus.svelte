<script>
    import image from '../../images/bus.jpg'
	import image2 from '../../images/bus2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">This is a profession that requires maximum concentration and good driving skills. The driver will have to carry passengers from one stop to another.</span></p>
    <p><span style="font-weight: 400;">After employment, you need to exit the City Hall and use the navigator on your phone, select ";<strong>GPS &gt; Works &gt; Bus Depot</strong>&raquo;. Upon arrival at the depot, you need to choose the bus you want, get in and rent it.</span></p>
    <p><span style="font-weight: 400;">After starting work, you will be given a specific route that you need to follow.</span></p>
    <p><span style="font-weight: 400;"><strong>Requirements</strong>: Class "<strong>С</strong>" Drivers License<br /></span>
		<span style="font-weight: 400;"><strong>Salary:</strong> from $2 to $6 per checkpoint, the amount depends on the route.<br /></span>
		<span style="font-weight: 400;"><strong>Bonuses</strong>: For this work, there are additional bonuses if you have VIP status.</span></p>
    <p><span style="font-weight: 400;"><strong>Gold VIP</strong> - Salary increase by 15%<br />
		<strong>Platinum VIP</strong> - Salary increase by 25%<br />
		<strong>Diamond VIP</strong> - Salary increase by 35%</span></p>
    
    <div style="display: flex; flex-direction: column;"><img src={image} alt="News" class="contentImg"/><img src={image2} alt="News" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show on Map <span class="icon map"/></div>
    </div>
</div>