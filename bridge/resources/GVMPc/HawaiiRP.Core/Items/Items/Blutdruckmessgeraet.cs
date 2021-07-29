using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Blutdruckmessgeraet : Item
    {

        public Blutdruckmessgeraet()
        {
            Id = 79;
            Name = "Blutdruckmessgeraet";
            ImagePath = "Blutdruckmessgeraet.png";
            WeightInG = 600;
            MaxStackSize = 25;
        }

        public override bool getItemFunction(Client p)
        {
            return true;
        }
    }
}