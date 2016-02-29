using System;
using System.Linq;
using System.Threading;

namespace abc_bank
{
    public class Bank : IBank
    {
        public string Name { get; set; }
        public CoreList<ICustomer> Customers { get; private set; }
        public BankingTimeZone TimeZone { get; set; }
        private static ReaderWriterLockSlim _slimLock = new ReaderWriterLockSlim();
        public IBankManager Manager { get; private set; }

        private static IBank _Bank;

        public static IBank Open(string name, BankingTimeZone timeZone = BankingTimeZone.Local)
        {
            _slimLock.EnterWriteLock();
            try {
                if (_Bank == null)
                {
                    _Bank = new Bank(name, timeZone);
                }
            }
            catch { throw; }
            finally { if (_slimLock.IsWriteLockHeld) _slimLock.ExitWriteLock(); } 

           return _Bank;
        }

        public IBankManager SetBankManager(string name)
        {
            ValidationHelper.StringNullEmpty(name, "name");
            this.Manager = new BankManager(name, this);
            return this.Manager;
        }

        public void SetBankManager(IBankManager manager)
        {
            ValidationHelper.NotNull(manager, "manager");
            this.Manager = manager;
            manager.Bank = this;
        }
        private Bank(string name, BankingTimeZone timeZone = BankingTimeZone.Local)
        {
           this.Name = name;
           DateProvider.SetTimeZone(timeZone);
           this.Customers = new CoreList<ICustomer>();
        }

        // Do not generate exception if the customer exists.
        public void AddCustomer(ICustomer customer)
        {
            ValidationHelper.NotNull(customer, "customer");
            Exception _exception = null;

            if (!_slimLock.IsReadLockHeld) _slimLock.EnterUpgradeableReadLock();

            var _customer = this.Customers.FirstOrDefault(x => x.SocialSecurityNumber == customer.SocialSecurityNumber);

            if (_customer == null)
            {
                _slimLock.EnterWriteLock();
                this.Customers.Add(_customer);
                if (_slimLock.IsWriteLockHeld) _slimLock.ExitWriteLock();
                if (_slimLock.IsUpgradeableReadLockHeld) _slimLock.ExitUpgradeableReadLock();
            }
            else
            {
               _exception = new DuplicateCustomerException("Customer already exists.");
            }


            if(_exception != null)
            {
                throw _exception;
            }
        }

        public ICustomer AddCustomer(string socialSecurityNumber, string name)
        {
            ValidationHelper.StringNullEmpty(socialSecurityNumber, "socialSecurityNumber");
            string _socialSecurityNumber = ValidationHelper.SocialSecurityNumber(socialSecurityNumber);
            ValidationHelper.StringNullEmpty(socialSecurityNumber, "name");
            if (!_slimLock.IsReadLockHeld) _slimLock.EnterUpgradeableReadLock();

            var _customer = this.Customers.FirstOrDefault(x => x.SocialSecurityNumber == _socialSecurityNumber);          

            if (_customer == null)
            {
                _slimLock.EnterWriteLock();
                _customer = new Customer(socialSecurityNumber, name);
                this.Customers.Add(_customer);
                if(_slimLock.IsWriteLockHeld) _slimLock.ExitWriteLock();
                if (_slimLock.IsUpgradeableReadLockHeld) _slimLock.ExitUpgradeableReadLock();
            }
            else
            {
                throw new DuplicateCustomerException("Customer already exists.");
            }

            return _customer;
        }

        public string GetFirstCustomer()
        {
            if (this.Customers != null)
            {
                if (this.Customers.Count() > 0)
                {
                    return this.Customers.First().GetName();
                }
            }

            throw new Exception("There are no custommers in the system");
        }

        public ICustomer GetCustomer(string socialSecurityNumber)
        {
            ValidationHelper.StringNullEmpty(socialSecurityNumber, "socialSecurityNumber");
            string _socialSecurityNumber = ValidationHelper.SocialSecurityNumber(socialSecurityNumber);

            var _customer = this.Customers.FirstOrDefault(x => x.SocialSecurityNumber == _socialSecurityNumber);

            return _customer;
        }
    }
}
