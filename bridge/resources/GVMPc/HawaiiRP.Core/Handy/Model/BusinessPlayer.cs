using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Handy
{
	public class BusinessPlayer
	{
		public string businessName { get; set; }

		public int businessRank { get; set; }

		public string playerName { get; set; }

		public BusinessPlayer(string businessName, int businessRank, string playerName)
		{
			this.businessName = businessName;
			this.businessRank = businessRank;
			this.playerName = playerName;
		}
	}
}
