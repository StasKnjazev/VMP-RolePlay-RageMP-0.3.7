using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Ipad
{
	public class openService
	{
		public string name
		{
			get;
			set;
		}

		public string message
		{
			get;
			set;
		}

		public int phonenumber
		{
			get;
			set;
		}

		public float x
		{
			get;
			set;
		}

		public float y
		{
			get;
			set;
		}

		public openService(string name, string message, int phonenumber, Vector3 pos)
		{
			this.name = name;
			this.message = message;
			this.phonenumber = phonenumber;
			x = pos.X;
			y = pos.Y;
		}

	}
}
