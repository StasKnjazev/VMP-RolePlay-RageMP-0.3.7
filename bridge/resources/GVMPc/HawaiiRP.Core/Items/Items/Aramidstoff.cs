using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Aramidstoff : Item
    {

        public Aramidstoff()
        {
            Id = 56;
            Name = "Aramidstoff";
            ImagePath = "Aramidstoff.png";
            WeightInG = 50;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}