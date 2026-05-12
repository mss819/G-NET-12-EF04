using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_NET_12_EF04.Models
{
    internal class Account
    {
        [Key]
        public string AccountNumber { get; set; }

        public decimal CurrentBalance { get; set; }

        public string AccountType { get; set; }

        public DateTime OpeningDate { get; set; }

        public string BranchCode { get; set; }

        public Branch Branch { get; set; }

        public ICollection<AccountCustomer> AccountCustomers { get; set; }
            = new HashSet<AccountCustomer>();

        public ICollection<Transaction> Transactions { get; set; }
            = new HashSet<Transaction>();
    }
}
