<script>
    import image from '../../images/barb1.png'
	import image2 from '../../images/barb2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;"><strong>The appearance of your character</strong> is very important. Do you want to change your hairstyle or makeup? The first step is to go to one of the barbershops in the state.</span></p>
    <p><span style="font-weight: 400;">The following services are <strong>available in the barbershop</strong>:</span></p>
    <p><strong>Hairstyles<br /></strong><strong>Beard<br /></strong><strong>Eyebrows<br /></strong><strong>Chest<br /></strong><strong>Eyes<br /></strong><strong>Lipstick<br /></strong><strong>Blush<br /></strong><strong>Shadows</strong></p>
    <p><span style="font-weight: 400;"><strong>All services</strong> will make your character unique, but do not forget that <strong>in some factions it is forbidden</strong> to have certain types of hairstyles, etc.</span></p>

<div style="display: flex; flex-direction: column;"><img src={image} alt="State Factions" class="contentImg"/><img src={image2} alt="State Factions" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show Nearest<span class="icon map"/></div>
    </div>
</div>