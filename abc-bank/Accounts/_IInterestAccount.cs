using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public interface IInterestAccount
    {
        long Id { get; }
        IList<IRateLimit> RateLimits { get; }
        DateTime StartDate { get;}
        double DefaultInterestRate { get; }
        decimal AvailableBalance { get; }
        InterestAccountType Type { get; }
        string TypeName { get; }
        CoreList<MonetaryCycle> MonetaryCycles { get; }
        InterestAccountType GetAccountType();
        string GetStatement();
        decimal InterestEarned();
        decimal InterestEarned(DateTime date);

    }
}
