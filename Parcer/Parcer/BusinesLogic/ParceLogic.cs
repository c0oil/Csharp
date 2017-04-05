using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Parcer.BaseControls;
using Parcer.Utils;

namespace Parcer.BusinesLogic
{
    public static class ParceLogic
    {
        public static IEnumerable<ColorWord> Highlight(string inText, string setting)
        {
            IEnumerable<Tuple<int, int, int>> words = Find(inText, setting);
            return words.Select(x => new ColorWord
            {
                Color = HighlightLogic.ColorSettings[x.Item1 % HighlightLogic.ColorSettings.Length],
                Start = x.Item2,
                End = x.Item3
            });
        }

        public static string Build(string inText, string additionalText, string setting, string buildSetting, string code)
        {
            if (string.IsNullOrEmpty(inText))
            {
                return inText;
            }

            IEnumerable<IEnumerable<string>> inMatches = FindMatches(inText, setting);
            if (inMatches == null)
            {
                return inText;
            }

            IEnumerable<string> additionalMatches = FindMatches(additionalText, setting).Select(x => x.First());

            IEnumerable<IEnumerable<string>> matches = inMatches.Where(x => additionalMatches.Contains(x.First()));

            buildSetting = Ecrane(buildSetting);
            IEnumerable<string> formattedMatches = matches.Select(x => string.Format(buildSetting, x.Cast<object>().ToArray()));
            return string.Concat(formattedMatches);
        }

        public static string Build(string inText, string setting, string replaceSetting, string code)
        {
            if (string.IsNullOrEmpty(inText))
            {
                return inText;
            }
            
            IEnumerable<IEnumerable<string>> stringMatches = FindMatches(inText, setting);
            if (stringMatches == null)
            {
                return inText;
            }
            
            stringMatches = DynamicCode.ExecuteMethod(code, stringMatches);

            replaceSetting = Ecrane(replaceSetting);
            IEnumerable<string> formattedMatches = stringMatches.Select(x => string.Format(replaceSetting, x.Cast<object>().ToArray()));
            return string.Concat(formattedMatches);
        }
        
        private static IEnumerable<Tuple<int, int, int>> Find(string inText, string setting)
        {
            if (string.IsNullOrEmpty(inText))
            {
                return null;
            }

            var r = new Regex(setting, RegexOptions.IgnoreCase);
            MatchCollection matches = r.Matches(inText);
            if (matches.Count == 0)
            {
                return null;
            }

            var highlightedWords = new List<Tuple<int, int, int>>();
            foreach (Match match in matches)
            {
                highlightedWords.AddRange(FindMatch(match));
            }

            return highlightedWords;
        }

        private static IEnumerable<IEnumerable<string>> FindMatches(string inText, string setting)
        {
            var r = new Regex(setting, RegexOptions.IgnoreCase);
            var matches = r.Matches(inText);
            if (matches.Count == 0)
            {
                return null;
            }

            IEnumerable<IEnumerable<string>> stringMatches = matches.Cast<Match>().Select(match => match.Groups.Cast<Group>().Skip(1).Select(x => x.Value));
            return stringMatches;
        }

        public static string Ecrane(string replaceSetting)
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

        private static IEnumerable<Tuple<int, int, int>> FindMatch(Match match)
        {
            return match.Groups.Cast<Group>().Skip(1).Select((x, i) => new Tuple<int, int, int>(i, x.Index, x.Index + x.Length));
        }

        private static string FormatMatch(Match match, string replaceSetting)
        {
            object[] args = match.Groups.Cast<Group>().Skip(1).Select(x => x.Value).Cast<object>().ToArray();
            return string.Format(replaceSetting, args);
        }

        public static string AggregateMatches<T>(string inText, IEnumerable<T> findedInfoWords, Func<T, string> formatWord, Func<T, Group> getGroup)
        {
            StringBuilder outText = new StringBuilder();
            Action<int, int> tryAppendText = (start, length) =>
            {
                if (length > 0)
                {
                    outText.Append(inText.Substring(start, length));
                }
            };

            int currPosition = 0;
            foreach (T info in findedInfoWords)
            {
                Group group = getGroup(info);
                tryAppendText(currPosition, group.Index - currPosition);

                string formatedWord = formatWord(info);
                outText.Append(formatedWord);

                currPosition = group.Index + group.Length;
            }
            tryAppendText(currPosition, inText.Length - currPosition);

            return outText.ToString();
        }
    }
}