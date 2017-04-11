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
            MatchCollection matches;
            if (!ParceLogic.TryMatch(inText, setting, out matches))
            {
                return null;
            }
            
            replaceSetting = ParceLogic.Ecrane(replaceSetting);
            string[] replaceSettings = replaceSetting.Split(new[] { separator }, StringSplitOptions.None);

            IEnumerable<TreeItem<MatchTreeItem>> treeMatches = PrepareTreeMatches(matches, replaceSettings);

            return ParceLogic.ReplaceMatches(inText, treeMatches, FormatMatchTree, x => x.Item.Group);
        }

        private static IEnumerable<TreeItem<MatchTreeItem>> PrepareTreeMatches(MatchCollection matches, string[] replaceSettings)
        {
            TreeItem<MatchTreeItem>[] treeMatches = matches.
                OfType<Match>().
                Select(TreeMatchCollectionUtils.ToTree).
                Select(x => x.Convert(y => new MatchTreeItem { Group = y })).
                ToArray();
            foreach (TreeItem<MatchTreeItem> treeMatch in treeMatches)
            {
                MatchTreeItem[] items = treeMatch.FromRootToLifes().ToArray();
                for (int i = 0; i < items.Length; i++)
                {
                    var item = items[i];
                    item.ReplaceSetting = i < replaceSettings.Length ? replaceSettings[i] : string.Empty;
                    item.Number = i;
                }
            }
            return treeMatches;
        }

        private static string FormatMatchTree(TreeItem<MatchTreeItem> tree)
        {
            var aggregatedItems = new Dictionary<int, string>();
            return tree.Aggregate<MatchTreeItem, string>((args, childrens, item) => FormatMatchTree(args, childrens, item, aggregatedItems));
        }

        private static string FormatMatchTree(IEnumerable<string> args, IEnumerable<MatchTreeItem> childrens, MatchTreeItem item, Dictionary<int, string> aggregatedItems)
        {
            string interValue;
            string[] argsStr = args.ToArray();
            if (argsStr.Any())
            {
                int number = 0;
                interValue = ParceLogic.ReplaceMatches(item.Group.Value, childrens,
                    x => $"{argsStr[number++]}", x => x.Group, item.Group.Index);
            }
            else
            {
                interValue = item.Group.Value;
            }

            int currNumber = item.Number;
            bool haveSettings = !string.IsNullOrEmpty(item.ReplaceSetting);
            if (!haveSettings)
            {
                aggregatedItems[currNumber] = interValue;
                return interValue;
            }

            string formatedValue = item.ReplaceSetting.
                Replace("{0}", interValue).
                Replace($"{{{currNumber}}}", interValue);
            foreach (int number in aggregatedItems.Keys)
            {
                string aggregatedItem;
                if (number == 0 ||
                    number == currNumber ||
                    !aggregatedItems.TryGetValue(number, out aggregatedItem))
                {
                    continue;
                }
                string s = $"{{{number}}}";
                formatedValue = formatedValue.Replace(s, aggregatedItem);
            }

            aggregatedItems[currNumber] = formatedValue;
            return formatedValue;
        }
    }

    public class MatchTreeItem
    {
        public Group Group { get; set; }
        public string ReplaceSetting { get; set; }
        public int Number { get; set; }
    }

}