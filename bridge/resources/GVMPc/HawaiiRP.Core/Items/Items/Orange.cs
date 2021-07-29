using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Orange : Item
	{

		public Orange()
		{
			Id = 221;
			Name = "Orangen";
			ImagePath = "orange.png";
			WeightInG = 1;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
