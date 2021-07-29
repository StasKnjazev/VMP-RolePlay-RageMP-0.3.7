using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Sprengstoff : Item
	{

		public Sprengstoff()
		{
			Id = 228;
			Name = "Sprengstoff";
			ImagePath = "AmmoRPG.png";
			WeightInG = 2500;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
