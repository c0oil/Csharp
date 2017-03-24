using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media;
using Parcer.BaseControls;
using Parcer.Utils;

namespace Parcer.BusinesLogic
{
    public class Parser
    {
        private readonly Color[] colors =
        {
            Colors.CadetBlue,
            Colors.Yellow,
            Colors.YellowGreen,
            Colors.DeepPink,
        };

        public IEnumerable<ColorWord> Highlight(string inText, string setting)
        {
            IEnumerable<Tuple<int, int, int>> words = Find(inText, setting);
            return words.Select(x => new ColorWord
            {
                Color = colors[x.Item1 % colors.Length],
                Start = x.Item2,
                End = x.Item3,
            });
        }

        public IEnumerable<Tuple<int, int, int>> Find(string inText, string setting)
        {
            if (string.IsNullOrEmpty(inText))
            {
                return null;
            }

            var r = new Regex(setting, RegexOptions.IgnoreCase);
            var matches = r.Matches(inText);
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

        public string Replace(string inText, string additionalText, string setting, string replaceSetting, string code)
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

            replaceSetting = Ecrane(replaceSetting);
            IEnumerable<string> formattedMatches = matches.Select(x => string.Format(replaceSetting, x.Cast<object>().ToArray()));
            return string.Concat(formattedMatches);
        }

        public string Replace(string inText, string setting, string replaceSetting, string code)
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

        private IEnumerable<IEnumerable<string>> FindMatches(string inText, string setting)
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

        private string Ecrane(string replaceSetting)
        {
            var simbols = new Dictionary<string, string>
            {
                { "f", "\f" },
                { "r", "\r" },
                { "t", "\t" },
                { "v", "\v" },
                { "n", "\n" },
                { "{", "{{" },
                { "}", "}}" },
            };

            var stringBuilder = new StringBuilder(replaceSetting);
            foreach (var simbol in simbols)
            {
                stringBuilder.Replace($"\\{simbol.Key}", $"{simbol.Value}");
            }
            return stringBuilder.ToString();
        }

        private IEnumerable<Tuple<int, int, int>> FindMatch(Match match)
        {
            return match.Groups.Cast<Group>().Skip(1).Select((x, i) => new Tuple<int, int, int>(i, x.Index, x.Index + x.Length));
        }

        private string FormatMatch(Match match, string replaceSetting)
        {
            object[] args = match.Groups.Cast<Group>().Skip(1).Select(x => x.Value).Cast<object>().ToArray();
            return string.Format(replaceSetting, args);
        }
    }
}