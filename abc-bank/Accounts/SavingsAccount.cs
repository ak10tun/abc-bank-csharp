using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace abc_bank
{
    public class SavingsAccount : InterestBearingAccount, IInterestAccount
    {
        
        public SavingsAccount(decimal initialDeposit, InterestCounpoundType compoundeType = InterestCounpoundType.Daily, double defaultRate = 0.0) : base(initialDeposit, compoundeType)
        {
            base.DefaultInterestRate = defaultRate;
            base.Type = InterestAccountType.Savings;
            base.TypeName = "Savings Account";
        }


        // This is better to be exposed to the external runner. The user of the library should identify the rates rules.
        protected override void RegisterRateProviders()
        {
            IRateLimit limit = new BalanceLimit(0, 1000, 0.001, 0.002);
            this.RateLimits.Add(limit);         
        }
    }
}
