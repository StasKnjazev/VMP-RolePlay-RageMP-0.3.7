using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Armypack : Item
    {

        public Armypack()
        {
            Id = 57;
            Name = "Armypack";
            ImagePath = "Armypack.png";
            WeightInG = 1200;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}