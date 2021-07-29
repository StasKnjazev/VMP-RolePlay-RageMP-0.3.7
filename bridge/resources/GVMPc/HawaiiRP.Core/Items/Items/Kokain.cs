using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Kokain : Item
	{

		public Kokain()
		{
			Id = 334;
			Name = "Kokain";
			ImagePath = "cocain.png";
			WeightInG = 50;
			MaxStackSize = 100;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
