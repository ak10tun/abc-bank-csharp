using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public interface IRateLimit
    {
       InterestRuleType Type { get; }
        double DefaultRate { get; }
        double LimitRate { get; }

    }
}
