using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GVMPc.Ipad
{
	public class RentModel
	{
		[JsonProperty(PropertyName = "playername")]
		public string name { get; set; }
		[JsonProperty(PropertyName = "playerphone")]
		public int phonenumber { get; set; }
		[JsonProperty(PropertyName = "vehicleid")]
		public string vehicleName { get; set; }
		[JsonProperty(PropertyName = "information")]
		public string vehDesc { get; set; }

		public RentModel(string name, int phonenumber, string vehicleName, string vehicleDescription)
		{
			this.name = name;
			this.phonenumber = phonenumber;
			this.vehicleName = vehicleName;
			this.vehDesc = vehicleDescription;
		}
	}
}
