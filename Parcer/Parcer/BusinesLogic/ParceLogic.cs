using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Parcer.Utils;

namespace Parcer.BusinesLogic
{
    public static class ParceLogic
    {
        public const int SkipFirstMatches = 1;

        public static string Build(string inText, string additionalText, string setting, string buildSetting, string code)
        {
            IEnumerable<IEnumerable<string>> inMatches = FindMatches(inText, setting);
            if (inMatches == null)
            {
                return inText;
            }

            IEnumerable<string> additionalMatches = FindMatches(additionalText, setting).Select(x => x.First());

            IEnumerable<IEnumerable<string>> matches = inMatches.Where(x => additionalMatches.Contains(x.First()));

            buildSetting = PrepareForFormat(buildSetting);
            IEnumerable<string> formattedMatches = matches.Select(x => string.Format(buildSetting, x.Cast<object>().ToArray()));
            return string.Concat(formattedMatches);
        }

        public static string Build(string inText, string setting, string replaceSetting, string code)
        {
            IEnumerable<IEnumerable<string>> stringMatches = FindMatches(inText, setting);
            if (stringMatches == null)
            {
                return inText;
            }
            
            stringMatches = DynamicCode.ExecuteMethod(code, stringMatches);

            replaceSetting = PrepareForFormat(replaceSetting);
            IEnumerable<string> formattedMatches = stringMatches.Select(x => string.Format(replaceSetting, x.Cast<object>().ToArray()));
            return string.Concat(formattedMatches);
        }

        private static IEnumerable<IEnumerable<string>> FindMatches(string inText, string setting)
        {
            MatchCollection matches;
            if (!RegExpHelper.TryMatch(inText, setting, out matches))
            {
                return null;
            }

            IEnumerable<IEnumerable<string>> stringMatches = matches.
                Cast<Match>().
                Select(match => match.Groups.Cast<Group>().
                Skip(SkipFirstMatches).
                Select(x => x.Value));
            return stringMatches;
        }

        public static string PrepareForFormat(string replaceSetting)
        {
            var simbols = new Dictionary<string, string>
            {
                { "f", "\f" },
                { "r", "\r" },
                { "t", "\t" },
                { "v", "\v" },
                { "n", "\n" },
                { "{", "{{" },
                { "}", "}}" }
            };

            var stringBuilder = new StringBuilder(replaceSetting);
            foreach (var simbol in simbols)
            {
                stringBuilder.Replace($"\\{simbol.Key}", $"{simbol.Value}");
            }
            return stringBuilder.ToString();
        }

        public static string ReplaceMatches<T>(string inText, IEnumerable<T> matches, Func<T, string> formatWord, Func<T, Group> getGroup, int offset = 0)
        {
            StringBuilder outText = new StringBuilder();
            Action<int, int> tryAppendText = (start, length) =>
            {
                if (length > 0)
                    outText.Append(inText.Substring(start, length));
            };

            EnumerateMatches(matches, 
                (s, l) => tryAppendText(s, l), 
                (s, l, info) => outText.Append(formatWord(info)),
                info => getGroup(info).Index,
                info => getGroup(info).Length,
                inText.Length, offset);

            return outText.ToString();
        }

        public static void EnumerateMatches<T>(IEnumerable<T> matches,
            Action<int, int> doNoMatched, Action<int, int, T> doMatched, 
            Func<T, int> getStart, Func<T, int> getLength,
            int endPosition,
            int offset = 0)
        {
            int currPosition = 0;
            foreach (T info in matches)
            {
                int start = getStart(info) - offset;
                int length = getLength(info);
                if (start - currPosition > 0)
                {
                    doNoMatched(currPosition, start - currPosition);
                }
                if (length > 0)
                {
                    doMatched(start, length, info);
                }
                currPosition = start + length;
            }
            doNoMatched(currPosition, endPosition - currPosition);
        }
    }
}