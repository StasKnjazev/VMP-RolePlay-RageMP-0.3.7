using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Geldanlage : Item
    {

        public Geldanlage()
        {
            Id = 129;
            Name = "Geldanlage";
            ImagePath = "Geldanlage.png";
            WeightInG = 1500;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}