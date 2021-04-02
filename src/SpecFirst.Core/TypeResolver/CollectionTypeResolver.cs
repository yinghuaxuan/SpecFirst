namespace SpecFirst.Core.TypeResolver
{
    using System;
    using System.Globalization;
    using System.Linq;

    public class CollectionTypeResolver
    {
        /// <summary>
        /// Supported data types: int, decimal, date, boolean, string, array,
        /// Special keywords: null, blank
        /// </summary>
        /// <param name="value">The value in string.</param>
        /// <param name="hintType">The possible type of the value.</param>
        /// <param name="parsedValue">The value in its type.</param>
        /// <returns>The real type of the value.</returns>
        public static Type Resolve(string value, Type hintType, out object parsedValue)
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

            if (char.IsDigit(value[0]) || value[0] == '-' && char.IsDigit(value[1]))
            {
                if (TryResolveAsInteger(value, hintType, isNullable, out ValueWithType valueWithType))
                {
                    parsedValue = valueWithType.Value;
                    return valueWithType.Type;
                }

                if (value.Count(v => v == '-') >= 2)
                {
                    if (DateTime.TryParseExact(value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                    {
                        parsedValue = result;
                        return InferColumnTypeFromDateTime(hintType, isNullable);
                    }
                }

                if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalValue))
                {
                    parsedValue = decimalValue;
                    return InferColumnTypeFromDecimal(hintType, isNullable);
                }

                return typeof(string);
            }

            if (bool.TryParse(value, out bool booleanValue))
            {
                parsedValue = booleanValue;
                return InferColumnTypeFromBoolean(hintType, isNullable);
            }

            return typeof(string);
        }

        private static bool TryResolveAsInteger(string value, Type hintType, bool isNullable, out ValueWithType valueWithType)
        {
            valueWithType = null;

            if (!value.Contains(".") && !value.Contains("E") && !value.Contains("e"))
            {
                if (int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int integerValue))
                {
                    valueWithType = new ValueWithType(integerValue, InferColumnTypeFromInteger(hintType, isNullable));
                    return true;
                }
            }

            return false;
        }

        private static Type InferColumnTypeFromBoolean(Type hintType, bool isNullable)
        {
            var type = isNullable ? typeof(bool?) : typeof(bool);
            if (type != hintType && hintType != null)
            {
                type = typeof(string);
            }

            return type;
        }

        private static Type InferColumnTypeFromDecimal(Type hintType, bool isNullable)
        {
            var type = isNullable ? typeof(decimal?) : typeof(decimal);
            if (type != hintType && hintType != typeof(int) && hintType != typeof(int?) && hintType != null)
            {
                type = typeof(string);
            }

            return type;
        }

        private static Type InferColumnTypeFromDateTime(Type hintType, bool isNullable)
        {
            var type = isNullable ? typeof(DateTime?) : typeof(DateTime);
            if (type != hintType && hintType != null)
            {
                type = typeof(string);
            }

            return type;
        }

        private static Type InferColumnTypeFromInteger(Type hintType, bool isNullable)
        {
            var type = isNullable ? typeof(int?) : typeof(int);
            if (type != hintType && hintType != typeof(decimal) && hintType != typeof(decimal?) && hintType != null)
            {
                type = typeof(string);
            }
            else if (type != hintType && (hintType == typeof(decimal) || hintType == typeof(decimal?)) && hintType != null)
            {
                type = isNullable ? typeof(decimal?) : typeof(decimal);
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

        private class ValueWithType
        {
            public ValueWithType(object value, Type type)
            {
                Value = value;
                Type = type;
            }

            public object Value { get; }
            public Type Type { get; }
        }
    }
}
