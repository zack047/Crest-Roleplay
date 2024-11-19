using System;
using System.Collections.Generic;
using GTANetworkAPI;
using NeptuneEvo.Handles;
using Newtonsoft.Json;
using NeptuneEvo.Core;
using Redage.SDK;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Database;
using LinqToDB;
using Localization;
using MySqlConnector;
using NeptuneEvo.Accounts;
using NeptuneEvo.Players.Models;
using NeptuneEvo.Players;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Character;
using NeptuneEvo.Chars.Models;
using NeptuneEvo.Functions;
using NeptuneEvo.Quests.Models;

namespace NeptuneEvo.Houses
{
    public class HouseFurniture
    {
        public string Name { get; }
        public string Model { get; }
        public int Id { get; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public bool IsSet { get; set; }

        [JsonIgnore]
        public GTANetworkAPI.Object obj { get; private set; }

        public HouseFurniture(int id, string name, string model)
        {
            Name = name;
            Model = model;
            Id = id;
            IsSet = false;
        }

        public GTANetworkAPI.Object Create(uint Dimension)
        {
            try
            {
                obj = NAPI.Object.CreateObject(NAPI.Util.GetHashKey(Model), Position, Rotation, 255, Dimension);
                Selecting.Objects.TryAdd(obj.Id, new Selecting.ObjData
                {
                    Type = (Name.Equals("Gun safe") ? "WeaponSafe" : 
                            Name.Equals("Clothes closet") ? "ClothesSafe" : 
                            Name.Equals("Burglary-proof safe") ? "BurglarProofSafe" :
                            Name.Equals("Filing cabinet") ? "SubjectSafe" :
                            "InteriorItem"),
                    entity = obj,

                });
                return obj;
            }
            catch (Exception e)
            {
                FurnitureManager.Log.Write($"Create Exception: {e.ToString()}");
                return null;
            }
        }
    }

    public class ShopFurnitureBuy
    {
        public string Prop { get; }
        public string Type { get; }
        public int Price;
        public Dictionary<ItemId, int> Items { get; }

