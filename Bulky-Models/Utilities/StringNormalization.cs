using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky_Models.Utilities
{
    public static class StringNormalization
    {
        public static string NormalizeString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            input = input
                .Replace("ي", "ی")
                .Replace("ك", "ک")
                .Trim();

            input = string.Join(" ", input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            return input;
        }
    }
}
