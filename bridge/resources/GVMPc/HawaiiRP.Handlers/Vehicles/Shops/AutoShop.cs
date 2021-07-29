using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Buy;

namespace GVMPc.Vehicles.Shops
{
    public class AutoShop
    {
        public int id { get; set; }

        public string name { get; set; }

        public Vector3 position { get; set; }

        public Vector3 ausparkPunkt { get; set; }

        public float ausparkPunktRotation { get; set; }

        public List<BuyCar> autoshopItems { get; set; }
    }
}
