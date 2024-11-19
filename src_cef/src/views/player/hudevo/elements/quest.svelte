<script>
    import { translateText } from 'lang'
    import { fly } from 'svelte/transition';
    import { getQuest } from 'json/quests/quests.js';
    import { storeQuests, selectQuest } from 'store/quest'
    import keys from 'store/keys'
    import keysName from 'json/keys.js'
    import { executeClient } from 'api/rage'
    import { isHelp } from 'store/hud'

    let QuestsList = [];
    let OldQuest = [];

    let quest = false;
    const onSelectQuest = (actorName) => {

        const listIndex = QuestsList.findIndex(q => q.ActorName == actorName);
        if (QuestsList [listIndex]) {
            quest = QuestsList [listIndex];
            return;
        }
        quest = false;
    }

    storeQuests.subscribe((value) => {
        if (value && value.length && OldQuest != value) {
            executeClient ("client.quest.selectQuest.Clear");

            OldQuest = value;
            QuestsList = [];

            value.forEach(questData => {
                const questInfo = getQuest(questData.ActorName, questData.Line);

                if (questInfo && questInfo.Title && questInfo.Tasks && questInfo.Tasks [questData.Stage]) {
                    executeClient ("client.quest.selectQuest.Add", questData.ActorName, (questInfo.Tasks.length - 1) === questData.Stage);

                    QuestsList.push ({
                        ActorName: questData.ActorName,
                        Title: questInfo.Title,
                        Text: questInfo.Tasks [questData.Stage]
                    });
                }
            });
            QuestsList = QuestsList;

            if (!quest && QuestsList.length && typeof QuestsList [0] === "object" && typeof $selectQuest !== "string") {
                quest = QuestsList [0];
                selectQuest.set (quest.ActorName);
                return;
            }

            onSelectQuest ($selectQuest);
        }
    });

    selectQuest.subscribe((value) => {
        onSelectQuest (value);
    });
</script>
{#if quest && quest.Title && $isHelp}
    <div class="hud_quest">
        <div class="quest_head">
            <svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg">
                <rect y="9.89948" width="14" height="14" rx="2" transform="rotate(-45 0 9.89948)" fill="#CFF80B"></rect>
                <rect x="5.10701" y="9.89948" width="6.77778" height="6.77778" rx="1.5" transform="rotate(-45 5.10701 9.89948)" stroke="white" stroke-opacity="0.7"></rect>
                <rect x="6.59961" y="9.89948" width="4.66667" height="4.66667" rx="1" transform="rotate(-45 6.59961 9.89948)" fill="white"></rect>
            </svg>
        <h1>{@html quest.Title}</h1>
        </div>
        <div class="quest_info">
            <svg width="11" height="11" viewBox="0 0 11 11" fill="none" xmlns="http://www.w3.org/2000/svg">
                <rect x="0.707107" y="5.49969" width="6.77778" height="6.77778" rx="1.5" transform="rotate(-45 0.707107 5.49969)" stroke="white"></rect>
                <rect x="2.19971" y="5.49969" width="4.66667" height="4.66667" rx="1" transform="rotate(-45 2.19971 5.49969)" fill="#CFF80B"></rect>
            </svg>
            <p>{@html quest.Text}</p>
        </div>
    </div>
{/if}