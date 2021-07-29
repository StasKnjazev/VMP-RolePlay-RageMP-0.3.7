using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Bohrer : Item
	{

		public Bohrer()
		{
			Id = 231;
			Name = "Bohrer";
			ImagePath = "drill.png";
			WeightInG = 1200;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
