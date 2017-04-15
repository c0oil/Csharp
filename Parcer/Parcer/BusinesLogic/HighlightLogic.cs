using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media;
using Parcer.BaseControls;

namespace Parcer.BusinesLogic
{
    public static class HighlightLogic
    {
        public static readonly Color[] ColorSettings =
        {
            Colors.DeepSkyBlue,
            Colors.DarkRed,
            Colors.Blue,
            Colors.Coral,
            Colors.DarkSeaGreen,
            Colors.DarkGoldenrod,
        };

        public static IEnumerable<ColorWord> Highlight(string inText, string setting)
        {
            MatchCollection matches;
            if (!RegExpHelper.TryMatch(inText, setting, out matches))
            {
                return null;
            }
            
            IEnumerable<TreeItem<ColorWord>> treeMatches = matches.
                OfType<Match>().
                Select(TreeMatchCollectionUtils.ToTree).
                Select((x, i) => x.Convert(y => 
                    new ColorWord
                    {
                        Start = y.Index,
                        End = y.Index + y.Length,
                        Value = y.Value,
                    })).
                ToArray();

            foreach (TreeItem<ColorWord> treeMatch in treeMatches)
            {
                ColorWord[] items = treeMatch.FromRootToLifes().ToArray();
                for (int i = ParceLogic.SkipFirstMatches; i < items.Length; i++)
                {
                    items[i].Color = ColorSettings[i % ColorSettings.Length];
                }
            }
            return treeMatches.SelectMany(SelectColorWords);
        }

        private static IEnumerable<ColorWord> SelectColorWords(TreeItem<ColorWord> tree)
        {
            var result = new List<ColorWord>();
            ColorWord item = tree.Item;
            if (tree.Childrens != null && tree.Childrens.Any())
            {
                TreeItem<ColorWord> last = tree.Childrens.First();
                result.Add(new ColorWord
                    {
                        Color = item.Color,
                        Start = item.Start,
                        End = last.Item.Start
                    });
                result.AddRange(SelectColorWords(last));

                foreach (TreeItem<ColorWord> children in tree.Childrens.Skip(1))
                {
                    result.Add(new ColorWord { Color = item.Color, Start = last.Item.End, End = children.Item.Start });
                    result.AddRange(SelectColorWords(children));
                    last = children;
                }
                result.Add(new ColorWord { Color = item.Color, Start = last.Item.End, End = item.End });
            }
            else
            {
                result.Add(item);
            }
            return result;
        }
    }
}