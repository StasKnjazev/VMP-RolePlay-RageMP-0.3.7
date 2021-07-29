using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Waffenteile : Item
	{

		public Waffenteile()
		{
			Id = 325;
			Name = "Waffenteile";
			ImagePath = "Waffenteile.png";
			WeightInG = 1200;
			MaxStackSize = 250;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
