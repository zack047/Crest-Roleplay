<script>
    import image from '../../images/gov.jpg'
	import image2 from '../../images/gov2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;"><strong>Fraction</strong>, including <strong>judicial</strong>, <strong>executive</strong>, And <strong>legislative</strong> power. The organization plays a pivotal role in shaping legislation, making critical decisions that impact the entire state. In addition, it organizes significant events and provides funding to various industries within the state.</span></p>
    <p><span style="font-weight: 400;">Can <strong>be a secretary</strong>, <strong>governor's guard</strong>, <strong>judge</strong>, <strong>prosecutor</strong>, <strong>secret service officer</strong>, or <strong>even the governor</strong> -  the possibilities are only limited by your potential.</span></p>
    <p><span style="font-weight: 400;">On it lies <strong>managing all</strong>&nbsp;public services. The uniform of the employees mainly consists of business-style clothes, and for the secret service employees this is the standard black uniform. <strong>Car park</strong> consists of cars for business trips and is painted white.</span></p>
    <p><span style="font-weight: 400;"><strong>For entry</strong> To join the ranks of the government, individuals are required to possess a valid passport and a medical card. Typically, employment occurs within the government building, commonly known as (City Hall), with announcements regarding vacancies being communicated through state news channels in advance.</span></p>
    <p><strong>Buttons</strong>
        <br><span style="font-weight: 400;"><strong>X</strong> - Remove/put on handcuffs;<br /></span><strong>Z</strong> - To lead<br>
        <strong> "G" > Fraction > Search</strong> - Search the player's belongings <br>
            <p><strong>Commands</strong>
            <br /><strong>/incar</strong> - put the offender in the car
            <br /><strong>/pull</strong> - kick the player out of the car
            <br /><strong>/takegunlic</strong> - revoke a gun license
            <br /><strong>/su</strong> - put a person on the wanted list
            <br /><strong>/ticket</strong> - issue a fine
            <br /><strong>/go</strong> - accept the player's call</p>
            
    <div style="display: flex; flex-direction: column;"><img src={image} alt="State factions" class="contentImg"/><img src={image2} alt="State factions" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show on Map <span class="icon map"/></div>
    </div>
</div>