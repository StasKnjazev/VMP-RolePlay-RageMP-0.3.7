using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Buy;
using GVMPc.Items;

namespace GVMPc.Shops
{
	class ElektroShop : Shop
	{
		public ElektroShop()
		{
			this.id = 12;
			this.title = "Elektronik Geschäft";
			this.position = new Vector3(-88.76816, 30.31269, 70.8519);
			this.items = new List<BuyItem>()
			  {
				new BuyItem(new Handy1(), 5500),
			  };
			this.customBlip = 459U;
			this.customBlipColor = (byte)2;
		}
	}
}
