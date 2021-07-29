using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Laptop
{
	public class openTicket
	{
		public string creator
		{
			get;
			set;
		}

		public string text
		{
			get;
			set;
		}

		public DateTime created_at
		{
			get;
			set;
		}
		public int id
		{
			get;
			set;
		}
		public openTicket(string name, string description, DateTime time, int id)
		{
			this.creator = name;
			this.text = description;
			this.created_at = time;
			this.id = id;
		}
	}
}
