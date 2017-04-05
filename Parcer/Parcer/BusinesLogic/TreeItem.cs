using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Parcer.BusinesLogic
{
    public class TreeItem<T> : IEnumerable<T>
    {
        public T Item { get; set; }
        public IEnumerable<TreeItem<T>> Childrens { get; set; }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.ToFlatten().GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.ToFlatten().GetEnumerator();
        }
    }

    public static class TreeEnumerator
    {
        public static IEnumerable<T> ToFlatten<T>(this TreeItem<T> tree)
        {
            var result = new List<T>();
            if (tree.Childrens != null)
            {
                foreach (TreeItem<T> children in tree.Childrens)
                {
                    result.AddRange(ToFlatten(children));
                }
            }
            result.Add(tree.Item);
            return result;
        }

        public static TResult Aggregate<T, TResult>(this TreeItem<T> tree, Func<IEnumerable<TResult>, T, TResult> aggregate)
        {
            IEnumerable<TResult> args = tree.Childrens.Select(item => Aggregate(item, aggregate));
            return aggregate(args, tree.Item);
        }

        public static TreeItem<TOut> Convert<TIn, TOut>(this TreeItem<TIn> tree, Func<TIn, TOut> convertItem)
        {
            return new TreeItem<TOut>
            {
                Item = convertItem(tree.Item),
                Childrens = tree.Childrens.Select(x => Convert(x, convertItem)).ToArray()
            };
        }
    }
}