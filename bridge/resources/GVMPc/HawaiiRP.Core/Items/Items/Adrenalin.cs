using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Adrenalin : Item
    {

        public Adrenalin()
        {
            Id = 19;
            Name = "Adrenalin";
            ImagePath = "Adrenalin.png";
            WeightInG = 80;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}