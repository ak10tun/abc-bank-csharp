using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.Models;

namespace abc_bank.Services
{
    public interface ITransactionService
    {
        IAccount SourceAccount { get; }
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        void Transfer(IAccount remoteAccount, decimal amount);
    
    }
}
