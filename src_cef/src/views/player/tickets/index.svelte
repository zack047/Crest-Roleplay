<script>
    import './main.sass'
    import './fonts/style.css'
    import { format } from 'api/formatter'
    import moment from 'moment'
    import { executeClient } from 'api/rage'
    export let viewData;

    if (!viewData)
        viewData = []

    if (viewData && typeof viewData === "string")
        viewData = JSON.parse (viewData)

    let selectData = null;
    let hoverIndex = 0;
    
    const onKeyUp = (event) => {
        const { keyCode } = event;
        
        switch (keyCode) {
            case 38:
                if (--hoverIndex < 0)
                    hoverIndex = viewData.length - 1;
                break;
            case 40:                
                if (++hoverIndex >= viewData.length)
                    hoverIndex = 0;
                break;
            case 13:
                onSelect (hoverIndex);
                break;
            case 27:
                onExit ();
                break;
        }
    }

    const onSelect = (index) => {        
        if (selectData == null) {
            hoverIndex = index;
            selectData = viewData [index];
        } else {
            executeClient("client.ticket.payment", selectData.AutoId)
            executeClient("client.ticket.close");
        }
    }

    const onExit = () => {        
        if (selectData !== null)
            selectData = null;
        else 
            executeClient("client.ticket.close")
    }
</script>

<svelte:window on:keyup={onKeyUp} />

<div id="playerticket">
    {#if selectData == null}
        <div class="playerticket__title">Drawing up a fine</div>
        <div class="playerticket__header">
            <div class="playerticket__carname">Model</div>
            <div class="playerticket__date">Date</div>
            <div class="playerticket__price">Penalty</div>
            <div class="playerticket__text">Number</div>
        </div>
        <div class="playerticket__list">
            {#each viewData as ticket, index}
            <div class="playerticket__element" class:active={hoverIndex === index} on:click={() => onSelect (index)}>
                <div class="playerticket__carname">
                    <span class="houseicon-car"></span>
                    {ticket.Model}
                </div>
                <div class="playerticket__date">{moment(ticket.Time).format('DD.MM.YYYY HH:mm')}</div>
                <div class="playerticket__price">${format("money", ticket.Price)}</div>
                <div class="playerticket__text">{ticket.VehNumber}</div>
            </div>
            {/each}
        </div>
        <div class="playerticket__buttons">
            <div class="playerticket_bottom_buttons center" on:click={() => onSelect (hoverIndex)}>
                <div class="playerticket_bottom_button">ENTER</div>
                <div>Select</div>
            </div>
            <div class="playerticket_bottom_buttons esc" on:click={onExit}>
                <div>Log out</div>
                <div class="playerticket_bottom_button">ESC</div>
            </div>
        </div>
    {:else}
        <div class="playerticket__title">Payment of the fine</div>
        <div class="playerticket__photo" style="background-image: url({selectData.Link})"></div>
        <div class="playerticket__line"></div>
        <div class="playerticket__title">Information</div>
        <div class="box-between">
            <div class="gray">Owner:</div>
            <div class="playerticket__line"></div>
            <div class="playerticket__name">{selectData.HolderName}</div>
        </div>
        <div class="box-between">
            <div class="gray">Evacuated:</div>
            <div class="playerticket__line"></div>
            <div class="playerticket__name">{selectData.PolicName.replace(/_/g, " ")}</div>
        </div>
        <div class="box-between">
            <div class="gray">Machine model:</div>
            <div class="playerticket__line"></div>
            <div class="playerticket__name">{selectData.Model}</div>
        </div>
        <div class="box-between">
            <div class="gray">Violation:</div>
            <div class="playerticket__line"></div>
            <div class="playerticket__name">{selectData.Text}</div>
        </div>
        <div class="box-between">
            <div class="gray">Price.:</div>
            <div class="playerticket__line"></div>
            <div class="playerticket__name">${format("money", selectData.Price)}</div>
        </div>
        <div class="playerticket__buttons">
            <div class="playerticket_bottom_buttons center" on:click={onSelect}>
                <div class="playerticket_bottom_button">ENTER</div>
                <div>Pay the fine</div>
            </div>
            <div class="playerticket_bottom_buttons esc" on:click={onExit}>
                <div>Log out</div>
                <div class="playerticket_bottom_button">ESC</div>
            </div>
        </div>
    {/if}
</div>