using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using abc_bank.Utils;
namespace abc_bank.Draft
{
    public class BalanceManager: IBalancerService
    {
       public List<IBalancer> BalanceSet { get; private set; }

        public BalanceManager(IAccount account)
        {
            this.BalanceSet = new List<IBalancer>();
        }

        public void AddBalancer()
        {
            

        }
        public IBalancer GetBalance()
        {
            return this.GetBalance(DateProvider.Now());
        }
        public Balancer GetBalance(DateTime date)
        {
            return null;
            
        }

        public void UpdateBalance(ITransaction transaction)
       {
          if((transaction.Date - this.BalanceSet.Last().Period.EndDateTime).Days == 0)
          {
                this.BalanceSet.Last().Transactions.Add(transaction);
                this.BalanceSet.Last().AdjustedStartBalance += transaction.Amount;
                this.BalanceSet.Last().
          }
            else
            {

            }
       }

        public decimal AccruedInterest()
        {
            return 0;
        }


    }
}
