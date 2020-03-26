using System;
using System.Collections.Generic;

namespace Project0.App
{
    public partial class StoreInventory
    {
        public int InvId { get; set; }
        public int? InvProdId { get; set; }
        public int? InvStoreLoc { get; set; }
        public int InvProdInventory { get; set; }

        public virtual Product InvProd { get; set; }
        public virtual StoreLocation InvStoreLocNavigation { get; set; }
    }
}
