<script>
    import image from '../../images/waxta.jpg'
	import image2 from '../../images/lesorub.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p>
      <span style="font-weight: 400;">An incredible opportunity not to miss a dime and get the most out of life - <strong>side jobs!</strong></span>
    </p>
    <p>
      <span style="font-weight: 400;">Currently you can dig ore and cut down trees.<br />
		<strong>GPS &gt; Side Job &gt; Mine/Lumberjack</strong>.<br /><br />
		<span>To work on any of the part-time jobs, you just need to have the necessary tools that can be purchased in a Hunting Store.<br />
		<strong>GPS &gt; Other &gt; Hunting Store</strong></span>
      </span>
    </p>
    <p>
      <span style="font-weight: 400;">How to Mine / cut wood:<br />
        <strong>All you have to do is go to a tree or a mine with a tool and hold "E"</strong>. You will earn resources that you can sell to earn money, or craft furniture for your house.</span>
    </p>
    <div style="display: flex; flex-direction: column;">
      <img src={image} alt="News" class="contentImg" />
      <img src={image2} alt="News" class="contentImg" />
    </div>
    <div class="button aligncenter">
      <div class="btn" on:click={onWayPoint}>Show on Map <span class="icon map" />
      </div>
    </div>
  </div>