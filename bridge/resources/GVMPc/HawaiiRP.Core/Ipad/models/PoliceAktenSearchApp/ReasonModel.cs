using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Ipad
{
	public class ReasonModel
	{
		public string name
		{
			get;
			set;
		}

		public int id
		{
			get;
			set;
		}

		public int jailCost
		{
			get;
			set;
		}

		public int jailTime
		{
			get;
			set;
		}

		public ReasonModel(string name, int id, int cost, int jailTime)
		{
			this.name = name;
			this.id = id;
			this.jailCost = cost;
			this.jailTime = jailTime;
		}
	}
}
