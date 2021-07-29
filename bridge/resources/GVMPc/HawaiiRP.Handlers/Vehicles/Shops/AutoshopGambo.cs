using GTANetworkAPI;
using GVMPc.Buy;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Vehicles.Shops
{
    internal class AutoshopGambo : AutoShop
    {
        public AutoshopGambo()
        {
            this.id = 1;
            this.name = "Sportwagen Autohaus";
            this.position = new Vector3(-32.855656, -1102.147, 25.32235);
            this.ausparkPunkt = new Vector3(-31.66658, -1091.284, 25.32228);
            this.ausparkPunktRotation = 150.0593f;
            this.autoshopItems = new List<BuyCar>()
              {
				new BuyCar("alpha", 100000),
				new BuyCar("banshee", 110000),
				new BuyCar("bestiagts", 120000),
				new BuyCar("blista2", 130000),
				new BuyCar("blista3", 140000),
				new BuyCar("buffalo", 150000),
				new BuyCar("buffalo", 160000),
				new BuyCar("comet2", 350000),
				new BuyCar("coquette4", 500000),
				new BuyCar("deveste", 550000),
				new BuyCar("elegy2", 500000),
				new BuyCar("feltzer2", 525000),
				new BuyCar("furoregt", 550000),
				new BuyCar("hotring", 600000),
				new BuyCar("italigto", 610000),
				new BuyCar("jugular", 625000),
				new BuyCar("kuruma", 660000),
				new BuyCar("jester", 660000),
				new BuyCar("neo", 600000),
				new BuyCar("raiden", 570000),
				new BuyCar("schafter3", 500000),
				new BuyCar("schafter4", 550000),
				new BuyCar("specter", 600000)
              };
        }
    }
}
