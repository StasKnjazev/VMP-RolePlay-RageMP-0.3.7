using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;

namespace GVMPc.Vehicles.Garages
{
    public class GarageModel
    {
        public string name { get; set; }

        public Vector3 position { get; set; }

        public Vector3 ausparkPunkt { get; set; }

        public float ausparkRotation { get; set; }

        public GarageModel(string name, Vector3 position, Vector3 ausparkPunkt, float ausparkRotation)
        {
            this.name = name;
            this.position = position;
            this.ausparkPunkt = ausparkPunkt;
            this.ausparkRotation = ausparkRotation;
        }
    }
}
