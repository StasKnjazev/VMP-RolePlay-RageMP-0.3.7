using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Buy;
using GVMPc.Menus;

namespace GVMPc.Vehicles.Shops
{
    class AutoShopRegister : Script
    {
        public static List<AutoShop> autoshopList = new List<AutoShop>();

		public static int numberplate = 0;

        [ServerEvent(Event.ResourceStart)]
        public void registerAutoShops()
        {
            AutoShopRegister.autoshopList.Add(new AutoshopDavis());
            AutoShopRegister.autoshopList.Add(new AutoshopFlughafen());
            AutoShopRegister.autoshopList.Add(new AutoshopVespucci());
            AutoShopRegister.autoshopList.Add(new AutoshopGambo());
			AutoShopRegister.autoshopList.Add(new BurritoHändler());

            foreach (AutoShop autoshop in AutoShopRegister.autoshopList)
            {
                ColShape val = NAPI.ColShape.CreateCylinderColShape(autoshop.position, 1.4f, 1.4f, 0);
                val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openAutoShop", NAPI.Util.ToJson(autoshop)));
                val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Benutze E um den Fahrzeugshop zu öffnen.", autoshop.name, "green", 3500));

                NAPI.Marker.CreateMarker(1, autoshop.position, new Vector3(), new Vector3(), 1.0f, new Color((int)byte.MaxValue, 140, 0), false, 0);
                NAPI.Blip.CreateBlip(225, autoshop.position, 1f, (byte)0, autoshop.name, byte.MaxValue, 0.0f, true, (short)0, 0);
            }
        }

        [RemoteEvent("openAutoShop")]
        public void openAutoShop(Client p, string autoshopstring)
        {
            if (autoshopstring == null)
                return;

            try
            {
                AutoShop autoShop = NAPI.Util.FromJson<AutoShop>(autoshopstring);

                List<NativeItem> Items = new List<NativeItem>();

                foreach (BuyCar buyCar in autoShop.autoshopItems)
                {
                    Items.Add(new NativeItem(Other.Utils.FirstletterUpper(buyCar.Vehicle_Name) + " - " + buyCar.Price.ToString() + " $", buyCar.Vehicle_Name + "-" + buyCar.Price + "-" + autoShop.name));
                }

                NativeMenu nativeMenu = new NativeMenu("AutoShop", autoShop.name.Split(" ")[1], Items);
                nativeMenu.showNativeMenu(p);
            } catch(Exception ex) { Log.Write(ex.Message); }
        }

        [RemoteEvent("nM-AutoShop")]
        public void buyVehicle(Client p, string jsonString)
        {

            if (jsonString == null)
                return;

            try
            {
                string name = (string)jsonString.Split("-")[0];
                int price = int.Parse(jsonString.Split("-")[1]);
                string shopname = (string)jsonString.Split("-")[2];
                AutoShop autoShop = new AutoshopFlughafen();

                foreach (AutoShop shop in autoshopList)
                {
                    if (shop.name == shopname)
                    {
                        autoShop = shop;
                        break;
                    }
                }

                if (Database.getMoney(p.Name) >= price)
                {
                    Database.changeMoney(p.Name, price, true);
                    Menus.NativeMenu.closeNativeMenu(p);
                    Notification.SendPlayerNotifcation(p, "Du hast das Fahrzeug " + Other.Utils.FirstletterUpper(name) + " erfolgreich gekauft.", 5000, "white", "FAHRZEUGSHOP", "white");

					numberplate = new Random().Next(11111, 99999);

                    Database.giveOwnedVehicle(p.Name, name, numberplate.ToString());
					Database.changeVehicleState(numberplate.ToString(), 1);
                }
                else
                {
                    Notification.SendPlayerNotifcation(p, "Du hast zu wenig Geld, um das Fahrzeug zu kaufen.", 5000, "white", "FAHRZEUGSHOP", "white");
                }
            } catch(Exception ex) { Log.Write(ex.Message); }
        }
    }
}
