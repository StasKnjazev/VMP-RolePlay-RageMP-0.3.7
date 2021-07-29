using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GVMPc.Banking
{
	public class AtmHistory
	{
		[JsonProperty(PropertyName = "text")]
		public string name { get; set; }

		[JsonProperty(PropertyName = "value")]
		public int value { get; set; }
		[JsonIgnore]
		public DateTime date { get; set; }

		public AtmHistory(string name, int value, DateTime dateTime)
		{
			this.name = name;
			this.value = value;
			this.date = dateTime;
		}
	}
}
