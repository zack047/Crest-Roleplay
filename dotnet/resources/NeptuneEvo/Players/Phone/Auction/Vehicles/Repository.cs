using System;
using System.Linq;
using Database;
using GTANetworkAPI;
using LinqToDB;
using NeptuneEvo.Core;
using NeptuneEvo.Players.Phone.Auction.Models;
using NeptuneEvo.Players.Phone.Messages.Models;

namespace NeptuneEvo.Players.Phone.Auction.Vehicles
{
    public class Repository
    {
        public static void SetOwner(AuctionData auctionData)
        {
            var uuid = -1;
            var playerName = String.Empty;

            if (Main.PlayerNames.ContainsKey(auctionData.LastBetUUID))
            {
                uuid = auctionData.LastBetUUID;
                playerName = Main.PlayerNames[uuid];   
                Messages.Repository.AddSystemMessageToUuid(auctionData.LastBetUUID, (int) DefaultNumber.Auction, $"Your bid won the auction. Lot {auctionData.Title} added to your possessions.", DateTime.Now);
                Messages.Repository.AddSystemMessageToUuid(auctionData.CreateUUID, (int) DefaultNumber.Auction, $"Your bid won the auction. Lot {auctionData.Title} added to your possessions.", DateTime.Now);
                Auction.Repository.ReturnMoney(auctionData.CreateUUID, auctionData.LastPrice, auctionData.Id, auctionData.Type, $"Your lot {auctionData.Title} purchased for {auctionData.LastPrice}. The money is credited to the account.");
            }
            else if (Main.PlayerNames.ContainsKey(auctionData.CreateUUID))
            {
                uuid = auctionData.CreateUUID;
                playerName = Main.PlayerNames[uuid];
                Messages.Repository.AddSystemMessageToUuid(uuid, (int) DefaultNumber.Auction, $"Auction cancelled. Lot {auctionData.Title} Retrieved.", DateTime.Now);
            }
            
            
            if (uuid != -1 && playerName != String.Empty)
            {
                var vehicleData = VehicleManager.Vehicles.Values
                    .FirstOrDefault(v => v.SqlId == auctionData.ElementId);


                if (vehicleData != null)
                {
                    vehicleData.Holder = playerName;
                    VehicleManager.SaveHolder(vehicleData.Number);
                }
                
                //var player = Main.GetPlayerByUUID(uuid);
            }
        }
    }
}