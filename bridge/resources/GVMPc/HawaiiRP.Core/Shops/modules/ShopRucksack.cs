using GTANetworkAPI;
using GVMPc.Buy;
using GVMPc.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Shops
{
	class ShopRucksack : Shop
	{
		public ShopRucksack()
		{
			this.id = 1;
			this.title = "Rucksack Shop";
			this.position = new Vector3(-1168.069, -1580.061, 3.301477);
			this.items = new List<BuyItem>()
			  {
				new BuyItem(new Alicepack(), 6000)
			  };
			this.customBlip = 280U;
		}
	}
}
