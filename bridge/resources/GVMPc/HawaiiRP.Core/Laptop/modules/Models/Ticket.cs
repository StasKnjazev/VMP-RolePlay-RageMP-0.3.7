using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Laptop
{
	public class Ticket
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
		public Ticket(string author, string description, int id, DateTime created_at)
		{
			this.creator = author;
			this.text = description;
			this.id = id;
			this.created_at = created_at;
		}
	}
}
