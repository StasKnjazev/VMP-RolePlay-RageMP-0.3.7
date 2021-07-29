using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.Other
{
	class Früchtehändler : Script
	{
		public static int Orangenpreis = 0;

		public static int Traubensaftpreis = 0;

		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			NAPI.Blip.CreateBlip(480, new Vector3(471.2844, 2607.474, 43.37725), 1f, 0, "Früchte Händler", 255, 0, true, 0, 0);
			ColShape val = NAPI.ColShape.CreateCylinderColShape(new Vector3(471.2844, 2607.474, 43.37725), 2f, 2f, 0);
			val.SetData("COLSHAPE_FUNCTION", new FunctionModel("openFruitSeller"));
			val.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um mit dem Früchtehändler zu interagieren", "FRÜCHTEHÄNDLER", "green", 4500));

		}

		[RemoteEvent("openFruitSeller")]
		public void openFruitSeller(Client p)
		{
			Orangenpreis = new Random().Next(1100, 12000);

			Traubensaftpreis = new Random().Next(1100, 12000);

			NativeMenu nativeMenu = new NativeMenu("Fruchthändler", "Angebote", new List<NativeItem>
			{
				new NativeItem("50x Trauben -" + Traubensaftpreis + " $", "fruit"),
				new NativeItem("50x Orangensaft - " + Orangenpreis + " $", "orange")
			});
			nativeMenu.showNativeMenu(p);
		}

		[RemoteEvent("nM-Fruchthändler")]
		public void nmFruchthändler(Client p, string selection)
		{

			if (selection == "fruit")
			{
				if(Database.getItemCount(p.Name, "Traubensaft") > 49)
				{
					Database.changeMoney(p.Name, Traubensaftpreis, false);
					Database.changeInventoryItem(p.Name, "Traubensaft", 50, true);
					Notification.SendPlayerNotifcation(p, "Du hast 50 Traubensaft für " + Traubensaftpreis + " $ verkauft", 4500, "white", "FRÜCHTEHÄNDLER", "");
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Traubensaft dabei", 4500, "red", "FRÜCHTEHÄNDLER", "");
				}
			} else if(selection == "orange")
			{
				if (Database.getItemCount(p.Name, "Orangensaft") > 49)
				{
					Database.changeMoney(p.Name, Orangenpreis, false);
					Database.changeInventoryItem(p.Name, "Orangensaft", 50, true);
					Notification.SendPlayerNotifcation(p, "Du hast 50 Orangensaft für " + Orangenpreis + " $ verkauft", 4500, "white", "FRÜCHTEHÄNDLER", "");
				}
				else
				{
					Notification.SendPlayerNotifcation(p, "Du hast nicht genug Orangesaft dabei", 4500, "red", "FRÜCHTEHÄNDLER", "");
				}
			}

		}

	}
}
