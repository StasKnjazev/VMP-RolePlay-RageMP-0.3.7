using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Tablet : Item
	{
		public Tablet()
		{
			Id = 330;
			Name = "Tablet";
			ImagePath = "Laptop.png";
			WeightInG = 2500;
			MaxStackSize = 2;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}

	}
}
