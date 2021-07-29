using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Reperaturkasten : Item
	{

		public Reperaturkasten()
		{
			Id = 20;
			Name = "Reperaturkasten";
			ImagePath = "REBEL-Koffer.png";
			WeightInG = 600;
			MaxStackSize = 200;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
