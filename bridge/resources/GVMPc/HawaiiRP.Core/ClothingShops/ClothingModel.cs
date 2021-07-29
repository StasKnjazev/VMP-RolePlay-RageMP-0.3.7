using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.ClothingShops
{
    public class ClothingModel
    {
        public string name { get; set; }

        public string category { get; set; }

        public int component { get; set; }

        public int drawable { get; set; }

        public int texture { get; set; }

		public int price { get; set; }

        public ClothingModel(string name, string category, int component, int drawable, int texture, int price)
        {
            this.name = name;
            this.category = category;
            this.component = component;
            this.drawable = drawable;
            this.texture = texture;
			this.price = price;
        }
    }
}
