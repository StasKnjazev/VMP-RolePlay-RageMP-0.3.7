using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Handy
{
	public class ContactModel
	{
		public string name
		{
			get;
			set;
		}

		public int number
		{
			get;
			set;
		}

		public bool favorite
		{
			get;
			set;
		} = false;

		public ContactModel(string name, int number, bool favorite)
		{
			this.name = name;
			this.number = number;
			this.favorite = favorite;
		}
	}
}
