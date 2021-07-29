using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Schwarzgeldstapel : Item
	{

		public Schwarzgeldstapel()
		{
			Id = 250;
			Name = "Schwarzgeld";
			ImagePath = "notes.png";
			WeightInG = 0;
			MaxStackSize = 25000;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
