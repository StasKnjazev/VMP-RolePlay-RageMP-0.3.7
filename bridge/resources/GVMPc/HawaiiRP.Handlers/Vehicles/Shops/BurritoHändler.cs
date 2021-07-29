using GTANetworkAPI;
using GVMPc.Buy;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Vehicles.Shops
{
	internal class BurritoHändler : AutoShop
	{
		public BurritoHändler()
		{
			this.id = 10;
			this.name = "Burrito Händler";
			this.position = new Vector3(-761.0125, -2629.681, 12.84462);
			this.ausparkPunkt = new Vector3(-767.1359, -2608.158, 12.7285);
			this.ausparkPunktRotation = 237.5005f;
			this.autoshopItems = new List<BuyCar>()
			  {
				new BuyCar("gburrito", 50000),
				new BuyCar("burrito", 50000),
				new BuyCar("benson", 1000000),
				new BuyCar("speedo", 75000)
			  };
		}
	}
}
