using System.Collections.Generic;

namespace abc_bank.Draft
{
    public interface IBalancer
    {
        IPeriod Period { get; }
        decimal StartBalance { get; set; }
        List<ITransaction> Transactions { get; set; }
        decimal AvailableBalance { get; set; }
        double InterestRate { get; set; }
        decimal AccruedInterest { get; set; }
        decimal EndBalance { get; set; }
    }
}