using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    
    public class MonetaryCycle : IMonetaryCycle
    {        
        public IPeriod Period { get; private set; }
        public decimal StartingBalance { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal AccruedInterest { get; set; }
        public CoreList<ITransaction> InTransactions { get; set; }
        public double InterestRate { get; set;}
        public decimal ClosingBalance { get; set; }

        public MonetaryCycle (DateTime date, decimal startingBalance = 0, double interestRate = 0.0 )
        {
            this.Period = new Period(date);
            this.StartingBalance = startingBalance;
            this.AvailableBalance = this.StartingBalance;
            this.ClosingBalance = this.StartingBalance;
            this.AccruedInterest = 0;
            this.InTransactions = new CoreList<ITransaction>();
            this.InterestRate = interestRate;
        }

    }
}
