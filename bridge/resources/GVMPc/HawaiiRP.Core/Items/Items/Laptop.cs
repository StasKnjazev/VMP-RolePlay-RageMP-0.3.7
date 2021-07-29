using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Laptop : Item
	{

		public Laptop()
		{
			Id = 191;
			Name = "Laptop";
			ImagePath = "Laptop.png";
			WeightInG = 1890;
			MaxStackSize = 1;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
