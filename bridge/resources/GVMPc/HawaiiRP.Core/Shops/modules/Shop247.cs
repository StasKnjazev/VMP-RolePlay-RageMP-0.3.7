using GTANetworkAPI;
using GVMPc.Buy;
using GVMPc.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Shops
{
    class Shop247 : Shop
    {
        public Shop247()
        {
            this.id = 1;
            this.title = "24/7 Shop";
            this.position = new Vector3(25.7567, -1346.8448, 28.397045);
			this.items = new List<BuyItem>()
			  {
				new BuyItem(new Reperaturkasten(), 5000),
				new BuyItem(new Verbandskasten(), 4500),
				new BuyItem(new Alicepack(), 3000),
				new BuyItem(new angel(), 4500),
				new BuyItem(new hamburger(), 200),
				new BuyItem(new beer(), 200),
				new BuyItem(new benzin(), 600),
				new BuyItem(new chillichips(), 300),
				new BuyItem(new Fesseln(), 1200)
              };
        }
    }
}
