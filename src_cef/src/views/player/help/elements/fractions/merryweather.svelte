<script>
    import image from '../../images/mws.jpg'
	import image2 from '../../images/mws2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;"><strong>This private company</strong>, primarily operates under government contracts, providing essential support to governmental entities through the deployment of top-tier equipment renowned for its exceptional quality. <strong>The primary objective</strong> of the organization is to guarantee the security and protection of events, individuals, and other entities with whom the organization has contractual agreements.&nbsp;</span></p>
    <p><span style="font-weight: 400;"><strong>The organization is actively</strong> involved in patrolling the highways surrounding the city of Los Santos, as well as providing assistance to LSPD/FIB officers in a diverse range of tasks upon their requests.&nbsp;</span></p>
    <p><span style="font-weight: 400;"><strong>MWS staff</strong> also has <strong>handcuffs</strong> And <strong>various special tools</strong>, which serve to detain persons who have committed an offense. By working in this faction, you will be able to provide great support to our state, which is dominated by law and order.</span></p>
    <p><span style="font-weight: 400;">Commands/Keys</span></p>
    <p><span style="font-weight: 400;">&ldquo;<strong>Z</strong>&rdquo; - to lead<br /></span><span style="font-weight: 400;">&ldquo;<strong>G</strong>&rdquo; -&gt; Fraction<br /></span><span style="font-weight: 400;"><strong>/incar</strong> - drag a person into a car<br /></span><span style="font-weight: 400;"><strong>/pull</strong> - pull the person out of the car<br /></span><span style="font-weight: 400;"><strong>/eject [ID]</strong> - Throw the person out of the car.</span></p>
    
	<div style="display: flex; flex-direction: column;"><img src={image} alt="mws" class="contentImg"/><img src={image2} alt="mws" class="contentImg"/></div>
	
    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show on Map <span class="icon map"/></div>
    </div>
</div>