using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_NET_12_EF04.Models
{
    internal class Branch
    {
        [Key]
        public string Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public Manager Manager { get; set; }

        public ICollection<Account> Accounts { get; set; } = new HashSet<Account>();













    }
}
