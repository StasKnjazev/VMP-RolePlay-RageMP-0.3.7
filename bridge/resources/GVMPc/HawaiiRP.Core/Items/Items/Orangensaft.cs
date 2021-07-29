using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Orangesaft : Item
	{

		public Orangesaft()
		{
			Id = 222;
			Name = "Orangensaft";
			ImagePath = "orangensaft.png";
			WeightInG = 250;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
