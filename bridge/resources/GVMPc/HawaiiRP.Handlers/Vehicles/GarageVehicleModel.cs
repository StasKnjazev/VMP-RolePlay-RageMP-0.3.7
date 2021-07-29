using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Vehicles
{
    public class GarageVehicleModel
    {
        public int Id
        {
            get;
            set;
        }

        public int OwnerID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Plate
        {
            get;
            set;
        } = "";


        public GarageVehicleModel(int Id, int OwnerID, string Name, string Plate)
        {
            this.Id = Id;
            this.OwnerID = OwnerID;
            this.Name = Name;
            this.Plate = Plate;
        }
    }
}
