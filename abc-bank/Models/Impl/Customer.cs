using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using abc_bank.Helpers;

namespace abc_bank.Models
{
    public class Customer
    {
        public string SocialSecurityNumber { get; private set; }
        protected String Name { get; set; }
        public IEnumerable<IAccount> Accounts { get; private set; }

        internal Customer(string socialSecurityNumber, string name)
        {
            ValidationHelper.StringNotNullEmpty(name, "name");
            ValidationHelper.StringNotNullEmpty(socialSecurityNumber, "socialSecurityNumber");
            string _socialSecurityNumber = string.Empty;

            ValidationHelper.TryValidateSocialSecurityNumber(socialSecurityNumber, out _socialSecurityNumber);

            this.SocialSecurityNumber = _socialSecurityNumber;
            this.Name = name;
            this.Accounts = new List<IAccount>();
        }
    }
}
