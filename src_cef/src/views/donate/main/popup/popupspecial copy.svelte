<script>
    export let popupData;
    let popupNabor = false;
</script>
<div class="newdonate__special">
    {#if popupNabor == false}
        <div class="newdonate__stars"/>
        <div class="newdonate__special-title">Special Offer</div>
    {/if}
    <div class="newdonate__special-block">
        <div class="newdonate__special-mainimg">
            <div class="newdonate__special-sunduk"/>
        </div>
        <div class="newdonate__special-info">
            {#if popupNabor == false}
            <div class="newdonate__special-title">Golden Chest <br> Hmm, Whats there?</div>
            <div class="newdonate__special-time">
                <div>The offer is limited</div>
                <div class="special-time-img">
                    <div>01:20:21</div>
                </div>
            </div>
            <div class="box-flex">
                <div class="redbucks-img"/>
                <div class="newdonate__special-price">
                    <div class="newdonate__special-current-price">2999 CC</div>
                    <div class="newdonate__special-old-price">4999</div>
                </div>
            </div>
            <div class="newdonate__button_small">
                <div class="newdonate__button-text">Pick up now</div>
            </div>
            <div class="newdonate__special-description">Postpone</div>
            {/if}
            {#if popupNabor == true}
            <div class="newdonate__special-title">Beginners Kit</div>
            <div class="newdonate__special-gray">Whats included in the set?</div>
            <div class="newdonate__special-nabor">
                <div class="box-flex">
                    <span class="icon-money"/>
                    <div class="newdonate__special-nabor-item">5000 CC</div>
                </div>
                <div class="box-flex">
                    <span class="icon-money"/>
                    <div class="newdonate__special-nabor-item">3 LVL</div>
                </div>
                <div class="box-flex">
                    <span class="icon-money"/>
                    <div class="newdonate__special-nabor-item">5000 CC</div>
                </div>
            </div>
            <div class="newdonate__special-text">
                Lorem ipsum dolor sit, amet consectetur adipisicing elit. Quam sequi eveniet modi eum molestiae exercitationem!
            </div>
            <div class="newdonate__button_small">
                <div class="newdonate__button-text">Buy for 500</div>
            </div>
            {/if}
        </div>
    </div>
    <div class="newdonate__escape">
        <div class="box-flex">
            <span class="donateicons-esc"/>
            <div class="newdonate__escape-title">ESC</div>
        </div>
        <div class="newdonate__escape-text">
            Click to close the offer <br>
            Please Note, That the offer is <b>Limited</b>
        </div>
    </div>
</div>