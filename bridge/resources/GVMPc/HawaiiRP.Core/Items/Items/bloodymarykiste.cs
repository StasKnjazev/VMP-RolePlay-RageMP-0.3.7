using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class bloodymarykiste : Item
    {

        public bloodymarykiste()
        {
            Id = 74;
            Name = "Bloodymary Coctailkiste";
            ImagePath = "bloodymarykiste.png";
            WeightInG = 1300;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}