using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CinemaLibrary.Core
{
    public class TypeIdentifier
    {
        public dynamic RepresentsType(string input)
        {
            Type targetType;

            if (bool.TryParse(input, out bool result))
                targetType = Type.GetType(result.GetType().FullName);

            else if (IsNumber(input))
                targetType = Type.GetType(ConvertBasedOnSuffix(GetSuffix(input), input).GetType().FullName);

            else if (IsValidDate(input))
                targetType = Type.GetType("System.DateTime");

            else
                targetType = Type.GetType("System.String");


            dynamic convertedValue = Convert.ChangeType(input, targetType);
            return convertedValue;
        }
        private bool IsNumber(string value) => Regex.IsMatch(value, @"^(-?\d+(\.\d+)?(u|U|l|L|f|F|d|D|m|M|s|S|b|B|sb|Sb|sB|SB|us|Us|uS|US|ul|Ul|uL|UL)?$)$");
        private bool IsValidDate(string value) => Regex.IsMatch(value, @"^(?:\d{4}\/(?:0?[1-9]|1[012])\/(?:0?[1-9]|[12][0-9]|3[01])|(?:0?[1-9]|[12][0-9]|3[01])\/(?:0?[1-9]|1[012])\/\d{4})$");
        private dynamic ConvertBasedOnSuffix(string suffix, string value)
        {
            dynamic result = default;
            switch (suffix)
            {
                case "u":
                    result = uint.Parse(value);
                    break;
                case "l":
                    result = long.Parse(value);
                    break;
                case "f":
                    result = float.Parse(value);
                    break;
                case "d":
                    result = double.Parse(value);
                    break;
                case "m":
                    result = decimal.Parse(value);
                    break;
                case "s":
                    result = short.Parse(value);
                    break;
                case "b":
                    result = byte.Parse(value);
                    break;
                case "ul":
                    result = uint.Parse(value);
                    break;
                case "sb":
                    result = sbyte.Parse(value);
                    break;
                case "us":
                    result = ushort.Parse(value);
                    break;
                default:
                    result = int.Parse(value);
                    break;
            }
            return result;
        }
        private string GetSuffix(string value) => Regex.Match(value, @"(u|U|l|L|f|F|d|D|m|M|s|S|b|B|sb|Sb|sB|SB|us|Us|uS|US|ul|Ul|uL|UL)?$").Value;
    }
}
