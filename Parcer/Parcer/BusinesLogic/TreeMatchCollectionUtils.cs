using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Parcer.BusinesLogic
{
    public static class TreeMatchCollectionUtils
    {
        public static TreeItem<Group> ToTree(Match match)
        {
            IEnumerable<Group> groups = match.Groups.Cast<Group>();
            Group parent = groups.First();
            var result = new TreeItem<Group> { Item = parent, Childrens = ToTreeChildrens(groups.Skip(1)) };
            return result;
        }

        private static IEnumerable<TreeItem<Group>> ToTreeChildrens(IEnumerable<Group> flatItems)
        {
            List<TreeItem<Group>> result = new List<TreeItem<Group>>();
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

                result.Add(new TreeItem<Group> { Item = currParent, Childrens = ToTreeChildrens(currChildrens) });

                index += currChildrens.Length + 1;
                if (end < 0)
                {
                    break;
                }
            }

            return result;
        }
    }
}