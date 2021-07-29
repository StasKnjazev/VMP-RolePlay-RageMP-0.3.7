using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class benzin : Item
    {

        public benzin()
        {
            Id = 69;
            Name = "Benzin";
            ImagePath = "benzin.png";
            WeightInG = 800;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}