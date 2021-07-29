using GTANetworkAPI;
using GVMPc.Buy;
using GVMPc.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Shops
{
	class ShopHochzeit : Shop
	{
		public ShopHochzeit()
		{
			this.id = 1;
			this.title = "Hochzeits Shop";
			this.position = new Vector3(-570.2587, -394.564, 33.95656);
			this.items = new List<BuyItem>()
			  {
			  };
		}
	}
}
