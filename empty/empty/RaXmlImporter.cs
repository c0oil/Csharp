using System;
using System.Xml;
using RaObjects.Objects;

namespace empty
{
    public class RaXmlImportGenerator
	{
  
        private RiskAggregationData ImportRiskAggregationData(XmlReader reader)
        {
            RiskAggregationData result = new RiskAggregationData();
            
            ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("fundAndInvestorDetails"))
				{
					result.FundAndInvestorDetails = ImportRaFundAndInvestorDetails(reader);
				}
				else if (reader.Name.Equals("equityExposure"))
				{
					result.EquityExposure = ImportRaEquityExposure(reader);
				}
            });
            return result;
        }
  
        private RaFundAndInvestorDetails ImportRaFundAndInvestorDetails(XmlReader reader)
        {
            RaFundAndInvestorDetails result = new RaFundAndInvestorDetails();
            
            ForEachSubItems(reader, () =>
            {
				if (reader.Name.Equals("fundName"))
				{
					result.FundName = ImportSimpleTypes.ImportString(reader);
				}
				else if (reader.Name.Equals("date"))
				{
					result.Date = ImportSimpleTypes.ImportDateTime(reader);
				}
            });
            return result;
        }
  
        private RaEquityExposure ImportRaEquityExposure(XmlReader reader)
        {
            RaEquityExposure result = new RaEquityExposure();
            
            ForEachSubItems(reader, () =>
            {
            });
            return result;
        }
  
        private RaExposureItem ImportRaExposureItem(XmlReader reader)
        {
            RaExposureItem result = new RaExposureItem();
            ImportRaExposureItemFields(result, reader);
            ForEachSubItems(reader, () =>
            {
				result.SubItems.Add(ImportRaExposureItem(reader));
            });
            return result;
        }
		private void ImportRaExposureItemFields(RaExposureItem result, XmlReader reader)
		{
			if (!reader.MoveToFirstAttribute())
			{
				return;
			}

			do
			{
				if (reader.Name.Equals("nonNettedShortAssetValue"))
				{
					result.NonNettedShortAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nonNettedLongAssetValue"))
				{
					result.NonNettedLongAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nettedShortAssetValue"))
				{
					result.NettedShortAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("nettedLongAssetValue"))
				{
					result.NettedLongAumExposure = ImportSimpleTypes.ImportDoubleNullable(reader);
				}
				else if (reader.Name.Equals("numberOfLongPositions"))
				{
					result.NumberOfLongPositions = ImportSimpleTypes.ImportLongNullable(reader);
				}
				else if (reader.Name.Equals("numberOfShortPositions"))
				{
					result.NumberOfShortPositions = ImportSimpleTypes.ImportLongNullable(reader);
				}
			} while (reader.MoveToNextAttribute());
			reader.MoveToElement();
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

		private static class ImportSimpleTypes
		{
			public static DateTime ImportDateTimeNullable(XmlReader reader)
			{
				return string.IsNullOrEmpty(reader.Value) ? DateTime.MinValue : Convert.ToDateTime(reader.Value);
			}

			public static DateTime? ImportDateTime(XmlReader reader)
			{
				return string.IsNullOrEmpty(reader.Value) ? new DateTime?() : Convert.ToDateTime(reader.Value);
			}

			public  static long ImportLongNullable(XmlReader reader)
			{
				return string.IsNullOrEmpty(reader.Value) ? 0 : Convert.ToInt64(reader.Value);
			}

			public static long? ImportLong(XmlReader reader)
			{
				return string.IsNullOrEmpty(reader.Value) ? new long?() : Convert.ToInt64(reader.Value);
			}

			public  static double ImportDouble(XmlReader reader)
			{
				return string.IsNullOrEmpty(reader.Value) ? Double.NaN : Convert.ToDouble(reader.Value);
			}

			public static double? ImportDoubleNullable(XmlReader reader)
			{
				return string.IsNullOrEmpty(reader.Value) ? new double?() : Convert.ToDouble(reader.Value);
			}

			public static string ImportString(XmlReader reader)
			{
				return reader.Value;
			}
		}
	}
}
