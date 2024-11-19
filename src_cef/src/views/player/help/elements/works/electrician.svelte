<script>
    import image from '../../images/electric.jpg'
	import image2 from '../../images/electric2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">One of the initial professions for newly arrived citizens. Electricians are engaged in the repair of equipment... At the power plant. &nbsp;</span></p>
    <p><span style="font-weight: 400;">After employment, you need to go to the power plant <strong>(M -&gt; Works -&gt; Power Plant)</strong>. You can rent a car at the transport parking lot, or, for example, call a taxi <strong>(M -&gt; Calls -&gt; Call a Taxi)</strong>.</span></p>
    <p><span style="font-weight: 400;">After arriving at the power plant, talk to the foreman, who is standing in the booth, to start the working day.&nbsp;&nbsp;</span></p>
    <p><span style="font-weight: 400;"><strong>Salary</strong>: $5 for one passed mark.</span><span style="font-weight: 400;"><br /></span><span style="font-weight: 400;"><strong>Bonuses</strong>: For this job, there are additional bonuses to the salary if available <strong>VIP Status</strong>.</span><span style="font-weight: 400;"><br /></span><span style="font-weight: 400;"><br /></span><span style="font-weight: 400;"><strong>Gold VIP</strong> - Salary increase by 15%; <br> <strong>Platinum VIP</strong> - Salary increase by 25%; <br> <strong>Diamond VIP</strong> - Salary increase by 35%.</span></p>

    <div style="display: flex; flex-direction: column;">
        <img src={image} alt="News" class="contentImg"/>
        <img src={image2} alt="News" class="contentImg"/>
    </div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show on Map<span class="icon map"/></div>
    </div>
</div>