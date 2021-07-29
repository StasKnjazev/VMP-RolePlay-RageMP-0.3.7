using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Business
{
	public class ÖlPumpenModel
	{
		public int id
		{
			get;
			set;
		}

		public string businessname
		{
			get;
			set;
		}

		public int amount
		{
			get;
			set;
		}

		public int cost
		{
			get;
			set;
		}

		public Vector3 position
		{
			get;
			set;
		}

		public ÖlPumpenModel(int id, string owner, int cost, Vector3 position)
		{
			this.id = id;
			this.businessname = owner;
			this.cost = cost;
			this.position = position;
		}
	}
}
