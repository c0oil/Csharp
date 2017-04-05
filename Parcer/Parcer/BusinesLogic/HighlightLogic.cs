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
            Colors.CadetBlue,
            Colors.Yellow,
            Colors.YellowGreen,
            Colors.DeepPink
        };

        public static IEnumerable<TreeItem<ColorWord>> Highlight(string inText, string setting)
        {
            if (string.IsNullOrEmpty(inText))
            {
                throw new ArgumentNullException();
            }

            var r = new Regex(setting, RegexOptions.IgnoreCase);
            MatchCollection matches = r.Matches(inText);
            if (matches.Count == 0)
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
                        Color = ColorSettings[i % ColorSettings.Length],
                        Value = y.Value,
                    }));

            return treeMatches;
        }
    }
}