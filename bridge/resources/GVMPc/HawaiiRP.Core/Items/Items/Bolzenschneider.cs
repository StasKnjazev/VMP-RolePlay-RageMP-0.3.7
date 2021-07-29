using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Bolzenschneider : Item
    {

        public Bolzenschneider()
        {
            Id = 80;
            Name = "Bolzenschneider";
            ImagePath = "Bolzenscheider.png";
            WeightInG = 1200;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}