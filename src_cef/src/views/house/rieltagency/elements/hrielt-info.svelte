<script>
    import { houseType } from 'json/realEstate.js'
    import { format } from 'api/formatter'
    export let selectData;
    export let buyPrice;
</script>
<div class="rielt__rielt_info">
    <div class="houseicon-house rielt__rielt_house-icon"></div>
    <div class="rielt__rielt_header">House Number:{selectData[0]}</div>
    <div class="rielt__gray">Wonderful house at an affordable price</div>
    <div class="rielt__rielt_stat">
        <div class="rielt__rielt_element">
            <div class="rielt__gray">Taxes:</div>
            <div>{selectData[3]}</div>
        </div>
        <div class="rielt__rielt_element">
            <div class="rielt__gray">Class:</div>
            <div>{houseType [selectData[1]]}</div>
        </div>
        <div class="rielt__rielt_element">
            <div class="rielt__gray">Garage spaces:</div>
            <div>{selectData[4]}</div>
        </div>
        <div class="rielt__rielt_element">
            <div class="rielt__gray">State Value:</div>
            <div>${format("money", selectData[2])}</div>
        </div>
    </div>
    <div class="box-column">
        <div class="rielt__rielt_price">${format("money", (selectData[1] + 1) * buyPrice)}</div>
        <div class="rielt__gray">Price for Information:</div>
    </div>
</div>