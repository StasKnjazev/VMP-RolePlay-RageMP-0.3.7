using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Holzplanke : Item
    {

        public Holzplanke()
        {
            Id = 151;
            Name = "Holzplanke";
            ImagePath = "Holzplanke.png";
            WeightInG = 890;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}