using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class Personalausweis : Item
	{

		public Personalausweis()
		{
			Id = 245;
			Name = "Personalausweis";
			ImagePath = "perso.png";
			WeightInG = 125;
			MaxStackSize = 250;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
