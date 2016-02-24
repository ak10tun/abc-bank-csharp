using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using abc_bank.Common;
using abc_bank.Helpers;

namespace abc_bank.Models
{
    public class InvestmentPeriod : IPeriod
    {

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int Duration { get; private set; }


        /// <summary>
        /// Need to check for the culture information.
        /// </summary>
        public InvestmentPeriod(DateTime startDate, DateTime endDate, PeriodUnit periodUnit = PeriodUnit.Day)
        {
            ValidationHelper.DateTimeGreaterInequality(startDate, "startDate", endDate, "endDate");
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Duration = (endDate - startDate).Days / (int)periodUnit;                ;
        }
    }


}
