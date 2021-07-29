using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class hamburger : Item
    {

        public hamburger()
        {
            Id = 142;
            Name = "Hamburger";
            ImagePath = "hamburger.png";
            WeightInG = 250;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}