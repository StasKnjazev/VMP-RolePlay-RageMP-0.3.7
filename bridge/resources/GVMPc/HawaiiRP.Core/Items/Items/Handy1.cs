using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Handy1 : Item
	{

		public Handy1()
		{
			Id = 232;
			Name = "Handy";
			ImagePath = "Smartphone.png";
			WeightInG = 2000;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
