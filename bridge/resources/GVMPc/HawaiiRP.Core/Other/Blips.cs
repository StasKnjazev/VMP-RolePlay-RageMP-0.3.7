using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Fraktionen;

namespace GVMPc.Other
{
    class Blips : Script
    {
        public bool BlipSetted = false;

        [ServerEvent(Event.ResourceStart)]
        public void ResourceStart()
        {
            if (BlipSetted == false)
            {
                BlipSetted = true;
                NAPI.Blip.CreateBlip(680, new Vector3(924.71, 47.01, 81.11), 1.0f, 0, "The Diamond Casino & Resort", 255, 0, true, 0, 0);
                NAPI.Blip.CreateBlip(468, new Vector3(356.1, -596.33, 27.77), 1.0f, 0, "Schönheitsklinik", 255, 0, true, 0, 0);
				//NAPI.Blip.CreateBlip(467, new Vector3(-621.0187, -1639.95, 25.25427), 1f, 2, "Müllstelle", 255, 0, true, 0, 0);
				//NAPI.Blip.CreateBlip(467, new Vector3(-594.6404, 2083.241, 130.2929), 1f, 2, "Mine", 255, 0, true, 0, 0);
				NAPI.Blip.CreateBlip(477, new Vector3(947.2971, -1250.208, 25.97585), 1f, 5, "LKW-Verleih", 255, 0, true, 0, 0);
				NAPI.Blip.CreateBlip(477, new Vector3(581.0804, -2286.703, 5.29065), 1f, 0, "Öl-Verarbeiter", 255, 0, true, 0, 0);
				NAPI.Blip.CreateBlip(93, new Vector3(114.5197, -1038.47, 28.19192), 1f, 0, "Cafe", 255, 0, true, 0, 0);
				NAPI.Blip.CreateBlip(108, new Vector3(237.3552, 217.8413, 105.1867), 1f, 2, "Staatsbank", 255, 0, true, 0, 0);
				NAPI.Blip.CreateBlip(108, new Vector3(149.3795, -1040.539, 28.27408), 1f, 2, "Fleeca Bank", 255, 0, true, 0, 0);
				NAPI.Blip.CreateBlip(108, new Vector3(-2962.564, 482.1318, 14.60309), 1f, 2, "Fleeca Bank", 255, 0, true, 0, 0);
				NAPI.Blip.CreateBlip(108, new Vector3(-351.22, -49.80474, 47.94256), 1f, 2, "Fleeca Bank", 255, 0, true, 0, 0);
				NAPI.Blip.CreateBlip(153, new Vector3(299.2475, -584.5608, 42.16089), 1f, 1, "Los Santos Medical Department", 255, 0, true, 0, 0);
				NAPI.Blip.CreateBlip(584, new Vector3(-1037.724, -2737.698, 19.06925), 1f, 0, "Airport", 255, 0, true, 0, 0);

				ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(356.1, -596.33, 27.77), 1.4f, 1.4f, 0);
                val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openSchönheitsklinik"));
                val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um die Schönheitsklinik zu öffnen.", "SCHÖNHEITSKLINIK", "white", 5000));

            }
        }

        [RemoteEvent("openSchönheitsklinik")]
        public void openSchönheitsklinik(Client p)
        {
            try
            {
                p.TriggerEvent("openWindow", new object[] { "CharacterCreator", "{\"customization\":{\"Gender\":0,\"Parents\":{\"FatherShape\":0,\"MotherShape\":0,\"FatherSkin\":0,\"MotherSkin\":0,\"Similarity\":1,\"SkinSimilarity\":1},\"Features\":[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],\"Hair\":{\"Hair\":0,\"Color\":0,\"HighlightColor\":0},\"Appearance\":[{\"Value\":255,\"Opacity\":1},{\"Value\":255,\"Opacity\":1},{\"Value\":1,\"Opacity\":1},{\"Value\":5,\"Opacity\":0.4},{\"Value\":0,\"Opacity\":0},{\"Value\":0,\"Opacity\":0},{\"Value\":255,\"Opacity\":1},{\"Value\":255,\"Opacity\":1},{\"Value\":0,\"Opacity\":0},{\"Value\":255,\"Opacity\":1},{\"Value\":255,\"Opacity\":1}],\"EyebrowColor\":0,\"BeardColor\":0,\"EyeColor\":0,\"BlushColor\":0,\"LipstickColor\":0,\"ChestHairColor\":0},\"level\":0}" });
                p.Position = new Vector3(402.8664, -996.4108, -99.00027);
                p.TriggerEvent("client:respawning");
                p.Eval("mp.players.local.setHeading(-185);");
                p.Dimension = (uint)new Random().Next(10000, 99999);

			} catch(Exception ex) { Log.Write(ex.Message); }
        }
    }
}
