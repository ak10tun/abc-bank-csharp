using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using abc_bank.Helpers;
using abc_bank.Common;

namespace abc_bank.Models
{
    public class Transaction:ITransaction
    {
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public TransactionType Type { get; private set; }

        public Transaction(decimal amount, TransactionType type)
        {
            ValidationHelper.IsNegativeNumeric<decimal>(amount, "amount");
            this.Amount = amount;
            this.Date = DateTimeHelper.DateProvider.Now(BankingTimeZone.Local);
            this.Type = type;
        }

        public override string ToString()
        {
            return this.Type.ToString();
        }
    }
    
}
