using System;
using System.Collections;
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

        public string ReplaceTree(string inText, string setting, string replaceSetting, string separator)
        {
            if (string.IsNullOrEmpty(inText))
            {
                return inText;
            }

            var r = new Regex(setting, RegexOptions.IgnoreCase);
            MatchCollection matches = r.Matches(inText);
            if (matches.Count == 0)
            {
                return null;
            }

            replaceSetting = Ecrane(replaceSetting);
            Dictionary<int, string> replaceSettings = replaceSetting.Split(new[] { separator }, StringSplitOptions.None).Select((x, i) => new {x, i}).ToDictionary(x => x.i, x => x.x);
            return TreeParcer.AggregateTreeFinded(inText, matches, x => replaceSettings[replaceSettings.Count - x - 1]);
        }

        /*public string Replace(string inText, string setting, string replaceSetting, string separator)
        {
            if (string.IsNullOrEmpty(inText))
            {
                return inText;
            }
            
            IEnumerable<Tuple<int, int, int>> words = Find(inText, setting);
            if (words == null)
            {
                return inText;
            }
            
            replaceSetting = Ecrane(replaceSetting);
            Dictionary<int, string> replaceSettings = replaceSetting.Split(new[] { separator }, StringSplitOptions.None).Select((x, i) => new {x, i}).ToDictionary(x => x.i, x => x.x);
            Func<int, string, string> format = (wordNumber, word) =>
            {
                string wordReplaceSetting;
                bool haveSetting = replaceSettings.TryGetValue(wordNumber, out wordReplaceSetting);
                return haveSetting ? string.Format(wordReplaceSetting.Replace($"{{{wordNumber}}}", "{0}"), word) : word;

            };
            return AggregateFinded(inText, words, format);
        }*/

        public string Build(string inText, string additionalText, string setting, string buildSetting, string code)
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

        public string Build(string inText, string setting, string replaceSetting, string code)
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

        private string AggregateFinded(string inText, IEnumerable<Tuple<int, int, int>> findedInfoWords, Func<int, string, string> format)
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
            foreach (Tuple<int, int, int> info in findedInfoWords)
            {
                tryAppendText(currPosition, info.Item2 - currPosition);

                string findedWord = inText.Substring(info.Item2, info.Item3 - info.Item2);
                string formatedWord = format(info.Item1, findedWord);
                outText.Append(formatedWord);

                currPosition = info.Item3;
            }
            tryAppendText(currPosition, inText.Length - currPosition);

            return outText.ToString();
        }
        
        private IEnumerable<Tuple<int, int, int>> Find(string inText, string setting)
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

    public static class TreeParcer
    {
        public static string AggregateTreeFinded(string inText, MatchCollection matches, Func<int, string> getFormat)
        {
            StringBuilder outText = new StringBuilder();
            IEnumerable<MatchTreeItem> treeMatches = matches.OfType<Match>().Select(ToTree);
            
            Action<int, int> tryAppendText = (start, length) =>
            {
                if (length > 0)
                {
                    outText.Append(inText.Substring(start, length));
                }
            };

            int currPosition = 0;
            foreach (MatchTreeItem treeMatch in treeMatches)
            {
                Group info = treeMatch.Item;

                tryAppendText(currPosition, info.Index - currPosition);

                int index = 0;
                string formatedWord = AggregationTree(treeMatch, () => getFormat(index++));
                outText.Append(formatedWord);

                currPosition = info.Index + info.Length;
            }
            tryAppendText(currPosition, inText.Length - currPosition);

            return outText.ToString();
        }

        private static string AggregationTree(TreeItem<Group> tree, Func<string> getFormat)
        {
            var args = tree.Childrens == null || !tree.Childrens.Any()
                ? new[] {tree.Item.Value}
                : tree.Childrens.Select(x => AggregationTree(x, getFormat)).ToArray();

            string format = getFormat();
            return string.Format(format, args);
        }

        private static void ForEachTree(IEnumerable<TreeItem<Group>> trees, Action<Group> doAction)
        {
            foreach (TreeItem<Group> tree in trees)
            {
                if (tree.Childrens != null && tree.Childrens.Any())
                {
                    ForEachTree(tree.Childrens, doAction);
                }
                doAction(tree.Item);
            }
        }

        private static MatchTreeItem ToTree(Match match)
        {
            IEnumerable<Group> groups = match.Groups.Cast<Group>();
            Group parent = groups.First();
            return new MatchTreeItem { Item = parent, Childrens = ToTreeChildrens(groups.Skip(1)) };
        }

        private static IEnumerable<MatchTreeItem> ToTreeChildrens(IEnumerable<Group> flatItems)
        {
            List<MatchTreeItem> result = new List<MatchTreeItem>();
            Group[] items = flatItems.ToArray();
            if (items.Length == 0)
            {
                return result;
            }

            int index = 0;
            while (true)
            {
                Group currParent = items[index];
                int nextOffset = 1;
                int start = index + nextOffset;
                Group[] restItems = items.Skip(start).ToArray();
                int end = Array.FindIndex(restItems, x => x.Index >= currParent.Index + currParent.Length);
                Group[] currChildrens = end < 0 ? restItems : restItems.Take(end).ToArray();

                result.Add(new MatchTreeItem { Item = currParent, Childrens = ToTreeChildrens(currChildrens) });

                index += currChildrens.Length + 1;
                if (end < 0)
                {
                    break;
                }
            }

            return result;
        }

    }

    public class MatchTreeItem : TreeItem<Group>
    {

    }

    public class TreeItem<T>
    {
        public T Item { get; set; }
        public IEnumerable<TreeItem<T>> Childrens { get; set; }
    }
}