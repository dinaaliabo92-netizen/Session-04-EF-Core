using Session_04_EF_Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session_04_EF_Core.Models
{
    public class Transaction
    {
        public int TransactionNumber { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal Amount { get; set; }

        public TransactionType TransactionType { get; set; }

        public string Note { get; set; } = string.Empty;

        // Foreign Key
        public string AccountNumber { get; set; } = string.Empty;

        // Navigation Property
        public Account Account { get; set; } = null!;
    }
}
