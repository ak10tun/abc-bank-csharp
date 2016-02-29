using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class CheckingAccount : InterestBearingAccount, IInterestAccount
    {
        public CheckingAccount(decimal initialDeposit, InterestCounpoundType compoundeType = InterestCounpoundType.Daily, double defaultRate = 0.0) : base(initialDeposit)
        {
            base.DefaultInterestRate = defaultRate;
            base.Type = InterestAccountType.Checking;
            base.TypeName = "Checking Account";
        }


        // This is better to be exposed to the external runner. The user of the library should identify the rates rules.
        protected override void RegisterRateProviders()
        {
            this.RateLimits = null;
            this.DefaultInterestRate = 0.001;
        }
    }
}
