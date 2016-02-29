using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using abc_bank.Services;

namespace abc_bank.Draft
{
    public interface IAccount
    {
        ITransactionService TransactionService { get; }
        IBalancerService BalancerService { get; }
        
        decimal AccruedInterest(DateTime date);   
        decimal AvailableBalance { get; }
        DateTime StartDate();
    }
}
