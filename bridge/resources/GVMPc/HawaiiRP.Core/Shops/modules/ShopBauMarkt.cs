using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Buy;
using GVMPc.Items;

namespace GVMPc.Shops
{
	class ShopBauMarkt : Shop
	{
		public ShopBauMarkt()
		{
			this.id = 12;
			this.title = "Baumarkt";
			this.position = new Vector3(2747.34, 3472.924, 54.56999);
			this.items = new List<BuyItem>()
			  {
				new BuyItem(new Bohrer(), 7500),
				new BuyItem(new Sprengstoff(), 15000),
				new BuyItem(new Schweißgeraet(), 8000),
				new BuyItem(new battery(), 4500),
				new BuyItem(new Dietrich(), 5500),
				new BuyItem(new Holzaxt(), 1000)
			  };
			this.customBlipColor = (byte)1;
		}
	}
}
