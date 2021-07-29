using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class KisteAdvanceRifle : Item
    {

        public KisteAdvanceRifle()
        {
            Id = 163;
            Name = "KisteAdvanceRifle";
            ImagePath = "KisteAdvanceRifle.png";
            WeightInG = 1600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}