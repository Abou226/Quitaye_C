using System;
using System.Collections.Generic;
using System.Text;

namespace Quitaye.Helpers
{
    public static class NumbersTools
    {
        public static string GetNember(float number)
        {
            if (number >= 1000000)
            {
                return $"{Math.Round(number / 1000000, MidpointRounding.ToEven)} M CFA";
            }

            if (number >= 1000)
            {
                return $"{Math.Round(number / 1000, MidpointRounding.ToEven)} K CFA";
            }
            return $"{Math.Round(number, MidpointRounding.ToEven)} CFA";
        }
    }
}
