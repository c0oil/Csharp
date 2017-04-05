using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parcer.BusinesLogic
{
    public static class TreeParceLogic
    {
        public static string Replace(string inText, string setting, string replaceSetting, string separator)
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

            replaceSetting = ParceLogic.Ecrane(replaceSetting);
            IEnumerable<string> replaceSettings = replaceSetting.Split(new[] { separator }, StringSplitOptions.None);

            return AggregateTreeFinded(inText, matches, replaceSettings);
        }

        private static string AggregateTreeFinded(string inText, MatchCollection matches, IEnumerable<string> replaceSettings)
        {
            TreeItem<MatchTreeItem>[] treeMatches = matches.
                OfType<Match>().
                Select(TreeMatchCollectionUtils.ToTree).
                Select(x => x.Convert(y => 
                    new MatchTreeItem
                    {
                        Group = y,
                    })).
                ToArray();
            string[] replaceSettingsReverse = replaceSettings.Reverse().ToArray();
            foreach (TreeItem<MatchTreeItem> treeMatch in treeMatches)
            {
                foreach (Tuple<MatchTreeItem, string> item in treeMatch.Zip(replaceSettingsReverse, (i, s) => new Tuple<MatchTreeItem, string>(i,s)))
                {
                    item.Item1.ReplaceSetting = item.Item2.Clone().ToString();
                }
            }
            Func<TreeItem<MatchTreeItem>, Group> getGroup = x => x.Item.Group;
            return ParceLogic.AggregateMatches(inText, treeMatches, FormatTree, getGroup);
        }

        private static string FormatTree(TreeItem<MatchTreeItem> tree)
        {
            Func<IEnumerable<string>, MatchTreeItem, string> aggregate = (args, item) =>
            {
                string format = string.IsNullOrEmpty(item.ReplaceSetting) ? "{0}" : item.ReplaceSetting;
                object[] formatArgs = args.Any() ? args.ToArray() : new[] { item.Group.Value };
                return string.Format(format, formatArgs);
            };
            return tree.Aggregate(aggregate);
        }
    }

    public class MatchTreeItem
    {
        public Group Group { get; set; }
        public string ReplaceSetting { get; set; }
    }

}