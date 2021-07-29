using System;
using System.Collections.Generic;
using System.Text;
using GTANetworkAPI;
using GVMPc.ClothingShops;

namespace GVMPc.Fraktionen
{
    public class Fraktion
    {
        public string fraktionName { get; set; }

        public string shortName { get; set; }

        public Vector3 spawnPoint { get; set; }

        public Vector3 fraktionsLagerPoint { get; set; }

		public Vector3 frakLaborPoint { get; set; }

        public uint fraktionsDimension { get; set; }

        public Vector3 garagePoint { get; set; }

        public Vector3 garageSpawnPoint { get; set; }

        public float garageSpawnPointRotation { get; set; }

        public int blipColor { get; set; }

        public Color rgbColor { get; set; }

        public List<ClothingModel> fraktionsClothes { get; set; } = new List<ClothingModel>()
        {
          new ClothingModel("Keine", "Maske", 1, 0, 0, 0),
          new ClothingModel("Standart Shirt", "Oberteil", 11, 0, 0, 0),
          new ClothingModel("Keine", "Unterteil", 8, 15, 0, 0),
          new ClothingModel("Koerper 1", "Koerper", 3, 0, 0, 0),
          new ClothingModel("Jogginghose Weiss", "Hose", 4, 5, 0, 0),
          new ClothingModel("Sneaker Schwarz", "Schuhe", 6, 1, 0, 0)
        };

        public bool isBadFraktion { get; set; }

        public Fraktion(string name, string shortname, Vector3 spawnpoint, Vector3 fraktionslagerpoint, Vector3 frakLaborPoint, int dimension, Vector3 garagepoint, Vector3 garagespawnpoint, float garagespawnpointrotation, int blipcolor, Color rgbcolor, List<ClothingModel> fraktionsclothes, bool isbadfrak)
        {
            this.fraktionName = name;
            this.shortName = shortname;
            this.spawnPoint = spawnpoint;
            this.fraktionsLagerPoint = fraktionslagerpoint;
			this.frakLaborPoint = frakLaborPoint;
            this.fraktionsDimension = (uint)dimension;
            this.garagePoint = garagepoint;
            this.garageSpawnPoint = garagespawnpoint;
            this.garageSpawnPointRotation = garagespawnpointrotation;
            this.blipColor = blipcolor;
            this.rgbColor = rgbcolor;
            this.fraktionsClothes = fraktionsclothes;
            this.isBadFraktion = isbadfrak;
        }

        
    }
}

