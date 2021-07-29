using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Acetylsalicylsaeure : Item
    {

        public Acetylsalicylsaeure()
        {
            Id = 16;
            Name = "Acetylsalicylsaeure";
            ImagePath = "Acetylsalicylsaeure.png";
            WeightInG = 100;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}