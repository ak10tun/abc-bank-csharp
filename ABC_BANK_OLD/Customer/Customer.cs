using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using abc_bank.Utils;
using abc_bank.Common;

namespace abc_bank.Draft.Models
{
    public class Customer : ICustomer
    {
        public string SocialSecurityNumber { get; private set; }
        protected String Name { get; private set; }
        public IEnumerable<IAccount> Accounts { get; private set; }

        public Customer(string socialSecurityNumber, string name)
        {
            ValidationHelper.StringNullEmpty(name, "name");
            ValidationHelper.StringNullEmpty(socialSecurityNumber, "socialSecurityNumber");

            string _socialSecurityNumber = ValidationHelper.SocialSecurityNumber(socialSecurityNumber);

            this.SocialSecurityNumber = _socialSecurityNumber;
            this.Name = name;
            this.Accounts = new List<IAccount>();
        }

        public IAccount OpenAccount(AccountType type, decimal initialDeposit)
        {
            switch(type)
            {
                case (AccountType.Checking):
                    return new SavingsAccount(initialDeposit);
            }

            return null;
        }

   

        public int OpenAccount(IAccount account)
        {
            return 0;
        }

        public IAccount OpenCheckingAccount(decimal initialDeposit)
        {
            return null;
        }


        public IAccount OpenSavingsAccount(decimal initialDeposit)
        {
            return null;
        }

        public IAccount OpenMaxiSavingsAccount(decimal initialDeposit)
        {
            return null;
        }





    }
}
