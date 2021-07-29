using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Other
{
	public class Lizenzen : Script
	{
		public static void showLicense(Client p, string name, int firstaid, int gunlicense, int driverlicense, int trucklicense, int motorcyclelicense, int boatlicense, int flyinglicensea, int flyinglicenseb, int taxilicense, int passengertransportlicense, int lawyerlicense, int registryofficelicense)
		{
			p.TriggerEvent("showLicense", name, firstaid, gunlicense, driverlicense, trucklicense, motorcyclelicense, boatlicense, flyinglicensea, flyinglicenseb, taxilicense, passengertransportlicense, lawyerlicense, registryofficelicense);
		}


	}
}
