using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_NET_12_EF04.Models
{
    internal class Customer
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public string NationalId { get; set; }

        public string CustomerType { get; set; }

        public ICollection<AccountCustomer> AccountCustomers { get; set; }
            = new HashSet<AccountCustomer>();
    }
}
