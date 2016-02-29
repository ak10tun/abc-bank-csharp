using System;

namespace abc_bank.Draft
{
    public interface ITransferable
    {
        void Transfer(IAccount receivingAccount, decimal amount, DateTime date);
        void Transfer(IAccount receivingAccount, decimal amount);

    }
}