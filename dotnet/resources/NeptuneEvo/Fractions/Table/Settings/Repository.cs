using System;
using NeptuneEvo.Fractions.Models;
using NeptuneEvo.Fractions.Player;
using NeptuneEvo.Character;
using NeptuneEvo.Players.Models;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Handles;
using NeptuneEvo.Table.Models;
using Redage.SDK;

namespace NeptuneEvo.Fractions.Table.Settings
{
    public class Repository
    {
        public static void UpdateStock(ExtPlayer player)
        {
            try
            {
                if (!player.IsFractionAccess(RankToAccess.OpenStock)) return;

                var fractionData = player.GetFractionData();
                var characterData = player.GetCharacterData();
                if (fractionData == null)
                    return;

                fractionData.IsOpenStock = !fractionData.IsOpenStock;

                if (fractionData.IsOpenStock)
                {
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "You allowed the crafting of weapons", 3000);
                    Fractions.Table.Logs.Repository.AddLogs(player, FractionLogsType.OpenStock, "Permission to Craft Weapons.");
                    Manager.sendFractionMessage(fractionData.Id, "!{#ADFF2F}[F] " + $"{player.Name} ({characterData.UUID}) allowed to craft weapons.", true);
                }
                else
                {
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "You have banned weapon crafting", 3000);
                    Fractions.Table.Logs.Repository.AddLogs(player, FractionLogsType.CloseStock, "He banned gun-running");
                    Manager.sendFractionMessage(fractionData.Id, "!{#ADFF2F}[F] " + $"{player.Name} ({characterData.UUID}) outlawed gunrunning.", true);
                }

                Trigger.ClientEvent(player, "client.frac.main.isStock", fractionData.IsOpenStock);
            }
            catch (Exception e)
            {
                Debugs.Repository.Exception(e);
            }
        }
        public static void UpdateGunStock(ExtPlayer player)
        {
            try
            {
                if (!player.IsFractionAccess(RankToAccess.OpenGunStock)) return;

                var fractionData = player.GetFractionData();
                var characterData = player.GetCharacterData();
                if (fractionData == null)
                    return;

                fractionData.IsOpenGunStock = !fractionData.IsOpenGunStock;

                if (fractionData.IsOpenGunStock)
                {
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "You have opened a faction warehouse", 3000);
                    Fractions.Table.Logs.Repository.AddLogs(player, FractionLogsType.OpenStock, "Opened a warehouse");
                    Manager.sendFractionMessage(fractionData.Id, "!{#ADFF2F}[F] " + $"{player.Name} ({characterData.UUID}) opened a warehouse.", true);
                }
                else
                {
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "You have closed the faction's warehouse", 3000);
                    Fractions.Table.Logs.Repository.AddLogs(player, FractionLogsType.CloseStock, "Closed the warehouse");
                    Manager.sendFractionMessage(fractionData.Id, "!{#ADFF2F}[F] " + $"{player.Name} ({characterData.UUID}) closed the warehouse.", true);
                }

                Trigger.ClientEvent(player, "client.frac.main.isGunStock", fractionData.IsOpenGunStock);
            }
            catch (Exception e)
            {
                Debugs.Repository.Exception(e);
            }
        }
    }
}