using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Holzspaene : Item
    {

        public Holzspaene()
        {
            Id = 152;
            Name = "Holzspäne";
            ImagePath = "Holzspaene.png";
            WeightInG = 3;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}