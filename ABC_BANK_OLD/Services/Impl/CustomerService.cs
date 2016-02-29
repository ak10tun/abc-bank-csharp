using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using abc_bank.Models;
using abc_bank.Utils;
using System.Threading;

namespace abc_bank.Draft.Services
{
    public class CustomerService:ICustomerService
    {
        public ICustomer AddCustomer()
        {
            throw new NotImplementedException();
        }
        /*
        public ICustomer AddCustomer(IBank bank, string socialSecurityNumber, string name)
        {
            ValidationHelper.NotNull(bank, "bank");
            ValidationHelper.StringNullEmpty(socialSecurityNumber, "socialSecurityNumber");
            ValidationHelper.StringNullEmpty(name, "name");

            ReaderWriterLockSlim _slimLock = new ReaderWriterLockSlim();

            Customer _customer = null;
            int _socialSecurityId = -1;

            if (ValidationHelper.TryValidateSocialSecurityNumber(socialSecurityNumber, out _socialSecurityId))
            {
                try
                {
                    if (_slimLock.TryEnterUpgradeableReadLock(5000))
                    {
                        // Returns the first occurance if customer already exists.
                        _customer = bank.Customers.Find(x => x.SocialSecurityNumber == socialSecurityNumber);

                        if (_customer == null)
                        {
                            if (_slimLock.TryEnterWriteLock(5000))
                            {
                                _customer = new  Customer(_socialSecurityId, name);
                                bank.Customers.Add(_customer);
                            }
                        }
                    }
                }
                catch { throw; }
                finally { _slimLock.ExitWriteLock(); _slimLock.ExitUpgradeableReadLock(); }

            }
            else
            {
                throw new SocialSecurityFormatException();
            }

            return _customer;
        }
        */
    

        public IAccount OpenAccount(ICustomer customer)
        {
            return null;
        }

        public IAccount OpenAccount(int socialSecurityNumber)
        {
            return null;
        }
    }
}
