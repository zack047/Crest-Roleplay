<script>
    import image from '../../images/lost.jpg'
	import image2 from '../../images/lost2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><strong>Place</strong>, Discover the realm where authentic bikers thrive—an enigmatic organization veiled in obscurity. At initial sight, it appears to be an ordinary collective of motorcyclists, leisurely traversing the state while pursuing their individual interests. However, the depths of their intentions remain concealed, leaving one to ponder what lies within their enigmatic minds.</p>
    <p><span style="font-weight: 400;">bikers are engaged <strong>looting</strong>, <strong>arms sales</strong>, <strong> sale of illegal substances</strong> And <strong>helping gangs and mafias cut off supplies or rob</strong>.</span></p>
    <p><span style="font-weight: 400;"><strong>Recruitment in The Lost</strong> Prospective candidates undergo a meticulous application process, wherein individuals must demonstrate their proficiency in motor vehicles and exhibit unwavering nerve to gain access to their inner circle.</span></p>

    <div style="display: flex; flex-direction: column;"><img src={image} alt="Lost" class="contentImg"/><img src={image2} alt="Lost" class="contentImg"/></div>

    <div class="button aligncenter">
	    <div class="btn" on:click={onWayPoint}>Show on Map <span class="icon map"/></div>
    </div>
</div>