using GTANetworkAPI;
using NeptuneEvo.Handles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Data;
using Redage.SDK;
using MySqlConnector;
using System.Text.RegularExpressions;
using System.Threading;
using NeptuneEvo.Chars.Models;
using NeptuneEvo.Chars;
using NeptuneEvo.Fractions;
using NeptuneEvo.Functions;
using NeptuneEvo.Accounts;
using NeptuneEvo.Players.Models;
using NeptuneEvo.Players;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Character;
using Database;
using LinqToDB;
using System.Threading.Tasks;
using Localization;
using NeptuneEvo.Fractions.Models;
using NeptuneEvo.Fractions.Player;
using NeptuneEvo.Players.Phone.Messages.Models;
using NeptuneEvo.VehicleData.Data;
using NeptuneEvo.VehicleData.LocalData;
using NeptuneEvo.VehicleData.LocalData.Models;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;


namespace NeptuneEvo.Core
{
    class Admin : Script
    {
        public static readonly nLog Log = new nLog("Core.Admin");
        public static bool IsServerStoping = false;
        public static bool TimeChanged = false;
        public static short[] SetTime = new short[3] { 0, 0, 0 };

        public static Dictionary<string, (string, string, int)> ChangeNameQueue = new Dictionary<string, (string, string, int)>(); // CurName, (newName, admName, admLvl)
        public static Dictionary<ExtPlayer, (int, string, int)> SetLeaderQueue = new Dictionary<ExtPlayer, (int, string, int)>(); // targetId (fracid, admName, admLvl)
        public static Dictionary<ExtPlayer, (string, int)> SendCreatorQueue = new Dictionary<ExtPlayer, (string, int)>(); // targetId (admName, admLvl)
        public static Dictionary<int, (string, string, int, DateTime)> GlobalQueue = new Dictionary<int, (string, string, int, DateTime)>(); // QueueID (globaltext, admName, admLvl, globalexp)
        public static int GlobalID = 0;



        public static void RemoveQueue(ExtPlayer player, string text = "player exit")

        {

            if (SetLeaderQueue.ContainsKey(player))

            {

                SetLeaderQueue.Remove(player);

                Trigger.SendToAdmins(2, $"{ChatColors.StrongOrange}[A] Request a leaderboard for {player.Name} ({player.Value}) deleted due to {text}.");

            }

            if (SendCreatorQueue.ContainsKey(player))

            {

                SendCreatorQueue.Remove(player);

                Trigger.SendToAdmins(2, $"{ChatColors.StrongOrange}[A] Request for a change of appearance for {player.Name} ({player.Value}) deleted due to {text}.");

            }

        }
        public static void AdminLog(int myadm, string message)
        {
            try
            {
                message = "!{#636363}[A] " + message;
                int hisadm = 0;
                foreach (ExtPlayer foreachPlayer in Main.AllAdminsOnline)
                {

                    var foreachCharacterData = foreachPlayer.GetCharacterData();

                    if (foreachCharacterData == null)
                        continue;

                    hisadm = foreachCharacterData.AdminLVL;



                    var foreachAdminConfig = foreachCharacterData.ConfigData.AdminOption;

                    if (foreachAdminConfig.ALog && hisadm >= 6 && hisadm >= myadm)
                        Trigger.SendChatMessage(foreachPlayer, message);
                }
            }
            catch (Exception e)
            {
                Log.Write($"AdminLog Exception: {e.ToString()}");
            }
        }

