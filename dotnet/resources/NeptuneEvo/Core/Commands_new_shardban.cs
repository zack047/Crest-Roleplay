using GTANetworkAPI;
using NeptuneEvo.Handles;
using NeptuneEvo.Accounts;
using NeptuneEvo.Chars;
using NeptuneEvo.Chars.Models;
using NeptuneEvo.Players;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Character;
using Redage.SDK;
using System;
using System.Linq;

namespace NeptuneEvo.Core
{
    class Commands_new_shardban : Script
    {
        public static readonly nLog Log = new nLog("Core.Commands_new_shardban");

        [Command("shardban", GreedyArg = true)]
        public static void CMD_shardban(ExtPlayer player, int id, int time, string reason)
        {
            try
            {
                var characterData = player.GetCharacterData();
                if (characterData == null) return;
                if (characterData.AdminLVL < 5) return;

                string playerLogin = player.GetLogin();

                ExtPlayer target = Main.GetPlayerByUUID(id);
                var targetCharacterData = target.GetCharacterData();
                if (targetCharacterData == null) return;
                if (player == target) return;
                string targetLogin = target.GetLogin();

                int tadmlvl = targetCharacterData.AdminLVL;
                if (tadmlvl == 9)
                {
                    Trigger.SendToAdmins(1, "!{#FF0000}" + $"[A] {player.Name} ({player.Value}) Tried to hard-bang {target.Name} ({target.Value}).");
                    Admin.BanMe(player, 0);
                    return;
                }
                else if (tadmlvl != 0 && tadmlvl >= characterData.AdminLVL)
                {
                    Trigger.SendToAdmins(3, $"{ChatColors.StrongOrange}[A] {player.Name} ({player.Value}) Hardcore banned {target.Name} ({target.Value}) and was banned by the system.");

                    Character.BindConfig.Repository.DeleteAdmin(target);
                    Character.BindConfig.Repository.DeleteAdmin(player);

                    Ban.Online(target, DateTime.MaxValue, true, reason, player.Name);
                    Ban.Online(player, DateTime.MaxValue, true, $"Banned by the system for admin ban {target.Name}", "server");

                    Notify.Send(target, NotifyType.Warning, NotifyPosition.Center, $"You received a banhammer forever as an administrator {player.Name}.", 30000);
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.Center, $"You received a banhammer forever by the system for banning an administrator {target.Name}.", 30000);

                    int AUUID = characterData.UUID;
                    GameLog.Ban(AUUID, targetCharacterData.UUID, targetLogin, DateTime.MaxValue, reason, true);
                    GameLog.Ban(-2, AUUID, playerLogin, DateTime.MaxValue, $"Banned by system for admin hardban {target.Name}", true);

                    target.Kick(reason);
                    player.Kick("Banned by system for admin hardban");
                }
                else
                {
                    if (Main.stringGlobalBlock.Any(c => reason.Contains(c)))
                    {
                        Trigger.SendToAdmins(1, $"{ChatColors.Red}[A] {player.Name} ({player.Value}) was removed by the system for a reason in the punishment: {reason}");
                        Character.BindConfig.Repository.DeleteAdmin(player);
                        return;
                    }
                    
                    if (NeptuneEvo.Character.Repository.LoginsBlck.Contains(targetLogin))
                    {
                        Trigger.SendToAdmins(3, "!{#FF0000}" + $"[A] {player.Name} ({player.Value}) Tried to hard-bang {target.Name} ({target.Value}).");
                        Admin.BanMe(player, 0);
                        return;
                    }
                   
                    if (!Admin.CheckMe(player, 4)) return;

                    DateTime unbanTime = (time >= 3650) ? DateTime.MaxValue : DateTime.Now.AddDays(time);
                    if (time >= 3650) Trigger.SendToAdmins(1, "!{#FFB833}" + $"[A] {player.Name} banned a player for life {target.Name} without too much noise. Reason: {reason}");
                    else Trigger.SendToAdmins(1, "!{#FFB833}" + $"[A] {player.Name} banned a player {target.Name} at {time}without too much noise. Reason: {reason}");

                    Ban.Online(target, unbanTime, true, reason, player.Name);
                    Notify.Send(target, NotifyType.Warning, NotifyPosition.Center, $"You got the Banhammer to {unbanTime.ToString()}", 30000);
                    Notify.Send(target, NotifyType.Warning, NotifyPosition.Center, $"Reason: {reason}", 30000);
                    
                    int AUUID = characterData.UUID;
                    int TUUID = targetCharacterData.UUID;
                    GameLog.Ban(AUUID, TUUID, targetLogin, unbanTime, reason, true);
                    
                    target.Kick(reason);
                }
            }
            catch (Exception e)
            {
                Log.Write($"CMD_shardban Exception: {e.ToString()}");
            }
        }
    }
}
