using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Fesseln : Item
	{

		public Fesseln()
		{
			Id = 238;
			Name = "Fesseln";
			ImagePath = "rope.png";
			WeightInG = 120;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
