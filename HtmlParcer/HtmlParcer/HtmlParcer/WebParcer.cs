using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ExcelLibrary;
using HtmlAgilityPack;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace HtmlParcer
{
    public class WebParcer
    {
        private Encoding encode = Encoding.GetEncoding("utf-8");
        
        private void GetInfo(Product product)
        {
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(GetHtmlString(product.Url));

            var productDetailNode = GetContent(html).
                GetChildByClassAttributeStartWith("product_card products_card");

            var productRightNode = productDetailNode.
                GetChildByClassAttribute("forms").
                GetChildByClassAttribute("product_card__inner").
                GetChildByClassAttribute("right");

            var priceNode = productRightNode.
                GetChildByClassAttribute("services_wrap").
                GetChildByClassAttribute("prices_block").
                GetChildByClassAttribute("price_byn").
                GetChildByClassAttribute("price");
            if (priceNode != null)
            {
                product.Price = $"{priceNode.FirstChild.InnerText}.{priceNode.GetChildByClassAttribute("cent").FirstChild.InnerText}";
            }

            var productFactsNode = productRightNode.GetChildByClassAttribute("description");
            product.Mark = productFactsNode.GetSecondColumnByFirst("Торговая марка")?.InnerText;
            product.Weight = productFactsNode.GetSecondColumnByFirst("Масса")?.InnerText;

            var productDescriptionNode = productRightNode.GetChildByClassAttribute("property_group property_group_8")?.ChildNodes[1];
            if (productDescriptionNode != null)
            {
                product.Description = productDescriptionNode.GetSecondColumnByFirst("Состав")?.InnerText;
                product.ShortDescription = productDescriptionNode.GetSecondColumnByFirst("Краткое описание")?.InnerText;
            }

            var productProducerNode = productDetailNode.
                GetChildByClassAttribute("property_group property_group_10");

            product.Producer = productProducerNode.ChildNodes[1].ChildNodes[0].ChildNodes[1].InnerText;
        }

        private HtmlNode GetContent(HtmlDocument html)
        {
            return html.GetElementbyId("body").
                GetChildByClassAttribute("main__wrap").
                GetChildByClassAttribute("main").
                GetChildByClassAttribute("wrapper").
                GetChildByClassAttribute("template_1_columns").
                GetChildByClassAttribute("content");
        }

        private HtmlNode GetCatalogContainerNode(HtmlDocument html)
        {
            return GetContent(html).
                //GetChildByClassAttribute("products_catalog filter_left").
                GetChildByClassAttributeStartWith("products_catalog").
                GetChildByClassAttribute("products_block__wrapper products_4_columns vertical");
        }

        private Product GetProductInfo(HtmlNode productNode)
        {
            var titleNode = productNode.
                GetChildByClassAttribute("form_wrapper").
                GetChildByClassAttribute("forms").
                GetChildByClassAttribute("title").
                GetChildByClassAttribute("fancy_ajax");

            return new Product
            {
                Name = titleNode.InnerText,
                Url = titleNode.Attributes["href"].Value,
            };
        }

        private List<Product> GetProductLinks(HtmlDocument html)
        {
            var result = new List<Product>();

            var container = GetCatalogContainerNode(html);
            foreach (HtmlNode childNode in container.ChildNodes.Where(x => x.IsStartWithClassAttribute("products_card")))
            {
                result.Add(GetProductInfo(childNode));
            }
            
            return result;
        }

        private string GetPostHtmlString(string url)
        {
            // create http request
            HttpWebRequest request = WebRequest.CreateHttp(url) as HttpWebRequest;
            // set post
            request.Method = "POST";
            request.KeepAlive = false;
            request.ContentType = "application/x-www-form-urlencoded";
            // add post data
            string postData = "_=1497649988716&amp;lazy_steep=3";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = byteArray.Length;
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(byteArray, 0, byteArray.Length);
            }
            // get response
            var response = request.GetResponse() as HttpWebResponse;
            using (var stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }
        }

        private string GetFileHtmlString(string fileName)
        {
            using (var fileStream = new FileStream($"{fileName}.htm", FileMode.Open))
            {
                using (StreamReader sReader = new StreamReader(fileStream, encode))
                {
                    return sReader.ReadToEnd();
                }
            }
        }

        private string GetHtmlString(string url)
        {
            var request = WebRequest.Create(url);
            request.Proxy = null;
            var response = request.GetResponse();
            using (StreamReader sReader = new StreamReader(response.GetResponseStream(), encode))
            {
                return sReader.ReadToEnd();
            }
        }
        
        private void SerializeToXml(List<Product> productList, string fileName)
        {
            var example = new Product();

            DataTable table = new DataTable("Product") {Locale = System.Threading.Thread.CurrentThread.CurrentCulture};
            table.Columns.Add(nameof(example.Name));
            table.Columns.Add(nameof(example.Producer));
            table.Columns.Add(nameof(example.Mark));
            table.Columns.Add(nameof(example.Weight));
            table.Columns.Add(nameof(example.Price));
            table.Columns.Add(nameof(example.ShortDescription));
            table.Columns.Add(nameof(example.Description));
            table.Columns.Add(nameof(example.Url));

            foreach (Product product in productList)
            {
                table.Rows.Add(product.Name ?? String.Empty, 
                    product.Producer ?? String.Empty,
                    product.Mark ?? String.Empty, 
                    product.Weight ?? String.Empty, 
                    product.Price ?? String.Empty, 
                    product.ShortDescription ?? String.Empty, 
                    product.Description ?? String.Empty,
                    product.Url ?? String.Empty);
            }

            for (int i = 0; i < 100; i++)
            {
                table.Rows.Add(Enumerable.Repeat(string.Empty, table.Columns.Count).ToArray());
            }

            DataSet ds = new DataSet("DataSet") {Locale = System.Threading.Thread.CurrentThread.CurrentCulture};
            ds.Tables.Add(table);
            
            DataSetHelper.CreateWorkbook($"{fileName}.xls", ds);
        }

        //public void ParcePage(string pageLink)
        public void ParcePage(string fileName)
        {
            HtmlNode.ElementsFlags.Remove("form");
            HtmlDocument html = new HtmlDocument();
            //html.LoadHtml(GetHtmlString(pageLink));
            html.LoadHtml(GetFileHtmlString(fileName));

            var productList = GetProductLinks(html);
            
            foreach (Product product in productList)
            {
                GetInfo(product);
            }

            SerializeToXml(productList, fileName);
        }
    }
}
