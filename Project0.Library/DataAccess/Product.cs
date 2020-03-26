using System;
using System.Collections.Generic;

namespace Project0.App
{
    public partial class Product
    {
        public Product()
        {
            StoreInventory = new HashSet<StoreInventory>();
        }

        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public string ProdDescription { get; set; }
        public decimal ProdPrice { get; set; }

        public virtual ICollection<StoreInventory> StoreInventory { get; set; }
    }
}
