<script>
    import image from '../../images/mech.jpg'
	import image2 from '../../images/mech2.png'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">If you like to repair car engines, put air in tires and repair body kits, then this is the job for you.</span></p>
    <p><span style="font-weight: 400;">After employment you need to exit the City Hall. Once outside open up your phone and go to GPS and select &laquo;<strong>GPS &gt; Works &gt; Mechanic</strong>&raquo;. After arriving at the mechanic NPC, you need to interact with them and rent yourself a free vehicle.</span></p>
    <p><span style="font-weight: 400;">If you want to <strong>not only repair transport, but also refuel it</strong>, then you need to buy some fuel at any of the available gas stations.</span></p>
    <p><span style="font-weight: 400;"><strong>Salary</strong>: From $5 to $30</span></p>
    <p><span style="font-weight: 400;"><strong>Commands</strong>:</span></p>
    <p><span style="font-weight: 400;"><strong>/repair [ID] [Price]</strong> - Offers the player to have his vehicle repaired.<br /></span>
		<strong>/sellfuel [ID] [Litres] [Price per 1 Litres]</strong> - Offers the player to refuel his vehicle.<br />
		<strong>/buyfuel [Quantity]</strong> - This Command buys fuel, this command can only be used at Gas Stations<br />
		<strong>/ma [ID]</strong> - take the job</p>

    <div style="display: flex; flex-direction: column;"><img src={image} alt="News" class="contentImg"/><img src={image2} alt="News" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show on Map <span class="icon map"/></div>
    </div>
</div>