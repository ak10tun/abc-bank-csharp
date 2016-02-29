using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{

    /* 
    
    Minimum unit of cycle (duration) is day. 
    Cycle is active object throughout the day which will be updated by transactions.
    The end of the cycle will generate accrued interest.
    
    A cycle > Beginning Balance >> Transactions (=> Updates available balance)
                >> Final Available Balance + Final  Available Balance * interest = End of Cycle Balance
          
    Each cycle starts with the beginning of the balance which is the closing balance of the previous cycle.
    I assumed that in a cycle transactions will change the available balance, and the accural will be applied to this available (updated) balance.
    In practice this might not be the case.
    So each transaction will update the cycle balance.


    We need more rules in order to handle the cycles more than days.
        For example: An account might have rules to accrue weekly, and there is no information for
                     how to handle transactions throughout the week and how the interest accrual works.
   
    Typically we need to implement an end of the day process to calculate the end of a cycle.
    We can add timer based or new task based libraries utilizing (await) asyncronuous methods, which automatically calculates balances and accrued interest at the end of the cycle.
    In this example, only external queries on a date for the balance or accrued interest generates new cycles and added to the cached cycles.

    Each cycle starts with the beginning of the balance which is the closing balance of the previous cycle.
    I assumed that in a cycle transactions will change the available balance, and the accural will be applied to this available (updated) balance.
    In practice this might not be the case.
    So each transaction will update the cycle balance.

    Start of the cycle balance starts generating interest income.

    If a deposit is made it will wait to start generating interest income when the next period starts.
    If a withdrawal is made that day's accrued interest is calculated based on the enxt 

    If the balance is changed due to a transaction, a new cycle is generated starting next day.


    Transactions appear throughout the day and recorded to the end of the cycle.
    
      
    Each transaction will generate a new cycle and attached to the available balance for start of the next day.
    Each balance start of the day should generate accrued interest based on the rules and 
    only at the end of the period based on the Account's interest accrual type.
    Transactions appear throughout the day and recorded to the end of   
    Each transaction will trigger a new monetary cycle.
    A transaction is recorded to the end of
    Period Start Time => Available Start Balance for the duration of the Period (Monetary) (End Balance of previous Period)
    Priod Duration =>  Available Balance = Available Start Balance + Net of Transactions
    Period End Time => End Balance = Available Balance + IRate * Available Start Balance (Accrued Interest)  

    */





    public class BalanceService: IBalanceService
    {
       public CoreList<MonetaryCycle> MonetaryCycles { get; private set; }
       public IAccount SourceAccount { get; private set; }

        private DateTime _LastCycleDate;
        public decimal AvailableBalance { get; private set; }
            

        public BalanceService(IAccount account)
        {
            ValidationHelper.NotNull(account ,"account");
            this.SourceAccount = account;
            this.MonetaryCycles = new CoreList<MonetaryCycle>();
        }

        /// <summary>
        /// Return cycle based on date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private MonetaryCycle _FindCycle(DateTime date)
        {
           return this.MonetaryCycles.FirstOrDefault(x => x.Period.StartDateTime.Date == date);
        }

        
        private MonetaryCycle _CreateCycle(DateTime date)
        {
            if(date < this.SourceAccount.StartDate)
            {
                throw new Exception("Cannot add a cycle before account generation.");
            }

            MonetaryCycle _cycle = null;

            if (this.MonetaryCycles.Count == 0)
            {
                // Opening the account or another initializing event.
                _cycle = new MonetaryCycle(date);
            }
            else {

                // if the cycle is already registered.
                _cycle = this._FindCycle(date);

                // Register new cycle.

                if (_cycle == null)
                {
                    decimal _previousClosingBalance = this.MonetaryCycles.Last().ClosingBalance;
                    _cycle = new MonetaryCycle(date, _previousClosingBalance);

                }

            }

            return _cycle;
        }


        #region UpdatingCycles

        // Cycles are updated when a balance query is set or a transaction is set.

        public void UpdateCycle(ITransaction transaction)
        {
            MonetaryCycle _cycle = this._FindCycle(transaction.Date);

            if (_cycle == null)
            {
                
            }
            
        }

        public void UpdateCycle(DateTime date)
        {

        }


        private decimal AccruedInterest(IMonetaryCycle cycle)
        {

        }

        #region

        private void _UpdateBalances(ITransaction transaction)
        {

        }
        
        

    }
}
