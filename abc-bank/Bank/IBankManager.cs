namespace abc_bank
{
    public interface IBankManager
    {
        IBank Bank { get; set; }
        string Name { get; }
        string CustomerSummary();
        decimal TotalInterestPaid();
    }
}