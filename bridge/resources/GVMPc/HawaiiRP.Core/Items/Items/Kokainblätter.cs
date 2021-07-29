using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Kokainblätter : Item
	{

		public Kokainblätter()
		{
			Id = 333;
			Name = "Kokainblätter";
			ImagePath = "cocaineleaf.png";
			WeightInG = 20;
			MaxStackSize = 100;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
