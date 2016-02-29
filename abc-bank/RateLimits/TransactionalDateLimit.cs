using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class TransactionDateLimit:IRateLimit
    {
        public int PastDuration { get; }
        public TransactionType TransactionType { get; }
        public double DefaultRate { get; }
        public double LimitRate { get; }
        public InterestRuleType Type { get; }

        public TransactionDateLimit(TransactionType transactionType, int pastDuration, double limitRate, double defaultRate = 0.0)
        {
            this.TransactionType = transactionType;
            this.DefaultRate = defaultRate;
            this.PastDuration = pastDuration;
            this.LimitRate = limitRate;
            this.DefaultRate = defaultRate;
        }

    }
}
