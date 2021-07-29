using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Brecheisen : Item
	{

		public Brecheisen()
		{
			Id = 13;
			Name = "Brecheisen";
			ImagePath = "Crowbar.png";
			WeightInG = 410000;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
