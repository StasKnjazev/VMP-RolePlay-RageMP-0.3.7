using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Ipad
{
	public class acceptedService
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

		public string acceptedUser
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

		public acceptedService(string name, string message, int phonenumber, string acceptedOwner, Vector3 position)
		{
			this.name = name;
			this.message = message;
			this.phonenumber = phonenumber;
			this.acceptedUser = acceptedOwner;
			x = position.X;
			y = position.Y;
		}

	}
}
