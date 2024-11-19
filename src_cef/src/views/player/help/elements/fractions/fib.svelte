<script>
    import image from '../../images/fib.jpg'
	import image2 from '../../images/fib2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;"><strong>Faction</strong>, that deals with criminal articles.;</span><span style="font-weight: 400;">To work in this faction, you need to have extensive experience in law enforcement agencies and excellent physical training.;</span></p>
    <p><span style="font-weight: 400;"><strong>Among all</strong> law enforcement agencies, FIB are a priority in various covert operations and are directly involved in command. <strong>Form</strong>, <strong>cars</strong> And <strong>weapon</strong> this faction corresponds to their status and is considered the best. By working in this faction, you will be able to provide great support for the state.</span></p>
    <p><strong>Buttons</strong>
        <br><span style="font-weight: 400;">&ldquo;<strong>U</strong>&rdquo;- On-board computer&nbsp;<br /></span>&ldquo;<strong>X</strong>&rdquo; - Handcuff a person
            <br />&ldquo;<strong>Z</strong>&rdquo; - Lead the person<br></p>
            <p><strong>Commands</strong><span style="font-weight: 400;"><br>
                <strong>/m</strong> - using a megaphone<br /></span>
    <span style="font-weight: 400;"><strong>/su</strong> - put a person on the wanted list<br /></span><span style="font-weight: 400;"><strong>/rfp</strong> - release a person from prison<br /></span>
    <span style="font-weight: 400;"><strong>/incar</strong> - drag a person into a car<br /></span><span style="font-weight: 400;"><strong>/pull</strong> - pull the person out of the car</span><br />
    <span style="font-weight: 400;"><strong>/opengs</strong> - Open Warehouse №2</span><br />
    <span style="font-weight: 400;"><strong>/closegs</strong> - Close Warehouse №2</span>
    <br /><strong>/pd</strong> - accept the player's call
    <br /><strong>/to</strong> - accept CODE 0
    <br /><strong>/arrest</strong> - put the player in the cell
    <br /><strong>/givegunlic</strong> - issue a gun license
    <br /><strong>/takegunlic</strong> - revoke a gun license
    <br /><strong>/warg</strong> - enable emergency mode
    <br /><strong>/carsu</strong> - put a car on the wanted list</p>
    

    <div style="display: flex; flex-direction: column;"><img src={image} alt="State factions" class="contentImg"/><img src={image2} alt="State factions" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show on Map <span class="icon map"/></div>
    </div>
</div>