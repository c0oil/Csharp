using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Parcer.BusinesLogic
{
    public static class RegExpHelper
    {

        public static bool TryMatch(string inText, string setting, out MatchCollection matches)
        {
            matches = null;
            if (string.IsNullOrEmpty(inText))
            {
                return false;
            }

            Regex r = new Regex(setting, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            matches = r.Matches(inText);
            return matches.Count != 0;
        }
    }
}
