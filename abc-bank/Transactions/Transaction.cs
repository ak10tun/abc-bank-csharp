using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace abc_bank
{
    public class Transaction:ITransaction
    {
        public decimal Value { get; private set; }
        public DateTime Date { get; private set; }
        public TransactionType Type { get; private set; }

        public Transaction(decimal amount, TransactionType type):this(amount, type, DateProvider.Now())
        {
        }

        public Transaction(decimal amount, TransactionType type, DateTime date)
        {
            ValidationHelper.NegativeNumeric<decimal>(amount, "amount");
            this.Value = amount;
            this.Date = date;
            this.Type = type;
        }

        public override string ToString()
        {
            return this.Type.ToString();
        }
    }
    
}
