using GTANetworkAPI;
using GVMPc.Buy;
using GVMPc.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Shops
{
	class ShopMirrorpark : Shop
	{
		public ShopMirrorpark()
		{
			this.id = 1;
			this.title = "Mirrorpark Shop";
			this.position = new Vector3(1135.66, -982.2441, 45.31579);
			this.items = new List<BuyItem>()
			{
				new BuyItem(new Reperaturkasten(), 5000),
				new BuyItem(new Verbandskasten(), 4500),
				new BuyItem(new Alicepack(), 3000),
				new BuyItem(new angel(), 4500),
				new BuyItem(new hamburger(), 200),
				new BuyItem(new beer(), 200),
				new BuyItem(new benzin(), 600),
				new BuyItem(new chillichips(), 300),
				new BuyItem(new Fesseln(), 1200)
			};
		}
	}
}
