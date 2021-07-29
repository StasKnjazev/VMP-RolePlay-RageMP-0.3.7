using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Handy
{
	public class TaxiModel
	{
		public string name { get; set; }

		public string number { get; set; }

		public string price { get; set; }

		public TaxiModel(string name, int number, int price)
		{
			this.name = name;
			this.number = "Tel.: " + number;
			this.price = price + "$/KM";
		}
	}
}
