using System;
using System.Collections.Generic;

namespace Project0.App
{
    public partial class StoreLocation
    {
        public StoreLocation()
        {
            ProductOrder = new HashSet<ProductOrder>();
            StoreInventory = new HashSet<StoreInventory>();
        }

        public int LocId { get; set; }
        public string LocStreet { get; set; }
        public string LocCity { get; set; }
        public string LocCountry { get; set; }

        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
        public virtual ICollection<StoreInventory> StoreInventory { get; set; }
    }
}
