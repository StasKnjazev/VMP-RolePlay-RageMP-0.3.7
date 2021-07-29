using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Fraktionen;


namespace GVMPc.Items
{
    internal class Schweißgeraet : Item
    {

        public Schweißgeraet()
        {
            base.Id = 170;
            base.Name = "Schweißgeraet";
            base.ImagePath = "Schweissgeraet.png";
            base.WeightInG = 2500;
            base.MaxStackSize = 1;
        }

        public override bool getItemFunction(Client client)
        {
			return true;
        }

    }
}

