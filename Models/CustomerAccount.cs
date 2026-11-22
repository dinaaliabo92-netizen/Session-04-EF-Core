using Session_04_EF_Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_04_EF_Core.Models
{
    public class CustomerAccount
    {
       
        public string AccountNumber { get; set; } = string.Empty;

        public int CustomerId { get; set; }

        
        public OwnershipType OwnershipType { get; set; }

        public DateTime OwnershipStartDate { get; set; }

        public AccountStatus AccountStatus { get; set; }

        public Account Account { get; set; } = null!;

        public Customer Customer { get; set; } = null!;
    }
}
