using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class blueprint : Item
    {

        public blueprint()
        {
            Id = 76;
            Name = "blueprint";
            ImagePath = "blueprint.png";
            WeightInG = 100;
            MaxStackSize = 5;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}