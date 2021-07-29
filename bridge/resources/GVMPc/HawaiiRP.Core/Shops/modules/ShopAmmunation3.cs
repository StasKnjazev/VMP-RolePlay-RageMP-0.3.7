using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Buy;
using GVMPc.Items;

namespace GVMPc.Shops
{
	class ShopAmmunation3 : Shop
	{
		public ShopAmmunation3()
		{
			this.id = 1;
			this.title = "Ammunation Shop";
			this.position = new Vector3(1692.75, 3759.492, 33.60532);
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
