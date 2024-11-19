<script>
    import image from '../../images/pd.jpg'
	import image2 from '../../images/pd2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">A public service that fulfills the crucial role of a regulatory authority, overseeing the enforcement of state laws, monitoring mass events, and ensuring road safety.&nbsp;</span></p>
    <p><span style="font-weight: 400;">By enlisting in the police force, you will have the opportunity to uphold order on the streets of the city, undertake exceptionally hazardous assignments, and delve into enigmatic and peculiar criminal cases.</span></p>
    <p><span style="font-weight: 400;">Members of the Police department<strong>they play a crucial role in facilitating</strong> the distribution of essential supplies, <strong> including materials and medicines, </strong> to various state organizations. <strong>Furthermore,</strong> they frequently undertake targeted operations to combat criminal activities within the city's districts.</span></p>
    <p><span style="font-weight: 400;">Recruitment<strong>In order to join the esteemed law enforcement officers,</strong> prospective candidates are required to attend an interview at the primary office of the Police Department. Moreover, it is imperative to possess a valid medical card, duly stamped to affirm sound health, as a mandatory prerequisite.</span></p>
    <p><strong>Buttons</strong>
    <br><span style="font-weight: 400;">&ldquo;<strong>U</strong>&rdquo;- On-board computer&nbsp;<br /></span>&ldquo;<strong>X</strong>&rdquo; - Handcuff a person
        <br />&ldquo;<strong>Z</strong>&rdquo; - Lead the person behind you.<br></p>
        <p><strong>Teams</strong>
        <br /><strong>/m</strong> - Using a megaphone.
        <br /><strong>/su</strong> - Put a person on the wanted list.
        <br /><strong>/rfp</strong> - Release a person from prison.
        <br /><strong>/incar</strong> - Drag a person into a car.
        <br /><strong>/pull</strong> - Pull the person out of the car.
        <br /><strong>/opengs</strong> - Open Warehouse. №2
        <br /><strong>/closegs</strong> - Close the Warehouse. №2
        <br /><strong>/pd</strong> - Accept the player's Call.
        <br /><strong>/to</strong> - Accept CODE 0.
        <br /><strong>/arrest</strong> - Put the player into the cell.
        <br /><strong>/givegunlic</strong> - Issue a gun license.
        <br /><strong>/takegunlic</strong> - Revoke a gun license.
        <br /><strong>/ticket</strong> - Issue a fine.
        <br /><strong>/warg</strong> - Enable emergency mode.
        <br /><strong>/carsu</strong> - Put a car on the Wanted List.</p>

    <div style="display: flex; flex-direction: column;"><img src={image} alt="State Factions." class="contentImg"/><img src={image2} alt="State Factions." class="contentImg"/></div>

    <div class="button aligncenter">
        <div style="display: flex; justify-content: between;"><div class="btn" on:click={onWayPoint}>Show on Map. <span class="icon map"/></div></div>
    </div>
</div>