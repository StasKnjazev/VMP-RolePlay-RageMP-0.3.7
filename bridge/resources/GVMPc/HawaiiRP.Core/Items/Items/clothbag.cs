using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class clothbag : Item
    {

        public clothbag()
        {
            Id = 92;
            Name = "Kleidungs Korb";
            ImagePath = "clothbag.png";
            WeightInG = 150;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}