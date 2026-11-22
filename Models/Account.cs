using Session_04_EF_Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_04_EF_Core.Models
{
    public class Account
    {
        public string AccountNumber { get; set; } = string.Empty;

        public decimal CurrentBalance { get; set; }

        public AccountType AccountType { get; set; }

        public DateTime OpeningDate { get; set; }

        // Foreign Key
        public int BranchCode { get; set; }

        // Navigation Properties
        public Branch Branch { get; set; } = null!;

        public ICollection<CustomerAccount> CustomerAccounts { get; set; }
            = new List<CustomerAccount>();

        public ICollection<Transaction> Transactions { get; set; }
            = new List<Transaction>();
    }
}
