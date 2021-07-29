using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Buy
{
    public class BuyCar
    {
        public string Vehicle_Name { get; set; }

        public int Price { get; set; }

        public BuyCar(string vehicle_name, int price)
        {
            this.Vehicle_Name = vehicle_name;
            this.Price = price;
        }
    }
}
