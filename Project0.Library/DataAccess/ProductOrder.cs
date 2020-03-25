using System;
using System.Collections.Generic;

namespace Project0
{
    public partial class ProductOrder
    {
        public int OrderId { get; set; }
        public int? OrderCstmId { get; set; }
        public int? OrderStrId { get; set; }
        public int? OrderOrdList { get; set; }
        public DateTime? OrderOrdDate { get; set; }

        public virtual Customer OrderCstm { get; set; }
        public virtual OrderList OrderOrdListNavigation { get; set; }
        public virtual StoreLocation OrderStr { get; set; }
    }
}
