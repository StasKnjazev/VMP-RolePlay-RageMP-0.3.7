using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.XMenu
{
    class XManager : Script
    {
        [RemoteEvent("nM-Kleidung")]
        public static void Kleidung(Client c, string arg)
        {
            try
            {
                if (arg == "hut")
                    c.SetAccessories(0, -1, 0);

                if (arg == "maske")
                    Clothing.PlayerClothes.setClothes(c, 1, 0, 0);

                if (arg == "hose")
                    Clothing.PlayerClothes.setClothes(c, 4, 21, 0);

                if (arg == "schuhe")
                    Clothing.PlayerClothes.setClothes(c, 6, 34, 0);

                if (arg == "anziehen")
                    Database.restoreCustomization(c);
				    Items.Weapons.WeaponSync(c);
				    Database.getLoadout(c.Name);
            }
            catch (Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("showKleidungsmenu")]
        public static void ShowAnimMenu(Client c)
        {
            try
            {
                new NativeMenu("Kleidung", "Menu", new List<NativeItem>()
                {
                    new NativeItem("Kleidung wieder anziehen", "anziehen"),
                    new NativeItem("Hut", "hut"),
                    new NativeItem("Maske", "maske"),
                    new NativeItem("Hose", "hose"),
                    new NativeItem("Schuhe", "schuhe")
                }).showNativeMenu(c);
            }
            catch (Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("nM-Animationen")]
        public static void playAnim(Client c, string selection)
        {
            try
            {
				if(selection == "exit")
				{
					NAPI.Player.StopPlayerAnimation(c);
					NativeMenu.closeNativeMenu(c);
				} else if(selection == "cryingground")
				{
					NAPI.Player.PlayPlayerAnimation(c, 33, "amb@code_human_cower@female@base", "base");
					NativeMenu.closeNativeMenu(c);
				} else if(selection == "coparmgürtel")
				{
					NAPI.Player.PlayPlayerAnimation(c, 33, "amb@code_human_wander_idles_cop@female@static", "static");
					NativeMenu.closeNativeMenu(c);
				} else if(selection == "knien")
				{
					NAPI.Player.PlayPlayerAnimation(c, 33, "amb@medic@standing@kneel@idle_a", "idle_a");
					NativeMenu.closeNativeMenu(c);
				} else if(selection == "knien2")
				{
					NAPI.Player.PlayPlayerAnimation(c, 33, "amb@medic@standing@tendtodead@base", "base");
					NativeMenu.closeNativeMenu(c);
				} else if(selection == "ergeben")
				{
					NAPI.Player.PlayPlayerAnimation(c, 33, "amb@world_human_bum_wash@male@low@idle_a", "idle_a");
					NativeMenu.closeNativeMenu(c);
				}


            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("showAnimMenu")]
        public static void showAnimMenu(Client c)
        {
            try
            {
                new NativeMenu("Animationen", "Menu", new List<NativeItem>()
                {
                    new NativeItem("Abbrechen", "exit"),
					new NativeItem("Weinen Boden", "cryingground"),
					new NativeItem("Arm am Gürtel", "coparmgürtel"),
					new NativeItem("Knien", "knien"),
					new NativeItem("Knien2", "knien2"),
					new NativeItem("Ergeben", "ergeben")
                }).showNativeMenu(c);
            }
            catch (Exception ex) { Log.Write(ex.Message); }
        }
    }
}
