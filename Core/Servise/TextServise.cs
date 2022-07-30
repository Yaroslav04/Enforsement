using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Enforsement.Core.Servise
{
    public static class TextServise
    {
        public static bool IsNumberValid(string _value)
        {
            return Regex.IsMatch(_value, @"^\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d");
        }

        public static bool IsDateValid(string _text)
        {
            Regex regex = new Regex(@"\d\d(.)\d\d(.)\d\d\d\d");
            return regex.IsMatch(_text);
        }
    }
}
