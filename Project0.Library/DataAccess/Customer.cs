using System;
using System.Collections.Generic;

namespace Project0
{
    public partial class Customer
    {
        public Customer()
        {
            ProductOrder = new HashSet<ProductOrder>();
        }

        public int CstmId { get; set; }
        public string CstmFirstName { get; set; }
        public string CstmLastName { get; set; }
        public string CstmEmail { get; set; }
        public int? CstmDefaultStoreLoc { get; set; }

        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
    }
}
