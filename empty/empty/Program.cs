using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using RaObjects.Objects;

namespace empty
{
    public static class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public static void Main(string[] args)
        {
            using (XmlReader reader = new XmlTextReader("opera.xml") { WhitespaceHandling = WhitespaceHandling.None })
            {
                while (reader.Name != "opera")
                {
                    reader.Read();
                }
                RiskAggregationData result = RaXmlImportGenerator.ImportRiskAggregationData(reader, null);
            }
            //new RaXmlImporterExample().Import();
            /*Opera newPerson = null;
            using (FileStream fs = new FileStream("opera.xml", FileMode.OpenOrCreate))
            {

                var serializer = new XmlSerializer(typeof(Opera));
                newPerson = serializer.Deserialize(fs) as Opera;
            }*/
        }
    }
}
