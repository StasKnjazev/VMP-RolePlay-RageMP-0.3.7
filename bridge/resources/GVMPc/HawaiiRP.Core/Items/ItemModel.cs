using System;
using System.Collections.Generic;
using System.Text;

namespace GVMPc.Items
{
    public class ItemModel
    {

        public int Id
        {
            get;
            set;
        }

        public int Slot
        {
            get;
            set;
        }

        public int Weight
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string ImagePath
        {
            get;
            set;
        }

        public int Amount
        {
            get;
            set;
        }

		public ItemModel(int Id, int Slot, int Weight, string Name, string ImagePath, int Amount)
        {
            this.Id = Id;
            this.Slot = Slot;
            this.Weight = Weight;
            this.Name = Name;
            this.ImagePath = ImagePath;
            this.Amount = Amount;
        }

    }
}
