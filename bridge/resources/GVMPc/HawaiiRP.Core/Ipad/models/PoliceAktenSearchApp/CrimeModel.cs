using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Ipad
{
	public class CrimeModel
	{
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

		public CrimeModel(int jailCost, int Jailtime)
		{
			this.jailCost = jailCost;
			this.jailTime = Jailtime;
		}
	}
}
