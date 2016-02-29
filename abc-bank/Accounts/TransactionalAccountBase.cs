using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public abstract class TransactionalAccountBase: AccountBase, IDepositable, IWithdrawable, ITransferable, IDisposable
    {
        public CoreList<ITransaction> Transactions { get; protected set; }

        public TransactionalAccountBase(decimal initialDeposit) : base(initialDeposit)
        {
            this.Transactions = new CoreList<ITransaction>();
            this.Transactions.OnAdd += Transactions_OnAdd;
        }

        private void Transactions_OnAdd(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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

            if (this.Transactions.Count == 0)
            {
                isInitial = true;
            }
            this.Transactions.Add(new Transaction(amount, isInitial ? TransactionType.InitialDeposit : TransactionType.Deposit, date));
        }

        public void Transfer(IInterestAccount receivingAccount, decimal amount)
        {
            this.Transfer(receivingAccount, amount, DateProvider.Now());
        }

        public void Transfer(IInterestAccount receivingAccount, decimal amount, DateTime date)
        {
            ValidationHelper.NotNull(receivingAccount, "receivingAccount");
            ValidationHelper.NegativeTransactionValue(amount, "amount");
            ValidationHelper.ZeroTransactionValue(amount, "amount");
            this.Transactions.Add(new Transaction(amount * -1, TransactionType.TransferOut, date));
            this.Transactions.Add(new Transaction(amount, TransactionType.TransferIn, date));
        }

        public void Withdraw(decimal amount)
        {
            this.Withdraw(amount, DateProvider.Now());
        }

        public void Withdraw(decimal amount, DateTime date)
        {
            ValidationHelper.NegativeTransactionValue(amount, "amount");
            ValidationHelper.ZeroTransactionValue(amount, "amount");
            ValidationHelper.SufficientFunds(amount, this.AvailableBalance, "amount");

            this.Transactions.Add(new Transaction(amount * -1, TransactionType.Withdraw, date));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                if (Transactions != null) Transactions.Dispose();           
        }

        ~TransactionalAccountBase()
        {
            Dispose(false);
        }
    }
}
