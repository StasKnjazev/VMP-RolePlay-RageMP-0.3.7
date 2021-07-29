using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Buy;
using GVMPc.Items;

namespace GVMPc.Shops
{
	class ShopAmmunation4 : Shop
	{
		public ShopAmmunation4()
		{
			this.id = 1;
			this.title = "Ammunation Shop";
			this.position = new Vector3(810.8647, -2157.689, 28.51899);
			this.items = new List<BuyItem>()
			  {
				new BuyItem(new Pistol(), 7000),
				new BuyItem(new Heavypistol(), 15000),
				new BuyItem(new AmmoHeavyPistol(), 7500),
				new BuyItem(new AmmoPistol(), 5000)
			  };
			this.customBlip = 110U;
			this.customBlipColor = (byte)1;
		}
	}
}
