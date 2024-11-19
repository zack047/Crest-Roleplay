<script>
    import image from '../../images/ems.jpg'
	import image2 from '../../images/ems2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">This faction is dedicated to providing healthcare services to patients across the state.&nbsp;</span></p>
    <p><span style="font-weight: 400;">In addition to providing medical treatment to the citizens of the state, a primary responsibility of the EMS (Emergency Medical Services) is to ensure the upkeep of first-aid kits within state factions. Working within the EMS offers an opportunity to establish oneself as a dependable and skilled healthcare professional in the field of medicine.</span></p>
    <p><span style="font-weight: 400;">To join this organization, you must come for an interview at the office &laquo;<strong>EMS</strong>&raquo;. You must possess a driver's license of the appropriate category. &laquo;<strong>B/C</strong>&raquo; and medical card.</span></p>
    <p><span style="font-weight: 400;">Commands</span></p>
    <p><span style="font-weight: 400;"><strong>/heal [ID] [Price]</strong> - write a check for treatment<br /></span><strong>/ems [ID]</strong> - take the call.<br /><strong>/givemedlic [ID]</strong> - issue a medical card.<br /><strong>/givepmlic [ID] [price]</strong> - sell paramedic license.<br /><strong>/openstock</strong> - open a warehouse<br /><strong>/closestock</strong> - close the warehouse</p>
    
    <div style="display: flex; flex-direction: column;"><img src={image} alt="State factions" class="contentImg"/><img src={image2} alt="State factions" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show on Map <span class="icon map"/></div>
    </div>
</div>