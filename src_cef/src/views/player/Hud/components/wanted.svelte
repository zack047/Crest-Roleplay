<script>
  import { charWanted } from 'store/chars'
</script>

{#if charWanted > 0}
<div class="hud__top_wanted">
  {#each Array(5) as _, i (i)}
    <img src="./assets/img/icons/star.svg" alt="" class:inactive="{charWanted < i}">
  {/each}
</div>
{/if}