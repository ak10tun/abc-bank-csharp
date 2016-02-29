using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class MaxiSavingsAccount : InterestBearingAccount, IInterestAccount
    {
        public MaxiSavingsAccount(decimal initialDeposit, InterestCounpoundType compoundeType = InterestCounpoundType.Daily, double defaultRate = 0.0) : base(initialDeposit)
        {
            base.DefaultInterestRate = defaultRate;
            base.Type = InterestAccountType.MaxiSavings;
            base.TypeName = "Maxi Savings Account";
        }


        // This is better to be exposed to the external runner. The user of the library should identify the rates rules.
        protected override void RegisterRateProviders()
        {
            IRateLimit limit = new TransactionDateLimit(TransactionType.Withdraw, 10, 0.05, 0.001);
            this.RateLimits.Add(limit);

        }
    }
}
