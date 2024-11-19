<script>
    import image from '../../images/daln.jpg'
	import image2 from '../../images/daln2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;">We are seeking a reliable and experienced long-haul truck driver to join our transportation team. As a trucker, you will be responsible for transporting goods over long distances, ensuring timely and safe delivery to various destinations. Your strong driving skills, adherence to traffic regulations, and ability to maintain a high level of alertness are crucial for the success of this role.</span></p>
    <p><span style="font-weight: 400;">After employment, you must exit the City Hall and use the GPS on your phone, select &laquo;<strong>GPS &gt; Works &gt; Truck Stop</strong>&raquo;.</span></p>
    <p><span style="font-weight: 400;">After arriving at the parking lot, you should proceed to an available car and get inside it. Then, you will be offered to rent it. To open the menu with orders, you need to enter the command &ldquo;<strong>/orders</strong>&rdquo; and choose a preferred order, which you will need to pick up from the warehouse and deliver to the customer.</span></p>
    <p><span style="font-weight: 400;"><strong>Requirements</strong>: Class &ldquo;<strong>C</strong>&rdquo; License<br /></span>
		<span style ="font-weight: 400;"><strong>Salary</strong>: From $800 to $3,500</span></p>
    <p><span style="font-weight: 400;"><strong>/orders</strong> - open menu with order selection (only works at goods loading point). Tags have been added to goods loading points that will also open the order menu.<br /></span>
		<strong>/t</strong> - truckers walkie-talkie<br />
		<strong>/findtrailer</strong> - if you lost the trailer, this command will help you find it<br />
		<strong>/mypoint</strong> - using this command, you can find out where you need to deliver the goods</p>
    <p><br /><br /></p>
    
    <div style="display: flex; flex-direction: column;"><img src={image} alt="News" class="contentImg"/><img src={image2} alt="News" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show on Map <span class="icon map"/></div>
    </div>
</div>