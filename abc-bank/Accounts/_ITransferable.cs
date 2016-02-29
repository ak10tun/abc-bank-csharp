using System;

namespace abc_bank
{
    public interface ITransferable
    {
        void Transfer(IInterestAccount receivingAccount, decimal amount, DateTime date);
        void Transfer(IInterestAccount receivingAccount, decimal amount);

    }
}