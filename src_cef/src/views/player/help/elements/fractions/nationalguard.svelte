<script>
    import image from '../../images/army.jpg'
	import image2 from '../../images/army2.jpg'
    export let onWayPoint;
</script>

<div class="text">
    <p><span style="font-weight: 400;"><strong>One of the main tasks</strong> This organization specializes in supplying military resources to other influential state structures, bolstering their power and capabilities.&nbsp;</span><span style="font-weight: 400;">Ensuring the safeguarding of military resources and thwarting any attempts by criminal elements to illicitly acquire them from the base is of paramount importance. Additionally, the military assumes the crucial responsibility of swiftly responding to emergencies within the state.</span></p>
<p><span style="font-weight: 400;"><strong>To join</strong> the ranks of the National Guard, it is imperative to adhere to the advance location announcements disseminated through state news channels. Prospective members must also carry essential documents such as a valid passport, a medical certificate, and a driver's license of the appropriate category. <strong>B</strong> And <strong>C</strong>.</span></p>
<p><span style="font-weight: 400;"><strong>Upon enlistment</strong> in the National Guard, you will begin your journey as a Cadet. To ascend the career ladder, you will be required to undertake a range of tasks assigned by the authorities. These tasks serve as stepping stones towards progression within the organization.</span></p>
<p><strong>Buttons</strong>
    <br><span style="font-weight: 400;"><strong>X</strong> - Remove/put on handcuffs;<br /></span><strong>Z</strong> - To lead<br>
    <strong> "G" > Fraction > Search</strong> - Search the player's belongings <br>
        <p><strong>Teams</strong><br><strong>/m</strong> - using a megaphone<br /><strong>/openstock</strong> - open a warehouse<br /><strong>/closestock</strong> - close the warehouse<br /><strong>/incar</strong> - drag a person into a car<br /><strong>/pull</strong> - pull the person out of the car<br /><strong>/opengs</strong> - Open Warehouse №2<br /><strong>/closegs</strong> - Close Warehouse №2</p>


    <div style="display: flex; flex-direction: column;"><img src={image} alt="mws" class="contentImg"/><img src={image2} alt="mws" class="contentImg"/></div>

    <div class="button aligncenter">
        <div class="btn" on:click={onWayPoint}>Show on Map <span class="icon map"/></div>
    </div>
</div>