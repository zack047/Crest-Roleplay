<script>
    import image from '../../images/post.jpg'
	import image2 from '../../images/post2.jpg'
    export let onWayPoint;
</script>

<div class="text">
  <p>
    <span style="font-weight: 400;">The postman needs to know the area. Without knowing the area, he won't be able to deliver the parcels and earn fast money.</span>
  </p>
  <p>
    <span style="font-weight: 400;">Go to the post office (<strong>M -&gt; Works -&gt; Post Office</strong>). A taxi service will help, or, for example, you can use rental cars near the city hall.</span>
  </p>
  <p>
    <span style="font-weight: 400;">When you arrive at the post office, go to the marker near the door and press "E".  You'll recieve a notification on your phone where you can start or finish work, and take additional parcels.</span>
  </p>
  <p>
    <span style="font-weight: 400;">To start work, you need to go to the marker "<strong>Take a working transport</strong>", and it will spawn a truck to deliver the parcels with. It is also possible to deliver your parcels with your private transport in the company of your friends.</span>
  </p>
  <p>
    <span style="font-weight: 400;">
      <strong>Requirements</strong>: Class &ldquo; <strong>B</strong>&rdquo; License<br />
    </span>
    <span style="font-weight: 400;">
      <strong>Salary</strong>: About $200 Per Hour<br />
    </span>
    <span style="font-weight: 400;">
      <strong>Bonuses</strong>: For this job, there is additional bonuses to the salary if you have VIP status.</span>
  </p>
  <p>
    <span style="font-weight: 400;">
      <strong>Gold VIP</strong> - 15% Salary Increase<br />
      <strong>PLatinum VIP</strong> - 25% Salary Increase<br />
      <strong>Diamond VIP</strong> - 35% Salary Increase</span>
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