        public ShopFurnitureBuy(string prop, string type, int price, Dictionary<ItemId, int> items)
        {
            Prop = prop;
            Type = type;
            Price = price;
            Items = items;
        }
    }
    class FurnitureManager : Script
    {
        public static readonly nLog Log = new nLog("Houses.HouseFurniture");
        public static Dictionary<int, Dictionary<int, HouseFurniture>> HouseFurnitures = new Dictionary<int, Dictionary<int, HouseFurniture>>();
        public static string QuestName = "npc_furniture";
        public static Vector3 FurnitureBuyPos = new Vector3(-591.12317, -285.2158, 35.45478);
        public static void Init()
        {
            try
            {
                using MySqlCommand cmd = new MySqlCommand
                {
                    CommandText = "SELECT * FROM `furniture`"
                };

                using DataTable result = MySQL.QueryRead(cmd);
                if (result == null || result.Rows.Count == 0)
                {
                    Log.Write("DB return null result.", nLog.Type.Warn);
                    return;
                }
                int id = 0;
                string furniture;
                foreach (DataRow Row in result.Rows)
                {
                    try
                    {
                        id = Convert.ToInt32(Row["uuid"].ToString());
                        furniture = Row["furniture"].ToString();
                        Dictionary<int, HouseFurniture> furnitures;
                        if (string.IsNullOrEmpty(furniture)) furnitures = new Dictionary<int, HouseFurniture>();
                        else furnitures = JsonConvert.DeserializeObject<Dictionary<int, HouseFurniture>>(furniture);
                        HouseFurnitures[id] = furnitures;
                    }
                    catch (Exception e)
                    {
                        Log.Write($"FurnitureManager Foreach Exception: {e.ToString()}");
                    }
                }
                Log.Write($"Loaded {HouseFurnitures.Count} players furnitures.", nLog.Type.Success);
                
                Main.CreateBlip(new Main.BlipData(566, "Furniture store",FurnitureBuyPos, 30, true));
                PedSystem.Repository.CreateQuest("s_m_y_airworker", FurnitureBuyPos, -64.57715f, questName: QuestName, title: "~y~NPC~w~ Ivan\nFurniture salesman", colShapeEnums: ColShapeEnums.FurnitureBuy);
            }
            catch (Exception e)
            {
                Log.Write($"FurnitureManager Exception: {e.ToString()}");
            }
        }
        [Interaction(ColShapeEnums.FurnitureBuy)]
        private static void Open(ExtPlayer player, int index)
        {
            var sessionData = player.GetSessionData();
            if (sessionData == null) return;
            if (!player.IsCharacterData()) return;
            if (sessionData.CuffedData.Cuffed)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.IsCuffed), 3000);
                return;
            }
            if (sessionData.DeathData.InDeath)
            {
                Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.IsDying), 3000);
                return;
            }
            if (Main.IHaveDemorgan(player, true)) return;

            player.SelectQuest(new PlayerQuestModel(QuestName, 0, 0, false, DateTime.Now));
            Trigger.ClientEvent(player, "client.quest.open", index, QuestName, 0, 0, 0);
        }
        public static Dictionary<string, ShopFurnitureBuy> NameModels = new Dictionary<string, ShopFurnitureBuy>()
		{
			{ "Gun safe", new ShopFurnitureBuy("prop_ld_int_safe_01", "Repositories", 0, new Dictionary<ItemId, int>()
				{
					{ ItemId.Iron, 200 },
					{ ItemId.Ruby, 5 },
					{ ItemId.Gold, 5 },
				}
			) },
			{ "Clothes closet", new ShopFurnitureBuy("bkr_prop_biker_garage_locker_01", "Repositories", 1, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodOak, 44 },
				{ ItemId.WoodMaple, 20 },
				{ ItemId.WoodPine, 10 },
			}) },
			{ "Filing cabinet", new ShopFurnitureBuy("hei_heist_bed_chestdrawer_04", "Repositories", 2, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodOak, 44 },
				{ ItemId.WoodMaple, 20 },
				{ ItemId.WoodPine, 10 },
			}) },
			{ "Burglary-proof safe", new ShopFurnitureBuy("p_secret_weapon_02", "Repositories", 3, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 1000 },
				{ ItemId.Gold, 200 },
				{ ItemId.Ruby, 70 },
			}) },

			{ "Ping Pong", new ShopFurnitureBuy("ch_prop_vault_painting_01a", "Paintings", 5, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodOak, 80 },
			}) },
			{ "Streamsniffers", new ShopFurnitureBuy("ch_prop_vault_painting_01b", "Paintings", 6, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodMaple, 40 },
			}) },
			{ "Factory", new ShopFurnitureBuy("ch_prop_vault_painting_01f", "Paintings", 7, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodPine, 25 },
			}) },
			{ "Negotiations", new ShopFurnitureBuy("ch_prop_vault_painting_01h", "Paintings", 8, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodOak, 80 },
			}) },
			{ "Girls.", new ShopFurnitureBuy("ch_prop_vault_painting_01j", "Paintings", 9, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodMaple, 40 },
			}) },

			{ "DAB", new ShopFurnitureBuy("vw_prop_casino_art_statue_01a", "Statues", 10, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodPine, 250 },
			}) },
			{ "Twerk", new ShopFurnitureBuy("vw_prop_casino_art_statue_02a", "Statues", 11, new Dictionary<ItemId, int>()
			{
				{ ItemId.Ruby, 150 },
			}) },
			{ "Nun", new ShopFurnitureBuy("vw_prop_casino_art_statue_04a", "Statues", 12, new Dictionary<ItemId, int>()
			{
				{ ItemId.Gold, 1000 },
			}) },

			{ "Paul Ridor", new ShopFurnitureBuy("hei_prop_drug_statue_01", "Figurines", 13, new Dictionary<ItemId, int>()
			{
				{ ItemId.Ruby, 7 },
			}) },
			{ "Oscar", new ShopFurnitureBuy("ex_prop_exec_award_gold", "Figurines", 14, new Dictionary<ItemId, int>()
			{
				{ ItemId.Emerald, 13 },
			}) },
			{ "Monkey King", new ShopFurnitureBuy("vw_prop_vw_pogo_gold_01a", "Figurines", 15, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },
			{ "Crash", new ShopFurnitureBuy("xs_prop_trophy_goldbag_01a", "Figurines", 16, new Dictionary<ItemId, int>()
			{
				{ ItemId.Ruby, 7 },
			}) },
			{ "Cup FIFA", new ShopFurnitureBuy("sum_prop_ac_wifaaward_01a", "Figurines", 17, new Dictionary<ItemId, int>()
			{
				{ ItemId.Emerald, 13 },
			}) },
			{ "Champagne", new ShopFurnitureBuy("xs_prop_trophy_champ_01a", "Figurines", 18, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },

			{ "Palm", new ShopFurnitureBuy("prop_fbibombplant", "Plants", 19, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodMaple, 27 },
			}) },
			{ "A small tree", new ShopFurnitureBuy("prop_plant_int_01a", "Plants", 20, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodPine, 20 },
			}) },
			{ "Round Tree", new ShopFurnitureBuy("prop_plant_int_02b", "Plants", 21, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodPine, 20 },
			}) },
			{ "Fern", new ShopFurnitureBuy("prop_plant_int_03b", "Plants", 22, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodOak, 60 },
			}) },
			{ "Money Tree", new ShopFurnitureBuy("prop_plant_int_04b", "Plants", 23, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodMaple, 40 },
			}) },
			{ "Cactus", new ShopFurnitureBuy("vw_prop_casino_art_plant_12a", "Plants", 24, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodMaple, 27 },
			}) },

			{ "Christmas Tree", new ShopFurnitureBuy("prop_xmas_tree_int", "Christmas trees", 25, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodPine, 60 },
			}) },
			{ "The Diamond Tree", new ShopFurnitureBuy("ch_prop_ch_diamond_xmastree", "Christmas trees", 26, new Dictionary<ItemId, int>()
			{
				{ ItemId.Ruby, 150 },
			}) },

			{ "Counting machine", new ShopFurnitureBuy("bkr_prop_money_counter", "Jewels", 27, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 200 },
			}) },
			{ "Money Mountain", new ShopFurnitureBuy("bkr_prop_moneypack_03a", "Jewels", 28, new Dictionary<ItemId, int>()
			{
				{ ItemId.Gold, 1000 },
			}) },
			{ "Mountain of money", new ShopFurnitureBuy("ba_prop_battle_moneypack_02a", "Jewels", 29, new Dictionary<ItemId, int>()
			{
				{ ItemId.Gold, 3000 },
			}) },
			{ "Money Box", new ShopFurnitureBuy("ex_prop_crate_money_bc", "Jewels", 30, new Dictionary<ItemId, int>()
			{
				{ ItemId.Gold, 5000 },
			}) },
			{ "Box of gold", new ShopFurnitureBuy("prop_ld_gold_chest", "Jewels", 31, new Dictionary<ItemId, int>()
			{
				{ ItemId.Gold, 300 },
			}) },
			{ "Trolley with gold", new ShopFurnitureBuy("p_large_gold_s", "Jewels", 32, new Dictionary<ItemId, int>()
			{
				{ ItemId.Gold, 10000 },
			}) },
			{ "Case with money", new ShopFurnitureBuy("prop_cash_case_02", "Jewels", 33, new Dictionary<ItemId, int>()
			{
				{ ItemId.Gold, 1700 },
			}) },

			{ "Case of beer", new ShopFurnitureBuy("hei_heist_cs_beer_box", "Alcohol", 34, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 50 },
			}) },
			{ "Romantic Set", new ShopFurnitureBuy("ba_prop_club_champset", "Alcohol", 35, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodOak, 110 },
			}) },

			{ "Figured", new ShopFurnitureBuy("vw_prop_casino_art_vase_08a", "Vases", 36, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },
			{ "Jug", new ShopFurnitureBuy("vw_prop_casino_art_vase_08a", "Vases", 37, new Dictionary<ItemId, int>()
			{
				{ ItemId.Emerald, 13 },
			}) },
			{ "Stuffed", new ShopFurnitureBuy("vw_prop_casino_art_vase_05a", "Vases", 38, new Dictionary<ItemId, int>()
			{
				{ ItemId.Ruby, 7 },
			}) },
			{ "Symmetrical", new ShopFurnitureBuy("apa_mp_h_acc_vase_06", "Vases", 39, new Dictionary<ItemId, int>()
			{
				{ ItemId.WoodOak, 40 },
			}) },
			{ "Mirror", new ShopFurnitureBuy("apa_mp_h_acc_vase_05", "Vases", 40, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },
			/*{ "цветок", new ShopFurnitureBuy("apa_mp_h_acc_plant_tall_01", "Vases", 41, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },
			{ "цветок", new ShopFurnitureBuy("prop_fbibombplant", "Vases", 40, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },
			{ "цветок", new ShopFurnitureBuy("prop_fbibombplant", "Vases", 40, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },
			{ "свечи", new ShopFurnitureBuy("apa_mp_h_acc_candles_02", "Vases", 40, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },
			{ "маска", new ShopFurnitureBuy("apa_mp_h_acc_dec_head_01", "Vases", 40, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },
			{ "пива", new ShopFurnitureBuy("beerrow_local", "Vases", 40, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },
			{ "дом кинотеатр", new ShopFurnitureBuy("hei_heist_str_avunitl_03", "Vases", 40, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },
			{ "дом кинотеатр", new ShopFurnitureBuy("apa_mp_h_str_avunitl_01_b", "Vases", 40, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },
			{ "статуя голова", new ShopFurnitureBuy("hei_prop_hei_bust_01", "Vases", 40, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },
			{ "статуя оружие", new ShopFurnitureBuy("ch_prop_ch_trophy_gunner_01a", "Vases", 40, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },
			{ "склад для оружия", new ShopFurnitureBuy("bkr_prop_gunlocker_01a", "Vases", 40, new Dictionary<ItemId, int>()
			{
				{ ItemId.Iron, 100 },
			}) },*/
		};
        public static async Task Save(ServerBD db, int houseId)
        {
            try
            {
	            if (HouseFurnitures.ContainsKey(houseId))
	            {
		            await db.Furniture
			            .Where(f => f.Uuid == houseId)
			            .Set(f => f.Furniture, JsonConvert.SerializeObject(HouseFurnitures[houseId]))
			            .UpdateAsync();
	            }
            }
            catch (Exception e)
            {
                Log.Write($"Save Exception: {e.ToString()}");
            }
        }
        public static void Create(int id)
        {
            try
            {
                if (!HouseFurnitures.ContainsKey(id))
                {
                    using MySqlCommand cmd = new MySqlCommand
                    {
                        CommandText = "INSERT INTO `furniture`(`uuid`,`furniture`,`access`) VALUES (@val0,@val1,@val3)"
                    };
                    cmd.Parameters.AddWithValue("@val0", id);
                    cmd.Parameters.AddWithValue("@val1", JsonConvert.SerializeObject(new Dictionary<int, HouseFurniture>()));
                    cmd.Parameters.AddWithValue("@val3", JsonConvert.SerializeObject(new List<string>()));
                    MySQL.Query(cmd);
                }
            }
            catch (Exception e)
            {
                Log.Write($"Create Exception: {e.ToString()}");
            }
        }

        public static void NewFurniture(int id, string name)
        {
            try
            {
                if (!HouseFurnitures.ContainsKey(id)) 
                    Create(id);
                
                var houseFurniture = HouseFurnitures[id];
                
                int i = 0;
                while (houseFurniture.ContainsKey(i)) 
                    i++;
                
                var furn = new HouseFurniture(i, name, NameModels[name].Prop);
                houseFurniture.Add(i, furn);
                
                if (NameModels[name].Type.Equals("Repositories")) 
                    Chars.Repository.RemoveAll($"furniture_{id}_{i}"); //оставалось видимо в хранилище, тестануть так
            }
            catch (Exception e)
            {
                Log.Write($"newFurniture Exception: {e.ToString()}");
            }
        }

        [RemoteEvent("acceptEdit")]
        public void ClientEvent_acceptEdit(ExtPlayer player, float X, float Y, float Z, float XX, float YY, float ZZ)
        {
            try
            {
                var sessionData = player.GetSessionData();
                if (sessionData == null) return;
                if (!player.IsCharacterData()) return;
                if (!sessionData.HouseData.Editing) return;
                sessionData.HouseData.Editing = false;
                var house = HouseManager.GetHouse(player, true);
                if (house == null)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.NoHome), 3000);
                    return;
                }
                Vector3 pos = new Vector3(X, Y, Z);
                if (player.Position.DistanceTo(pos) >= 6f)
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.MebelDomTooFar), 5000);
                    return;
                }
                if (!HouseFurnitures.ContainsKey(house.ID))
                {
                    Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.MebelError), 5000);
                    return;
                }

                var furnitures = HouseFurnitures[house.ID];
                foreach (HouseFurniture p in furnitures.Values)
                {
                    if (p != null && p.IsSet && p.Position != null && p.Position.DistanceTo(pos) <= 0.5f)
                    {
                        Notify.Send(player, NotifyType.Error, NotifyPosition.BottomCenter, LangFunc.GetText(LangType.Ru, DataName.MebelTooNear), 3000);
                        return;
                    }
                }
                int id = sessionData.HouseData.EditID;
                furnitures[id].IsSet = true;
                Vector3 rot = new Vector3(XX, YY, ZZ);
                furnitures[id].Position = pos;
                furnitures[id].Rotation = rot;
                house.DestroyFurnitures();
                house.CreateAllFurnitures();
                house.IsFurnitureSave = true;
            }
            catch (Exception e)
            {
                Log.Write($"ClientEvent_acceptEdit Exception: {e.ToString()}");
            }
        }

        [RemoteEvent("cancelEdit")]
        public void ClientEvent_cancelEdit(ExtPlayer player)
        {
            try
            {
                var sessionData = player.GetSessionData();
                if (sessionData == null) return;
                sessionData.HouseData.Editing = false;
            }
            catch (Exception e)
            {
                Log.Write($"ClientEvent_cancelEdit Exception: {e.ToString()}");
            }
        }
    }
}
