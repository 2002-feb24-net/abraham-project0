using System;
using System.Collections.Generic;
using System.Text;

namespace Project0.Library.Models
{
    public class Customer
    {
        public int CstmId { get; set; }
        public string CstmFirstName { get; set; }
        public string CstmLastName { get; set; }
        public string CstmEmail { get; set; }
        public int CstmDefaultStoreLoc { get; set; }
    }
}
