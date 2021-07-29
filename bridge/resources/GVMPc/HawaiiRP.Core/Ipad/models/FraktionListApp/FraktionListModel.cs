using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using Newtonsoft.Json;

namespace GVMPc.Ipad
{
	public class FraktionListModel
	{
		[JsonProperty(PropertyName = "rang")]
		public int rang { get; set; }

		[JsonProperty(PropertyName = "title")]
		public string title { get; set; }
		[JsonProperty(PropertyName = "name")]
		public string name { get; set; }
		[JsonProperty(PropertyName = "payday")]
		public int payday { get; set; }
		[JsonProperty(PropertyName = "manage")]
		public bool manage { get; set; }

		public FraktionListModel(int rang, string title, string name, int payday, bool manage)
		{
			this.rang = rang;
			this.title = title;
			this.name = name;
			this.payday = payday;
			this.manage = manage;
		}
	}
}
