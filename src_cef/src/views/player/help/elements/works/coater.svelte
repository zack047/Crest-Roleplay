<script>
    import image from '../../images/inkas.jpg'
	import image2 from '../../images/inkas2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">In this job you will be delivering money to various locations. You can be followed by multiple criminals who will rob you for the money you are delivering.</span></p>
    <p><span style="font-weight: 400;">After employment, you need to exit the City Hall and open your phone. On your phone click GPS and then select &laquo;<strong>GPS &gt; Works &gt; Collectors</strong>&raquo;. After arriving at the collector NPC, you need to interact with them and rent yourself a free vehicle.</span></p>
    <p><span style="font-weight: 400;"><strong>Requirements</strong>: Class &ldquo;<strong>C</strong>&rdquo; License<br /></span>
		<span style="font-weight: 400;"><strong>Bonuses</strong>: In this job, there is additional bonuses on top of your salary if you have VIP status.</span></p>
    <p><span style="font-weight: 400;"><strong>Gold VIP</strong> - 15% Salary Increase<br />
		<strong>Platinum VIP</strong> - 25% Salary Increase<br />
		<strong>Diamond VIP</strong> - 35% Salary Increase</span></p>
    <p><span style="font-weight: 400;"><strong>Commands</strong>:</span></p>
    <p><span style="font-weight: 400;"><strong>/mypoint</strong> - this command will set a GPS showing you where to deliver the money</span></p>
    
    <div style="display: flex; flex-direction: column;"><img src={image} alt="News" class="contentImg"/><img src={image2} alt="News" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show on Map <span class="icon map"/></div>
    </div>
</div>