using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Helpers
{

    /// <summary>
    /// Custom exception class for invalid arguments of any methods.
    /// I will be using CuttingEdge.Consitions for arguments and some of the code checkings to save some time writing exceptions and code contracts.
    /// 
    /// </summary>
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException()
        {
        }

        public InvalidArgumentException(string message)
            : base(message)
        {
        }

        public InvalidArgumentException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public InvalidArgumentException(string Argument, string message, Exception inner)
           : base(message, inner)
        {
        }
    }


    /// <summary>
    /// Provided social security number format must be valid.
    /// </summary>
    public class SocialSecurityFormatException : Exception
    {
        public SocialSecurityFormatException()
        {
        }
        public SocialSecurityFormatException(string message)
            : base(message)
        {
        }
    }


    /// <summary>
    /// Bank shouldn't register the same customer more than once.
    /// </summary>
    public class DuplicateCustomerException : Exception
    {
        public DuplicateCustomerException()
        {
        }
        public DuplicateCustomerException(string message)
            : base(message)
        {
        }
    }



    /// <summary>
    /// If a date is greater than an earlier date.
    /// </summary>
    public class DateInequalityException : Exception
    {
        public DateInequalityException()
        {
        }
        public DateInequalityException(string message)
            : base(message)
        {
        }
    }
}
