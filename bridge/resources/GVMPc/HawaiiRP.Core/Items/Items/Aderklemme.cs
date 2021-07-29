using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Aderklemme : Item
    {

        public Aderklemme()
        {
            Id = 17;
            Name = "Aderklemme";
            ImagePath = "Aderklemme.png";
            WeightInG = 350;
            MaxStackSize = 5;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}