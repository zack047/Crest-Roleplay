using GTANetworkAPI;
using NeptuneEvo.Handles;
using NeptuneEvo.Accounts;
using Redage.SDK;
using System;
using System.Threading.Tasks;
using NeptuneEvo.Accounts.Models;
using NeptuneEvo.Character.Models;

namespace NeptuneEvo.Character.Load
{
    class Events : Script
    {
        private static readonly nLog Log = new nLog("Core.Character.Load");

        [RemoteEvent("selectchar")]
        public void ClientEvent_selectCharacter(ExtPlayer player, int uuid, int spawnid)
        {
            try
            {
                player.SetSharedData("DENI_UUID2", uuid);
                Main.PlayerUUIDToPlayerId[uuid] = player.Value;
                var accountData = player.GetAccountData();
                if (accountData == null) return;
                if (!accountData.Chars.Contains(uuid)) return;
                Log.Write($"{player.Name}({uuid}) select char - spawnid({spawnid})");

                accountData.LastSelectCharUUID = uuid;
                
                Trigger.SetTask(async () =>
                {
                    await Repository.Load(player, uuid, spawnid);

                });
            }
            catch (Exception e)
            {
                Log.Write($"ClientEvent_selectCharacter Exception: {e.ToString()}");
            }
        }
    }
}
