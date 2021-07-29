using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.PedModel
{
	public class PedModel
	{
		public int id
		{
			get;
			set;
		}

		public string ped
		{
			get;
			set;
		}

		public Vector3 pos
		{
			get;
			set;
		}

		public float heading
		{
			get;
			set;
		}

		public uint dimension
		{
			get;
			set;
		}
	}
}
