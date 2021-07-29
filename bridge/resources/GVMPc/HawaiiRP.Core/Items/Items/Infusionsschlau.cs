using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Infusionsschlau : Item
    {

        public Infusionsschlau()
        {
            Id = 155;
            Name = "Infusionsschlau";
            ImagePath = "Infusionsschlau.png";
            WeightInG = 2300;
            MaxStackSize = 100;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}