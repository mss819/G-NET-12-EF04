using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_NET_12_EF04.Models
{
    internal class Transaction
    {
        [Key]
        public int TransactionNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal Amount { get; set; }

        public string TransactionType { get; set; }

        public string Note { get; set; }

        // FK
        public string AccountNumber { get; set; }

        // Navigation
        public Account Account { get; set; }
    }
}
