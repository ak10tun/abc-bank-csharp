using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace abc_bank
{
    public abstract class InterestBearingAccount : TransactionalAccountBase, IInterestBearable, IStatementViewable
    {
        public IList<IRateLimit> RateLimits { get; protected set; }
        public CoreList<MonetaryCycle> MonetaryCycles { get; private set; }
        public InterestAccountType Type { get; protected set; }

        protected InterestCounpoundType CompoundType { get; }


        public InterestBearingAccount(decimal initialDeposit, InterestCounpoundType compoundeType = InterestCounpoundType.Daily) : base(initialDeposit)
        {
            MonetaryCycles = new CoreList<MonetaryCycle>();
            this._CreateCycle(DateProvider.Now().Date);
            UpdateCycle(this.Transactions[0]);

            RateLimits = new List<IRateLimit>();
            this.CompoundType = compoundeType;
            this.RegisterRateProviders();
        }

        protected abstract void RegisterRateProviders();

        public decimal InterestEarned()
        {
            return this.InterestEarned(DateProvider.Now());
        }

        public decimal InterestEarned(DateTime date)
        {
            try
            {
                this.UpdateCycle(date);
                return this.MonetaryCycles.Sum(x => x.AccruedInterest);
            }
            catch { throw; }
        }

        public InterestAccountType GetAccountType()
        {
            return this.Type;
        }

        public override decimal GetBalance()
        {
            return this.GetBalance(DateProvider.Now());
        }

        public override decimal GetBalance(DateTime date)
        {
            this.UpdateCycle(date);
            return this.MonetaryCycles.Last().AvailableBalance;
        }

        #region RateProcessing

        public double GetEffectiveRate()
        {
            return this.GetEffectiveRate(DateProvider.Now());
        }

        public double GetEffectiveRate(DateTime date)
        {
            double? _rate = GetRate(date, this.GetBalance(date));

            if (_rate.HasValue)
            {
                return _rate.Value;
            }
            else
            {
               return this.DefaultInterestRate;
            }
        }



        protected double? GetRate(DateTime date, decimal balance)
        {
            double? _rate = 0.0;

            if(this.RateLimits == null)
            {
                return this.DefaultInterestRate;
            }

            foreach( IRateLimit limit in this.RateLimits)
            {
                switch (limit.Type)
                {
                    case InterestRuleType.BalanceLimit:
                        _rate = this.GetBalanceLimitRate(balance, (BalanceLimit) limit);
                        break;
                    case InterestRuleType.TransactionLimit:
                        _rate = this.GetTransactionalLimitRate(date, (TransactionDateLimit)limit);
                        break;
                    default:
                        throw new Exception("The type is not registered.");
                }
            }

            return _rate;
        }

        protected double? GetTransactionalLimitRate(DateTime limitDate, TransactionDateLimit limit)
        {
            ReaderWriterLockSlim _slimLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            _slimLock.EnterReadLock();

            double? _rate = limit.DefaultRate;
       
            //transactions.Where(x=> (date - x.Date).TotalDays >= 10).Where(y=> y.Type == limit.Type).First()
            foreach (var transaction in this.Transactions)
            {
                if (transaction.Type == limit.TransactionType)
                {
                    if ((limitDate - transaction.Date).TotalDays <= limit.PastDuration)
                    {
                        _rate = limit.LimitRate;
                    }
                }
            }
            if (_slimLock.IsReadLockHeld) _slimLock.ExitReadLock();
            return _rate;
        }

        protected double? GetBalanceLimitRate(decimal balance, BalanceLimit limit)
        {
            ReaderWriterLockSlim _slimLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            _slimLock.EnterReadLock();

            double? _rate = limit.DefaultRate;
 
            if (balance >= limit.Floor && balance <= limit.Ceiling)
            {
                _rate = limit.LimitRate;
            }
            else
            {
                _rate = limit.DefaultRate;
            }
            if (_slimLock.IsReadLockHeld) _slimLock.ExitReadLock();
            return _rate;
        }
        
        #endregion

        #region CycleProcessing

        protected MonetaryCycle _FindCycle(DateTime date)
        {
            ReaderWriterLockSlim _slimLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            _slimLock.EnterReadLock();
            MonetaryCycle _cycle = this.MonetaryCycles.FirstOrDefault(x => x.Period.StartDateTime.Date == date.Date);
            if(_slimLock.IsReadLockHeld)  _slimLock.ExitReadLock();
            return _cycle;
        }

        protected MonetaryCycle _CreateCycle(DateTime date)
        {
            ReaderWriterLockSlim _slimLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            _slimLock.TryEnterWriteLock(1000);
            try {

                if (date.Date < this.StartDate.Date)
                {
                    throw new Exception("Cannot add a cycle before account generation.");
                }

                MonetaryCycle _cycle = null;

                if (this.MonetaryCycles.Count == 0)
                {
                    // Opening the account or another initializing event.
                    _cycle = new MonetaryCycle(date.Date);
                }
                else
                {

                    // if the cycle is already registered.
                    _cycle = this._FindCycle(date);

                    // Register new cycle.

                    if (_cycle == null)
                    {
                        decimal _previousClosingBalance = this.MonetaryCycles.Last().ClosingBalance;
                        _cycle = new MonetaryCycle(date, _previousClosingBalance);
                    }

                }

                this.MonetaryCycles.Add(_cycle);
                //this.AvailableBalance = _cycle.AvailableBalance;
                return _cycle;
            }
            catch { throw; }
            finally { if (_slimLock.IsWriteLockHeld) _slimLock.ExitWriteLock(); }
        }
        
        // Cycles are updated when a balance query is set or a transaction is set.
        // Any type of account related activity (transaction, balance checking) will trigger cycle update.

        protected void UpdateCycle(ITransaction transaction)
        {
            MonetaryCycle _cycle = this._FindCycle(transaction.Date);

            if (_cycle == null)
            {
                UpdateCycle(transaction.Date);
                _cycle = this._FindCycle(transaction.Date);
            }

            _cycle.InTransactions.Add(transaction);
            _cycle.AvailableBalance += transaction.Value;
        }

        protected void UpdateCycle(DateTime date)
        {
            ReaderWriterLockSlim _slimLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

            _slimLock.TryEnterWriteLock(1000);

            try
            {
                IMonetaryCycle _cycleOut = this._FindCycle(date);

                if (_cycleOut == null)
                {
                    while (this.MonetaryCycles.Last().Period.StartDateTime.Date < date.Date)
                    {
                        IMonetaryCycle _cycleIn = this._CreateCycle(this.MonetaryCycles.Last().Period.StartDateTime.AddDays(1));
                        this._UpdateAccruedInterest(ref _cycleIn);
                        /*
                        if (this.MonetaryCycles.Last().Period.StartDateTime.Date != date)
                        {
                            this._UpdateAccruedInterest(ref _cycleIn);
                        }
                         this.AvailableBalance = this.MonetaryCycles.Last().AvailableBalance; 
                        */

                    }
                }
                else
                {
                    this._UpdateAccruedInterest(ref _cycleOut);
                }
            }
            catch { throw; }
            finally { if (_slimLock.IsWriteLockHeld) _slimLock.ExitWriteLock(); }
        }

        private void _UpdateAccruedInterest(ref IMonetaryCycle _cycle)
        {
            double? _rateIn = GetRate(_cycle.Period.StartDateTime.Date, _cycle.AvailableBalance);
            _cycle.InterestRate = _rateIn.Value;
            if (_rateIn.HasValue) _cycle.AccruedInterest = this.GetCycleInterest(_cycle.AvailableBalance, _rateIn.Value);
            _cycle.ClosingBalance = _cycle.AccruedInterest + _cycle.AvailableBalance;
        }

        protected decimal GetCycleInterest(decimal balance, double rate)
        {
            return (balance * (decimal)rate / ((int)this.CompoundType));
        }

        #endregion

        #region statements

        public string GetStatement()
        {
            DateTime _date = DateProvider.Now();
            UpdateCycle(_date);

            String _report = this.TypeName + "\n";
            foreach (Transaction transaction in this.Transactions)
            {
                _report += " " + transaction.Type.ToString() + " " + transaction.Value.ToDollars() + "\n";
            }

            _report += "Total " + this.MonetaryCycles.Last().AvailableBalance.ToDollars();

            return _report;
        }

        public override void Transactions_OnAdd(object sender, EventArgs e)
        {
            UpdateCycle((ITransaction)sender);
        }

        #endregion
    }
}

 