using System;

namespace abc_bank
{
    public interface ITransaction
    {
        TransactionType Type { get; }
        decimal Value { get; }
        DateTime Date { get; }
    }
}
