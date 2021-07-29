using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Banknoten : Item
	{

		public Banknoten()
		{
			Id = 230;
			Name = "Banknoten";
			ImagePath = "Notizblock.png";
			WeightInG = 120;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
