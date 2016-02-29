using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Draft.Models
{
    public abstract class PeriodAccruedInterest
    {
        public IPeriod Period { get; }
        public decimal BeginBalance { get; }
        public abstract decimal Generate(decimal beginBalance, double interestRate, IPeriod period);
    }
}
