﻿using System;
using System.Collections.Generic;

namespace Project0.App
{
    public partial class ProductOrder
    {
        public ProductOrder()
        {
            OrderList = new HashSet<OrderList>();
        }

        public int OrderId { get; set; }
        public int? OrderCstmId { get; set; }
        public int? OrderStrId { get; set; }
        public DateTime? OrderOrdDate { get; set; }

        public virtual Customer OrderCstm { get; set; }
        public virtual StoreLocation OrderStr { get; set; }
        public virtual ICollection<OrderList> OrderList { get; set; }
    }
}
