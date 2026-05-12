using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_NET_12_EF04.Models
{
    internal class AccountCustomer
    {
        public int CustomerId { get; set; }

        public string AccountNumber { get; set; }

        public string OwnershipType { get; set; }

        public DateTime OwnershipStartDate { get; set; }

        public bool AccountStatus { get; set; }

        public Customer Customer { get; set; }

        public Account Account { get; set; }
    }
}
