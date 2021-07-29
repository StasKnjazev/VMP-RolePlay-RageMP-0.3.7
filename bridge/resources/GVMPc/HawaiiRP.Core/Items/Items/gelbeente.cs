using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class gelbeente : Item
    {

        public gelbeente()
        {
            Id = 128;
            Name = "´gelbe Ente";
            ImagePath = "gelbeente.png";
            WeightInG = 300;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}