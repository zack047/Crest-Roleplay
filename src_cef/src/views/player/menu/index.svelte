<script>
    import { translateText } from 'lang'
    import './main.sass';
    import './fonts/inv/style.css';
    import './fonts/items/style.css';
    import './fonts/gamemenu/style.css';

    import { serverDateTime, isEvent } from 'store/server'
    import { TimeFormat } from 'api/moment'

    export let visible = true;
    
    let selectView = "Inventory";
    let timerView = "Inventory";

    import Inventory from "./elements/inventory/inventory.svelte";
    import Stats from "./elements/stats.svelte";
    import Settings from "./elements/settings/index.svelte";
    import Quests from "./elements/quests/index.svelte";
    import Events from "./elements/events.svelte";
    import Support from "./elements/support/index.svelte";
    import RewardsList from "./elements/rewardslist/index.svelte";
    import Table from "./elements/fractions/index.svelte";

    const Views = {
        Stats,
        Settings,
        Quests,
        Events,
        Support,
        RewardsList,
        Fractions: Table,
        Organization: Table,
    }
    
    window.gameMenuView = (wiew) => {
        selectView = wiew;
        timerView = wiew;
    }
    const defaultSorted = ["Inventory", "Stats", "Settings", "Quests", "RewardsList", "Fractions", "Organization", "Events"];
    let _pagesSorted = ["Inventory", "Stats", "Settings", "Quests", "RewardsList"];
    let PagesSorted = ["Inventory", "Stats", "Settings", "Quests", "RewardsList"];

    const updatePage = (name, value) => {

        const index = _pagesSorted.indexOf(name)

        if (index !== -1 && !value)
            _pagesSorted.splice(index, 1);
        else if (index === -1 && value)
            _pagesSorted.push(name)

        let sorted = [];

        defaultSorted.forEach((value) => {
            if (_pagesSorted.includes (value))
                sorted.push(value)
        })

        PagesSorted = sorted;
    }

    import { charFractionID, charOrganizationID } from 'store/chars'


    isEvent.subscribe(value => {
        updatePage ("Events", value);
    });
    charFractionID.subscribe(value => {
        updatePage ("Fractions", value > 0);
    });
    charOrganizationID.subscribe(value => {
        updatePage ("Organization", value > 0);
    });


    let UseVisible = visible;
    let TimeQE = 0;

    import { onInputBlur } from './elements/fractions/data'

    $: {
        if (UseVisible != visible) {
            UseVisible = visible;
            TimeQE = new Date().getTime() + 250;
            if (!visible) {
                selectView = "Inventory";
                timerView = "Inventory";
                onInputBlur ();
            }
        }
    }

    let isFocusInput = false;

    window.onFocusInput = (value) => isFocusInput = value;

    let timerId = null;

    const setPage = () => {
        if (timerId !== null)
            clearTimeout(timerId);
            
        timerId = setTimeout(() => {
            selectView = timerView;
            timerId = null;
        }, 200)
    }

    function onClickQ() {
        let index = PagesSorted.findIndex (p => p === selectView)
        
        if(--index >= 0) {
            selectView = PagesSorted [index];
            //setPage ();
        }
    }

    function onClickE() {
        let index = PagesSorted.findIndex (p => p === selectView)

        if (++index < PagesSorted.length) {
            selectView = PagesSorted [index];
            //setPage ();
        }
    }
    
    const onKeyUp = (event) => {
        if (isFocusInput)
            return;

        if (!visible) return;
        else if (TimeQE > new Date().getTime()) return;

        const { keyCode } = event;
        
        if(keyCode == 65) {
            onClickQ ();
        } else if(keyCode == 68) { 
            onClickE ();
        }
    }
</script>
<svelte:window on:keyup={onKeyUp} />
<div id="box-menu" style="display: {visible ? 'flex' : 'none'}">
        
    <div class="box-nav">
        <div class="header" />
        <div class="nav">
            <div class="box-key" on:click={onClickQ}>A</div>
            <div class="nav-lists">
                <div style="width: 7vw" class="item" class:active={selectView === "Inventory"} on:click={() => window.gameMenuView ("Inventory")}>
                    {translateText('player1', 'Инвентарь')}
                </div>
                <div style="width: 7vw"  class="item" class:active={selectView === "Stats"} on:click={() => window.gameMenuView ("Stats")}>
                    {translateText('player1', 'Статистика')}
                </div>
                <div style="width: 7vw" class="item" class:active={selectView === "Settings"} on:click={() => window.gameMenuView ("Settings")}>
                    {translateText('player1', 'Настройки')}
                </div>             
                <div style="width: 7vw"  class="item" class:active={selectView === "Fractions"} on:click={() => window.gameMenuView ("Fractions")}>
                    {translateText('player1', 'Фракция')}
                </div>
            </div>
            <div style="border: 1px solid var(--1111, #F13E53);background: var(--555, linear-gradient(to bottom right, rgba(241, 62, 83, 0.13) 0%, rgba(241, 62, 83, 0.21) 50%) bottom right / 50% 50% no-repeat, linear-gradient(to bottom left, rgba(241, 62, 83, 0.13) 0%, rgba(241, 62, 83, 0.21) 50%) bottom left / 50% 50% no-repeat, linear-gradient(to top left, rgba(241, 62, 83, 0.13) 0%, rgba(241, 62, 83, 0.21) 50%) top left / 50% 50% no-repeat, linear-gradient(to top right, rgba(241, 62, 83, 0.13) 0%, rgba(241, 62, 83, 0.21) 50%) top right / 50% 50% no-repeat);;border-radius: .25vw" class="box-key"on:click={onClickE}>D</div>
        </div>
    </div>
    <Inventory visible={selectView === "Inventory"} />
    <svelte:component this={Views[selectView]} {visible} {selectView} />
</div>