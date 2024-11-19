<script>
    import {executeClient, executeClientToGroup} from "api/rage";
    export let closeMenu;

    export let selectTaxi;

    const onStartPoint = () => {
        executeClient ("createWaypoint", selectTaxi.pos.x, selectTaxi.pos.y);
        executeClientToGroup ("close");
    }

    const onCancelOrder = () => {
        if (!window.loaderData.delay ("taxijob.onCancelOrder", 2))
            return;

        executeClientToGroup ("taxijob.cancel");
    }
    import { fade } from 'svelte/transition'

</script>

<div in:fade class="box-100">
    <div class="box-between newphone__project_padding20">
        <div class="newphone__maps_header">Active Orders</div>
        <div class="phoneicons-add1" on:click={closeMenu}></div>
    </div>
    
    <div class="newphone__maps_list">
        <div class="box-flex" style="width: 100%">
            <div class="newmphone__maps_circle"><div class="newmphone__maps_circle2"></div></div>
            <div class="newphone__maps_column">
                <div class="newphone__column_title">{selectTaxi.aStreet}</div>
                <div class="newphone__column_subtitle">{selectTaxi.aArea}</div>
            </div>
        </div>
        <div class="newphone__maps_title">Route has been set.</div>
        <div class="newphone__maps_subtitle">The destination has been marked in your GPS navigator. Payment will be deducted from the client automatically as the trip progresses.</div>
        <div class="newphone__project_button mt-0" on:click={onStartPoint}>Show on Map</div>
        <div class="newphone__project_button mt-0" on:click={onCancelOrder}>Cancel Order</div>
    </div>
</div>