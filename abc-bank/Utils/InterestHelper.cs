using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace abc_bank
{


    /// <summary>
    /// Helper methods for accrued interest return calculator.
    /// </summary>
    public static class InterestHelper
    {

        public static decimal SimpleInterestCompounding(decimal principle, double annualRate, int periodCount, InterestCounpoundType compoundType)
        {
            ValidationHelper.NegativeNumeric<decimal>(principle, "principle");
            ValidationHelper.NegativeNumeric<double>(annualRate, "annualRate");
            ValidationHelper.NegativeNumeric<int>(periodCount, "periodCount");

            decimal calculated_value = 0;

            switch (compoundType)
            {
                case InterestCounpoundType.Daily:
                    calculated_value =  _InterestCompoundCalculation(principle, annualRate, periodCount, 365);
                    break;
            }

            return calculated_value;
        }

        private static decimal _InterestCompoundCalculation(decimal principle, double annualRate, int numberOfInvestmentPeriods, int numberOfCompoundingPeriods)
        {
            // Not checking for arguments.
            return principle * (decimal)Math.Pow(1 + annualRate / numberOfCompoundingPeriods, numberOfInvestmentPeriods);
        }

    }
}
