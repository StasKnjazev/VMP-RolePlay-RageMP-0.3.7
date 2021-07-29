using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Trauben : Item
	{

		public Trauben()
		{
			Id = 223;
			Name = "Trauben";
			ImagePath = "Trauben.png";
			WeightInG = 5;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
