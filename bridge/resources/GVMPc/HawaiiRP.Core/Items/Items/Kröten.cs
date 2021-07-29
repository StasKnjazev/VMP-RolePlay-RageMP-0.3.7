using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Kröten : Item
	{

		public Kröten()
		{
			Id = 14;
			Name = "Kroeten";
			ImagePath = "Kroeten.png";
			WeightInG = 70;
			MaxStackSize = 200;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
