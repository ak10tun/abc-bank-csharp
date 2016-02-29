using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using abc_bank.Models;

namespace abc_bank.Draft.Services
{
    public interface ICustomerService
    {
        ICustomer AddCustomer();

        IAccount OpenAccount(ICustomer customer);

        IAccount OpenAccount(int socialSecurityNumber);

    }
}
