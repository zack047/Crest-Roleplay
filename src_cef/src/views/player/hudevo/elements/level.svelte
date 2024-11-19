<script>
    import { fly } from 'svelte/transition';
    import { charLVL, charEXP } from 'store/chars'
    import { translateText } from 'lang'
    import { TimeFormat, GetTime } from 'api/moment'
    import { serverDateTime } from 'store/server'
    import { charFractionID } from 'store/chars'
    import { format } from 'api/formatter'

    let cashValue = 0;
    window.PayDay = async (cash) => {
        //if (window.closeTip ())
        //    await window.wait(250);

        cashValue = cash;
        if (cashValue > 0) {
            window.hudStore.HideHelp (true);            
            await window.wait(5000);
            window.hudStore.HideHelp (false);
            cashValue = 0;
        } else {
            window.hudStore.HideHelp (false);
        }
    }


    const FractionTypes = {
        0: -1,
        1: 1, // The Families
        2: 1, // The Ballas Gang
        3: 1,  // Los Santos Vagos
        4: 1, // Marabunta Grande
        5: 1, // Blood Street
        6: 2, // Cityhall
        7: 2, // LSPD police
        8: 2, // Emergency care
        9: 2, // FBI 
        10: 0, // La Cosa Nostra 
        11: 0, // Russian Mafia
        12: 0, // Yakuza 
        13: 0, // Armenian Mafia 
        14: 2, // Army
        15: 2, // News
        16: 4, // The Lost
        17: 3, // Merryweather
        18: 2, // Sheriff
    };
    let visible = false;

    let level = -1;
    let maxExp = 0;
    let progress = 0;

    const GetMaxExp = (lvl) => {
        maxExp = 3 + lvl * 3;
    }

    const GetProgress = (exp) => {
        progress = exp * 100 / maxExp;
        
        if (progress > 100) progress = 100;
        else if (level != $charLVL) progress = 100;
    }

    window.updateLevel = async () => {
        onInit ();
        visible = true;
        await window.wait(500);
        GetProgress ($charEXP);
        await window.wait(5000);
        visible = false;
        if (level != $charLVL) {
            level = $charLVL;
            progress = 0;
            GetMaxExp (level);
            await window.wait(250);
            window.NewLvl ()
        }
    }

	const onInit = () => {
        if (level === -1) {
            level = $charLVL;
            GetMaxExp (level);
            GetProgress ($charEXP);
        }
    }
</script>
{#if visible}
    <div class="hud_level">
        <h1>LEVEL UP!</h1>
        <div class="lvl_info">
            <b>{level}</b>
            <div class="lvl_barbg">
                <div class="lvl_bar" style="width: {100/maxExp*$charEXP}%;"></div>
            </div>
            <b>{level + 1}</b>
        </div>
        <p>{$charEXP}/{maxExp} EXP</p>
        {#if cashValue > 0}
        <p><span>{format("money", cashValue)}$</span> {!(FractionTypes [$charFractionID] === 2 || FractionTypes [$charFractionID] === 3) ? 'Пособие по безработице' : 'Зарплата'}</p>
        {/if}
    </div>
{/if}