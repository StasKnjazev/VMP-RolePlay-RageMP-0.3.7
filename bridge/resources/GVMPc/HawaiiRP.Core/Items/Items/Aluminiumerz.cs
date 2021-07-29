using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Aluminiumerz : Item
    {

        public Aluminiumerz()
        {
            Id = 25;
            Name = "Aluminiumerz";
            ImagePath = "Aluminiumerz.png";
            WeightInG = 200;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}