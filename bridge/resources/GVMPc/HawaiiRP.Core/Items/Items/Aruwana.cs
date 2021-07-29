using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Aruwana : Item
    {

        public Aruwana()
        {
            Id = 56;
            Name = "Aruwana";
            ImagePath = "Aruwana.png";
            WeightInG = 125;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}