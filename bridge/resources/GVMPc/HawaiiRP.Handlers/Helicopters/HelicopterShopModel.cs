using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Buy;

namespace GVMPc.Helicopters
{
	public class HelicopterShopModel 
	{
		public string shopName { get; set; }

		public Vector3 location { get; set; }

		public List<BuyCar> helicopters { get; set; }
	}
}
