using GTANetworkAPI;
using GVMPc.Buy;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Vehicles.Shops
{
    internal class AutoshopFlughafen : AutoShop
    {
        public AutoshopFlughafen()
        {
            this.id = 1;
            this.name = "Gebrauchtwagen Autohaus";
            this.position = new Vector3(-966.4791, -2713.069, 12.83069);
            this.ausparkPunkt = new Vector3(-965.3254, -2699.191, 13.40804);
            this.ausparkPunktRotation = 150.0593f;
            this.autoshopItems = new List<Buy.BuyCar>()
              {
                new BuyCar("Faggio", 3500),
                new BuyCar("Oracle", 10000)
              };
        }
    }
}
