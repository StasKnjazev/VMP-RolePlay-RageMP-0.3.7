using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Items
{
    class Alicepack : Item
	{
		public static bool AliceBool;

		public Alicepack()
        {
            Id = 23;
            Name = "Alicepack";
            ImagePath = "Alicepack.png";
            WeightInG = 1500;
            MaxStackSize = 2;
        }

        public override bool getItemFunction(Client p)
        {
            try
			{
				AliceBool = true;

			} catch(Exception ex)
			{
				Log.Write(ex.Message);
			}

			return true;
        }
    }
}
