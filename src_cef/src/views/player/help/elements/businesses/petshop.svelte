<script>
    import image from '../../images/zoo.jpg'
	import image2 from '../../images/zoo2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">In a pet store, you can buy yourself a <strong>pet</strong>. A fluffy pet will be waiting for you at home every day, to greet you with love and affection.</span></p>
    <p><span style="font-weight: 400;">A pet can only be bought <strong>if you own a house of the "Comfort+" class</strong> or higher.</span></p>
	<p><span style="font-weight: 400;"><strong>Pets are used only in the house</strong>. After buying a pet, he will be on the couch in the house.</span></p>
    <p><span style="font-weight: 400;">In the list of <strong>animals available for purchase</strong>, you can find such pets as:<br />
    Husky<br />Pug<br />Poodle<br />Retriever<br />Rottweiler<br />Sherherd<br />Westy<br />Cat<br />Rabbit</span></p>

<div style="display: flex; flex-direction: column;"><img src={image} alt="Pet Shop" class="contentImg"/><img src={image2} alt="Pet Shop" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show Nearest<span class="icon map"/></div>
    </div>
</div>