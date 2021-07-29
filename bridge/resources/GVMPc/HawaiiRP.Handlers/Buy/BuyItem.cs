using GVMPc.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Buy
{
    public class BuyItem
    {
        public string name { get; set; }

        public int price { get; set; }

        public string url { get; set; } = "";

        public BuyItem(Item item, int price)
        {
            this.name = item.Name;
            this.price = price;
            this.url = item.ImagePath;
        }
    }
}
