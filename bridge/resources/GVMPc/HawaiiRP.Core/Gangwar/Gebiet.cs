using GTANetworkAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Gangwar
{
    public class Gebiet
    {
        public string fraktion { get; set; }

        public string name { get; set; }

        public Vector3 position { get; set; }

        public float gebietRadius { get; set; }

        public Vector3 flagOne { get; set; }

        public Vector3 flagTwo { get; set; }

        public Vector3 flagThree { get; set; }

        public Vector3 flagFour { get; set; }

        public Gebiet(string name, string fraktion, Vector3 position, float radius, Vector3 flagOne, Vector3 flagTwo, Vector3 flagThree, Vector3 flagFour)
        {
            this.name = name;
            this.fraktion = fraktion;
            this.position = position;
            this.gebietRadius = radius;
            this.flagOne = flagOne;
            this.flagTwo = flagTwo;
            this.flagThree = flagThree;
            this.flagFour = flagFour;
        }
    }
}
