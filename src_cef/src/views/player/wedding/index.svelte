<script>
    import { executeClient } from 'api/rage'
    import { format } from 'api/formatter'
    import { charMoney } from 'store/chars.js'
    import './main.sass';
    import InputCustom from 'components/input/index.svelte'
    import { validate } from 'api/validation';
    export let viewData;

    const price = viewData.price;

    let weddingType = viewData.type; // 0 - женить / 1 - развод

    const weddingTypeText = [
        "You have come to a place where lovers can seal their feelings with the bonds of marriage. The world is only for you two!",
        "There are different situations in life... Sometimes what seems like love can turn out to be a crush... We all understand...",
    ];

    let typeSurname = 0;

    let nameInput = "";

    const onBuy = () => {
        if (weddingType == 0) {
            if (nameInput.length === 0) {
                window.notificationAdd(4, 9, `You did not enter the person's ID or name`, 3000);
                return;
            }
            let check;
            check = validate("name", nameInput);
            if (/\D/.test(nameInput) && !check.valid) {
                window.notificationAdd(4, 9, check.text, 3000);
                return;
            }
        }
        if (price > Number ($charMoney)) { 
            window.notificationAdd(4, 9, `You don't have enough money`, 3000);
            return;   
        }
        executeClient ("client.wedding.married", nameInput, typeSurname);
    }

</script>
<div id="wedding" class:divorce={weddingType == 1}>
    <div class="wedding__header">Zags</div>
    <div class="wedding__text">{weddingTypeText [weddingType]}</div>
    {#if weddingType === 0}
    <InputCustom setValue={(value) => nameInput = value} value={nameInput} placeholder="Name or ID" type="text" icon="auth-user"/>
    <div class="wedding__subtitle">Choose a last name</div>
    <div class="box-flex" style="margin-bottom: 0">
        <div class="wedding__selector" class:active={typeSurname == 0} on:click={() => typeSurname = 0}>Groom</div>
        <div class="wedding__selector" class:active={typeSurname == 1} on:click={() => typeSurname = 1}>Brides</div>
    </div>
    <div class="box-center">
        <div class="wedding__selector" class:active={typeSurname == 2} on:click={() => typeSurname = 2}>Don't change</div>
    </div>
    {/if}
    <div class="wedding__subtitle">${format("money", price)}</div>
    <div class="wedding__text">The cost of the service</div>
    <div class="wedding__button" on:click={onBuy}>Pay</div>
    <div class="wedding__exit">
        <div class="wedding__exit_text">Log out</div>
        <div class="wedding__exit_button">ESC</div>
    </div>
</div>