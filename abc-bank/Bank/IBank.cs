namespace abc_bank
{
    public interface IBank
    {
        CoreList<ICustomer> Customers { get; }
        IBankManager Manager { get;  }
        string Name { get; set; }
        BankingTimeZone TimeZone { get; set; }
        void AddCustomer(ICustomer customer);
        ICustomer AddCustomer(string socialSecurityNumber, string name);
        ICustomer GetCustomer(string socialSecurityNumber);
        string GetFirstCustomer();
        IBankManager SetBankManager(string name);
        void SetBankManager(IBankManager manager);
    }
}