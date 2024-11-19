<script>
    import image from '../../images/gazon.jpg'
	import image2 from '../../images/gazon2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">An excellent opportunity for some initial income, being a visitor. The work is not dirty, and besides, it does not require licenses or experience.</span></p>
    <p><span style="font-weight: 400;">After taking the assignment you will need to speak with the Lawn Mower NPC (<strong>M -&gt; Works -&gt; Lawn Mower Parking</strong>). In order to get there, you can try to ask the people standing at City Hall to give you a lift to work or use a rental car near the building.</span></p>
    <p><span style="font-weight: 400;">After arriving at the lawn mower parking lot, you <strong>need to go to a free lawn mower and get into it</strong>, after that a window will appear in which you need to confirm the rental of the lawn mower.</span></p>
    <p><span style="font-weight: 400;"><strong>Salary</strong>: $3 per mark passed.<br /></span>
		<span style="font-weight: 400 ;"><strong>Bonuses</strong>: For this job, there are additional salary bonuses for VIP status.</span><span style="font-weight: 400;"><br /><br /></span >
		<span style="font-weight: 400;"><strong>Gold VIP</strong> - 15% Salary Increase<br />
		<strong>Platinum VIP</strong> - 25% Salary Increase<br />
		<strong>Diamond VIP</strong> - 35% Salary Increase</span></p>

    <div style="display: flex; flex-direction: column;"><img src={image} alt="News" class="contentImg"/><img src={image2} alt="News" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show on Map <span class="icon map"/></div>
    </div>
</div>