using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace abc_bank
{
    public class TransactionService : ITransactionService
    {
        public IAccount SourceAccount { get; private set; }
        public CoreList<ITransaction> Transactions { get; private set; }

        public TransactionService(IAccount sourceAccount)
        {
            ValidationHelper.NotNull(sourceAccount, "sourceAccount");
            this.SourceAccount = sourceAccount;
            this.Transactions = new CoreList<ITransaction>();
            this.Transactions.OnAdd += Transactions_OnAdd;
        }

        private void Transactions_OnAdd(object sender, EventArgs e)
        {
            this.SourceAccount.BalancerService.UpdateBalance((ITransaction)sender);
        }

        public void Deposit(decimal amount)
        {
            this.Deposit(amount, DateProvider.Now());
        }

        public void Deposit(decimal amount, DateTime date)
        {
            ValidationHelper.NegativeTransactionValue(amount, "amount");
            ValidationHelper.ZeroTransactionValue(amount, "amount");

            bool isInitial = false;

            if(this.Transactions.Count == 0)
            {
                isInitial = true;
            }
            this.Transactions.Add(new Transaction(amount, isInitial? TransactionType.InitialDeposit: TransactionType.Deposit, date));
        }

        public void Transfer(IAccount receivingAccount, decimal amount)
        {
            this.Transfer(receivingAccount, amount, DateProvider.Now());
        }

        public void Transfer(IAccount receivingAccount, decimal amount, DateTime date)
        {
            ValidationHelper.NotNull(receivingAccount, "receivingAccount");
            ValidationHelper.NegativeTransactionValue(amount, "amount");
            ValidationHelper.ZeroTransactionValue(amount, "amount");
            this.Transactions.Add(new Transaction(amount * -1, TransactionType.TransferOut, date));
            receivingAccount.TransactionService.Transactions.Add(new Transaction(amount, TransactionType.TransferIn, date));
        }

        public void Withdraw(decimal amount)
        {
            this.Withdraw(amount, DateProvider.Now());
        }

        public void Withdraw(decimal amount, DateTime date)
        {
            ValidationHelper.NegativeTransactionValue(amount, "amount");
            ValidationHelper.ZeroTransactionValue(amount, "amount");
            ValidationHelper.SufficientFunds(amount, this.SourceAccount.AvailableBalance, "amount");

            this.Transactions.Add(new Transaction(amount * -1, TransactionType.Withdraw, date));
        }


        


        /*
        public void Activity(TransactionType type, decimal amount)
        {
            throw new NotImplementedException();
        }

        public void Activity(TransactionType type, decimal amount, DateTime time)
        {
            throw new NotImplementedException();
        }
        */


    }
}
