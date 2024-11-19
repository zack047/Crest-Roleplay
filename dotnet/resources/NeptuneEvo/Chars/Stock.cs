using GTANetworkAPI;
using NeptuneEvo.Handles;
using NeptuneEvo.Functions;
using NeptuneEvo.Players;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Character;
using NeptuneEvo.Quests;
using Redage.SDK;
using System;
using Localization;
using NeptuneEvo.Quests.Models;

namespace NeptuneEvo.Chars
{
    class Stock : Script
    {
        private static readonly nLog Log = new nLog("Chars.MergerToServer");
        [ServerEvent(Event.ResourceStart)]
        public void Init()
        {
            try
            {
                //-- Moved these to core\Blips.cs --//
                //Main.CreateBlip(new Main.BlipData(478, "Storage 'GoPostal'", new Vector3(-545.02136, -204.28348, 38.2), 32, true));
                //Main.CreateBlip(new Main.BlipData(525, "Employment center", new Vector3(436.5074, -627.4617, 28.707539), 0, true));
                //NAPI.Marker.CreateMarker(1, new Vector3(-545.02136, -204.28348, 38.2) - new Vector3(133.13315, 96.48856, 83.508484), new Vector3(133.13315, 96.48856, 83.508484), new Vector3(133.13315, 96.48856, 83.508484), 1f, new Color(255, 255, 255, 220));
                NAPI.Marker.CreateMarker(30, new Vector3(133.13315, 96.48856, 83.508484), new Vector3(), new Vector3(), 1f, new Color(255, 255, 0, 255), dimension: 0);
                NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~w~Post Office"), new Vector3(133.13315, 96.48856, 83.508484), 5F, 0.3F, 0, new Color(255, 255, 255), dimension: 0);
                NAPI.TextLabel.CreateTextLabel(Main.StringToU16("~w~Exit"), new Vector3(1048.2255, -3097.1624, -38.9999), 5F, 0.3F, 0, new Color(255, 255, 255), dimension: 5);
                CustomColShape.CreateCylinderColShape(new Vector3(133.16599, 96.30163, 83.5), 1f, 2, 0, ColShapeEnums.WarehouseEnter);
                CustomColShape.CreateCylinderColShape(new Vector3(1048.2255, -3097.1624, -38.9999), 1f, 2, 5, ColShapeEnums.WarehouseExit);
                PedSystem.Repository.CreateQuest("s_m_m_postal_01", new Vector3(1071.4469, -3107.637, -38.99992), 44.169724f, 5, title: "~y~NPC~w~ Letter Carrier Alexander\nPost Office", colShapeEnums: ColShapeEnums.Warehouse);
            }
            catch (Exception e)
            {
                Log.Write($"StartWork Exception: {e.ToString()}");
            }
        }
        [Interaction(ColShapeEnums.WarehouseEnter)]
        public static void WarehouseEnter(ExtPlayer player)
        {
            var characterData = player.GetCharacterData();
            if (characterData == null) return;
            player.Position = new Vector3(1048.2255, -3097.1624, -38.9999);
            Trigger.Dimension(player, 5);
            characterData.IsInPostalStock = true;
        }
        [Interaction(ColShapeEnums.WarehouseExit)]
        public static void WarehouseExit(ExtPlayer player)
        {
            var characterData = player.GetCharacterData();
            if (characterData == null) return;
            player.Position = new Vector3(132.9969, 96.3529, 83.5076);
            Trigger.Dimension(player, 0);
            characterData.IsInPostalStock = false;
        }
        [Interaction(ColShapeEnums.Warehouse)]
        public static void OpenWarehouse(ExtPlayer player, int index)
        {
            var sessionData = player.GetSessionData();
            if (sessionData == null) return;
            else if (sessionData.CuffedData.Cuffed)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.IsCuffed), 3000);
                return;
            }
            else if (sessionData.DeathData.InDeath)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.IsDying), 3000);
                return;
            }
            else if (Main.IHaveDemorgan(player, true)) return;

            player.SelectQuest(new PlayerQuestModel("npc_stock", 0, 0, false, DateTime.Now));
            Trigger.ClientEvent(player, "client.quest.open", index, "npc_stock", 0, 0, 0);
            BattlePass.Repository.UpdateReward(player, 151);
        }
        public static void Perform(ExtPlayer player, int index)
        {
            try
            {
                var characterData = player.GetCharacterData();
                if (characterData == null) return;
                if (index == 0)
                {
                    if (!FunctionsAccess.IsWorking("warehouseopen"))
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.FunctionOffByAdmins), 3000);
                        return;
                    }

                    Repository.LoadOtherItemsData(player, "warehouse", characterData.UUID.ToString(), 10);
                }
                else Jobs.WorkManager.JobJoin(player, 2);
            }
            catch (Exception e)
            {
                Log.Write($"OnWarehouse Exception: {e.ToString()}");
            }
        }
    }
}