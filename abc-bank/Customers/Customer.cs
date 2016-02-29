using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace abc_bank
{
    public class Customer : ICustomer, IStatementViewable
    {
        public string SocialSecurityNumber { get; private set; }
        public String Name { get; private set; }
        public CoreList<IInterestAccount> InterestAccounts { get; private set; }

        public Customer(string socialSecurityNumber, string name)
        {
            ValidationHelper.StringNullEmpty(name, "name");
            ValidationHelper.StringNullEmpty(socialSecurityNumber, "socialSecurityNumber");

            string _socialSecurityNumber = ValidationHelper.SocialSecurityNumber(socialSecurityNumber);
            this.SocialSecurityNumber = _socialSecurityNumber;
            this.Name = name;
            this.InterestAccounts = new CoreList<IInterestAccount>();
        }

        public string GetName()
        {
            return this.Name;
        }

        public int GetNumberOfAccounts()
        {
            int _count = 0;

            if (this.InterestAccounts != null)
            {
                _count =  this.InterestAccounts.Count();
            }

            return _count;
        }


        public decimal TotalInterestEarned()
        {
            decimal _total = 0;

            if (this.InterestAccounts != null)
            {
               if(this.InterestAccounts.Count() > 0)
                {
                    _total = this.InterestAccounts.Sum(x => x.InterestEarned());
                }
            }

            return _total;
        }

        public IInterestAccount OpenInterestAccount(InterestAccountType type, decimal initialDeposit, InterestCounpoundType compoundType = InterestCounpoundType.Daily, double defaultRate = 0.0)
        {
            ValidationHelper.NotNull(type, "type");
            ValidationHelper.NegativeNumeric<decimal>(initialDeposit, "initialDeposit");
            ValidationHelper.NegativeNumeric<double>(defaultRate, "defaultRate");

            IInterestAccount _account = null;

            switch (type)
            {
                case (InterestAccountType.Checking):
                    _account = new CheckingAccount(initialDeposit, compoundType, defaultRate);
                    break;

                case (InterestAccountType.Savings):
                    _account = new SavingsAccount(initialDeposit, compoundType, defaultRate);
                    break;

                case (InterestAccountType.MaxiSavings):
                    _account = new MaxiSavingsAccount(initialDeposit, compoundType, defaultRate);
                    break;
                default:
                    throw new Exception("Account type is not yet supported");
            }

            this.InterestAccounts.Add(_account);
            return _account;
        }

        public void OpenInterestAccount(IInterestAccount account)
        {
            ValidationHelper.NotNull(account, "account");
            this.InterestAccounts.Add(account);
        }

        public string GetStatement()
        {
            decimal _total = 0;
            string _statement = "Statement for " + this.Name + "\n";

            if (this.InterestAccounts != null)
            {
                if (this.InterestAccounts.Count() > 0)
                {
                    foreach (IInterestAccount account in this.InterestAccounts)
                    {
                        _statement += "\n" + account.GetStatement() + "\n";
                        _total += account.MonetaryCycles.Last().AvailableBalance;
                    }
                }
            }

            _statement += "\nTotal In All Accounts " + _total.ToDollars();
            return _statement;
        }
    }
}