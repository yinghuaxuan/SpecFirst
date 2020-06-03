using System;
using System.Globalization;

namespace DecisionMarkd.DecisionTable
{
    public class TypeResolver
    {
        /// <summary>
        /// Supported data types: int, decimal, date, boolean, string, list/array,
        /// Special keywords: null, blank
        /// </summary>
        /// <param name="value">The value in string.</param>
        /// <param name="hintType">The possible type of the value.</param>
        /// <param name="parsedValue">The value in its type.</param>
        /// <returns>The real type of the value.</returns>
        public static Type InferTypeFromValue(string value, Type hintType, out object parsedValue)
        {
            Type type = hintType;
            parsedValue = value;

            if (type == typeof(string)) // no need to process further if it is already a string type
            {
                return type;
            }

            if (value.Equals("null", StringComparison.OrdinalIgnoreCase))
            {
                return ConvertTypeToNullable(hintType ?? typeof(int));
            }

            bool isNullable = IsNullable(hintType);

            if (char.IsDigit(value[0]))
            {
                type = typeof(string);

                if (value.Contains("."))
                {
                    if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
                    {
                        type = isNullable ? typeof(decimal?) : typeof(decimal);
                        if (type != hintType && hintType != typeof(int) && hintType != typeof(int?) && hintType != null)
                        {
                            type = typeof(string);
                        }
                        parsedValue = result;
                    }
                }
                else if (value.Contains("-"))
                {
                    if (DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                    {
                        type = isNullable ? typeof(DateTime?) : typeof(DateTime);
                        if (type != hintType && hintType != null)
                        {
                            type = typeof(string);
                        }
                        parsedValue = result;
                    }
                }
                else if (int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int result))
                {
                    type = isNullable ? typeof(int?) : typeof(int);
                    if (type != hintType && hintType != typeof(decimal) && hintType != typeof(decimal?) && hintType != null)
                    {
                        type = typeof(string);
                    }
                    else if (type != hintType && (hintType == typeof(decimal) || hintType == typeof(decimal?)) && hintType != null)
                    {
                        type = isNullable ? typeof(decimal?) : typeof(decimal);
                    }
                    parsedValue = result;
                }
            }
            else if (bool.TryParse(value, out bool result))
            {
                type = isNullable ? typeof(bool?) : typeof(bool);
                if (type != hintType && hintType != null)
                {
                    type = typeof(string);
                }
                parsedValue = result;
            }
            else
            {
                type = typeof(string);
            }

            return type;
        }

        private static Type ConvertTypeToNullable(Type type)
        {
            // Use Nullable.GetUnderlyingType() to remove the Nullable<T> wrapper if type is already nullable.
            type = Nullable.GetUnderlyingType(type) ?? type; // avoid type becoming null
            if (type.IsValueType)
                return typeof(Nullable<>).MakeGenericType(type);
            else
                return type;
        }

        private static bool IsNullable(Type type)
        {
            if (type == null) return false;

            if (!type.IsValueType) // ref-type
            {
                return true;
            }

            if (Nullable.GetUnderlyingType(type) != null) // Nullable<T>
            {
                return true;
            }

            return false; // value-type
        }
    }
}
