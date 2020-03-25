using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Models
{
    public class ProductOrder
    {
        public int OrderId { get; set; }
        public Customer OrderCstm { get; set; }
        public StoreLocation OrderStrLoc { get; set; }
        public OrderList OrdList { get; set; }
        public DateTime OrderOrdDate { get; set; }
    }
}
