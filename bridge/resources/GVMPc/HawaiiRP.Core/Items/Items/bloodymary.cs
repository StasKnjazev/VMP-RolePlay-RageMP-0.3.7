using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class bloodymary : Item
    {

        public bloodymary()
        {
            Id = 73;
            Name = "Bloodymary Coctail";
            ImagePath = "bloodymary.png";
			WeightInG = 350;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}