        public static void AdminsLog(int myadm, string message, byte levels_to = 2, string color = "#D289FF", bool highRankActionHide = true, byte hideAdminLevel = 8)
        {
            try
            {
                message = "!{" + color + "}[A] " + message; // "!{#FFB833}[A] " + message;

                if (myadm == 9) // if Project Manager, only show in admin logs to other PMs
                {
                    foreach (ExtPlayer foreachPlayer in Main.AllAdminsOnline)
                    {
                        var foreachCharacterData = foreachPlayer.GetCharacterData();

                        if (foreachCharacterData == null) continue;
                        if (foreachCharacterData.AdminLVL == 9) Trigger.SendChatMessage(foreachPlayer, message);
                    }
                }
                else if (myadm >= hideAdminLevel && highRankActionHide == true) 
                {
                    foreach (ExtPlayer foreachPlayer in Main.AllAdminsOnline)
                    {
                        var foreachCharacterData = foreachPlayer.GetCharacterData();

                        if (foreachCharacterData == null) continue;
                        if (foreachCharacterData.AdminLVL >= hideAdminLevel) Trigger.SendChatMessage(foreachPlayer, message);
                    }
                }
                else
                {
                    foreach (ExtPlayer foreachPlayer in Main.AllAdminsOnline)
                    {

                        var foreachCharacterData = foreachPlayer.GetCharacterData();

                        if (foreachCharacterData == null) continue;
                        if (foreachCharacterData.AdminLVL >= levels_to) Trigger.SendChatMessage(foreachPlayer, message);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Write($"AdminsLog Exception: {e.ToString()}");
            }
        }

        public static void ErrorLog(string message)
        {
            try
            {
                message = "!{#d35400}[A][ELOG] " + message;
                foreach (ExtPlayer foreachPlayer in Main.AllAdminsOnline)
                {

                    var foreachCharacterData = foreachPlayer.GetCharacterData();

                    if (foreachCharacterData == null)

                        continue;



                    var foreachAdminConfig = foreachCharacterData.ConfigData.AdminOption;

                    if (foreachAdminConfig.ELog && foreachCharacterData.AdminLVL >= 6)
                        Trigger.SendChatMessage(foreachPlayer, message);
                }
            }
            catch (Exception e)
            {
                Log.Write($"ErrorLog Exception: {e.ToString()}");
            }
        }

        public static void WinLog(string message)
        {
            try
            {
                message = "!{#EAEAB3}[A][WINLOG] " + message;
                foreach (ExtPlayer foreachPlayer in Main.AllAdminsOnline)
                {
                    var foreachCharacterData = foreachPlayer.GetCharacterData();
                    if (foreachCharacterData == null)
                        continue;

                    var foreachAdminConfig = foreachCharacterData.ConfigData.AdminOption;
                    if (foreachAdminConfig.WinLog && foreachCharacterData.AdminLVL >= 5)
                        Trigger.SendChatMessage(foreachPlayer, message);
                }
            }
            catch (Exception e)
            {
                Log.Write($"WinLog Exception: {e.ToString()}");
            }
        }
        public static void onPlayerDeathHandler(ExtPlayer player, ExtPlayer entityKiller, uint weapon, Vector3 pos)
        {
            try
            {
                var sessionData = player.GetSessionData();
                if (sessionData == null)
                    return;

                var characterData = player.GetCharacterData();
                if (characterData == null)
                    return;

                characterData.Deaths++;
              
                var killerCharacterData = entityKiller.GetCharacterData();
                if (killerCharacterData != null)
                {
                    killerCharacterData.Kills++;
                    sessionData.DeathData.LastDeath = $"{DateTime.Now.ToString("s")} | {entityKiller.Name} ({entityKiller.Value}) killed {player.Name} with {weapon}";
                    GameLog.Kills($"player({killerCharacterData.UUID})", weapon.ToString(), $"player({characterData.UUID})", $"{pos.X} {pos.Y} {pos.Z}");

                    var killerSessionData = entityKiller.GetSessionData();
                    if (killerSessionData != null && killerSessionData.KillData.DamageDisabled == true)
                    {
                        killerSessionData.KillData.Count++;
                        if (killerSessionData.KillData.Count == 1) Notify.Send(entityKiller, NotifyType.Info, NotifyPosition.BottomCenter, $"If you kill another person before reaching level 1, you will be kicked.", 10000);
                        else if (killerSessionData.KillData.Count >= 2) entityKiller.Kick();
                    }
                }
                else
                {
                    GameLog.Kills($"system", "", $"player({characterData.UUID})", $"{pos.X} {pos.Y} {pos.Z}");
                    //message = $"~r~{player.Name}({player.Value}) ~s~умер";
                }
            }
            catch (Exception e)
            {
                Log.Write($"PlayerDeathHandler Exception: {e.ToString()}");
            }
        }


        public static void SendKillLog(ExtPlayer player, ExtPlayer killer, ExtPlayer victim, uint weapon)
        {
            Trigger.ClientEvent(player, "hud.kill", killer != null ? killer.Id : -1, victim != null ? victim.Id : -1, weapon);
        }
        public static void sendRedbucks(ExtPlayer player, ExtPlayer target, int amount)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Givereds)) return;
                var targetAccountData = target.GetAccountData();
                if (targetAccountData == null) return;
                if (!target.IsCharacterData()) return;
                if (targetAccountData.RedBucks + amount < 0) amount = 0;
                UpdateData.RedBucks(target, amount, msg: "Sending RB");
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.RbOutcome, target.Name, amount), 3000);
                //Notify.Send(target, NotifyType.Success, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.RbIncome, amount, player.Name), 3000);
                GameLog.Admin(player.Name, $"givereds({amount})", target.Name);
                Players.Phone.Messages.Repository.AddSystemMessage(target, (int)DefaultNumber.RedAge, LangFunc.GetText(LangType.Ru, DataName.RbIncome, amount, player.Name), DateTime.Now);
            }
            catch (Exception e)
            {
                Log.Write($"sendRedbucks Exception: {e.ToString()}");
            }
        }
        public static void giveFreeCase(ExtPlayer player, ExtPlayer target, byte caseid)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Givecase)) return;

                var targetAccountData = target.GetAccountData();
                if (targetAccountData == null) return;
                if (caseid < 0 || caseid > 2) return;
                Chars.Repository.AddNewItemWarehouse(target, ItemId.Case0 + caseid, 1);
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You issued {target.Name} ({target.Value}) Case: #{caseid}", 3000);
                Notify.Send(target, NotifyType.Success, NotifyPosition.BottomCenter, $"Congratulations you have been given a Free Case #{caseid} in the Free-Roulette Menu!", 3000);
                GameLog.Admin(player.Name, $"givecase{caseid}", target.Name);
            }
            catch (Exception e)
            {
                Log.Write($"giveFreeCase Exception: {e.ToString()}");
            }
        }
        public static void stopServer(ExtPlayer sender, string reason = "Server restart initiated!")
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(sender, AdminCommands.Restart)) return;
                stopServer($"{sender.Name}", reason);
            }
            catch (Exception e)
            {
                Log.Write($"stopServer Exception: {e.ToString()}");
            }
        }

        public static async void SaveServer()
        {
            try
            {
                foreach (var foreachPlayer in Character.Repository.GetPlayers())
                    foreachPlayer.SavePlayerPosition();

                var vehiclesLocalData = RAGE.Entities.Vehicles.All.Cast<ExtVehicle>()
                    .Where(v => v.VehicleLocalData != null)
                    .Where(v => v.VehicleLocalData.Access == VehicleAccess.Garage || v.VehicleLocalData.Access == VehicleAccess.Personal)
                    .ToList();

                foreach (var vehicle in vehiclesLocalData)
                    vehicle.SavePosition();

                await NAPI.Task.WaitForMainThread();

                Trigger.SetTask(async () =>
                {
                    try
                    {
                        Log.Write("Saving Database...");

                        await using var db = new ServerBD("MainDB");//In a seperate Thread

                        foreach (ExtPlayer foreachPlayer in Character.Repository.GetPlayers())
                        {
                            try
                            {
                                var targetSessionData = foreachPlayer.GetSessionData();
                                if (targetSessionData == null) continue;
                                if (!targetSessionData.IsConnect)
                                    continue;

                                var targetCharacterData = foreachPlayer.GetCharacterData();
                                if (targetCharacterData == null)
                                    continue;

                                await Character.Save.Repository.SaveSql(db, foreachPlayer);
                                await Accounts.Save.Repository.SaveSql(db, foreachPlayer);
                            }
                            catch (Exception e)
                            {
                                Log.Write($"saveDatabase Foreach #1 Exception: {e.ToString()}");
                            }
                        }
                        Log.Write("Players and Accounts has been saved to DB");
                        /*foreach (int acc in MoneySystem.Bank.Accounts.Keys)
                        {
                            try
                            {
                                if (!MoneySystem.Bank.Accounts.ContainsKey(acc)) continue;
                                MoneySystem.Bank.Save(acc);
                            }
                            catch (Exception e)
                            {
                                Log.Write($"saveDatabase Foreach #2 Exception: {e.ToString()}");
                            }
                        }
                        Log.Write("Bank Saved");*/
                        foreach (var number in VehicleManager.Vehicles.Keys)
                        {
                            if (!VehicleData.LocalData.Repository.IsVehicleToNumber(VehicleAccess.Personal, number)) continue;
                            await VehicleManager.SaveSql(db, number);
                        }
                        Log.Write("Vehicles has been saved to DB");
                        BusinessManager.SavingBusiness();
                        await Organizations.Manager.SaveOrganizations(db);
                        Houses.HouseManager.SavingHouses();
                        await Stocks.SaveFractions(db);
                        WeaponRepository.SaveWeaponsDB();
                        AlcoFabrication.SaveAlco();
                        await Main.SaveDoorsControl(db);
                        await Players.Phone.Tinder.Repository.Saves(db);
                        //
                        Ban.Delete();

                        Log.Write("Database was saved");
                    }
                    catch (Exception e)
                    {
                        Log.Write($"saveDatabase Exception: {e.ToString()}");
                    }
                });
            }
            catch (Exception e)
            {
                Log.Write($"saveDatabase Exception: {e.ToString()}");
            }
        }


        public static void stopServer(string Admin = "server", string reason = "There is a scheduled server reboot!")
        {
            try
            {
                Log.Write("Force saving database...", nLog.Type.Warn);
                IsServerStoping = true;
                GameLog.Admin(Admin.Replace('_', ' '), $"stopped server ({reason})", "");

                Log.Write("Force kicking players...", nLog.Type.Warn);
                foreach (var foreachPlayer in RAGE.Entities.Players.All.Cast<ExtPlayer>())
                {
                    Ems.ReviveFunc(foreachPlayer, true);
                    Trigger.ClientEvent(foreachPlayer, "restart");
                    Trigger.Dimension(foreachPlayer, Dimensions.RequestPrivateDimension(foreachPlayer.Value));

                }


                foreach (var foreachPlayer in RAGE.Entities.Players.All.Cast<ExtPlayer>())
                    Players.Disconnect.Repository.OnPlayerDisconnect(foreachPlayer, DisconnectionType.Kicked, reason, isSave: false);

                Trigger.SetTask(async () =>
                {
                    var speedSave = DateTime.Now;
                    Log.Write($"[{DateTime.Now - speedSave}] All players were kicked!", nLog.Type.Success);

                    await using var db = new ServerBD("MainDB");//В отдельном потоке

                    foreach (var foreachPlayer in RAGE.Entities.Players.All.Cast<ExtPlayer>())
                    {
                        try
                        {
                            var targetSessionData = foreachPlayer.GetSessionData();
                            if (targetSessionData == null)
                                continue;

                            await Character.Save.Repository.SaveSql(db, foreachPlayer);
                            await Accounts.Save.Repository.SaveSql(db, foreachPlayer);
                        }
                        catch (Exception e)
                        {
                            Log.Write($"saveDatabase Foreach #1 Exception: {e.ToString()}");
                        }
                    }

                    Log.Write($"[{DateTime.Now - speedSave}] All players saved!", nLog.Type.Success);

                    await Accounts.Email.Repository.VerificationsDelete();

                    BusinessManager.SavingBusiness();

                    await Organizations.Manager.SaveOrganizations(db);

                    Houses.HouseManager.SavingHouses(true);

                    await Stocks.SaveFractions(db);

                    WeaponRepository.SaveWeaponsDB();

                    await Main.SaveDoorsControl(db);

                    await Players.Phone.Tinder.Repository.Saves(db);

                    //Log.Write($"[{DateTime.Now - speedSave}] Save property", nLog.Type.Success);

                    //await SaveAllPlayersServer();

                    Log.Write($"[{DateTime.Now - speedSave}] Restart All data has been saved!", nLog.Type.Success);

                    Timers.StartOnce(1000 * 60, () => { Environment.Exit(0); }, true);
                });


            }
            catch (Exception e)
            {
                Log.Write($"stopServer Exception: {e.ToString()}");
            }
        }

        private static async Task SaveAllPlayersServer()
        {
            var isSave = true;
            Console.WriteLine("SaveAllPlayersServer 1");

            do
            {
                //Thread.Sleep(1000 * 10);

                isSave = RAGE.Entities.Players.All.Cast<ExtPlayer>()
                    .Any(p => !p.IsRestartSaveAccountData || !p.IsRestartSaveCharacterData);

                await Task.Delay(1000);

            } while (isSave);

            Console.WriteLine("SaveAllPlayersServer 2");
        }
        public static void saveCoords(ExtPlayer player, string msg)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Save)) return;
                Vector3 pos = NAPI.Entity.GetEntityPosition(player);
                //pos.Z -= 1.12f;
                Vector3 rot = NAPI.Entity.GetEntityRotation(player);
                if (NAPI.Player.IsPlayerInAnyVehicle(player))
                {
                    var vehicle = (ExtVehicle)player.Vehicle;
                    pos = NAPI.Entity.GetEntityPosition(vehicle) + new Vector3(0, 0, 0.5);
                    rot = NAPI.Entity.GetEntityRotation(vehicle);
                }
                using (StreamWriter saveCoords = new StreamWriter("coords.txt", true, Encoding.UTF8))
                {
                    saveCoords.Write($"{msg}: new Vector3({pos.X}, {pos.Y}, {pos.Z}),\r\n");
                    saveCoords.Close();
                }
            }
            catch (Exception e)
            {
                Log.Write($"saveCoords Exception: {e.ToString()}");
            }
        }
        public static void setPlayerAdminGroup(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Setadmin)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null)

                    return;

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null)
                    return;

                if (targetCharacterData.AdminLVL >= 1)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Error: This player is already an Administrator", 3000);
                    return;
                }

                Character.Save.Repository.SaveAdminLvl(targetCharacterData.UUID, 1);

                targetCharacterData.AdminLVL = 1;

                Character.BindConfig.Repository.InitAdmin(target);

                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have successfully given admin rights to the player {target.Name}", 3000);
                Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter, $"{player.Name} Admin rights granted", 3000);
                AdminsLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) {target.Name} was granted level 1 admin rights", 1, "#FFB833", false);
                GameLog.Admin($"{player.Name}", AdminCommands.Setadmin, $"{target.Name}");
            }
            catch (Exception e)
            {
                Log.Write($"setPlayerAdminGroup Exception: {e.ToString()}");
            }
        }
        public static void OffdelPlayerAdminGroup(ExtPlayer player, string targetName)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Offdeladmin)) return;

                var sessionData = player.GetSessionData();
                if (sessionData == null)
                    return;

                var characterData = player.GetCharacterData();
                if (characterData == null)
                    return;

                var target = (ExtPlayer)NAPI.Player.GetPlayerFromName(targetName);
                if (target.IsCharacterData())
                {
                    delPlayerAdminGroup(player, target);
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "The player was online, so offdeladmin was changed to deladmin", 3000);
                    return;
                }
                if (!Main.PlayerUUIDs.ContainsKey(targetName))
                {
                    Notify.Send(target, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.CantFindMan), 3000);
                    return;
                }
                var targetuuid = Main.PlayerUUIDs[targetName];

                Trigger.SetTask(async () =>
                {
                    try
                    {
                        await using var db = new ServerBD("MainDB");//In a seperate thread

                        var character = await db.Characters
                            .Select(c => new
                            {
                                c.Uuid,
                                c.Firstname,
                                c.Lastname,
                                c.Adminlvl,
                            })
                            .Where(v => v.Uuid == targetuuid)
                            .FirstOrDefaultAsync();

                        if (character == null)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Player not found", 3000);
                            return;
                        }

                        if (character.Adminlvl <= 0)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"This player does not have Admin rights", 3000);
                            return;
                        }

                        if (character.Adminlvl >= characterData.AdminLVL)
                        {
                            Trigger.SendToAdmins(3, "!{#FF0000}" + $"[A] {sessionData.Name} ({sessionData.Value}) tried to remove {targetName} (offline), who has a higher level of Administration.");
                            return;
                        }

                        Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have removed Administrator access from {targetName}", 3000);
                        AdminsLog(characterData.AdminLVL, $"{sessionData.Name} ({sessionData.Value}) Removed {targetName} from Administrator", 1, "#FFB833", false);
                        GameLog.Admin($"{sessionData.Name}", $"OffDelAdmin", $"{targetName}");

                        await db.Characters
                            .Where(c => c.Uuid == character.Uuid)
                            .Set(c => c.Adminlvl, 0)
                            .UpdateAsync();
                    }
                    catch (Exception e)
                    {
                        Log.Write($"OffdelPlayerAdminGroup SetTask Exception: {e.ToString()}");
                    }
                });
            }
            catch (Exception e)
            {
                Log.Write($"OffdelPlayerAdminGroup Exception: {e.ToString()}");
            }
        }
        public static void delPlayerAdminGroup(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Deladmin)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null)
                    return;
                if (player == target)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"You cannot take away admin rights from yourself", 3000);
                    return;
                }

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null)
                    return;
                if (targetCharacterData.AdminLVL >= characterData.AdminLVL)
                {
                    Trigger.SendToAdmins(3, "!{#FF0000}" + $"[A] {player.Name} ({player.Value}) tried to remove {target.Name} ({target.Value}), who has a higher level of Administration.");
                    return;
                }
                if (targetCharacterData.AdminLVL < 1)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Error: This player is not an Administrator", 3000);
                    return;
                }

                Character.BindConfig.Repository.DeleteAdmin(target);

                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have removed Administrator access from {target.Name}", 3000);
                Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter, $"You have been removed from Administrator", 3000);
                AdminsLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) has removed {target.Name} from Administrator", 1, "#FFB833", false);
                GameLog.Admin($"{player.Name}", $"delAdmin", $"{target.Name}");
            }
            catch (Exception e)
            {
                Log.Write($"delPlayerAdminGroup Exception: {e.ToString()}");
            }
        }
        public static void setPlayerAdminRank(ExtPlayer player, ExtPlayer target, int rank)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Arank)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                if (player == target)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"You cannot assign your own rank", 3000);
                    return;
                }

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null) return;
                if (targetCharacterData.AdminLVL < 1)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "This player is not an Administrator!", 3000);
                    return;
                }
                if (targetCharacterData.AdminLVL >= characterData.AdminLVL)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Insufficient permissions to alter this admin's level", 3000);
                    return;
                }
                if (rank < 1 || rank >= characterData.AdminLVL)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Insufficient permissions", 3000);
                    return;
                }
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have granted Admin Level {rank} to {target.Name}", 3000);
                Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter, $"You have been granted Admin Level {rank}", 3000);
                AdminsLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) granted {target.Name} with Admin Level {rank}", 1, "#FFB833", false);
                targetCharacterData.AdminLVL = rank;
                target.SetSharedData("ALVL", rank);
                Character.Save.Repository.SaveAdminLvl(targetCharacterData.UUID, rank);
                GameLog.Admin($"{player.Name}", $"setAdminRank({rank})", $"{target.Name}");
            }
            catch (Exception e)
            {
                Log.Write($"setPlayerAdminRank Exception: {e.ToString()}");
            }
        }

        public static void offSetPlayerAdminRank(ExtPlayer player, string targetName, int rank)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Offarank)) return;

                var sessionData = player.GetSessionData();
                if (sessionData == null)
                    return;

                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                if (rank < 1 || rank >= characterData.AdminLVL)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"It is impossible to give out such a rank", 3000);
                    return;
                }

                ExtPlayer target = (ExtPlayer)NAPI.Player.GetPlayerFromName(targetName);

                if (target.IsCharacterData())
                {
                    setPlayerAdminRank(player, target, rank);
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "The player was online, so offarank was replaced by arank", 3000);
                    return;
                }

                if (!Main.PlayerUUIDs.ContainsKey(targetName))
                {
                    Notify.Send(target, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.CantFindMan), 3000);
                    return;
                }
                var targetuuid = Main.PlayerUUIDs[targetName];

                Trigger.SetTask(async () =>
                {
                    try
                    {
                        await using var db = new ServerBD("MainDB");//В отдельном потоке

                        var character = await db.Characters
                            .Select(c => new
                            {
                                c.Uuid,
                                c.Firstname,
                                c.Lastname,
                                c.Adminlvl,
                            })
                            .Where(v => v.Uuid == targetuuid)
                            .FirstOrDefaultAsync();

                        if (character == null)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Man not found", 3000);
                            return;
                        }
                        if (character.Adminlvl <= 0)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"The person is not an administrator!", 3000);
                            return;
                        }

                        if (character.Adminlvl >= characterData.AdminLVL)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"You cannot change this administrator's permission level", 3000);
                            return;
                        }

                        Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You gave a player {targetName} {rank} level of administrative rights", 3000);
                        AdminsLog(characterData.AdminLVL, $"{sessionData.Name} ({sessionData.Value}) retrieved {targetName} {rank} level of administrative rights", 1, "#FFB833", false);
                        GameLog.Admin($"{sessionData.Name}", $"offSetAdminRank({rank})", $"{targetName}");

                        await db.Characters
                            .Where(c => c.Uuid == character.Uuid)
                            .Set(c => c.Adminlvl, rank)
                            .UpdateAsync();
                    }
                    catch (Exception e)
                    {
                        Log.Write($"offSetPlayerAdminRank SetTask Exception: {e.ToString()}");
                    }
                });
            }
            catch (Exception e)
            {
                Log.Write($"offSetPlayerAdminRank Exception: {e.ToString()}");
            }

        }
        public static void setPlayerVipLvl(ExtPlayer player, ExtPlayer target, int rank, ushort days)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Givevip)) return;



                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                if (rank > 5 || rank < 0)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"It is impossible to give out such a level of VIP account", 3000);
                    return;
                }
                if (days > 1095)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"The maximum term of VIP for issue is 3 years (1,095 days)", 3000);
                    return;
                }

                var targetAccountData = target.GetAccountData();
                if (targetAccountData == null) return;
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You gave the man {target.Name} {Group.GroupNames[rank]}", 3000);
                EventSys.SendCoolMsg(target, "Administration", "VIP issuance", $"Administrator {player.Name} gave you {Group.GroupNames[rank]}", "", 7000);
                AdminsLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) put up {target.Name} status {Group.GroupNames[rank]} at {days} days.");
                targetAccountData.VipLvl = rank;
                if (targetAccountData.VipDate > DateTime.Now) targetAccountData.VipDate = targetAccountData.VipDate.AddDays(days);
                else targetAccountData.VipDate = DateTime.Now.AddDays(days);
                GameLog.Admin($"{player.Name}", $"setVipLvl({rank},{days})", $"{target.Name}");
            }
            catch (Exception e)
            {
                Log.Write($"setPlayerVipLvl Exception: {e.ToString()}");
            }
        }

        public static void setFracLeader(ExtPlayer player, ExtPlayer target, int fractionId)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Setleader)) return;

                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                var targetCharacterData = target.GetCharacterData();
                if (targetCharacterData == null) return;

                var fractionData = Manager.GetFractionData(fractionId);
                if (fractionData != null)
                {
                    if (!target.IsCharacterData()) return;

                    if (characterData.AdminLVL == 4 && player == target)
                    {
                        if (SetLeaderQueue.ContainsKey(target))
                            SetLeaderQueue.Remove(target);

                        target.RemoveFractionMemberData();
                        target.ClearAccessories();
                        target.AddFractionMemberData(fractionId, fractionData.LeaderRank());
                        Customization.ApplyCharacter(target);

                        // log actions in the admin log and database
                        AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) made {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) leader of {Manager.GetName(fractionId)} (ID: {fractionId})", 2, "#D289FF", true, hideAdminLevel: 6);
                        GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"set as leader of {Manager.GetName(fractionId)} ({fractionId})", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");

                        // notify the admin that they have been assigned as leader
                        EventSys.SendCoolMsg(target, "Administrator", $"{ player.Name.Replace('_', ' ')}", $"Made you leader of {Manager.GetName(fractionId)} (ID: {fractionId})", "", 7000);

                        if (Fractions.FractionClothingSets.FractionMainCloakrooms.ContainsKey(fractionId))
                        {
                            Notify.Send(target, NotifyType.Success, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.StartWorkDay), 3000);
                            Manager.SetSkin(target);
                        }
                    }
                    else if (characterData.AdminLVL >= 4 || (SetLeaderQueue.ContainsKey(target) && SetLeaderQueue[target].Item1 == fractionId))
                    {
                        if (characterData.AdminLVL <= 3)
                        {
                            if (!SetLeaderQueue.ContainsKey(target)) return;
                            if (SetLeaderQueue[target].Item2 == player.Name)
                            {
                                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Only another administrator can confirm it!", 3000);
                                return;
                            }

                            // log actions in the admin log
                            AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) made {SetLeaderQueue[target].Item2.Replace('_', ' ')} ({targetCharacterData.UUID}) leader of {Manager.GetName(fractionId)} (ID: {fractionId})", 2, "#D289FF", true, hideAdminLevel: 6);
                            AdminsLog(SetLeaderQueue[target].Item3, $"{SetLeaderQueue[target].Item2.Replace('_', ' ')} made {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) leader of {Manager.GetName(fractionId)} (ID: {fractionId})", 2, "#D289FF", true, hideAdminLevel: 6);

                            // log actions in the adminlog table in the database
                            GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"agreed to set as leader of {Manager.GetName(fractionId)} ({fractionId})", $"{SetLeaderQueue[target].Item2.Replace('_', ' ')} ({targetCharacterData.UUID})");
                            GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"set as leader of {Manager.GetName(fractionId)} ({fractionId})", $"{SetLeaderQueue[target].Item2.Replace('_', ' ')} ({targetCharacterData.UUID})");

                            // notify the admin when they approve the request
                            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You confirmed the request to make {SetLeaderQueue[target].Item2.Replace('_', ' ')} ({targetCharacterData.UUID}) leader of {Manager.GetName(fractionId)} (ID: {fractionId})", 5000);
                        }
                        else
                        {
                            // log actions in the admin log
                            AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) made {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) leader of {Manager.GetName(fractionId)} (ID: {fractionId})", 2, "#D289FF", true, hideAdminLevel: 6);

                            // log actions in the adminlog table in the database
                            GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"set as leader of {Manager.GetName(fractionId)} ({fractionId})", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");

                            // notify the player when they have been set as leader
                            Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You made {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) leader of {Manager.GetName(fractionId)} (ID: {fractionId})", 7000);
                        }

                        if (SetLeaderQueue.ContainsKey(target))
                            SetLeaderQueue.Remove(target);

                        target.RemoveFractionMemberData();
                        target.ClearAccessories();
                        target.AddFractionMemberData(fractionId, fractionData.LeaderRank());
                        Customization.ApplyCharacter(target);

                        // notify the admin that they have been assigned as leader
                        EventSys.SendCoolMsg(target, "Administrator", $"{player.Name.Replace('_', ' ')}", $"Made you leader of  {Manager.GetName(fractionId)} (ID: {fractionId})", "", 8000);

                        if (Fractions.FractionClothingSets.FractionMainCloakrooms.ContainsKey(fractionId))
                        {
                            Notify.Send(target, NotifyType.Success, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.StartWorkDay), 3000);
                            Manager.SetSkin(target);
                        }
                    }
                    else
                    {
                        if (SetLeaderQueue.ContainsKey(target) && SetLeaderQueue[target].Item1 != fractionId)
                        {
                            SetLeaderQueue.Remove(target);
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The leader request for this character has been removed", 4000);
                        }
                        SetLeaderQueue.Add(target, (fractionId, player.Name, characterData.AdminLVL));
                        Trigger.SendToAdmins(2, "!{#FFB833}" + $"[A] Request from {player.Name.Replace('_', ' ')} ({characterData.UUID}) to set {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) as leader of {Manager.GetName(fractionId)} (ID: {fractionId}). To confirm the action - type: /setleader {targetCharacterData.UUID} {fractionId}");
                    }
                }
            }
            catch (Exception e)
            {
                Log.Write($"setFracLeader Exception: {e.ToString()}");
            }
        }
        public static void delFracLeader(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                // check it they have access to this admin command
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Delleader)) return;

                var characterData = player.GetCharacterData();
                if (characterData == null)
                    return;

                var targetCharacterData = target.GetCharacterData();
                if (targetCharacterData == null)
                    return;

                // check if they are in a faction
                var targetMemberFractionData = target.GetFractionMemberData();
                if (targetMemberFractionData == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"{target.Name.Replace('_', ' ')} has no faction", 5000);
                    return;
                }

                // check if they are in a faction
                var fractionData = Manager.GetFractionData(targetMemberFractionData.Id);
                if (fractionData == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"{target.Name.Replace('_', ' ')} has no faction", 5000);
                    return;
                }

                // check to make sure they are actually leader of a faction
                if (targetMemberFractionData.Rank < fractionData.LeaderRank())
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"{target.Name.Replace('_', ' ')} is not a leader of their faction", 5000);
                    return;
                }

                // remove them from the faction, and remove all faction outfits / gear
                target.RemoveFractionMemberData();
                target.ClearAccessories();
                Customization.ApplyCharacter(target);

                // notify the admins that they have been removed as leader
                EventSys.SendCoolMsg(target, "Administrator", $"{player.Name.Replace('_', ' ')}", $"Has removed you as leader of {fractionData.Name} (ID: {fractionData.Id})", "", 7000);
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You've removed {target.Name.Replace('_', ' ')} as leader of {fractionData.Name} (ID: {fractionData.Name})", 7000);

                // log actions in the admin log and database
                AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) removed {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) as leader of {fractionData.Name} (ID: {fractionData.Id})", 2, "#D289FF", true, hideAdminLevel: 6);
                GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"removed from leader of {fractionData.Name} ({fractionData.Id})", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
            }
            catch (Exception e)
            {
                Log.Write($"delFracLeader Exception: {e.ToString()}");
            }
        }
        public static void delJob(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                // check if the player has access to this admin command
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Deljob)) return;

                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                var targetSessionData = target.GetSessionData();
                if (targetSessionData == null) return;

                var targetCharacterData = target.GetCharacterData();
                if (targetCharacterData == null) return;

                // check to see if the target is actually employed
                if (targetCharacterData.WorkID != 0)
                {
                    // check to see if the target is in uniform
                    if (targetSessionData.WorkData.OnWork)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) must be out of uniform", 5000);
                        return;
                    }
                    
                    // remove the target from their job
                    UpdateData.Work(target, 0);

                    // notify the player and the target that they've been removed from their job
                    Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter, $"{player.Name.Replace('_', ' ')} removed you from your job", 5000);
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You've removed {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) from their job", 5000);

                    // update the admin log and database log
                    AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) fired {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) from their job", 2, "#D289FF", true, hideAdminLevel: 6);
                    GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"fired from their job", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
                }
                // notify the player that the target has no job
                else Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) has no job", 5000);
            }
            catch (Exception e)
            {
                Log.Write($"delJob Exception: {e.ToString()}");
            }
        }
        public static void delFrac(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Delfrac)) return;

                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                var targetMemberFractionData = target.GetFractionMemberData();
                if (targetMemberFractionData == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"The man has no faction", 3000);
                    return;
                }

                var fractionData = Manager.GetFractionData(targetMemberFractionData.Id);
                if (fractionData == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"The man has no faction", 3000);
                    return;
                }

                if (targetMemberFractionData.Rank >= fractionData.LeaderRank())
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"The person is the leader of a faction", 3000);
                    return;
                }

                target.RemoveFractionMemberData();
                target.ClearAccessories();
                Customization.ApplyCharacter(target);

                Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter, $"Administrator {player.Name.Replace('_', ' ')} kicked you out of the faction", 3000);
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You threw out {target.Name.Replace('_', ' ')} from the fraction", 3000);
                //Chars.Repository.PlayerStats(target);
                Admin.AdminsLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) kicked out of the faction {target.Name} ({target.Value})");
                GameLog.Admin($"{player.Name}", $"delFrac", $"{target.Name}");
            }
            catch (Exception e)
            {
                Log.Write($"delFrac Exception: {e.ToString()}");
            }
        }

        public static void giveBankMoney(ExtPlayer player, int bankid, int amount)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Givemoney)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                if (!MoneySystem.Bank.Accounts.ContainsKey(bankid))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player with this ID/account number was not found", 3000);
                    return;
                }
                MoneySystem.Bank.Data data = MoneySystem.Bank.Accounts[bankid];
                if (data.Type != 1)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The bank account you are trying to enter does not belong to the person.", 3000);
                    return;
                }
                MoneySystem.Bank.Change(bankid, amount, false);
                GameLog.Money($"player({characterData.UUID})", $"bank({bankid})", amount, "admin");
                GameLog.Admin($"{player.Name}", $"giveBankMoney({amount})", $"{data.Holder}");
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You gave the man {data.Holder} {amount}$ into a bank account {bankid}", 3000);

                //if (amount >= 1000000)
                //    Admin.AdminsLog(1, $"[ВНИМАНИЕ] Игрок {data.Holder} получил {amount}$ единой операцией от {player.Name}({player.Value}) (giveBankMoney)", 1, "#FF0000");

                // var target = (ExtPlayer)NAPI.Player.GetPlayerFromName(data.Holder);
                // if (target.IsCharacterData())
                // {
                //     var targetSessionData = target.GetSessionData();
                //     if (targetSessionData == null) return;
                //     
                //     if (amount >= 10000 && targetSessionData.LastBankOperationSum == amount)
                //     {
                //         Admin.AdminsLog(1, $"[ВНИМАНИЕ] Игрок {target.Name}({target.Value}) два раза подряд получил по {amount}$ от {player.Name}({player.Value}) (giveBankMoney)", 1, "#FF0000");
                //         targetSessionData.LastBankOperationSum = 0;
                //     }
                //     else
                //     {
                //         targetSessionData.LastBankOperationSum = amount;
                //     }
                // }
            }
            catch (Exception e)
            {
                Log.Write($"giveBankMoney Exception: {e.ToString()}");
            }
        }
        public static void giveMoney(ExtPlayer player, ExtPlayer target, int amount)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Givemoney)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null) return;
                MoneySystem.Wallet.Change(target, amount);
                GameLog.Money($"player({characterData.UUID})", $"player({targetCharacterData.UUID})", amount, "admin");
                GameLog.Admin($"{player.Name}", $"giveMoney({amount})", $"{target.Name}");
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You gave the man {target.Name} {amount}$", 3000);
                Players.Phone.Messages.Repository.AddSystemMessage(target, (int)DefaultNumber.Bank, LangFunc.GetText(LangType.Ru, DataName.MoneyIncome, amount), DateTime.Now);
            }
            catch (Exception e)
            {
                Log.Write($"giveMoney Exception: {e.ToString()}");
            }
        }
        public static void OffGiveMoney(ExtPlayer player, string name, int amount)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Offgivemoney)) return;

                var sessionData = player.GetSessionData();
                if (sessionData == null)
                    return;

                var characterData = player.GetCharacterData();
                if (characterData == null)
                    return;

                if (!Main.PlayerNames.Values.Contains(name) || !Main.PlayerUUIDs.ContainsKey(name))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "A person with this name was not found", 3000);
                    return;
                }
                ExtPlayer target = (ExtPlayer)NAPI.Player.GetPlayerFromName(name);
                if (target.IsCharacterData())
                {
                    giveMoney(player, target, amount);
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "The player was online, so offgivemoney was replaced by givemoney", 3000);
                    return;
                }
                int targetuuid = Main.PlayerUUIDs[name];
                Trigger.SetTask(async () =>
                {
                    try
                    {
                        await using var db = new ServerBD("MainDB");//В отдельном потоке

                        var character = await db.Characters
                            .Select(c => new
                            {
                                c.Uuid,
                                c.Money,
                            })
                            .Where(v => v.Uuid == targetuuid)
                            .FirstOrDefaultAsync();

                        if (character == null)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Man not found", 3000);
                            return;
                        }

                        var money = Convert.ToInt32(character.Money) + amount;
                        if (money < 0)
                            money = 0;

                        GameLog.Money($"player({characterData.UUID})", $"player({character.Uuid})", amount, "admin");
                        GameLog.Admin($"{sessionData.Name}", $"offGiveMoney({amount})", $"{name}");
                        //Players.Phone.Messages.Repository.AddSystemMessage(target, (int)DefaultNumber.Bank, LangFunc.GetText(LangType.Ru, DataName.MoneyIncome, amount), DateTime.Now); //НЕ РАБОТАЕТ
                        Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You have established {name} {money}$ (+{amount}$)", 3000);

                        await db.Characters
                            .Where(c => c.Uuid == character.Uuid)
                            .Set(c => c.Money, money)
                            .UpdateAsync();
                    }
                    catch (Exception e)
                    {
                        Log.Write($"OffGiveMoney SetTask Exception: {e.ToString()}");
                    }
                });
            }
            catch (Exception e)
            {
                Log.Write($"OffGiveMoney Exception: {e.ToString()}");
            }
        }
        public static void mutePlayer(ExtPlayer player, ExtPlayer target, int time, string reason)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Mute)) return;
                if (!player.IsCharacterData()) return;

                var targetSessionData = target.GetSessionData();

                if (targetSessionData == null) return;

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null) return;
                if (player == target) return;
                if (time < 5 || time > 10080)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"You can't give a mute for more than 10080 minutes", 3000);
                    return;
                }
                if (Main.stringGlobalBlock.Any(c => reason.Contains(c)))
                {
                    Trigger.SendToAdmins(1, $"{ChatColors.Red}[A] {player.Name} ({player.Value}) was removed by the system for a reason in the punishment: {reason}");
                    Character.BindConfig.Repository.DeleteAdmin(player);
                    return;
                }
                if (!CheckMe(player, 0)) return;
                int firstTime = time * 60;
                string deTimeMsg = " minutes";
                if (time > 60)
                {
                    deTimeMsg = " hours";
                    time /= 60;
                    if (time > 24)
                    {
                        deTimeMsg = " days";
                        time /= 24;
                    }
                }
                targetCharacterData.Unmute = firstTime;
                targetCharacterData.VoiceMuted = true;
                if (targetSessionData.TimersData.MuteTimer != null) Timers.Stop(targetSessionData.TimersData.MuteTimer);
                targetSessionData.TimersData.MuteTimer = Timers.Start(1000, () => timer_mute(target));
                target.SetSharedData("vmuted", true);
                Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} Gave a mute to a player {target.Name}({target.Value}) at {time} {deTimeMsg}. Reason: {reason}", target);
                GameLog.Admin($"{player.Name}", $"mutePlayer({time}{deTimeMsg}, {reason})", $"{target.Name}");
                Players.Phone.Messages.Repository.AddSystemMessage(target, (int)DefaultNumber.RedAge, $"{player.Name} Gave you a mute on the {time} {deTimeMsg}. Reason: {reason}", DateTime.Now);
            }
            catch (Exception e)
            {
                Log.Write($"mutePlayer Exception: {e.ToString()}");
            }
        }
        public static void unmutePlayer(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!player.IsCharacterData()) return;

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null) return;
                if (targetCharacterData.Unmute >= 0)
                {
                    targetCharacterData.Unmute = 1;
                    Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} Removed the mute from a player {target.Name}({target.Value})");
                    GameLog.Admin($"{player.Name}", $"unmutePlayer", $"{target.Name}");
                    Players.Phone.Messages.Repository.AddSystemMessage(target, (int)DefaultNumber.RedAge, $"{player.Name} Took you off the mute.", DateTime.Now);
                }
                else Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player doesn't have a mutation", 1500);
            }
            catch (Exception e)
            {
                Log.Write($"unmutePlayer Exception: {e.ToString()}");
            }
        }

        public static void OffMutePlayer(ExtPlayer player, string targetName, int time, string reason)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Offmute)) return;
                ExtPlayer target = (ExtPlayer)NAPI.Player.GetPlayerFromName(targetName);
                if (target.IsCharacterData())
                {
                    mutePlayer(player, target, time, reason);
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "The player was online, so offmute was replaced by mute", 3000);
                    return;
                }
                if (!Main.PlayerNames.Values.Contains(targetName) || !Main.PlayerUUIDs.ContainsKey(targetName))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "A person with this name was not found", 3000);
                    return;
                }
                if (time < 5 || time > 10080)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"You can't give a mute for more than 10080 minutes", 3000);
                    return;
                }
                if (Main.stringGlobalBlock.Any(c => reason.Contains(c)))
                {
                    Trigger.SendToAdmins(1, $"{ChatColors.Red}[A] {player.Name} ({player.Value}) was removed by the system for a reason in the punishment: {reason}");
                    Character.BindConfig.Repository.DeleteAdmin(player);
                    return;
                }
                if (player.Name.Equals(targetName)) return;
                if (!CheckMe(player, 0)) return;
                int firstTime = time * 60;
                string deTimeMsg = " минут";
                if (time > 60)
                {
                    deTimeMsg = " часов";
                    time /= 60;
                    if (time > 24)
                    {
                        deTimeMsg = " дней";
                        time /= 24;
                    }
                }
                int targetuuid = Main.PlayerUUIDs[targetName];
                string[] split = targetName.Split('_');

                Character.Save.Repository.SaveUnMute(targetuuid, firstTime);

                Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} Gave a mute to a player {targetName} offline at {time} {deTimeMsg}. Reason: {reason}");
                GameLog.Admin($"{player.Name}", $"mutePlayer({time}{deTimeMsg}, {reason})", $"{targetName}");
            }
            catch (Exception e)
            {
                Log.Write($"OffMutePlayer Exception: {e.ToString()}");
            }

        }
        public static void OffUnMutePlayer(ExtPlayer player, string targetName)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Offunmute)) return;
                ExtPlayer target = (ExtPlayer)NAPI.Player.GetPlayerFromName(targetName);
                if (target.IsCharacterData())
                {
                    unmutePlayer(player, target);
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "The player was online, so offunmute was replaced by unmute", 3000);
                    return;
                }
                if (!Main.PlayerNames.Values.Contains(targetName) || !Main.PlayerUUIDs.ContainsKey(targetName))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "A person with this name was not found", 3000);
                    return;
                }
                if (player.Name.Equals(targetName)) return;
                int targetuuid = Main.PlayerUUIDs[targetName];
                string[] split = targetName.Split('_');

                Character.Save.Repository.SaveUnMute(targetuuid, 0);

                Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} Removed the mute from a player {targetName} offline.");
                GameLog.Admin($"{player.Name}", $"unmutePlayer", $"{targetName}");
            }
            catch (Exception e)
            {
                Log.Write($"OffUnMutePlayer Exception: {e.ToString()}");
            }
        }



        public static void getRb(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Getlogin)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;

                var targetAccountData = target.GetAccountData();

                if (targetAccountData == null) return;
                int targetRb = targetAccountData.RedBucks;
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"A player has {target.Name} ({target.Value}) - {targetRb} RedBucks", 5000);
                AdminLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) learned the number of RBs from {target.Name} ({target.Value}) - {targetRb}");
            }
            catch (Exception e)
            {
                Log.Write($"getRb Exception: {e.ToString()}");
            }
        }

        public static void getVip(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.GetVip)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;

                var targetAccountData = target.GetAccountData();

                if (targetAccountData == null) return;
                int targetVip = targetAccountData.VipLvl;
                DateTime targetVipDate = targetAccountData.VipDate;
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"A player has {target.Name} ({target.Value}) - VIP {targetVip} levels up to {targetVipDate} ", 5000);
                AdminLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) found out VIP status {target.Name} ({target.Value}) - {targetVip}");
            }
            catch (Exception e)
            {
                Log.Write($"getRb Exception: {e.ToString()}");
            }
        }

        public static void getLogin(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Getlogin)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                if (player == target) return;
                string targetlogin = target.GetLogin();
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Player Login {target.Name} ({target.Value}) - {targetlogin}", 5000);
                AdminLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) Verifies the registration {target.Name} ({target.Value}) - {targetlogin}");
            }
            catch (Exception e)
            {
                Log.Write($"getLogin Exception: {e.ToString()}");
            }
        }
        public static bool CheckMe(ExtPlayer player, byte type)
        {
            try
            {

                var sessionData = player.GetSessionData();

                if (sessionData == null) return false;

                var characterData = player.GetCharacterData();

                if (characterData == null) return false;
                if (characterData.AdminLVL == 0) return false;
                if (Main.ServerNumber == 0 || characterData.AdminLVL == 9) return true;
                int alvl = characterData.AdminLVL;
                int limit = (alvl >= 1 && alvl <= 5) ? 5 : 10;
                switch (type)
                {
                    case 0:
                        if (sessionData.AdminData.MuteCount == limit)
                        {
                            Trigger.SendToAdmins(3, $"{ChatColors.Red}[A] {player.Name} ({player.Value}) was removed by the security system for exceeding the penalty limit");
                            BanMe(player, 1);
                            return false;
                        }
                        sessionData.AdminData.MuteCount++;
                        break;
                    case 1:
                        if (sessionData.AdminData.KickCount == limit)
                        {
                            Trigger.SendToAdmins(3, $"{ChatColors.Red}[A] {player.Name} ({player.Value}) was removed by the security system for exceeding the penalty limit");
                            BanMe(player, 1);
                            return false;
                        }
                        sessionData.AdminData.KickCount++;
                        break;
                    case 2:
                        if (sessionData.AdminData.JailCount == limit)
                        {
                            Trigger.SendToAdmins(3, $"{ChatColors.Red}[A] {player.Name} ({player.Value}) was removed by the security system for exceeding the penalty limit");
                            BanMe(player, 1);
                            return false;
                        }
                        sessionData.AdminData.JailCount++;
                        break;
                    case 3:
                        if (sessionData.AdminData.WarnCount == limit)
                        {
                            Trigger.SendToAdmins(3, $"{ChatColors.Red}[A] {player.Name} ({player.Value}) was removed by the security system for exceeding the penalty limit");
                            BanMe(player, 1);
                            return false;
                        }
                        sessionData.AdminData.WarnCount++;
                        break;
                    case 4:
                        if (sessionData.AdminData.BansCount == limit)
                        {
                            Trigger.SendToAdmins(3, $"{ChatColors.Red}[A] {player.Name} ({player.Value}) was removed by the security system for exceeding the penalty limit");
                            BanMe(player, 1);
                            return false;
                        }
                        sessionData.AdminData.BansCount++;
                        break;
                    case 5:
                        if (sessionData.AdminData.AclearCount == 5)
                        {
                            Trigger.SendToAdmins(3, $"{ChatColors.Red}[A] {player.Name} ({player.Value}) was removed by the security system for exceeding the penalty limit");
                            BanMe(player, 1);
                            return false;
                        }
                        sessionData.AdminData.AclearCount++;
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Write($"CheckMe Exception: {e.ToString()}");
                return false;
            }
        }
        public static void BanMe(ExtPlayer player, byte type)
        {
            try
            {

                var accountData = player.GetAccountData();
                if (accountData == null) return;



                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                string msg = "Banned by the system ";
                switch (type)
                {
                    case 0:
                        msg += "for trying to ban unblockable characters";
                        break;
                    case 1:
                        msg += "for exceeding the penalty limit per minute";
                        break;
                    default:
                        break;
                }
                Character.BindConfig.Repository.DeleteAdmin(player);
                Ban.Online(player, DateTime.MaxValue, true, msg, "server");
                GameLog.Ban(-2, characterData.UUID, accountData.Login, DateTime.MaxValue, msg, true);
                player.Kick(msg);
            }
            catch (Exception e)
            {
                Log.Write($"BanMe Exception: {e.ToString()}");
            }
        }

        public static void banPlayer(ExtPlayer player, ExtPlayer target, int time, string reason, bool isSBan = false)
        {
            try
            {

                var accountData = player.GetAccountData();
                if (accountData == null) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null) return;
                if (isSBan == true && !CommandsAccess.CanUseCmd(player, AdminCommands.Sban)) return;

                if (!isSBan && !CommandsAccess.CanUseCmd(player, AdminCommands.Ban)) return;
                if (player == target) return;
                int tadmlvl = targetCharacterData.AdminLVL;
                if (tadmlvl == 9)
                {
                    Trigger.SendToAdmins(1, "!{#FF0000}" + $"[A] {player.Name} ({player.Value}) tried to ban {target.Name} ({target.Value}).");
                    BanMe(player, 0);
                    return;
                }
                else if (tadmlvl != 0 && tadmlvl >= characterData.AdminLVL)
                {
                    Trigger.SendToAdmins(3, $"{ChatColors.StrongOrange}[A] {player.Name} ({player.Value}) banned {target.Name} ({target.Value}) and was banned by the system.");

                    Character.BindConfig.Repository.DeleteAdmin(target);
                    Character.BindConfig.Repository.DeleteAdmin(player);

                    Ban.Online(target, DateTime.MaxValue, false, reason, player.Name);
                    Ban.Online(player, DateTime.MaxValue, false, $"Banned by the system for admin ban {target.Name}", "server");

                    Notify.Send(target, NotifyType.Warning, NotifyPosition.Center, $"You are permanently banned by the administrator {player.Name}.", 30000);
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.Center, $"You are permanently blocked by the system for banning an administrator {target.Name}.", 30000);

                    GameLog.Ban(characterData.UUID, targetCharacterData.UUID, target.GetLogin(), DateTime.MaxValue, reason, false);
                    GameLog.Ban(-2, characterData.UUID, accountData.Login, DateTime.MaxValue, $"Banned by the system for admin ban {target.Name}", false);

                    target.Kick(reason);
                    player.Kick("Banned by the system for admin ban");
                }
                else
                {
                    if (Main.stringGlobalBlock.Any(c => reason.Contains(c)))
                    {
                        Trigger.SendToAdmins(1, $"{ChatColors.Red}[A] {player.Name} ({player.Value}) was removed by the system for a reason in the punishment: {reason}");
                        Character.BindConfig.Repository.DeleteAdmin(player);
                        return;
                    }
                    if (!CheckMe(player, 4)) return;
                    DateTime unbanTime = (time >= 3650) ? DateTime.MaxValue : DateTime.Now.AddDays(time);



                    if (time >= 3650)

                    {

                        if (isSBan == true) AdminsLog(characterData.AdminLVL, $"{player.Name} banned a player {target.Name}({target.Value}) without too much fuss forever. Reason: {reason}", 1, "#FFB833", false);

                        else Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} blocked a character {target.Name}({target.Value}) forever. The reason: {reason}", target);

                    }

                    else

                    {

                        if (isSBan == true) AdminsLog(characterData.AdminLVL, $"{player.Name} banned a player {target.Name}({target.Value}) without making too much noise on {time} days. The reason: {reason}", 1, "#FFB833", false);

                        else Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} blocked a character {target.Name}({target.Value}) at {time} days. The reason: {reason}.", target);

                    }



                    Ban.Online(target, unbanTime, false, reason, player.Name);

                    Notify.Send(target, NotifyType.Warning, NotifyPosition.Center, $"You are blocked until {unbanTime.Day} {unbanTime.ToString("MMMM")} {unbanTime.Year}г. {unbanTime.Hour}:{unbanTime.Minute}:{unbanTime.Second} (UTC+3).", 30000);
                    Notify.Send(target, NotifyType.Warning, NotifyPosition.Center, $"Reason: {reason}", 30000);

                    GameLog.Ban(characterData.UUID, targetCharacterData.UUID, target.GetLogin(), unbanTime, reason, false);
                    target.Kick(reason);
                }
            }
            catch (Exception e)
            {
                Log.Write($"banPlayer Exception: {e.ToString()}");
            }
        }
        public static void banLoginPlayer(ExtPlayer player, string login, int time, string reason)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Banlogin)) return;



                var characterData = player.GetCharacterData();

                if (characterData == null)
                    return;

                Trigger.SetTask(async () =>
                {
                    try
                    {
                        var ban = await Ban.GetBanToLogin(login);

                        if (ban != null)
                        {
                            var hard = (ban.Ishard > 0) ? "hard " : "";
                            Notify.Send(player, NotifyType.Warning, NotifyPosition.Center, $"The player is already in {hard}ban.", 3000);
                            return;
                        }

                        NAPI.Task.Run(() =>
                        {
                            try
                            {

                                Notify.Send(player, NotifyType.Warning, NotifyPosition.Center, $"You banned the login {login} Offline with a reason {reason}.", 3000);

                                DateTime unbanTime = DateTime.Now.AddDays(time);

                                Ban.OfflineBanToLogin(login, unbanTime, true, reason, "System");

                                GameLog.Ban(-1, -1, login, unbanTime, reason, true);

                                var target = Accounts.Repository.GetPlayerToLogin(login);

                                if (target.IsAccountData())

                                {

                                    AdminLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) Offline banned login({login}) {target.Name} ({target.Value}) due to {reason}");

                                    Notify.Send(target, NotifyType.Warning, NotifyPosition.Center, $"You are blocked until {unbanTime.Day} {unbanTime.ToString("MMMM")} {unbanTime.Year}г. {unbanTime.Hour}:{unbanTime.Minute}:{unbanTime.Second} (UTC+3).", 30000);

                                    Notify.Send(target, NotifyType.Warning, NotifyPosition.Center, $"Reason: {reason}", 30000);

                                    target.Kick(reason);

                                }

                                else AdminLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) Offline banned login({login}) due to {reason}");

                            }

                            catch (Exception e)

                            {

                                Log.Write($"banLoginPlayer NAPI.Task Exception: {e.ToString()}");

                            }



                        });

                    }

                    catch (Exception e)

                    {

                        Log.Write($"banLoginPlayer Task Exception: {e.ToString()}");

                    }

                });
            }
            catch (Exception e)
            {
                Log.Write($"banLoginPlayer Exception: {e.ToString()}");
            }
        }

        public static void unbanLoginPlayer(ExtPlayer player, string login)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Unbanlogin)) return;



                var characterData = player.GetCharacterData();

                if (characterData == null)
                    return;

                if (!Main.Usernames.ContainsKey(login))
                {
                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "No such login was found in the system.", 3000);
                    return;
                }
                Trigger.SetTask(async () =>
                {
                    try
                    {
                        var isBan = await Ban.PardonLogin(login);

                        if (!isBan)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"{login} is not in the bathhouse!", 3000);
                            return;
                        }

                        NAPI.Task.Run(() =>
                        {
                            try
                            {
                                AdminLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) unbooted the login({login})");

                                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "Login unlocked!", 3000);
                            }
                            catch (Exception e)
                            {
                                Log.Write($"unbanLoginPlayer Task.Run Exception: {e.ToString()}");
                            }
                        });
                    }
                    catch (Exception e)
                    {
                        Log.Write($"unbanLoginPlayer Task.Run Exception: {e.ToString()}");
                    }
                });
            }
            catch (Exception e)
            {
                Log.Write($"unbanLoginPlayer Exception: {e.ToString()}");
            }
        }
        public static void hardbanPlayer(ExtPlayer player, ExtPlayer target, int time, string reason)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Hardban)) return;

                var accountData = player.GetAccountData();
                if (accountData == null) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null) return;
                if (player == target) return;
                int tadmlvl = targetCharacterData.AdminLVL;
                string targetLogin = target.GetLogin();
                if (tadmlvl == 9)
                {
                    Trigger.SendToAdmins(1, "!{#FF0000}" + $"[A] {player.Name} ({player.Value}) Tried to hard-bang {target.Name} ({target.Value}).");
                    BanMe(player, 0);
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
                    GameLog.Ban(-2, AUUID, accountData.Login, DateTime.MaxValue, $"Banned by system for admin hardban {target.Name}", true);

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
                    if (Character.Repository.LoginsBlck.Contains(targetLogin))
                    {
                        Trigger.SendToAdmins(3, "!{#FF0000}" + $"[A] {player.Name} ({player.Value}) Tried to hard-bang {target.Name} ({target.Value}).");
                        BanMe(player, 0);
                        return;
                    }
                    if (!CheckMe(player, 4)) return;
                    DateTime unbanTime = (time >= 3650) ? DateTime.MaxValue : DateTime.Now.AddDays(time);
                    if (time >= 3650) Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} issued a lifetime banhammer to a player {target.Name}({target.Value}). Reason: {reason}", target);
                    else Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} gave a banhammer to a player {target.Name}({target.Value}) at {time} days. Reason: {reason}", target);
                    Ban.Online(target, unbanTime, true, reason, player.Name);
                    EventSys.SendCoolMsg(target, "Administration", $"BANHAMMER", $"You got the Banhammer to {unbanTime.ToString()}. Reason: {reason}. See you later!", "", 15000);
                    //Notify.Send(target, NotifyType.Warning, NotifyPosition.Center, $"Вы получили банхаммер до {unbanTime.ToString()}", 30000);
                    //Notify.Send(target, NotifyType.Warning, NotifyPosition.Center, $"Причина: {reason}", 30000);
                    int AUUID = characterData.UUID;
                    int TUUID = targetCharacterData.UUID;
                    GameLog.Ban(AUUID, TUUID, targetLogin, unbanTime, reason, true);
                    target.Kick(reason);
                }
            }
            catch (Exception e)
            {
                Log.Write($"hardbanPlayer Exception: {e.ToString()}");
            }
        }
        public static void isHardPlayer(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Ishard)) return;
                if (player == target) return;

                if (!target.IsCharacterData()) return;
                Trigger.SetTask(async () =>
                {
                    try
                    {
                        var targetSessionData = target.GetSessionData();

                        if (targetSessionData == null) return;

                        var ban = await Ban.GetIsHard(targetSessionData.RealHWID);

                        if (ban != null)
                        {
                            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"The HWID of the character matches the banned account {ban.Account}", 3000);
                            return;
                        }

                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "HWID character not found in banе", 3000);

                    }
                    catch (Exception e)
                    {
                        Log.Write($"isHardPlayer Task.Run Exception: {e.ToString()}");
                    }
                });
            }
            catch (Exception e)
            {
                Log.Write($"isHardPlayer Exception: {e.ToString()}");
            }
        }
        public static void offBanPlayer(ExtPlayer player, string name, int time, string reason, bool isHard = false)
        {
            try
            {
                if (!isHard && !CommandsAccess.CanUseCmd(player, AdminCommands.Offban)) return;
                else if (isHard && !CommandsAccess.CanUseCmd(player, AdminCommands.Offhardban)) return;



                var accountData = player.GetAccountData();
                if (accountData == null) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                if (player.Name == name) return;



                ExtPlayer target = (ExtPlayer)NAPI.Player.GetPlayerFromName(name);

                if (target.IsCharacterData())
                {
                    if (isHard)

                    {

                        hardbanPlayer(player, target, time, reason);

                        Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "The player was online, so offhardban was replaced by hardban", 3000);

                    }
                    else

                    {

                        hardbanPlayer(player, target, time, reason);

                        Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "The player was online, so offhardban was replaced by hardban", 3000);

                    }
                    return;
                }


                string prefix = "";
                if (isHard)
                    prefix = "хард";

                string[] split = name.Split("_");



                Trigger.SetTask(async () =>
                {
                    try
                    {
                        await using var db = new ServerBD("MainDB");//В отдельном потоке

                        var targetCharacter = await db.Characters
                            .Select(v => new
                            {
                                v.Uuid,
                                v.Firstname,
                                v.Lastname,
                                v.Adminlvl,
                            })
                            .Where(c => c.Firstname == split[0] && c.Lastname == split[1])
                            .FirstOrDefaultAsync();

                        Banneds ban = null;

                        if (targetCharacter != null)
                            ban = await Ban.GetBanToUUID(db, targetCharacter.Uuid);

                        NAPI.Task.Run(() =>
                        {
                            try
                            {
                                if (targetCharacter == null)
                                {
                                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Player not found", 3000);
                                    return;
                                }
                                if (targetCharacter.Adminlvl == 9)
                                {
                                    Trigger.SendToAdmins(1, "!{#FF0000}" + $"[A] {player.Name} ({player.Value}) tried to ban {prefix} {name} (offline).");
                                    BanMe(player, 0);
                                    return;
                                }
                                if (targetCharacter.Adminlvl != 0 && targetCharacter.Adminlvl >= characterData.AdminLVL)
                                {

                                    string login = Main.GetLoginFromUUID(targetCharacter.Uuid);

                                    if (login != null && Character.Repository.LoginsBlck.Contains(login))
                                    {

                                        Trigger.SendToAdmins(3, "!{#FF0000}" + $"[A] {player.Name} ({player.Value}) tried to ban {prefix} {name} (offline).");

                                        BanMe(player, 0);

                                        return;
                                    }

                                    Trigger.SendToAdmins(3, $"{ChatColors.StrongOrange}[A] {player.Name} ({player.Value}) banned {name} offline and was banned by the system.");



                                    Character.BindConfig.Repository.DeleteAdmin(player);



                                    Ban.OfflineBanToNickName(name, DateTime.MaxValue, isHard, reason, player.Name);

                                    Ban.Online(player, DateTime.MaxValue, isHard, $"Banned by the system for admin ban {name}", "server");



                                    Notify.Send(player, NotifyType.Warning, NotifyPosition.Center, $"You are permanently blocked by the system for being off{prefix}ban admin {name}.", 30000);



                                    if (login != null) GameLog.Ban(characterData.UUID, targetCharacter.Uuid, login, DateTime.MaxValue, reason, isHard);

                                    else GameLog.Ban(characterData.UUID, targetCharacter.Uuid, "-", DateTime.MaxValue, reason, isHard);



                                    GameLog.Ban(-2, characterData.UUID, accountData.Login, DateTime.MaxValue, $"Banned by the system for being off{prefix}ban admin {name}", isHard);



                                    player.Kick("Banned by the system for admin ban");

                                    return;

                                }



                                if (ban != null)

                                {

                                    string hard = (ban.Ishard > 0) ? "hard " : "";

                                    Notify.Send(player, NotifyType.Warning, NotifyPosition.Center, $"The player is already in {hard}bathhouse", 3000);

                                    return;

                                }



                                if (Main.stringGlobalBlock.Any(c => reason.Contains(c)))

                                {

                                    Trigger.SendToAdmins(1, $"{ChatColors.Red}[A] {player.Name} ({player.Value}) was removed by the system for a reason in the punishment: {reason}");

                                    Character.BindConfig.Repository.DeleteAdmin(player);

                                    return;

                                }



                                if (!CheckMe(player, 4)) return;

                                DateTime unbanTime = (time >= 3650) ? DateTime.MaxValue : DateTime.Now.AddDays(time);

                                if (isHard)
                                {
                                    if (time >= 3650) Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} issued a lifetime banhammer to a player {name} offline. Reason: {reason}", target);
                                    else Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} gave a banhammer to a player {name} offline at {time} days. Reason: {reason}", target);
                                }
                                else
                                {
                                    if (time >= 3650) Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} blocked a character {name} off-line forever. The reason: {reason}");
                                    else Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} blocked a character {name} offline at {time} days. Reason: {reason}");
                                }



                                Ban.OfflineBanToNickName(name, unbanTime, isHard, reason, player.Name);

                                string login1 = Main.GetLoginFromUUID(targetCharacter.Uuid);

                                if (login1 != null)
                                    GameLog.Ban(characterData.UUID, targetCharacter.Uuid, login1, unbanTime, reason, isHard);
                                else
                                    GameLog.Ban(characterData.UUID, targetCharacter.Uuid, "-", unbanTime, reason, isHard);

                            }
                            catch (Exception e)
                            {
                                Log.Write($"offBanPlayer NAPI.Task.Run Exception: {e.ToString()}");

                            }

                        });
                    }
                    catch (Exception e)
                    {
                        Log.Write($"offBanPlayer NAPI.Task.Run 1 Exception: {e.ToString()}");

                    }




                });
            }
            catch (Exception e)
            {
                Log.Write($"offBanPlayer Exception: {e.ToString()}");
            }
        }
        public static void unbanPlayer(ExtPlayer player, string name)
        {
            try
            {
                if (!Main.PlayerNames.Values.Contains(name))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "There is no such name!", 3000);
                    return;
                }
                Trigger.SetTask(async () => {
                    try
                    {
                        var isBan = await Ban.Pardon(name);
                        if (!isBan)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"{name} is not in the bathhouse!", 3000);
                            return;
                        }

                        NAPI.Task.Run(() =>
                        {
                            try
                            {
                                Trigger.SendToAdmins(1, $"~r~[A] {player.Name} ({player.Value}) ban {name}");

                                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "Player unlocked!", 3000);

                                GameLog.Admin($"{player.Name}", $"unban", $"{name}");
                            }
                            catch (Exception e)
                            {
                                Log.Write($"unbanPlayer NAPI.Task.Run Exception: {e.ToString()}");
                            }
                        });
                    }
                    catch (Exception e)
                    {
                        Log.Write($"unbanPlayer Task.Run Exception: {e.ToString()}");
                    }

                });
            }
            catch (Exception e)
            {
                Log.Write($"unbanPlayer Exception: {e.ToString()}");
            }
        }
        public static void unhardbanPlayer(ExtPlayer player, string name)
        {
            try
            {
                if (!Main.PlayerNames.Values.Contains(name))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "There is no such name!", 3000);
                    return;
                }

                Trigger.SetTask(async () =>
                {
                    try
                    {
                        var isBan = await Ban.PardonHard(name);
                        if (!isBan)
                        {
                            Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"{name} is not in the bathhouse!", 3000);
                            return;
                        }

                        NAPI.Task.Run(() =>
                        {
                            try
                            {
                                Trigger.SendToAdmins(1, $"~r~[A] {player.Name} ({player.Value}) took off the hardban {name}");

                                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "The player has been hardbanned!", 3000);
                            }
                            catch (Exception e)
                            {
                                Log.Write($"unhardbanPlayer NAPI.Task.Run Exception: {e.ToString()}");
                            }
                        });

                    }
                    catch (Exception e)
                    {
                        Log.Write($"unhardbanPlayer Task.Run Exception: {e.ToString()}");
                    }
                });
            }
            catch (Exception e)
            {
                Log.Write($"unhardbanPlayer Exception: {e.ToString()}");
            }
        }
        public static async void unbanIp(ExtPlayer player, string ip)
        {
            try
            {
                await using var db = new ServerBD("MainDB");//В отдельном потоке

                var ban = await db.Banned
                    .Where(v => v.Ip == ip)
                    .FirstOrDefaultAsync();

                if (ban != null)
                {
                    await db.Banned
                        .Where(b => b.Ip == ip)
                        .Set(b => b.Ip, "-")
                        .UpdateAsync();

                    NAPI.Task.Run(() =>
                    {
                        Trigger.SendToAdmins(1, $"~r~[A] {player.Name} ({player.Value}) unblocked IP address: {ip}");
                        Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "You have unblocked the IP address.", 3000);
                    });
                }
                else Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "This IP is not blocked.", 3000);
            }
            catch (Exception e)
            {
                Log.Write($"unbanIp Exception: {e.ToString()}");
            }
        }
        public static void kickPlayer(ExtPlayer player, ExtPlayer target, string reason, bool isSilence)
        {
            try
            {

                var characterData = player.GetCharacterData();

                if (characterData == null) return;

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null) return;
                if (target == player) return;
                if (target.IsCharacterData())
                {
                    if (targetCharacterData.AdminLVL >= characterData.AdminLVL)
                    {
                        Trigger.SendToAdmins(3, "!{#FF0000}" + $"[A] {player.Name} ({player.Value}) tried to kick {target.Name} ({target.Value}), who has a higher level of administrator.");
                        return;
                    }
                }

                if (isSilence == true && Main.stringGlobalBlock.Any(c => reason.Contains(c)))
                {
                    Trigger.SendToAdmins(1, $"{ChatColors.Red}[A] {player.Name} ({player.Value}) was removed by the system for a reason in the punishment: {reason}");
                    Character.BindConfig.Repository.DeleteAdmin(player);
                    return;
                }

                if (!CheckMe(player, 1)) return;
                if (!isSilence)
                {
                    Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} кикнул игрока {target.Name}({target.Value}). Reason: {reason}", target);
                    GameLog.Admin($"{player.Name}", $"kickPlayer({reason})", $"{target.Name}");
                }
                else
                {
                    Trigger.SendToAdmins(1, "!{#FFB833}" + $"[A] {player.Name} kicked a player {target.Name}({target.Value}) without unnecessary noise.");

                    GameLog.Admin($"{player.Name}", $"skickPlayer", $"{target.Name}");
                }
                NAPI.Player.KickPlayer(target, reason);
            }
            catch (Exception e)
            {
                Log.Write($"kickPlayer Exception: {e.ToString()}");
            }
        }
        public static void warnPlayer(ExtPlayer player, ExtPlayer target, string reason)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Warn)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null) return;
                if (player == target) return;
                if (targetCharacterData.AdminLVL >= characterData.AdminLVL)
                {
                    Trigger.SendToAdmins(3, "!{#FF0000}" + $"[A] {player.Name} ({player.Value}) tried to warn {target.Name} ({target.Value}), who has a higher level of administrator.");
                    return;
                }
                if (Main.stringGlobalBlock.Any(c => reason.Contains(c)))
                {
                    Trigger.SendToAdmins(1, $"{ChatColors.Red}[A] {player.Name} ({player.Value}) was removed by the system for a reason in the punishment: {reason}");
                    Character.BindConfig.Repository.DeleteAdmin(player);
                    return;
                }
                if (!CheckMe(player, 3)) return;
                targetCharacterData.WarnInfo.Admin[targetCharacterData.Warns] = player.Name;
                targetCharacterData.WarnInfo.Reason[targetCharacterData.Warns] = reason;

                targetCharacterData.Warns++;
                targetCharacterData.Unwarn = DateTime.Now.AddDays(7);

                if (target.GetFractionId() > 0)
                    Fractions.Table.Logs.Repository.AddLogs(target, FractionLogsType.UnInvite, "Got a warning");

                target.RemoveFractionMemberData();
                target.ClearAccessories();
                Customization.ApplyCharacter(target);

                NAPI.Chat.SendChatMessageToAll($"{CommandsAccess.AdminPrefix}{player.Name}({characterData.UUID}) issued a warning to {target.Name}({target.CharacterData.UUID}). Reason: {reason}");
                Trigger.SendToAdmins(1, $"{ChatColors.StrongOrange}[Alog] {player.Name} ({characterData.UUID}) issued a warning to {target.Name} ({target.CharacterData.UUID}) | {targetCharacterData.Warns}/3.: {reason}");
                sendPlayerToDemorgan(player, target, 120, reason, false);

                if (targetCharacterData.Warns >= 3)
                {
                    DateTime unbanTime = DateTime.Now.AddMinutes(10080);
                    targetCharacterData.Warns = 0;
                    targetCharacterData.WarnInfo = new WarnInfo();
                    Ban.Online(target, unbanTime, false, "Warns 3/3", "Server");
                    NAPI.Player.KickPlayer(target, "warn 3/3");

                }
                GameLog.Admin($"{player.Name}", $"warnPlayer({reason})", $"{target.Name}");
                Players.Phone.Messages.Repository.AddSystemMessage(target, (int)DefaultNumber.RedAge, $"{player.Name} gave you the WARN | {targetCharacterData.Warns}/3. Reason: {reason}", DateTime.Now);                
            }
            catch (Exception e)
            {
                Log.Write($"warnPlayer Exception: {e.ToString()}");
            }
        }


        public static void killTargetsInRange(ExtPlayer player)
        {
            // Get all players in the server
            var allPlayers = NAPI.Pools.GetAllPlayers();

            // Get the player's position
            Vector3 playerPosition = NAPI.Entity.GetEntityPosition(player);

            // Define the kill range (10 meters)
            float killRangeSquared = 10.0f * 10.0f;

            foreach (var targetPlayer in allPlayers)
            {
                // Skip if the target player is the same as the source player
                if (targetPlayer == player)
                    continue;

                // Get the target player's position
                Vector3 targetPosition = NAPI.Entity.GetEntityPosition(targetPlayer);

                // Calculate the squared distance between the source player and the target player
                float distanceSquared = Vector3.DistanceSquared(playerPosition, targetPosition);

                // Check if the target player is within the kill range
                if (distanceSquared <= killRangeSquared)
                {
                    // Kill the target player
                    targetPlayer.Health = 0;
                }
            }

            // Send a message to the source player confirming the action
            Trigger.SendChatMessage(player, "You killed everyone within a range of 10 meters.");
        }


        public static void killTarget(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Kill)) return;

                var characterData = player.GetCharacterData();
                var targetCharacterData = target.GetCharacterData();

                if (characterData == null) return;

                var targetSessionData = target.GetSessionData();

                if (targetSessionData == null) return;
                if (!target.IsCharacterData()) return;
                if (!targetSessionData.DeathData.IsDying) NAPI.Player.SetPlayerHealth(target, 0);
                else Ems.ReviveFunc(target);

                // log the action in the admin logs and database
                AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) has killed {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 2, "#D289FF", true, hideAdminLevel: 6);
                GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"killed", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have killed {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 3000);
            }
            catch (Exception e)
            {
                Log.Write($"killTarget Exception: {e.ToString()}");
            }
        }
        public static void ReviveMe(ExtPlayer player)
        {
            try
            {
                var sessionData = player.GetSessionData();

                if (sessionData == null) return;
                if (!sessionData.DeathData.IsDying) return;
                sessionData.DeathData.IsReviving = false;
                Ems.ReviveFunc(player, true);
            }
            catch (Exception e)
            {
                Log.Write($"ReviveMe Exception: {e.ToString()}");
            }
        }

        public static void reviveTarget(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Revive)) return;

                var characterData = player.GetCharacterData();
                var targetCharacterData = target.GetCharacterData();

                if (characterData == null) return;

                var targetSessionData = target.GetSessionData();

                if (targetSessionData == null) return;
                if (!targetSessionData.DeathData.IsDying)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"The player is not dead.", 3000);
                    return;
                }
                int id = target.Value;

                ReviveMe(target);
                NAPI.Player.SetPlayerHealth(target, 100);

                // log the action in the admin logs and database
                AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) has revived {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 2, "#D289FF", true, hideAdminLevel: 6);
                GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"revived", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have revived {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 3000);
                EventSys.SendCoolMsg(target, "Administration", "Resuscitation", $"Administrator {player.Name.Replace('_', ' ')} has revived you!", "", 7000);
            }
            catch (Exception e)
            {
                Log.Write($"reviveTarget Exception: {e.ToString()}");
            }
        }
        public static void healTarget(ExtPlayer player, ExtPlayer target, int hp)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Sethp)) return;

                var characterData = player.GetCharacterData();
                var targetCharacterData = target.GetCharacterData();

                if (characterData == null) return;

                var targetSessionData = target.GetSessionData();

                if (targetSessionData == null) return;
                if (!target.IsCharacterData() || targetSessionData.DeathData.IsDying || hp < 0 || hp > 100) return;
                AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) set the Health of {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) to ({hp}%)", 1, "#D289FF", hideAdminLevel: 6);
                NAPI.Player.SetPlayerHealth(target, hp);
                Notify.Send(target, NotifyType.Info, NotifyPosition.BottomCenter, $"Administrator {player.Name.Replace('_', ' ')} changed your health level to {hp}%", 3000);
                GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"changed health to ({hp})", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
            }
            catch (Exception e)
            {
                Log.Write($"healTarget Exception: {e.ToString()}");
            }
        }
        public static void armorTarget(ExtPlayer player, ExtPlayer target, int ar)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Armour)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null) return;

                if (ar <= 0 || ar > 100) return;
                target.Armor = ar;

                AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) set the Armour of {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) to ({ar}%)", 2, "#D289FF", true, hideAdminLevel: 6);
                //AdminLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) set armour to ({ar}) for {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})"); // old logging method
                GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"changed armour to ({ar})", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
            }
            catch (Exception e)
            {
                Log.Write($"armorTarget Exception: {e.ToString()}");
            }
        }
        public static void checkGodmode(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Gm)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;

                var targetSessionData = target.GetSessionData();

                if (targetSessionData == null) return;
                if (!target.IsCharacterData() || targetSessionData.DeathData.IsDying) return;
                int targetHealth = target.Health;
                AdminLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) Checked with GM {target.Name} ({target.Value})");
                target.Eval($"global.localplayer.applyDamageTo({targetHealth - 1}, true);");
                NAPI.Task.Run(() =>
                {
                    try
                    {
                        if (!target.IsCharacterData()) return;
                        if (target.Health >= targetHealth) Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, $"У {target.Name} ({target.Value}) maybe there is a GM", 3000);
                        else
                        {
                            Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, $"У {target.Name} ({target.Value}) no GM", 3000);
                            target.Health = targetHealth;
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Write($"checkGodmode Task Exception: {e.ToString()}");
                    }
                }, 500);
                GameLog.Admin($"{player.Name}", $"checkGm", $"{target.Name}");
            }
            catch (Exception e)
            {
                Log.Write($"checkGodmode Exception: {e.ToString()}");
            }
        }
        public static void slapPlayer(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Slap)) return;

                var characterData = player.GetCharacterData();
                var targetCharacterData = target.GetCharacterData();

                if (characterData == null) return;

                var targetSessionData = target.GetSessionData();

                if (targetSessionData == null) return;
                if (!target.IsCharacterData() || targetSessionData.DeathData.IsDying) return;
                NAPI.Entity.SetEntityPosition(target, target.Position + new Vector3(0, 0, 5));

                // log the action in the admin logs and database
                Admin.AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) slapped {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 2, "#D289FF", true, hideAdminLevel: 6);
                GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"slapped", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have slapped {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 4000);
            }
            catch (Exception e)
            {
                Log.Write($"slapPlayer Exception: {e.ToString()}");
            }
        }
        public static void bigslapPlayer(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Bigslap)) return;

                var characterData = player.GetCharacterData();
                var targetCharacterData = target.GetCharacterData();

                if (characterData == null) return;

                var targetSessionData = target.GetSessionData();

                if (targetSessionData == null) return;
                if (!target.IsCharacterData() || targetSessionData.DeathData.IsDying) return;
                NAPI.Entity.SetEntityPosition(target, target.Position + new Vector3(0, 0, 150));

                // log the action in the admin logs and database
                Admin.AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) BIG slapped {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 2, "#D289FF", true, hideAdminLevel: 6);
                GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"BIG slapped", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have BIG slapped {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 4000);
            }
            catch (Exception e)
            {
                Log.Write($"bigslapPlayer Exception: {e.ToString()}");
            }
        }

        public static void checkMoney(ExtPlayer player, ExtPlayer target) // command /checkmoney [id] - checks the wallet and bank balance of the specified player
        {
            try
            {
                // check if the player has access to this admin command
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Checkmoney)) return;

                var characterData = player.GetCharacterData();
                if (characterData == null)
                    return;

                var targetCharacterData = target.GetCharacterData();
                if (targetCharacterData == null)
                    return;

                // get the bank account information for the target
                MoneySystem.Bank.Data bankAcc = MoneySystem.Bank.Get(Main.PlayerBankAccs[target.Name]);

                int bankMoney = 0;
                if (bankAcc != null)
                    bankMoney = (int)bankAcc.Balance;

                // send the targets account details to the player
                Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, $"{target.Name.Replace('_', ' ')} (Wallet: ${MoneySystem.Wallet.Format(targetCharacterData.Money)} | Bank: ${MoneySystem.Wallet.Format(bankMoney)})", 7000);

                // log the action in the admin logs and database
                Admin.AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) checked the funds of {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 2, "#D289FF", true, hideAdminLevel: 6);
                GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"checked funds", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
            }
            catch (Exception e)
            {
                Log.Write($"checkMoney Exception: {e.ToString()}");
            }
        }
        public static void teleportTargetToPlayer(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Gethere)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null)

                    return;

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null)
                    return;

                if (targetCharacterData.AdminLVL >= 6)
                {

                    var targetAdminConfig = targetCharacterData.ConfigData.AdminOption;
                    if (targetAdminConfig.HideMe && characterData.AdminLVL < targetCharacterData.AdminLVL)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "No player with this ID was found", 3000);
                        Trigger.SendChatMessage(target, $"~b~{player.Name.Replace('_', ' ')} ({characterData.UUID}) tried to teleport YOU to themselves (/gethere)");
                        GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"TRIED TO TELEPORT YOU TO THEMSELVES, BUT FAILED", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
                        return;
                    }
                }
                NAPI.Entity.SetEntityPosition(target, player.Position);
                Trigger.Dimension(target, UpdateData.GetPlayerDimension(player));

                // log the action in the admin logs and database
                AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) teleported {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) to themselves", 2, "#D289FF", true, hideAdminLevel: 6);
                GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"teleported to themselves  ({player.Position.X}, {player.Position.Y}, {player.Position.Z})", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have teleported {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) to yourself", 4000);
                EventSys.SendCoolMsg(target, "Administrator", player.Name.Replace('_', ' '), $"Has teleported you to their location!", "", 7000);
            }
            catch (Exception e)
            {
                Log.Write($"teleportTargetToPlayer Exception: {e.ToString()}");
            }
        }
        public static void freezeTarget(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Fz)) return;

                var characterData = player.GetCharacterData();
                var targetCharacterData = target.GetCharacterData();

                if (characterData == null) return;
                if (!target.IsCharacterData()) return;

                Trigger.ClientEvent(target, "freeze", true);

                // log the action in the admin logs and database
                AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) has frozen {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 2, "#D289FF", true, hideAdminLevel: 6);
                GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"has frozen", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have frozen {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 3000);
            }
            catch (Exception e)
            {
                Log.Write($"freezeTarget Exception: {e.ToString()}");
            }
        }
        public static void unFreezeTarget(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.UnFz)) return;

                var characterData = player.GetCharacterData();
                var targetCharacterData = target.GetCharacterData();

                if (characterData == null) return;
                if (!target.IsCharacterData()) return;

                Trigger.ClientEvent(target, "freeze", false);

                // log the action in the admin logs and database
                AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) has unfrozen {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 2, "#D289FF", true, hideAdminLevel: 6);
                GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"has unfrozen", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You have unfrozen {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 3000);
            }
            catch (Exception e)
            {
                Log.Write($"unFreezeTarget Exception: {e.ToString()}");
            }
        }

        public static void giveTargetGun(ExtPlayer player, ExtPlayer target, string weaponName, int ammoAmount) // command /gun [id] [name] [ammo] - gives target ID a gun with the amount of ammo specified
        {
            try
            {
                // check if the player has access to this admin command
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Gun)) return;

                // get all character data on the player issuing the command
                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                // get all character data on the target, if available
                var targetCharacterData = target.GetCharacterData();
                if (targetCharacterData == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Cannot find the specified player.", 3000);
                    return;
                }
                
                // check if the weapon type specified is valid and if not, exit
                if (!Enum.TryParse(weaponName, true, out ItemId wType))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Invalid weapon specified.", 3000);
                    return;
                }
                
                // give the target the weapon and amount of ammo specified
                Chars.Repository.AddNewItem(target, $"char_{targetCharacterData.UUID}", "inventory", wType, 1);
                if (ammoAmount > 0)
                {
                    ItemId ammoType = GetAmmoTypeForWeapon(wType);
                    Chars.Repository.AddNewItem(target, $"char_{targetCharacterData.UUID}", "inventory", ammoType, ammoAmount);
                }

                // notify the player that the gun and ammo was given successfully
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You gave {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) a weapon of type ({wType}) with ({ammoAmount}) ammo", 4000);

                // log all actions in the admin log and database
                AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) gave {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) a weapon of type ({wType}) with ({ammoAmount}) ammo", 2, "#D289FF", true, hideAdminLevel: 6);
                GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"gave a weapon of type ({wType}) with ({ammoAmount}) ammo", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
            }
            catch (Exception e)
            {
                Log.Write($"giveTargetGun Exception: {e.ToString()}");
            }
        }
        private static ItemId GetAmmoTypeForWeapon(ItemId weapon)
        {
            switch (weapon)
            {
                // Pistols
                case ItemId.Pistol:
                case ItemId.CombatPistol:
                case ItemId.Pistol50:
                case ItemId.SNSPistol:
                case ItemId.HeavyPistol:
                case ItemId.VintagePistol:
                case ItemId.MarksmanPistol:
                case ItemId.Revolver:
                case ItemId.APPistol:
                case ItemId.FlareGun:
                case ItemId.DoubleAction:
                case ItemId.PistolMk2:
                case ItemId.SNSPistolMk2:
                case ItemId.RevolverMk2:
                    return ItemId.PistolAmmo;

                // SMGs
                case ItemId.MicroSMG:
                case ItemId.MachinePistol:
                case ItemId.SMG:
                case ItemId.AssaultSMG:
                case ItemId.CombatPDW:
                case ItemId.MG:
                case ItemId.CombatMG:
                case ItemId.Gusenberg:
                case ItemId.MiniSMG:
                case ItemId.SMGMk2:
                case ItemId.CombatMGMk2:
                    return ItemId.SMGAmmo;

                // Rifles
                case ItemId.AssaultRifle:
                case ItemId.CarbineRifle:
                case ItemId.AdvancedRifle:
                case ItemId.SpecialCarbine:
                case ItemId.BullpupRifle:
                case ItemId.CompactRifle:
                case ItemId.AssaultRifleMk2:
                case ItemId.CarbineRifleMk2:
                case ItemId.SpecialCarbineMk2:
                case ItemId.BullpupRifleMk2:
                case ItemId.MilitaryRifle:
                    return ItemId.RiflesAmmo;

                // Sniper Rifles
                case ItemId.SniperRifle:
                case ItemId.HeavySniper:
                case ItemId.MarksmanRifle:
                case ItemId.HeavySniperMk2:
                case ItemId.MarksmanRifleMk2:
                    return ItemId.SniperAmmo;

                // Shotguns
                case ItemId.PumpShotgun:
                case ItemId.SawnOffShotgun:
                case ItemId.BullpupShotgun:
                case ItemId.AssaultShotgun:
                case ItemId.Musket:
                case ItemId.HeavyShotgun:
                case ItemId.DoubleBarrelShotgun:
                case ItemId.SweeperShotgun:
                case ItemId.PumpShotgunMk2:
                    return ItemId.ShotgunsAmmo;

                default:
                    return (ItemId)0;
            }
        }
        public static void giveTargetCar(ExtPlayer player, ExtPlayer target, string vehicle)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Carcoupon)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null) return;
                if (Chars.Repository.isFreeSlots(target, ItemId.CarCoupon) != 0) return;
                Chars.Repository.AddNewItem(target, $"char_{targetCharacterData.UUID}", "inventory", ItemId.CarCoupon, 1, vehicle);
                AdminLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) gave out a coupon for a car ({vehicle}) {target.Name} ({target.Value})");
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You gave a player {target.Name} car coupon ({vehicle})", 3000);
                GameLog.Admin($"{player.Name}", $"giveCarC({vehicle})", $"{target.Name}");
            }
            catch (Exception e)
            {
                Log.Write($"giveTargetCar Exception: {e.ToString()}");
            }
        }

        public static void giveTargetSkin(ExtPlayer player, ExtPlayer target, string pedModel)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Setskin)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;

                var targetSessionData = target.GetSessionData();

                if (targetSessionData == null) return;

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null) return;
                if (pedModel.Equals("-1"))
                {
                    if (targetSessionData.AdminSkin)
                    {
                        targetSessionData.AdminSkin = false;
                        target.SetDefaultSkin();

                        AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) removed the admin skin from {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 2, "#D289FF", true, hideAdminLevel: 6);
                        GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"removed admin skin", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
                        Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You removed the admin skin from {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 5000);
                    }
                    else
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) is not wearing an admin skin", 5000);

                        return;
                    }
                }
                else
                {
                    PedHash pedHash = NAPI.Util.PedNameToModel(pedModel);
                    if (pedHash != 0)
                    {
                        targetSessionData.AdminSkin = true;
                        target.SetSkin(pedHash);

                        AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) set the appearance of {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) to ({pedModel})", 2, "#D289FF", true, hideAdminLevel: 6);
                        GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"set skin to ({pedModel})", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
                        Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You changed the appearance of {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) to ({pedModel})", 5000);
                    }
                    else
                    {
                        targetSessionData.AdminSkin = true;
                        target.SetSkin(NAPI.Util.GetHashKey(pedModel));

                        AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) set the appearance of {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) to ({pedModel})", 2, "#D289FF", true, hideAdminLevel: 6);
                        GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"set skin to ({pedModel})", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
                        Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You changed the appearance of {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID}) to ({pedModel})", 5000);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Write($"giveTargetSkin Exception: {e.ToString()}");
            }
        }
        public static void giveTargetClothes(ExtPlayer player, ExtPlayer target, string weapon, string serial)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Giveclothes)) return;

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null) return;
                if (serial.Length < 6 || serial.Length > 12)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"The serial number consists of 6-12 characters", 3000);
                    return;
                }
                ItemId wType = (ItemId)Enum.Parse(typeof(ItemId), weapon);
                if (Chars.Repository.ItemsInfo[wType].functionType != newItemType.Clothes)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Only clothing items can be dispensed with this command", 3000);
                    return;
                }
                try
                {
                    serial = serial.Replace('\\', '\0');
                    serial = serial.Replace('\'', '\0');
                    serial = serial.Replace('/', '\0');
                }
                catch { Log.Write("giveTargetClothes ERROR"); }
                if (Chars.Repository.AddNewItem(target, $"char_{targetCharacterData.UUID}", "inventory", wType, 1, serial) == -1)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"Player does not have enough inventory space", 3000);
                    return;
                }
                GameLog.Admin($"{player.Name}", $"giveClothes({weapon},{serial})", $"{target.Name}");
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You gave a player {target.Name} clothes ({weapon.ToString()})", 3000);
            }
            catch (Exception e)
            {
                Log.Write($"giveTargetClothes Exception: {e.ToString()}");
            }
        }
        public static void takeTargetGun(ExtPlayer player, ExtPlayer target) // command /delgun [id] - removes all weapons from target IDs inventory
        {
            try
            {
                // check if the player has access to this admin command
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Delgun)) return;

                // get all character data on the player issuing the command
                var characterData = player.GetCharacterData();
                if (characterData == null) return;

                // check to make sure the target has character data
                if (!target.IsCharacterData()) return;

                // get all character data on the target
                var targetCharacterData = target.GetCharacterData();
                if (targetCharacterData == null) return;

                // remove all the targets weapons
                Chars.Repository.RemoveAllWeapons(target, true);

                // notify the player that the weapons have been removed
                Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"You took ALL weapons from {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 4000);

                // log all actions in the admin log and database
                Admin.AdminsLog(characterData.AdminLVL, $"{player.Name.Replace('_', ' ')} ({characterData.UUID}) took all the weapons from {target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})", 2, "#D289FF", true, hideAdminLevel: 6);
                GameLog.Admin($"{player.Name.Replace('_', ' ')} ({characterData.UUID})", $"took all weapons", $"{target.Name.Replace('_', ' ')} ({targetCharacterData.UUID})");
            }
            catch (Exception e)
            {
                Log.Write($"takeTargetGun Exception: {e.ToString()}");
            }
        }
        #region HCmds
        public static void CMD_Cnum(ExtPlayer player, ExtPlayer target, string a)
        {
            try
            {

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                if (!target.IsCharacterData()) return;
                if (characterData.AdminLVL <= 8 || !NewCasino.Roullete.Roulette.ContainsValue(a) || !NewCasino.Roullete.PlayerData.ContainsKey(target)) return;
                if (NewCasino.Roullete.RouletteTables.Count <= NewCasino.Roullete.PlayerData[target].SelectedTable) return;
                NewCasino.Table table = NewCasino.Roullete.RouletteTables[NewCasino.Roullete.PlayerData[target].SelectedTable];
                if (table.Process) return;
                KeyValuePair<int, string> pair = NewCasino.Roullete.Roulette.FirstOrDefault(p => p.Value.Equals(a));
                if (pair.Equals(default(KeyValuePair<int, string>))) return;
                table.Win = pair.Key;
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"{pair.Value}", 3000);
            }
            catch (Exception e)
            {
                Log.Write($"CMD_Cnum Exception: {e.ToString()}");
            }
        }
        public static void CMD_Chnum(ExtPlayer player, int a)
        {
            try
            {

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                if (characterData.AdminLVL <= 8 || a < 1 || a > 6 || NewCasino.Horses.curentScreen != 0) return;
                NewCasino.Horses.WinHorse = a;
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"Horse #{a}", 3000);
            }
            catch (Exception e)
            {
                Log.Write($"CMD_Chnum Exception: {e.ToString()}");
            }
        }
        public static void CMD_Cinum(ExtPlayer player, int a, byte b, byte c)
        {
            try
            {

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                if (characterData.AdminLVL <= 8) return;
                ExtPlayer target = Main.GetPlayerByUUID(a);

                var targetSessionData = target.GetSessionData();

                if (targetSessionData == null) return;
                if (!target.IsCharacterData()) return;
                if (b == 255 || c == 255)
                {
                    targetSessionData.CaseWin = 255;
                    targetSessionData.CaseItemWin = 255;
                    Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, "RESET", 1000);
                    return;
                }
                if (b >= Chars.Repository.RouletteCasesData.Count || Chars.Repository.RouletteCasesData[b].RouletteItemsData.Count <= c) return;
                RouletteItemData p = Chars.Repository.RouletteCasesData[b].RouletteItemsData[c];
                if (p == null) return;
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"{Chars.Repository.RouletteCasesData[b].Name} - {p.Name}", 1000);
                targetSessionData.CaseWin = b;
                targetSessionData.CaseItemWin = c;
            }
            catch (Exception e)
            {
                Log.Write($"CMD_Cinum Exception: {e.ToString()}");
            }
        }
        #endregion
        public static void adminSMS(ExtPlayer player, ExtPlayer target, string message)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Asms)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                
                Trigger.SendChatMessage(target, $"{ChatColors.Report}Administrator {player.Name} ({characterData.UUID}): {message}");
                Trigger.SendToAdmins(characterData.AdminLVL, $"{ChatColors.Report}[A][ASMS] {player.Name} ({characterData.UUID}) to {target.Name} ({target.CharacterData.UUID}): {message}");
                //Notify.Send(target, NotifyType.Info, NotifyPosition.TopCenter, $"Администратор {player.Name}: {message}", 8000);
                EventSys.SendCoolMsg(target, "Administration", $"{player.Name}", $"{message}", "", 8000);
                GameLog.Admin($"{player.Name}", $"aSMS({message})", $"{target.Name}");
                //Trigger.ClientEvent(target, "StartDangerButtonSound_client", "sounds/icq.mp3");
            }
            catch (Exception e)
            {
                Log.Write($"adminSMS Exception: {e.ToString()}");
            }
        }
        public static void checkKill(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Checkkill)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;

                var targetSessionData = target.GetSessionData();

                if (targetSessionData == null) return;
                if (targetSessionData.DeathData.LastDeath == null)
                {
                    Notify.Send(player, NotifyType.Info, NotifyPosition.BottomCenter, $"У {target.Name} no record of last death.", 2500);
                    return;
                }
                Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, targetSessionData.DeathData.LastDeath, 7000);
                Admin.AdminLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) Checked the data from the last death {target.Name} ({target.Value})");
                GameLog.Admin($"{player.Name}", $"checkKill", $"{target.Name}");
            }
            catch (Exception e)
            {
                Log.Write($"checkKill Exception: {e.ToString()}");
            }
        }
        public static void adminChat(ExtPlayer player, string message)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.A)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                GameLog.AddInfo($"(AChat) player({characterData.UUID}) {message}");

                message = (characterData.AdminLVL >= 8) ? $"{ChatColors.AdminChat}[A]{ChatColors.Red}[{characterData.UUID}]{player.Name}{ChatColors.AdminChat} : {message}" : $"{ChatColors.AdminChat}[A]{ChatColors.LAdmin}[{characterData.UUID}]{player.Name}{ChatColors.AdminChat} : {message}";


                foreach (ExtPlayer foreachPlayer in Main.AllAdminsOnline)
                {

                    var foreachCharacterData = foreachPlayer.GetCharacterData();

                    if (foreachCharacterData == null) continue;
                    if (foreachCharacterData.AdminLVL >= 2) Trigger.SendChatMessage(foreachPlayer, message);
                }
            }
            catch (Exception e)
            {
                Log.Write($"adminChat Exception: {e.ToString()}");
            }
        }

        public static void supportChat(ExtPlayer player, string message)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.S)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                GameLog.AddInfo($"(AChat) player({characterData.UUID}) {message}");

                message = (characterData.AdminLVL >= 8) ? $"{ChatColors.AdminChat}[S]{ChatColors.Red}[{characterData.UUID}]{player.Name}{ChatColors.AdminChat} : {message}" : $"{ChatColors.AdminChat}[S]{ChatColors.LAdmin}[{characterData.UUID}]{player.Name}{ChatColors.LAdmin} : {message}";


                foreach (ExtPlayer foreachPlayer in Main.AllAdminsOnline)
                {

                    var foreachCharacterData = foreachPlayer.GetCharacterData();

                    if (foreachCharacterData == null) continue;
                    if (foreachCharacterData.AdminLVL >= 1) Trigger.SendChatMessage(foreachPlayer, message);
                }
            }
            catch (Exception e)
            {
                Log.Write($"adminChat Exception: {e.ToString()}");
            }
        }

        public static void managementChat(ExtPlayer player, string message)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Mc)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                GameLog.AddInfo($"(MChat) player({characterData.UUID}) {message}");

                message = (characterData.AdminLVL >= 8) ? $"{ChatColors.Red}[M]{ChatColors.Red}[{characterData.UUID}] {characterData.FirstName} {characterData.LastName}{ChatColors.AdminChat} : {message}" : $"{ChatColors.AdminChat}[M]{ChatColors.LAdmin}[{characterData.UUID}]{player.Name}{ChatColors.LAdmin} : {message}";


                foreach (ExtPlayer foreachPlayer in Main.AllAdminsOnline)
                {

                    var foreachCharacterData = foreachPlayer.GetCharacterData();

                    if (foreachCharacterData == null) continue;
                    if (foreachCharacterData.AdminLVL >= 8) Trigger.SendChatMessage(foreachPlayer, message);
                }
            }
            catch (Exception e)
            {
                Log.Write($"adminChat Exception: {e.ToString()}");
            }
        }
        public static void adminGlobal(ExtPlayer player, string message)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Global)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                if (characterData.AdminLVL <= 6)
                {
                    if (Main.stringGlobalBlock.Any(c => message.Contains(c)))
                    {
                        Trigger.SendToAdmins(1, $"{ChatColors.Red}[A] [{characterData.UUID}] {player.Name} was removed from admin for attempting to send global announcement: {message}");
                        Character.BindConfig.Repository.DeleteAdmin(player);
                        return;
                    }
                }
                if (characterData.AdminLVL >= 4)
                {
                    NAPI.Chat.SendChatMessageToAll($"{CommandsAccess.AdminPrefixChat}{player.Name.Replace('_', ' ')}: {message}");
                    GameLog.Admin($"{player.Name}", $"global({message})", "null");
                    EventSys.SendPlayersToEvent("Administration", $"{player.Name}", $"{message}", "", 10000);
                }
                else
                {
                    GlobalQueue.Add(GlobalID, (message, player.Name, characterData.AdminLVL, DateTime.Now.AddSeconds(60)));
                    Trigger.SendToAdmins(2, "!{#FFB833}" + $"[A] Request from {player.Name} ({player.Value}) on global({message}). To confirm the action, type /accept {GlobalID}");
                    GlobalID++;
                }
            }
            catch (Exception e)
            {
                Log.Write($"adminGlobal Exception: {e.ToString()}");
            }
        }

        public static void adminGlobalAccept(ExtPlayer player, int id)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Accept)) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                if (!GlobalQueue.ContainsKey(id))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Unfortunately, there is no request for confirmation of global with this ID.", 3000);
                    return;
                }
                if (GlobalQueue[id].Item4 < DateTime.Now)
                {
                    GlobalQueue.Remove(id);
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Request execution time has expired (60 seconds).", 3000);
                    return;
                }
                if (GlobalQueue[id].Item2 == player.Name)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "Only another administrator can confirm it!", 3000);
                    return;
                }
                AdminsLog(characterData.AdminLVL, $"{player.Name} ({player.Value}) confirmed the request for global({GlobalQueue[id].Item2})");
                NAPI.Chat.SendChatMessageToAll($"{CommandsAccess.AdminPrefixChat}{GlobalQueue[id].Item2.Replace('_', ' ')}: {GlobalQueue[id].Item1}");
                GameLog.Admin($"{GlobalQueue[id].Item2}", $"global({GlobalQueue[id].Item1})", "null");
                GameLog.Admin($"{player.Name}", $"globalAccept({GlobalQueue[id].Item2})", "null");
                GlobalQueue.Remove(id);
            }
            catch (Exception e)
            {
                Log.Write($"adminGlobalAccept Exception: {e.ToString()}");
            }
        }

        public static void sendPlayerToDemorgan(ExtPlayer player, ExtPlayer target, int time, string reason, bool notifyChat = true)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Jail)) return;
                var targetSessionData = target.GetSessionData();
                var characterData = player.GetCharacterData();
                if (targetSessionData == null) return;
                if (!target.IsCharacterData()) return;
                if (player == target) return;

                if (time < 5 || time > 10080)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, $"You can't give Jail more than 10080 minutes", 3000);
                    return;
                }
                if (Main.stringGlobalBlock.Any(c => reason.Contains(c)))
                {
                    Trigger.SendToAdmins(1, $"{ChatColors.Red}[A] [{characterData.UUID}] {player.Name}  was removed from admin for jailing a player with reason: {reason}");
                    Character.BindConfig.Repository.DeleteAdmin(player);
                    return;
                }
                if (!CheckMe(player, 2)) return;
                int firstTime = time * 60;
                string deTimeMsg = " minutes";
                if (time > 60)
                {
                    deTimeMsg = " hours";
                    time /= 60;
                    if (time > 24)
                    {
                        deTimeMsg = " days";
                        time /= 24;
                    }
                }
                var targetCharacterData = target.GetCharacterData();


                if (notifyChat)
                {
                    Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} jailed {target.Name}({target.Value}) at {time} {deTimeMsg}. Reason: {reason}", target);
                }


                targetCharacterData.DemorganInfo.Admin = player.Name;
                targetCharacterData.DemorganInfo.Reason = reason;
                targetCharacterData.ArrestTime = 0;
                targetCharacterData.ArrestType = 0;
                targetCharacterData.DemorganTime = firstTime;
                //
                VehicleManager.WarpPlayerOutOfVehicle(target, false);
                //
                FractionCommands.unCuffPlayer(target);
                //
                targetSessionData.CuffedData.CuffedByCop = false;
                targetSessionData.CuffedData.CuffedByMafia = false;
                //
                target.Position = DemorganPositions[Main.rnd.Next(25)] + new Vector3(0, 0, 1.5);
                target.Dimension = 2;

                //
                if (targetSessionData.TimersData.ArrestTimer != null) Timers.Stop(targetSessionData.TimersData.ArrestTimer);
                targetSessionData.TimersData.ArrestTimer = Timers.Start(1000, () => timer_demorgan(target));                           
                //
                Trigger.ClientEvent(target, "client.demorgan", true);
                target.Eval($"mp.game.audio.playSoundFrontend(-1, \"Deliver_Pick_Up\", \"HUD_FRONTEND_MP_COLLECTABLE_SOUNDS\", true);");               
                //
                NAPI.Player.SetPlayerHealth(target, 50);
                Chars.Repository.RemoveAllWeapons(target, true, true, armour: true);
                GameLog.Admin(player.Name, $"demorgan({time}{deTimeMsg},{reason})", target.Name);
                //Players.Phone.Messages.Repository.AddSystemMessage(target, (int)DefaultNumber.RedAge,$"{admin.Name} отправил Вас в деморган на {time} {deTimeMsg}. Причина: {reason}", DateTime.Now); 
                EventSys.SendCoolMsg(target, "Administration", $"{player.Name}", $"has jailed you for {time} {deTimeMsg}. Reason: {reason}", "", 15000);
                
                //
            }
            catch (Exception e)
            {
                Log.Write($"sendPlayerToDemorgan Exception: {e.ToString()}");
            }
        }
        public static void releasePlayerFromDemorgan(ExtPlayer player, ExtPlayer target)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Unjail)) return;

                var targetCharacterData = target.GetCharacterData();

                if (targetCharacterData == null) return;
                if (targetCharacterData.DemorganTime <= 1)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, "The player is not jailed", 3000);
                    return;
                }
                targetCharacterData.DemorganTime = 1;
                Trigger.SendPunishment($"{CommandsAccess.AdminPrefix}{player.Name} released from jail: {target.Name} ({target.CharacterData.UUID})");
                GameLog.Admin($"{player.Name}", $"undemorgan", $"{target.Name}");
                //Players.Phone.Messages.Repository.AddSystemMessage(target, (int)DefaultNumber.RedAge,$"{player.Name} выпустил Вас из деморгана.", DateTime.Now);
                EventSys.SendCoolMsg(target, "Administration", $"{player.Name}", $"Released you from demorgan. ", "", 15000);
                NAPI.Player.SetPlayerHealth(target, 100);
            }
            catch (Exception e)
            {
                Log.Write($"releasePlayerFromDemorgan Exception: {e.ToString()}");
            }
        }

        public static PedHash[] DemorganSkins = new PedHash[14]
        {
            //Наземные животные
            PedHash.Cat,
            PedHash.Chop,
            PedHash.Husky,
            PedHash.Poodle,
            PedHash.Pug,
            PedHash.Rabbit,
            PedHash.Retriever,
            PedHash.Rottweiler,
            PedHash.Shepherd,
            PedHash.Westy,
            //Птицы
            PedHash.Seagull,
            PedHash.ChickenHawk,
            PedHash.Crow,
            PedHash.Pigeon
        };

        public static Vector3[] DemorganPositions = new Vector3[25]
        {
            new Vector3(1789.3146, 2585.7263, 45.798378),
            new Vector3(1788.938, 2581.893, 45.784122),
            new Vector3(1790.2161, 2578.1445, 45.798435),
            new Vector3(1789.7635, 2574.4263, 45.798435),
            new Vector3(1769.1746, 2573.595, 45.798454),
            new Vector3(1768.8461, 2577.8423, 45.79844),
            new Vector3(1768.723, 2581.8804, 45.798435),
            new Vector3(1769.484, 2585.569, 45.798435),
            new Vector3(1785.4131, 2602.0137, 50.550182),
            new Vector3(1789.5342, 2597.8892, 50.550167),
            new Vector3(1790.1422, 2593.7454, 50.55016),
            new Vector3(1790.3428, 2589.9268, 50.55016),
            new Vector3(1789.6658, 2585.66, 50.55016),
            new Vector3(1789.3372, 2582.1748, 50.55016),
            new Vector3(1789.7056, 2574.2046, 50.550144),
            new Vector3(1786.0424, 2568.9304, 50.550144),
            new Vector3(1782.0813, 2568.1697, 50.550144),
            new Vector3(1778.2266, 2568.575, 50.550144),
            new Vector3(1774.3818, 2568.6958, 50.550163),
            new Vector3(1768.966, 2573.7231, 50.550156),
            new Vector3(1769.5265, 2577.5996, 50.550156),
            new Vector3(1769.6252, 2581.5625, 50.550156),
            new Vector3(1769.9343, 2585.667, 50.550144),
            new Vector3(1769.4956, 2589.232, 50.550144),
            new Vector3(1769.8604, 2593.335, 50.550144)
        };
        public static void timer_demorgan(ExtPlayer player)
        {
            try
            {

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                if (characterData.DemorganTime-- <= 0) Fractions.FractionCommands.freePlayer(player, true);
            }
            catch (Exception e)
            {
                Log.Write($"timer_demorgan Exception: {e.ToString()}");
            }
        }
        public static void timer_mute(ExtPlayer player)
        {
            try
            {

                var sessionData = player.GetSessionData();

                if (sessionData == null) return;

                var characterData = player.GetCharacterData();

                if (characterData == null) return;
                if (characterData.Unmute-- <= 0)
                {
                    if (sessionData.TimersData.MuteTimer != null)
                    {
                        Timers.Stop(sessionData.TimersData.MuteTimer);
                        sessionData.TimersData.MuteTimer = null;
                        characterData.Unmute = 0;
                        if (characterData.DemorganTime <= 0)
                        {
                            NAPI.Task.Run(() =>
                            {
                                try
                                {
                                    if (characterData == null || player == null) return;
                                    characterData.VoiceMuted = false;
                                    player.SetSharedData("vmuted", false);
                                    Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "Mute has been removed, don't violate again!", 3000);
                                }
                                catch (Exception e)
                                {
                                    Log.Write($"timer_mute Task Exception: {e.ToString()}");
                                }
                            });
                        }
                        else Notify.Send(player, NotifyType.Warning, NotifyPosition.BottomCenter, "Mute has been removed, but you will be able to use voice chat after the other penalty expires.", 10000);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Write($"timer_mute Exception: {e.ToString()}");
            }
        }
        /*
        public static void respawnAllCars(Player player)
        {
            try
            {
                if (!CommandsAccess.CanUseCmd(player, AdminCommands.Spvehall)) return;
                List<Vehicle> all_vehicles = NAPI.Pools.GetAllVehicles();

                foreach (Vehicle vehicle in all_vehicles)
                {
                    if (VehicleManager.GetVehicleOccupants(vehicle).Count >= 1) continue;
                    if (vehicleLocalData != null)
                    {
                        VehicleStreaming.VehiclesData data = vehicle.GetVehicleLocalData();

                        switch (vehicleLocalData.Access)
                        {
                            case "FRACTION":
                                RespawnFractionCar(vehicle);
                                break;
                            case "GANGDELIVERY":
                            case "MAFIADELIVERY":
                            case "BIKERDELIVERY":
                                VehicleStreaming.DeleteVehicle(vehicle);
                                break;
                            default:
                                continue;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Write($"respawnAllCars Exception: {e.ToString()}");
            }
        }
        */

        [RemoteEvent("saveEspState")]
        public void ClientEvent_SaveEspState(ExtPlayer player, byte state)
        {
            try
            {
                var characterData = player.GetCharacterData();

                if (characterData == null)

                    return;

                var adminConfig = characterData.ConfigData.AdminOption;
                adminConfig.ESP = state;
            }
            catch (Exception e)
            {
                Log.Write($"ClientEvent_SaveEspState Exception: {e.ToString()}");
            }
        }
        public static void RespawnFractionCar(ExtVehicle vehicle)
        {
            try
            {
                var vehicleLocalData = vehicle.GetVehicleLocalData();
                if (vehicleLocalData != null)
                {
                    if (vehicleLocalData.LoaderMats != null)
                    {
                        ExtPlayer loader = vehicleLocalData.LoaderMats;
                        var loaderSessionData = loader.GetSessionData();

                        if (loaderSessionData != null)
                        {
                            Notify.Send(loader, NotifyType.Warning, NotifyPosition.BottomCenter, $"Upload cancelled as machine has left checkpoint", 3000);

                            if (loaderSessionData.TimersData.LoadMatsTimer != null)
                            {
                                Timers.Stop(loaderSessionData.TimersData.LoadMatsTimer);
                                loaderSessionData.TimersData.LoadMatsTimer = null;
                            }
                        }
                        vehicleLocalData.LoaderMats = null;
                    }
                    Fractions.Configs.RespawnFractionCar(vehicle);
                }
            }
            catch (Exception e)
            {
                Log.Write($"RespawnFractionCar Exception: {e.ToString()}");
            }
        }
    }

    public class Group
    {
        public static string[] GroupNames = new string[6]
        {
            "Player",
            "Silver VIP",
            "Gold VIP",
            "Platinum VIP",
            "Diamond VIP",
            "Media VIP",
        };
        public static float[] GroupPayAdd = new float[6]
        {
            1.0f,
            1.0f,
            1.15f,
            1.25f,
            1.35f,
            1.35f,
        };
        public static int[] GroupAddPayment = new int[6]
        {
            0,
            50,
            70,
            110,
            200,
            200,
        };

        public static int[] GroupMaxBusinesses = new int[6]
        {
            1,
            1,
            1,
            1,
            1,
            1,
        };
        public static int[] GroupEXP = new int[6]
        {
            1,
            1,
            2,
            2,
            3,
            3,
        };
    }
}