using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Kevlar : Item
	{

		public Kevlar()
		{
			Id = 234;
			Name = "Kevlar";
			ImagePath = "kevlar.png";
			WeightInG = 50;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
