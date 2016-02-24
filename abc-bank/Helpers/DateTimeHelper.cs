using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using abc_bank.Common;

namespace abc_bank.Helpers
{
    public static class DateTimeHelper
    {
        public static class DateProvider
        {
            public static DateTime Now(BankingTimeZone timeZone = BankingTimeZone.Local)
            {
                switch (timeZone)
                {
                    case BankingTimeZone.Local:
                        return DateTime.Now;
                    case BankingTimeZone.Universal:
                        return DateTime.Now.ToUniversalTime();
                    default:
                        return DateTime.Now;
                }
            }

        }
    }
}
