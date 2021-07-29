using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Blumenerde : Item
    {

        public Blumenerde()
        {
            Id = 77;
            Name = "Blumenerde";
            ImagePath = "Blumenerde.png";
            WeightInG = 50;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}