using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class FakeID : Item
	{

		public FakeID()
		{
			Id = 230;
			Name = "FakeID";
			ImagePath = "perso.png";
			WeightInG = 200;
			MaxStackSize = 1;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
