using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class hanfplanze : Item
    {

        public hanfplanze()
        {
            Id = 146;
            Name = "Hanfpflanze";
            ImagePath = "hanfpflanze.png";
            WeightInG = 120;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
