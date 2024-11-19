<script>
  import { executeClient } from 'api/rage'
  import { getQuest } from 'json/quests/quests.js';
  import { storeQuests, selectQuest } from 'store/quest'

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

      onSelectQuest($selectQuest);
    }
  });

  selectQuest.subscribe((value) => {
      onSelectQuest (value);
  });
</script>

{#if quest && quest.Title}
<div class="hud__top_quest">
  <img src="./assets/img/trophy.svg" alt="" class="hud__top_quest-trophy">
  <h1>{quest.Title}</h1>
  
  <h2 class:completed="{false}">{@html quest.Text}</h2>
</div>
{/if}