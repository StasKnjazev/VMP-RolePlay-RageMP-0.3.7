using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Menus;

namespace GVMPc.Dealer
{
	class DealerRegister
	{
		public static List<DealerModel> dealer = new List<DealerModel>();

		[ServerEvent(Event.ResourceStart)]
		public void onResourceStart()
		{
			if(dealer.Count < 1)
			{
				dealer.Add(new VinewoodDealer());
			}

			foreach(DealerModel dealer in dealer)
			{
				ColShape col = NAPI.ColShape.CreateCylinderColShape(dealer.position, 2f, 2f, 0);
				col.SetData("COLSHAPE_FUNCTION", new FunctionModel("openDealer"));
				col.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um mit dem Dealer zu handeln", dealer.name, "grey", 5000));
			}

			Log.Write("Dealer | " + dealer.Count + " Dealer geladen");
		}

		[RemoteEvent("openDealer")]
		public void openDealer(Client p)
		{
			foreach (DealerModel dealer in dealer) {

				NativeMenu nativeMenu = new NativeMenu("Dealer", "Angebote", new List<NativeItem>()
				{
					new NativeItem("Schließen", "close"),
				    new NativeItem("V | Meth (238 $ pro Kristall)", "meth"),
				    new NativeItem("V | Kiste Meth (470 $ pro Stück)", "methbox"),
				    new NativeItem("V | Waffenset (2100 $ pro Set)", "weapons"),
				    new NativeItem("V | Kiste Cannabis (480 $ pro Stück)", "cannabis"),
				    new NativeItem("V | Goldbarren (11270 $ pro Barren)", "gold"),
				    new NativeItem("V | Juwelen (5658 $ pro Juwel)", "jewels")
				});
				nativeMenu.showNativeMenu(p);
		    }
		}
	}
}
