<script>
    import image from '../../images/247.jpg'
	import image2 from '../../images/2472.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">In any <strong>24/7</strong> store, you will be able to buy the following items:</span></p>
    <p><span style="font-weight: 400;"><strong>Wrench</strong> - Used for self-defense purposes, and is also used to repair vehicles.<br />
	    <strong>Crowbar</strong> - Used for self-defense and with its help, criminal organizations can rob houses.<br />
	    <strong>Flashlight</strong> - Will help to illuminate dark places.<br />
	    <strong>Hammer</strong> - Most often used for self-defense.<br />
	    <strong>SIM Card</strong> - Required to change your phone number and use the phone.<br />
		<strong>Gasoline Canister</strong> - Used for refueling vehicles.<br />
		<strong>Key Ring</strong> - Collects all your keys in one big bundle and saves space in your inventory.<br />
		<strong>Pizza</strong> - Replenishes your health by 30%.<br />
		<strong>Chips</strong> - Replenishes your health by 30%.<br />
		<strong>Beer</strong> - If you drink a lot, you can get the effect of intoxication.<br />
		<strong>Lottery Ticket</strong> - The drawing of the prize takes place every hour at 00 minutes. The chance of winning depends on the number of tickets you bought.<br />
		<strong>Bag</strong> - Designed for storing items, used as an additional inventory.</span></p>
    
	<div style="display: flex; flex-direction: column;"><img src={image} alt="Shops" class="contentImg"/><img src={image2} alt="Shops" class="contentImg"/></div>
	
    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show Nearest<span class="icon map"/></div>
    </div>
</div>