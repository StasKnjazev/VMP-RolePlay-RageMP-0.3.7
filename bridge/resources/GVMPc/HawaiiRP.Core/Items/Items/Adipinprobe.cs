using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Adipinprobe : Item
    {

        public Adipinprobe()
        {
            Id = 19;
            Name = "Adipinprobe";
            ImagePath = "Adipinprobe.png";
            WeightInG = 90;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}