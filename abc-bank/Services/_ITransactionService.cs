using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.Models;

namespace abc_bank
{
    public interface ITransactionService:IActivity
    {
        IAccount SourceAccount { get; }
        CoreList<ITransaction> Transactions { get; }

    }
}
