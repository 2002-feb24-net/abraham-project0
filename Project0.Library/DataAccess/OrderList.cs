using System;
using System.Collections.Generic;

namespace Project0.App
{
    public partial class OrderList
    {
        public int? LstOrderId { get; set; }
        public int? LstProdId { get; set; }

        public virtual ProductOrder LstOrder { get; set; }
        public virtual Product LstProd { get; set; }
    }
}
