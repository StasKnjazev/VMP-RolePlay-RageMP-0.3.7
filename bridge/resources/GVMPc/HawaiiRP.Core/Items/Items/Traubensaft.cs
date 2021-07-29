using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Traubensaft : Item
	{

		public Traubensaft()
		{
			Id = 224;
			Name = "Traubensaft";
			ImagePath = "Traubensaft.png";
			WeightInG = 125;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
