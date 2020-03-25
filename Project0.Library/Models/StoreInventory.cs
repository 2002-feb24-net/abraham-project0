using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Models
{
    public class StoreInventory
    {
        public int InvId { get; set; }
        public Product InvProduct { get; set; }
        public StoreLocation InvStoreLoc { get; set; }
        public int InvProdInventory { get; set; }
    }
}
