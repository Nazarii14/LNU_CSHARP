using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Task_2
{
    class Validation
    {
        public static string ValidateNumber(string value)
        {
            if (int.TryParse(value, out int result))
                return value;
            else
                throw new ArgumentException("No letters/characters in number!");
        }

        public static string ValidateStr(string value)
        {
            foreach (char c in value)
            {
                if (char.IsDigit(c))
                {
                    throw new ArgumentException("No integers in string!");
                }
            }
            return value;
        }

        public static string ValidateIBAN(string value)
        {
            if (!Regex.IsMatch(value.Substring(2), @"^\d+$") || value.Substring(0, 2) != "UA" || value.Length > 16)
            {
                throw new ArgumentException("IBAN must look like this: UA##############");
            }
            return value;
        }

        public static double ValidateAmount(double value)
        {
            try
            {
                string strValue = value.ToString();
                if (!Regex.IsMatch(strValue, @"^\d+(\.\d{1,2})?$"))
                {
                    throw new ArgumentException("Amount must be a number with up to 2 decimal places!");
                }
                return value;
            }
            catch (FormatException)
            {
                throw new ArgumentException("Amount must be a number with up to 2 decimal places!");
            }
        }

        public static string ValidateDate(string value)
        {
            //vaue == "0" ? => value;
            if (value == "0")
            {
                return value;
            }
            if (!DateTime.TryParse(value, out DateTime dateValue))
            {
                throw new ArgumentException("Invalid date format. Date must be in the format of 'yyyy/MM/dd'");
            }
            if (DateTime.Compare(DateTime.Now.Date, dateValue.Date) < 0)
            {
                throw new ArgumentException("Nonexistent Date.");
            }
            return dateValue.ToString("yyyy/MM/dd");
        }

        public static string ValidateCurrency(string value)
        {
            //vaue == "0" ? => value;
            if (value == "0")
            {
                return value;
            }
            string[] validCurrencies = { "usd", "eur", "uah", "yen" };
            if (!validCurrencies.Any(currency => value.EndsWith(currency)))
            {
                throw new ArgumentException("Invalid currency!");
            }
            return value;
        }
    }
}
