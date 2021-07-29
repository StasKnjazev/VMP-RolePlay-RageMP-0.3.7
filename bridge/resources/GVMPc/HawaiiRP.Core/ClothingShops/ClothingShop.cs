using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.ClothingShops
{
    public class ClothingShop
    {
        public string name { get; set; }

        public Vector3 position { get; set; }

        public ClothingShop(string name, Vector3 pos)
        {
            this.name = name;
            this.position = pos;
        }
    }
}
