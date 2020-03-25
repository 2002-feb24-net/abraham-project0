using System;
using System.Collections.Generic;

namespace Project0
{
    public partial class OrderList
    {
        public OrderList()
        {
            ProductOrder = new HashSet<ProductOrder>();
        }

        public int LstOrderId { get; set; }
        public int? LstProdId { get; set; }

        public virtual Product LstProd { get; set; }
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
    }
}
