using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abc_bank.Models;

namespace abc_bank.Services
{
    public class TransactionService : ITransactionService
    {
        public IAccount SourceAccount { get; private set; }

        public TransactionService(IAccount SourceAccount)
        {

        }

        public void Deposit(decimal amount)
        {
            throw new NotImplementedException();
        }

        public void Transfer(IAccount remoteAccount, decimal amount)
        {
            throw new NotImplementedException();
        }

        public void Withdraw(decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
