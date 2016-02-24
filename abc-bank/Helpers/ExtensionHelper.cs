using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abc_bank.Helpers
{
    public static class ExtensionHelper
    {
        /// <summary>
        /// Extension method to check if the object is any numeric type.
        /// </summary>
        /// <param name="o">Object type</param>
        /// <returns>boolean indicating if the object parameter is numeric.</returns>
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
    }
}