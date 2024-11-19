<script>
  let prize = '70 hours';
</script>

{#if prize}
<div class="hud__top_gift">
    <div class="hud__top_gift-text">
        <h1>Play for</h1>
        <h2>{prize}</h2>
        <h1>And get a BMW!</h1>
    </div>
    <img src="./assets/img/gift.png" alt="">
</div>
{/if}