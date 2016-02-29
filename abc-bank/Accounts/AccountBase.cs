using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public abstract class AccountBase
    {
        protected decimal InitialDeposit { get; }
        public long Id { get; }
        public double DefaultInterestRate { get; protected set; }
        public decimal AvailableBalance { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public string TypeName { get; protected set; }

        public AccountBase(decimal initialDeposit)
        {
            ValidationHelper.NegativeTransactionValue(initialDeposit, "initialDeposit");
            this.InitialDeposit = initialDeposit;
            this.StartDate = DateProvider.Now();
            this.Id = IdentifierHelper.NewAccountId();

        }

    }
}
