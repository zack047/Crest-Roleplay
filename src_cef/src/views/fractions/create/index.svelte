<script>
    import { translateText } from 'lang'
    import { format } from "api/formatter";
    import { executeClient } from "api/rage";

    import './main.sass' 

    export let viewData;

    let isCrime = false;
    const onSelectType = (_isCrime) => {
        isCrime = _isCrime;
    }


    let orgName = ""

    const onCreate = () => {
        let check = format("createOrg", orgName);
        if (!check.valid) {
            window.notificationAdd(4, 9, check.text, 3000);
            return;
        }

        executeClient('client.org.create.buy', isCrime, orgName)
    }

    const onClose = () => {
        executeClient('client.org.create.close')

    }
    const onKeyUp = (event) => {
        const { keyCode } = event;


        if (keyCode == 13)
            onCreate ()

        if (keyCode == 27)
            onClose ()
    }
</script>

<svelte:window on:keyup={onKeyUp}/>

<div id="fractionscreate">
    <div class="newproject__buttonblock" on:click={onClose}>
        <div class="newproject__button">ESC</div>
        <div>{translateText('player', 'Закрыть')}</div>
    </div>
    <div class="fractionscreate__title">Creating an organization</div>
    <div class="fractionscreate__subtitle">In this menu you can choose the type of your organization and create it. Take the creation process seriously!</div>
    <div class="fractionscreate__text_title">Type of organization:</div>
    <div class="fractionscreate__element mb-32" class:active={isCrime} on:click={() => onSelectType (true)}>
        <div class="fractionscreate__checkbox">
            <div class="active"></div>
        </div>
        <div class="box-column">
            <div class="box-flex">
                <div class="fractionscreate__icon crime"></div>
                <div class="fractionscreate__element_title">Grouping</div>
            </div>
            <div class="fractionscreate__element_type crime">Criminal organization</div>
            <div class="fractionscreate__element_text">A cell of the underworld whose activities consist of forming alliances with other criminal factions, wars of influence, robberies, kidnappings, and clashes with law enforcement and legal structures.</div>
        </div>
    </div>
    <div class="fractionscreate__element" class:active={!isCrime} on:click={() => onSelectType (false)}>
        <div class="fractionscreate__checkbox">
            <div class="active"></div>
        </div>
        <div class="box-column">
            <div class="box-flex">
                <div class="fractionscreate__icon gos"></div>
                <div class="fractionscreate__element_title">Community</div>
            </div>
            <div class="fractionscreate__element_type gos">Legal organization</div>
            <div class="fractionscreate__element_text">A private enterprise that signs a contract with the government to protect the rights and interests of the state. It has the ability to influence political and security forces and is part of the state system.</div>
        </div>
    </div>
    <div class="fractionscreate__text_title">Name of organization:</div>
    <div class="box-flex">
        <input class="fractionscreate__input" placeholder="Enter name.." bind:value={orgName}>
        <div class="box-column">
            <div class="fractionscreate__small">The cost of creation:</div>
            <div>${format("money", viewData)}</div>
        </div>
    </div>
    <div class="fractionscreate__info">
        Title from 3 and up to 30 characters
        <br>
        It is forbidden to use obscene language, insults or the names of existing organizations in the name.
    </div>
    <div class="fractionscreate__button" on:click={onCreate}>
        Create an organization
    </div>
</div>