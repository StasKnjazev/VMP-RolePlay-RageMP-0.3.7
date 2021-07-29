using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Ipad
{
	public class PersonDataModel
	{
		public string name
		{
			get;
			set;
		}

		public string address
		{
			get;
			set;
		}

		public string membership
		{
			get;
			set;
		}

		public int phonenumber
		{
			get;
			set;
		}

		public string note
		{
			get;
			set;
		}

		public PersonDataModel(string name, string address, string membership, int phonenumber, string info)
		{
			this.name = name;
			this.address = address;
			this.membership = membership;
			this.phonenumber = phonenumber;
			this.note = info;
		}
	}
}
