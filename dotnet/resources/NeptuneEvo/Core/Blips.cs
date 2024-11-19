using System;
using GTANetworkAPI;
using Localization;
using NeptuneEvo.Handles;
using NeptuneEvo.Accounts;
using NeptuneEvo.Players.Models;
using NeptuneEvo.Players;
using NeptuneEvo.Character.Models;
using NeptuneEvo.Character;
using NeptuneEvo.Functions;
using NeptuneEvo.GUI;
using NeptuneEvo.Players.Popup.List.Models;
using NeptuneEvo.Quests;
using NeptuneEvo.VehicleData.LocalData;
using NeptuneEvo.VehicleData.LocalData.Models;
using Redage.SDK;

namespace NeptuneEvo.Core
{
    class Blips : Script
    {
        private static readonly nLog Log = new nLog("Core.Blips");

        [ServerEvent(Event.ResourceStart)]
        public void Init()
        {
            try
            {
                //State Blips
                Main.CreateBlip(new Main.BlipData(478, "Post Office", new Vector3(133.16599, 96.30163, 83.5), 32, true));
                Main.CreateBlip(new Main.BlipData(525, "Employment Center", new Vector3(436.5074, -627.4617, 28.707539), 0, true));
                Main.CreateBlip(new Main.BlipData(526, "SAHP", new Vector3(-443.91403, 6008.21, 40.47677), 71, true));
                Main.CreateBlip(new Main.BlipData(526, "SAHP Sandy", new Vector3(1850.4873, 3689.5474, 39.06812), 71, true));
                Main.CreateBlip(new Main.BlipData(188, "Prison", new Vector3(1819.3936, 2592.6497, 64.19961), 62, true, 1.5f));

                //Gang Blips
                Main.CreateBlip(new Main.BlipData(437, "Bloods", new Vector3(498.49777, -1523.8828, 29.289001), 1, true));
                Main.CreateBlip(new Main.BlipData(437, "Vagos", new Vector3(1364.3604, -1508.2136, 57.675503), 5, true));
                Main.CreateBlip(new Main.BlipData(437, "Marabunta", new Vector3(968.8027, -2199.605, 39.712364), 3, true));
                Main.CreateBlip(new Main.BlipData(437, "Ballas", new Vector3(99.5739, -1932.9452, 20.803705), 7, true));
                Main.CreateBlip(new Main.BlipData(437, "Families", new Vector3(-196.0793, -1625.5515, 33.813244), 25, true));

                //Mafia Blips
                Main.CreateBlip(new Main.BlipData(679, "La Cosa Nostra", new Vector3(1387.341, 1156.044, 114.3335), 4, true));
                Main.CreateBlip(new Main.BlipData(679, "Russian Mafia", new Vector3(-121.650406, 987.01324, 235.74655), 4, true));
                Main.CreateBlip(new Main.BlipData(679, "Yakuza", new Vector3(-1462.3617, -31.557522, 54.646114), 4, true));
                Main.CreateBlip(new Main.BlipData(679, "Armenian Mafia", new Vector3(-1809.7378, 444.31104, 128.48253), 4, true));

                // Club Blips
                Main.CreateBlip(new Main.BlipData(661, "Lost MC", new Vector3(966.1703, -131.52985, 74.353165), 21, true));

                // NPC Blips
                Main.CreateBlip(new Main.BlipData(94, "AirDrop Informant", new Vector3(-54.35258, -1213.5707, 28.684637), 52, true));
                Main.CreateBlip(new Main.BlipData(491, LangFunc.GetText(LangType.Ru, DataName.Events), new Vector3(-265.74036, -2017.554, 30.145578), 6, true)); // arena referee


                // Mining Blips
                Main.CreateBlip(new Main.BlipData(527, "Mine", new Vector3(1919.3615, 3281.5898, 50.12463), 4, true) { color = 2 });
                Main.CreateBlip(new Main.BlipData(527, "Mine", new Vector3(1943.94, 3225.8254, 52.625126), 4, true) { color = 2 });
                Main.CreateBlip(new Main.BlipData(527, "Mine", new Vector3(2004.0458, 3280.0454, 53.13491), 4, true) { color = 2 });
                Main.CreateBlip(new Main.BlipData(527, "Mine", new Vector3(2119.7205, 3300.3992, 54.486687), 4, true) { color = 2 });
                Main.CreateBlip(new Main.BlipData(527, "Mine", new Vector3(2213.1575, 3191.8555, 59.082912), 4, true) { color = 2 });
                Main.CreateBlip(new Main.BlipData(527, "Mine", new Vector3(1979.2334, 3485.2764, 59.082912), 4, true) { color = 2 });
                Main.CreateBlip(new Main.BlipData(527, "Mine", new Vector3(2001.4651, 3563.8215, 48.753044), 4, true) { color = 2 });
                Main.CreateBlip(new Main.BlipData(527, "Mine", new Vector3(1909.8771, 3481.2292, 62.54292), 4, true) { color = 2 });
                Main.CreateBlip(new Main.BlipData(527, "Mine", new Vector3(1862.2681, 3385.0261, 48.22948), 4, true) { color = 2 });
                Main.CreateBlip(new Main.BlipData(527, "Mine", new Vector3(1309.7509, 3002.2893, 54.068874), 4, true) { color = 2 });
                Main.CreateBlip(new Main.BlipData(527, "Mine", new Vector3(1680.4172, 3174.1406, 53.16145), 4, true) { color = 2 });

                // Other Blips
                Main.CreateBlip(new Main.BlipData(681, "Casino", new Vector3(936.0022, 47.08651, 81.08987), 18, true));
            }
            catch (Exception e)
            {
                Log.Write($"StartWork Exception: {e.ToString()}");
            }
        }
    }
}