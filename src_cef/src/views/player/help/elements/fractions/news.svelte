<script>
    import image from '../../images/news.jpg'
	import image2 from '../../images/news2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;"><strong>Organization</strong>, Dedicated to the dissemination and publication of diverse and valuable information, this organization aims to provide comprehensive resources that prove beneficial to every resident within the state.&nbsp;</span></p>
    <p><span style="font-weight: 400;"><strong>Constant duty</strong> The diligent employees of LSN ensure the prompt and accurate publication of announcements from state residents seeking to sell their vehicles, houses, and other properties.&nbsp;</span></p>
    <p><span style="font-weight: 400;"><strong>Salary</strong> The employees at News excel based on their level of literacy and creativity. Working within this organization offers individuals the opportunity to forge a prosperous career and attain recognition as esteemed figures within the state.</span></p>
    <p><span style="font-weight: 400;"><strong>For entry</strong> in the ranks you need to come for an interview in the office &laquo;<strong>LS NEWS</strong>&raquo;. You must have a medical card with you.</span></p>
    <p><strong>Buttons/Commands</strong></p>
    <p><span style="font-weight: 400;">&ldquo;<strong>F7</strong>&rdquo; - open the ad editing window.</span></p>
    <p><span style="font-weight: 400;"><strong>/delad</strong> - delete ad</span></p>
    
   <div style="display: flex; flex-direction: column;"><img src={image} alt="News" class="contentImg"/><img src={image2} alt="News" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show on Map <span class="icon map"/></div>
    </div>
</div>