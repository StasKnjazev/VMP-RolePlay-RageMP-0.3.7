using GTANetworkAPI;
using GVMPc.Buy;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Vehicles.Shops
{
    internal class AutoshopVespucci : AutoShop
    {
        public AutoshopVespucci()
        {
            this.id = 1;
            this.name = "Supersportwagen Autohaus";
            this.position = new Vector3(-1666.2074, -977.5398, 7.004927);
            this.ausparkPunkt = new Vector3(-1668.2297, -954.22046, 6.5905194);
            this.ausparkPunktRotation = 339;
            this.autoshopItems = new List<Buy.BuyCar>()
              {
				new BuyCar("adder", 1000000),
				new BuyCar("entity2", 1600000),
				new BuyCar("emerus", 1750000),
				new BuyCar("italigtb", 180000),
				new BuyCar("nero", 195000),
				new BuyCar("nero2", 200000),
				new BuyCar("t20", 2400000),
				new BuyCar("xa21", 2700000),
				new BuyCar("zentorno", 3000000),
              };
        }
    }
}
