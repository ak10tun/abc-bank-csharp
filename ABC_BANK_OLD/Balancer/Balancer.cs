using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Draft
{
    public class Balancer: IBalancer
    {
       public IPeriod Period { get; private set; }
       public decimal StartBalance { get; private set; }
       public ITransaction Transactions { get; private set; }
       public decimal AdjustedStartBalance { get; private set; }
       public double InterestRate { get; private set; }
       public decimal AccruedInterest { get; private set; }
       public decimal EndBalance { get; private set; }
    }
}
