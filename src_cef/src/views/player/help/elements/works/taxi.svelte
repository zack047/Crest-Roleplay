<script>
    import image from '../../images/taxi.jpg'
	import image2 from '../../images/taxi3.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">A great opportunity to help residents and visiting citizens, find a lot of new acquaintances and learn more about what is happening in the state.</span></p>
    <p><span style="font-weight: 400;">After employment, you must exit the City Hall and use the navigator on your phone by selecting &laquo;<strong>GPS &gt; Works &gt; Taxi Depot</strong>&raquo;. Upon arrival at the Taxi Depot, you need to choose the right car (<strong>one of the 4 classes</strong>), then get in and rent it.</span></p>
    <p><span style="font-weight: 400;">Taxi Rental and Fare:</span></p>
    <p><span style="font-weight: 400;"><strong>Economy</strong> - $184 Rent, $5 - $50 Fare<br /></span>
		<strong>Comfort</strong> - $720 Rent, $20 - $100 Fare<br />
		<strong>Business</strong> - $2880 Rent, $80 - $400 Fare<br />
		<strong>Ultimate</strong > - $5760 Rent, $160 - $800 Fare</p>
    <p><span style="font-weight: 400;">In the Taxi Menu, on your phone, you can find comprehensive information about the order, for example, the distance to the passenger or his name.</span></p>
    <p><span style="font-weight: 400;"><strong>Requirements</strong>: Class &ldquo;<strong>B</strong>&rdquo; License</span></p>
    <p><span style="font-weight: 400;"><strong>Commands</strong>:</span></p>
    <p><span style="font-weight: 400;"><strong>/ta [ID]</strong> - p<em>accept a call from a client<br /></em></span>
		<strong>/tprice [ID] [Amount]</strong> - <em>issue a check to pay the client</em></p>
    <p></p>

    <div style="display: flex; flex-direction: column;"><img src={image} alt="News" class="contentImg"/><img src={image2} alt="News" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show on Map <span class="icon map"/></div>
    </div>
</div>