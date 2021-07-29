using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Eisenerz : Item
	{

		public Eisenerz()
		{
			Id = 333;
			Name = "Eisenerz";
			ImagePath = "Eisenerz.png";
			WeightInG = 100;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
