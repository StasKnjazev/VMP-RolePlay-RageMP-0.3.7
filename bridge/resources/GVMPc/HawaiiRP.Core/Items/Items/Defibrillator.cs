using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Defibrillator : Item
    {

        public Defibrillator()
        {
            Id = 108;
            Name = "Defibrillator";
            ImagePath = "Defibrillator.png";
            WeightInG = 1900;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}