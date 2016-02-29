using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using abc_bank.Utils;
using abc_bank.Common;
using abc_bank.Services;

namespace abc_bank.Draft
{
    public class SavingsAccount : AccountBase, IAccount
    {

        public ITransactionService TransactionService { get; private set; }
        public IBalancerService BalancerService { get; private set; }
        


        public decimal AvailableBalance { get; private set; }

        public SavingsAccount(decimal initialDeposit):base(initialDeposit)
        {       
            this.TransactionService = new TransactionService(this);
            this.BalancerService = new BalancerService(this);     
            this.TransactionService.Deposit(initialDeposit);  
            this.BalancerService.Balancer.Add(new Balancer
                { AccruedInterest = this.PeriodInterestRate,
                  AdjustedStartBalance  = initialDeposit,
                  EndBalance = AccruedInterest() 
        }

        
        public DateTime StartDate()
        {
            return base._StartDate;
        }

        public decimal GetBalance()
        {
            this.BalancerService.
        }

        public decimal GetBalance(DateTime date)
        {

        }


        public decimal GetAccruedInterest()
        {

        }


        public decimal GetAccruedInterest(DateTime date)
        {

        }


        public double PeriodInterestRate(IBalancer balancer)
        {
            double _rate = 0.002;

            if (balancer.AdjustedStartBalance < 1000)
            {
                _rate = 0.001;
            }

            return _rate;
        }


        private List<IPeriod> GetPeriods(DateTime startDate, DateTime endDate)
        {
            List<IPeriod> iList = new List<IPeriod>();
            for(DateTime i = startDate; i< endDate; i.AddDays(1))
            {
                iList.Add(new InvestmentPeriod(i, i.AddDays(1), Common.PeriodUnit.Day));
            }

            return iList;
        }



        public decimal AccruedInterest(DateTime date)
        {
            int numberOfPeriods = (int)DateProvider.Duration(this._StartDate, date);

            decimal balance = Transactions.Find(x => x.Type == TransactionType.InitialDeposit).Amount;
            decimal accruedInterest = 0;

            List<IPeriod> periods = Periods(this._StartDate, date);

            for (int i=1; i<=periods.Count(); i++)
            {
                var u = Transactions.Find(x => x.Date == periods[i].StartDate).Amount;

                    balance += u;
                    if (i != periods.Count())
                    accruedInterest += balance * (decimal)(1 + InterestRate(balance) / 365);
            }



            return accruedInterest;
        }

    }
}
