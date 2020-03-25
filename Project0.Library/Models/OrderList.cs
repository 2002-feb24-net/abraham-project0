using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Models
{
    public class OrderList
    {
        public int LstOrderId { get; set; }
        public List<Product> LstProducts { get; set; } = new List<Product>();
    }
}
