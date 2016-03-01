using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public static class DateProvider
    {
        private static BankingTimeZone _TimeZone = BankingTimeZone.Local;

        public static void SetTimeZone(BankingTimeZone timeZone)
        {
            _TimeZone = timeZone;
        }
        public static DateTime Now()
        {
            switch (_TimeZone)
            {
                case BankingTimeZone.Local:
                    return DateTime.Now;
                case BankingTimeZone.Universal:
                    return DateTime.Now.ToUniversalTime();
                default:
                    return DateTime.Now;
            }
        }

        public static DateTime Range(int duration)
        {
            return Now().AddDays(duration);
        }
        
        public static int RollingYearDayCount(DateTime date)
        { 
            var _rolling1YearDate = new DateTime(date.Year +1 , date.Month, date.Day);
            int diff = (_rolling1YearDate - date).Days;

            return diff;
        }

        public static double Duration(DateTime startDate, DateTime endDate, PeriodUnit unit = PeriodUnit.Day)
        {
            ValidationHelper.DateTimeGreaterInequality(startDate, "startDate", endDate, "endDate");
            TimeSpan _span = endDate - startDate;


            return ((int)_span.TotalDays / (int)unit);

        }
    }
}
