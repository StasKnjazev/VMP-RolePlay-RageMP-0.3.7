using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Ipad
{
	public class OwnVehicleModel
	{
		public string vehiclehash
		{
			get;
			set;
		}

		public int id
		{
			get;
			set;
		}

		public int inGarage
		{
			get;
			set;
		}

		public string besitzer
		{
			get;
			set;
		}

		public string garage
		{
			get;
			set;
		}

		public OwnVehicleModel(string model, int id, int inGarage, string owner, string garage)
		{
			this.vehiclehash = model;
			this.id = id;
			this.inGarage = inGarage;
			this.besitzer = owner;
			this.garage = garage;
		}
	}
}
