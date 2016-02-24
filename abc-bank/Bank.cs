using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using abc_bank.Services;
using abc_bank.Models;

/// <summary>
/// TO_DO: 
///     1- Finish setting up the types and relations.
///        Apply accrued interest and balance calculations, a passed date or a new transaction should 
///        generate a new sub-investmment period (end) calculatable for rolling interest income.
///     2- Seperate the cencerns and finish up the services.
///     3- Apply the tests for each class.
///     4- Assuming the clients would use Thread.CurrentThread.CurrentCulture and Thread.CurrentThread.CurrentUICulture, 
///        apply datetime localization and culture information properly.
///     5- Apply Composite Reuse and Decorators if enough time.
/// </summary>



namespace abc_bank
{
    public interface IBank
    {
       List<ICustomer> Customers { get; }
       ITransactionService TransactionService { get; }
       IReportingService ReportingService { get; }
       ICustomerService CustomerService { get; }
       IAccountService AccountService { get; }

       
        

    }
    public class AbcBank: IBank
    {
       public List<ICustomer> Customers { get; internal set; }
       public ITransactionService TransactionService { get; private set; }
       public IReportingService ReportingService { get; private set; }
       public ICustomerService CustomerService { get; private set; }
       public IAccountService AccountService { get; private set; }

        static AbcBank()
        {       

        }
    }
}
