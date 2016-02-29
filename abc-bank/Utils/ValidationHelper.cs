using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Using CodeContracts to check arguments preconditions.
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using System.Threading;

namespace abc_bank
{
    /// <summary>
    /// A custom class to simplify argument (parameter) checking throughout the library.
    /// Implicitly code contracts are being used which throw exceptions which helps while testing.
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Pattern to match for valid social socurity format.
        /// </summary>
        private static string _RegexPattern_SSN = @"^(?!\b(\d)\1+-(\d)\1+-(\d)\1+\b)(?!123-45-6789|219-09-9999|078-05-1120)(?!666|000|9\d{2})\d{3}-(?!00)\d{2}-(?!0{4})\d{4}$";

        /// <summary>
        /// Validates social security number.
        /// </summary>
        /// <param name="socialSecurityNumber">string</param>
        /// <returns>Formatted social security number as string.</returns>
        public static string SocialSecurityNumber(string socialSecurityNumber)
        {
            bool _flag = false;
         
            ValidationHelper.StringNullEmpty(socialSecurityNumber, "socialSecurityNumber");

            if(socialSecurityNumber.ToInt() != null)
            {
                socialSecurityNumber = socialSecurityNumber.Insert(5, "-").Insert(3, "-");
            }
            _flag = new Regex(_RegexPattern_SSN).IsMatch(socialSecurityNumber);

            if(!_flag)  throw new SocialSecurityFormatException();

            return socialSecurityNumber;
        }


        [ContractArgumentValidator]
        public static void StringNullEmpty(string argument, string parameterName,
                                              string exceptionMessage = "The parameter cannot be null or empty.")
        {
            if (string.IsNullOrEmpty(argument)) throw new ArgumentException(parameterName, exceptionMessage);
            Contract.EndContractBlock();
        }

        /// <summary>
        /// Checks if the numeric parameter is negative.
        /// </summary>
        [ContractArgumentValidator]
        public static void NegativeNumeric<T>(T argument, string parameterName,
                                              string exceptionMessage = "A positive paramater value is expected.")
        {

            if (!argument.IsNumericType())
                throw new ArgumentException(parameterName, "Numeric parameter is expected");

            if (Comparer<T>.Default.Compare(argument, default(T)) < 0) throw new ArgumentException(parameterName, exceptionMessage);
            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void DateTimeGreaterInequality(DateTime argument1, string parameter1Name, DateTime argument2, string parameter2Name)
        {
            if (argument2 < argument1) throw new DateInequalityException(parameter2Name + " must be greater than " + parameter2Name);
            Contract.EndContractBlock();
        }


        [ContractArgumentValidator]
        public static void NotNull(object argument, string parameterName)
        {
            if (argument == null) throw new ArgumentNullException(parameterName,
                                                                  "The object parameter cannot be null.");
            Contract.EndContractBlock();
        }


        [ContractArgumentValidator]
        public static void ZeroTransactionValue(decimal argument, string parameterName)
        {
            if (argument == 0) throw new ArgumentNullException(parameterName,
                                                                  "The transaction amount must be greater than zero.");
            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void NegativeTransactionValue(decimal argument, string parameterName)
        {
            if (argument < 0) throw new ArgumentNullException(parameterName,
                                                                  "The transaction amount must be greater than zero.");
            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        public static void SufficientFunds(decimal amount, decimal balance, string parameterName)
        {
            if (balance < amount) throw new InsufficientFundsException("Insufficient funds to withdraw.");
            Contract.EndContractBlock();
        }



        [ContractArgumentValidator]
        public static void InRange<T>(IList<T> list, int index, string listName, string indexName)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(indexName,
                                                      "The index cannot be negative.");
            if (index >= list.Count)
                throw new ArgumentOutOfRangeException(indexName,
                                                      "The index is outside the bounds of the array.");
            Contract.EndContractBlock();
        }

    }
}