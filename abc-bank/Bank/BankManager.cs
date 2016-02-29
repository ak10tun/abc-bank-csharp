using System.Linq;

namespace abc_bank
{
    public class BankManager : IInterestPaidViewable, ICustomerSummaryViewable, IBankManager
    {
        public string Name { get; }
        public IBank Bank { get; set; }

        public BankManager(string name, IBank bank)
        {
            ValidationHelper.StringNullEmpty(name, "name");
            ValidationHelper.NotNull(bank, "bank");

            this.Name = name;
            this.Bank = bank;
        }

        public decimal TotalInterestPaid()
        {
            decimal _paid = 0;

            if (this.Bank.Customers != null)
            {
                if (this.Bank.Customers.Count() > 0)
                {
                    //  Alternative
                    // _paid = this.Bank.Customers.Sum(x => x.InterestAccounts.Sum(y => y.InterestEarned()));

                    foreach (ICustomer customer in this.Bank.Customers)
                    {
                        if (customer.InterestAccounts != null)
                        {
                            if (customer.InterestAccounts.Count > 0)
                            {
                                foreach (IInterestAccount account in customer.InterestAccounts)
                                {
                                    _paid += account.InterestEarned();
                                }
                            }
                        }
                    }
                }
            }

            return _paid;
        }

        public string CustomerSummary()
        {
            string summary = "Customer Summary";
            foreach (ICustomer customer in this.Bank.Customers)
            {
                summary += "\n - " + customer.GetName() + " (" + StringHelper.FormatStringPlurality(customer.GetNumberOfAccounts(), "account") + ")";
            }
            return summary;
        }
    }
}
