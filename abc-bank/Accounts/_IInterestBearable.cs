using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public interface IInterestBearable
    {
        IList<IRateLimit> RateLimits { get; }
        decimal InterestEarned(DateTime date);

    }
}
