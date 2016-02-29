using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class BalanceLimit:IRateLimit
    {
        public decimal Floor { get; }
        public decimal Ceiling { get; }
        public double DefaultRate { get; }
        public double LimitRate { get; }
        public InterestRuleType Type { get; }

        public BalanceLimit(decimal floor, decimal ceiling,  double limitRate, double defaultRate = 0.0)
        {
            this.Type = InterestRuleType.BalanceLimit;
            this.Floor = floor;
            this.Ceiling = ceiling;
            this.LimitRate = limitRate;
            this.DefaultRate = defaultRate;
        }
    }
}
