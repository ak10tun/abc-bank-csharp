namespace abc_bank
{
    public interface IMonetaryCycle
    {
        decimal AccruedInterest { get; set; }
        decimal AvailableBalance { get; set; }
        decimal ClosingBalance { get; set; }
        CoreList<ITransaction> InTransactions { get; set; }
        double InterestRate { get; set; }
        IPeriod Period { get; }
        decimal StartingBalance { get; set; }
    }
}