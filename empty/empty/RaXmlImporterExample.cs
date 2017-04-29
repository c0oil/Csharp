using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using RaObjects.Objects;

namespace empty
{
    public class RaXmlImporterExample
    {
        public RiskAggregationData Import()
        {
            ElementInfo element = new ElementInfo();


            using (XmlReader reader = new XmlTextReader("opera.xml") { WhitespaceHandling = WhitespaceHandling.None })
            {
                RiskAggregationData result = null;
                ForEachSubItems(reader, () =>
                {
                    if (reader.Name.Equals("opera"))
                    {
                        result = ImportData(reader);
                    }
                });
                return result;
            }
        }

        private IEnumerable<Tuple<string, string>> mapElementToObject = new List<Tuple<string, string>>
        {
            new Tuple<string, string>("fundAndInvestorDetails", "FundAndInvestorDetails"),
            new Tuple<string, string>("equityExposure", "EquityExposure"),
        };
        
        private class ElementInfo
        {
            public string XmlName { get; set; }
            public string ObjectName { get; set; }
            public AttributionGroupInfo Attribution { get; set; }
            public List<ElementInfo> SubItems { get; set; }
        }

        private class AttributionGroupInfo
        {
            public string XmlName { get; set; }
            public string ElementObjectType { get; set; }
            public string ObjectName { get; set; }

            public List<AttributionInfo> Attributions { get; set; }
        }

        private class AttributionInfo
        {
            public string XmlName { get; set; }
            public string ObjectName { get; set; }
            public string ObjectType { get; set; }
        }

        private void ForEachSubItems(XmlReader reader, Action doAction)
        {
            if (reader.IsEmptyElement)
            {
                return;
            }

            reader.Read();
            int depth = reader.Depth;
            do
            {
                if (reader.Depth == depth && reader.IsStartElement() && reader.NodeType == XmlNodeType.Element)
                {
                    doAction();
                }
            } while (reader.Read() && reader.Depth >= depth);
        }

        private RiskAggregationData ImportData(XmlReader reader)
        {
            RiskAggregationData result = new RiskAggregationData();

            ForEachSubItems(reader, () =>
            {
                if (reader.Name.Equals("fundAndInvestorDetails"))
                {
                    result.FundAndInvestorDetails = ImportFundAndInvestorDetails(reader);
                }
                else if (reader.Name.Equals("equityExposure"))
                {
                    result.EquityExposure = ImportEquityExposure(reader);
                }
            });

            return result;
        }

        private RaFundAndInvestorDetails ImportFundAndInvestorDetails(XmlReader reader)
        {
            RaFundAndInvestorDetails result = new RaFundAndInvestorDetails();

            ForEachSubItems(reader, () =>
            {
                if (reader.Name.Equals("fundName"))
                {
                    result.FundName = ImportString(reader);
                }
            });

            return result;
        }

        private RaEquityExposure ImportEquityExposure(XmlReader reader)
        {
            RaEquityExposure result = new RaEquityExposure();

            ForEachSubItems(reader, () =>
            {
                if (reader.Name.Equals("totalAssetValue"))
                {
                    result.TotalExposure = ImportRaExposureItem(reader);
                }
                else if(reader.Name.Equals("totalAumPercentage"))
                {
                    result.PercentagePortfolio = ImportRaExposureItem(reader);
                }
                else if (reader.Name.Equals("totalNumberOfPositions"))
                {
                    result.AggregateNumberOfPositions = ImportRaExposureItem(reader);
                }
                else if (reader.Name.Equals("regionExposure"))
                {
                    result.Region = ImportRaExposureItem(reader);
                }
            });

            return result;
        }

        private RaExposureItem ImportRaExposureItem(XmlReader reader)
        {
            RaExposureItem result = new RaExposureItem();
            ImportRaExposureFields(result, reader);
            ForEachSubItems(reader, () =>
            {
                result.SubItems.Add(ImportRaExposureItem(reader));
            });
            return result;
        }

        private void ImportRaExposureFields(RaExposureItem result, XmlReader reader)
        {
            if (!reader.MoveToFirstAttribute())
            {
                return;
            }

            do
            {
                if (reader.Name.Equals("nonNettedShortAssetValue"))
                {
                    result.NonNettedShortAumExposure = Convert.ToDouble(reader.Value);
                }
                else if (reader.Name.Equals("nonNettedLongAssetValue"))
                {
                    result.NonNettedLongAumExposure = Convert.ToDouble(reader.Value);
                }
                else if (reader.Name.Equals("nettedShortAssetValue"))
                {
                    result.NettedShortAumExposure = Convert.ToDouble(reader.Value);
                }
                else if (reader.Name.Equals("nettedLongAssetValue"))
                {
                    result.NettedLongAumExposure = Convert.ToDouble(reader.Value);
                }
            } while (reader.MoveToNextAttribute());
            reader.MoveToElement();
        }

        private string ImportString(XmlReader reader)
        {
            reader.Read();
            return reader.ReadContentAsString();
        }
    }
}
