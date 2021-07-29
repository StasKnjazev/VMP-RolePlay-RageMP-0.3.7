using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Goldnugget : Item
    {

        public Goldnugget()
        {
            Id = 136;
            Name = "Goldnugget";
            ImagePath = "Goldnugget.png";
            WeightInG = 111;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}