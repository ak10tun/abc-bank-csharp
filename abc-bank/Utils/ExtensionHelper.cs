using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank
{
    public static class ExtensionHelper
    {
        /// <summary>
        /// Extension method to check if the object is any of the numeric types.
        /// </summary>
        /// <param name="o">Object type to check if numeric.</param>
        /// <returns>Boolean indicating if the object parameter is numeric.</returns>
        public static bool IsNumericType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Returns integer from string.
        /// </summary>
        /// <param name="o">string</param>
        /// <returns>nullable integer</returns>
        public static int? ToInt(this string o)
        {
            int _parsed;
            int? _parsedNullable = null;

            if(int.TryParse(o, out _parsed))
            {
                _parsedNullable = _parsed;
            }
            return _parsedNullable;
        }

        /// <summary>
        /// Returns dollar format for any decimal.
        /// </summary>
        /// <param name="d">decimal monetary value.</param>
        /// <returns>string representation.</returns>

        public static string ToDollars(this decimal d)
        {
            return String.Format("{0:C}", d);
        }
    }
}