using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public class Period : IPeriod
    {
        public DateTime StartDateTime { get; private set; }
        public DateTime EndDateTime { get; private set; }
        public int Duration { get; private set; }


        /// <summary>
        /// Need to check for the culture information.
        /// </summary>
        public Period(DateTime startDateTime, PeriodUnit periodUnit = PeriodUnit.Day)
        {
            this.StartDateTime = startDateTime;
            this.Duration = (int)periodUnit;
            this.EndDateTime = this.StartDateTime.AddDays(this.Duration);
        }
    }


}
