using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Menus;
using GVMPc.Buy;

namespace GVMPc.Helicopters
{
	public class HeliShopRegister : Script
	{
		public static List<HelicopterShopModel> shops = new List<HelicopterShopModel>();

		[ServerEvent(Event.ResourceStart)]
		public void Start()
		{
			if(shops.Count < 1)
			{

			}

			foreach(HelicopterShopModel shopModel in shops)
			{
				ColShape col = NAPI.ColShape.CreateCylinderColShape(shopModel.location, 2f, 2f, 0);
				col.SetData("COLSHAPE_FUNCTION", new FunctionModel("openHeliShop", NAPI.Util.ToJson(shopModel)));
				col.SetData("COLSHAPE_MESSAGE", new Notification.Message("Drücke E um die einen Helikopter zu kaufen", shopModel.shopName, "grey", 5000));
			}
		}

		[RemoteEvent("openHeliShop")]
		public void openHeliShop(Client c, string model)
		{
			HelicopterShopModel helicopterShop = NAPI.Util.FromJson<HelicopterShopModel>(model);

			List<NativeItem> items = new List<NativeItem>();

			foreach(BuyCar buyCar in helicopterShop.helicopters)
			{
				items.Add(new NativeItem(Other.Utils.FirstletterUpper(buyCar.Vehicle_Name), "buyCar"));
			}

			NativeMenu nativeMenu = new NativeMenu(helicopterShop.shopName, "Angebote", new List<NativeItem>());
		}
	}
}
