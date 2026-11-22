using Session_04_EF_Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_04_EF_Core.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Address { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string NationalId { get; set; } = string.Empty;

        public CustomerType CustomerType { get; set; }

        // Navigation Property
        public ICollection<CustomerAccount> CustomerAccounts { get; set; }
            = new List<CustomerAccount>();
    }
}
