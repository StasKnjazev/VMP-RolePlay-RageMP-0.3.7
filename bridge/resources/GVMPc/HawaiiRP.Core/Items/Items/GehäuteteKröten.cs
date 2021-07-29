using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
	class GehäuteteKröten : Item
	{

		public GehäuteteKröten()
		{
			Id = 15;
			Name = "Gehäutete_Kröten";
			ImagePath = "gehkroete.png";
			WeightInG = 400;
			MaxStackSize = 25;
		}

		public override bool getItemFunction(Client p)
		{
			return true;
		}
	}
}
