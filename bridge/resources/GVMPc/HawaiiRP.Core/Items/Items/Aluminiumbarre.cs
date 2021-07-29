using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Aluminiumbarre : Item
    {

        public Aluminiumbarre()
        {
            Id = 24;
            Name = "Aluminiumbarre";
            ImagePath = "Aluminiumbarre.png";
            WeightInG = 999;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}