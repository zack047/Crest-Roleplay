<script>
    import { fly } from 'svelte/transition';
    export let SafeSone;

    import { addListernEvent } from "api/functions";

    let visible = false;
    addListernEvent ("hud.bp.info", (_visible) => {
        visible = _visible;
    });
</script>
{#if visible}
<div class="annon">
    <div class="bgicon">
        <svg width="64" height="64" viewBox="0 0 64 64" fill="none" xmlns="http://www.w3.org/2000/svg">
            <g filter="url(#filter0_d_97_76)">
            <path d="M53 32C53 43.598 43.598 53 32 53C20.402 53 11 43.598 11 32C11 20.402 20.402 11 32 11C43.598 11 53 20.402 53 32ZM12.9663 32C12.9663 42.512 21.488 51.0337 32 51.0337C42.512 51.0337 51.0337 42.512 51.0337 32C51.0337 21.488 42.512 12.9663 32 12.9663C21.488 12.9663 12.9663 21.488 12.9663 32Z" fill="white"/>
            </g>
            <path fill-rule="evenodd" clip-rule="evenodd" d="M23.7445 24.0439C23.1231 24.2678 22.6464 24.7261 22.2316 25.4986C21.0101 27.7732 20.6388 32.2196 21.3825 35.6657C21.8236 37.7094 22.5584 39.0399 23.5007 39.5012C23.7541 39.6251 23.7925 39.6321 24.2196 39.6307L24.6709 39.6292L27.3998 38.5494C28.9008 37.9554 30.1626 37.4602 30.204 37.4488C30.2745 37.4294 30.2792 37.5088 30.2792 38.7241C30.2792 39.9651 30.2826 40.0191 30.3585 39.9966C30.402 39.9836 32.1377 39.3048 34.2155 38.4882L37.9933 37.0034L38.0148 35.6861L38.0363 34.3689L38.9388 34.0116L39.8413 33.6542L41.4206 33.6533L43 33.6523V31.82V29.9878H41.4279H39.8557L32.2633 27.0023L24.6709 24.0167L24.2841 24.0028C24.0271 23.9936 23.8461 24.0073 23.7445 24.0439ZM24.6053 25.4274C25.2722 26.0724 25.7969 27.6669 26.053 29.8267C26.1571 30.7049 26.1571 32.9352 26.053 33.8133C25.7969 35.9732 25.2722 37.5677 24.6053 38.2127C24.4163 38.3954 24.3381 38.4442 24.2341 38.4442C24.1 38.4442 23.9271 38.3416 23.7637 38.1648L23.6729 38.0665L23.7788 37.8427C24.4532 36.4169 24.8635 34.1387 24.8635 31.82C24.8635 29.4977 24.4533 27.2236 23.7763 25.7922L23.668 25.5631L23.8775 25.3795C24.1766 25.1174 24.2926 25.125 24.6053 25.4274ZM36.7159 35.5504L36.7041 36.2002L34.1546 37.2014C32.7525 37.7521 31.5965 38.2026 31.5858 38.2026C31.5751 38.2026 31.5717 37.9095 31.5781 37.5512L31.59 36.8998L34.1255 35.9017C35.5201 35.3528 36.6761 34.903 36.6944 34.9021C36.7127 34.9013 36.7224 35.193 36.7159 35.5504Z" fill="white"/>
            <defs>
            <filter id="filter0_d_97_76" x="0" y="0" width="64" height="64" filterUnits="userSpaceOnUse" color-interpolation-filters="sRGB">
            <feFlood flood-opacity="0" result="BackgroundImageFix"/>
            <feColorMatrix in="SourceAlpha" type="matrix" values="0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 127 0" result="hardAlpha"/>
            <feOffset/>
            <feGaussianBlur stdDeviation="5.5"/>
            <feComposite in2="hardAlpha" operator="out"/>
            <feColorMatrix type="matrix" values="0 0 0 0 1 0 0 0 0 1 0 0 0 0 1 0 0 0 0.55 0"/>
            <feBlend mode="normal" in2="BackgroundImageFix" result="effect1_dropShadow_97_76"/>
            <feBlend mode="normal" in="SourceGraphic" in2="effect1_dropShadow_97_76" result="shape"/>
            </filter>
            </defs>
        </svg>            
    </div>
    <div class="bgname">
        <div class="nameannon">
            <p>Winter season!</p>
        </div>
    </div>
    <span>A new winter festival has become available on the project, at which you can get rare gifts.!</span>
</div>
{/if}