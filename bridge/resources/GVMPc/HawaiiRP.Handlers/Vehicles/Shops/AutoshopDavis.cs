using GTANetworkAPI;
using GVMPc.Buy;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Vehicles.Shops
{
    internal class AutoshopDavis : AutoShop
    {
        public AutoshopDavis()
        {
            this.id = 1;
            this.name = "Mittelklasse Autohaus";
            this.position = new Vector3(-41.09109, -1675.191, 28.4411);
            this.ausparkPunkt = new Vector3(-51.40001, -1688.573, 29.37488);
            this.ausparkPunktRotation = 150.0593f;
            this.autoshopItems = new List<BuyCar>()
              {
				new BuyCar("blista", 25000),
				new BuyCar("issi2", 35000),
				new BuyCar("sentinel", 55000),
				new BuyCar("zion", 60000),
				new BuyCar("zion2", 61000),
				new BuyCar("dominator", 85000),
				new BuyCar("dominator3", 90000),
				new BuyCar("buccaneer2", 95000)
              };
        }
    }
}
