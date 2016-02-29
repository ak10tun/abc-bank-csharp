using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using abc_bank.Utils;

namespace abc_bank.Draft
{
    public abstract class AccountBase
    {
        protected DateTime _StartDate { get; private set; }      
        protected long ID;
        public AccountBase(decimal initialDeposit)
        {
            ValidationHelper.NegativeTransactionValue(initialDeposit, "initialDeposit");
            this._StartDate = DateProvider.Now();
            this.ID = IdentifierHelper.GetNewId();
        }
    }
}
