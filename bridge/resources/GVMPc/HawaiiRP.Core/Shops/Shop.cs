using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.Buy;

namespace GVMPc.Shops
{
    public class Shop
    {
        public int id { get; set; }

        public string title { get; set; }

        public uint customBlip { get; set; } = 0;

        public byte customBlipColor { get; set; }

		public bool robbed { get; set; } = false;

		public int cooldown { get; set; } = 0;

		public Vector3 robPosition { get; set; }

        public Vector3 position { get; set; }

        public List<BuyItem> items { get; set; }
    }
}
