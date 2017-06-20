using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HtmlParcer
{
    public static class HtmlNodeHelper
    {
        public static HtmlNode GetSecondColumnByFirst(this HtmlNode node, string value)
        {
            return node.ChildNodes.FirstOrDefault(x => x.ChildNodes[0].InnerText.StartsWith(value))?.ChildNodes[1];
        }

        public static HtmlNode GetChildByClassAttribute(this HtmlNode node, string value)
        {
            if (node == null)
                return null;

            var result = node.ChildNodes.FirstOrDefault(x => HaveAttibute(x, "class", value));
            if (result == null)
            {
                
            }
            return result;
        }

        public static HtmlNode GetChildByClassAttributeStartWith(this HtmlNode node, string value)
        {
            return node.ChildNodes.First(x => HaveAttibuteStartWith(x, "class", value));
        }

        private static bool HaveAttibute(HtmlNode node, string name, string value)
        {
            var classAttr = node.Attributes[name];
            return classAttr != null && classAttr.Value == value;
        }

        private static bool HaveAttibuteStartWith(HtmlNode node, string name, string value)
        {
            var classAttr = node.Attributes[name];
            return classAttr != null && classAttr.Value.StartsWith(value);
        }

        public static bool IsStartWithClassAttribute(this HtmlNode node, string value)
        {
            return node.Attributes["class"].Value.StartsWith(value);
        }

    }
}
