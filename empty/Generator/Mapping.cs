using System.Collections.Generic;

namespace Generator
{
    public enum ElementsContentAs
    {
        Field,
        SubItems,
        Array,
    }

    public class XmlMap
    {
        public string Element { get; set; }
        public string Field { get; set; }

        public XmlElementContentMap ContentMap { get; }
        #region ContentMap properties
        
        public string AttributionGroup
        {
            get { return ContentMap.AttributionGroup; }
            set { ContentMap.AttributionGroup = value; }
        }

        public string Attribution
        {
            get { return ContentMap.Attribution; }
            set { ContentMap.Attribution = value; }
        }

        public string ChildrenElementsMap
        {
            get { return ContentMap.ChildrenElementsMap; }
            set { ContentMap.ChildrenElementsMap = value; }
        }

        public ElementsContentAs ContentAs
        {
            get { return ContentMap.ContentAs; }
            set { ContentMap.ContentAs = value; }
        }

        #endregion

        public XmlMap()
        {
            ContentMap = new XmlElementContentMap();
            ContentAs = ElementsContentAs.Field;
        }
    }

    public class XmlElementContentMap
    {
        public string AttributionGroup { get; set; }
        public string Attribution { get; set; }
        public string ChildrenElementsMap { get; set; }
        public ElementsContentAs ContentAs { get; set; }

        public XmlMap[] ChildrenElementsMapResolved { get; set; }
        public XmlMap[] AttributionsMapResolved { get; set; }
    }

    public static class Mapping
    {
        // Attributions
        private static readonly XmlMap[] aumPercentageAssetValueAttributes =
        {
            new XmlMap { Element = "aumPercentage", Field = "PercentAge"},
            new XmlMap { Element = "assetValue", Field = "Amount"},
        };

        private static Dictionary<string, XmlMap[]> attributionsMaps = new Dictionary<string, XmlMap[]>
        {
            { "aumPercentageAssetValue", aumPercentageAssetValueAttributes},
        };

        // Elements
        private static readonly XmlMap[] fundAumDetailsMaps =
        {
            new XmlMap { Element = "aumCalculationMethod", Field = "AumCalcMethod"},
            new XmlMap { Element = "totalFundAum", Attribution = "assetValue", Field = "TotalAum"},
            new XmlMap { Element = "totalFundAum", Attribution = "assetValue", Field = "AumDetails", ContentAs = ElementsContentAs.Array},
        };

        private static readonly XmlMap[] managerDetailsMaps =
        {
            new XmlMap { Element = "managerName", Field = "InvestmentManagerName"},
            new XmlMap { Element = "totalFirmAum", Field = "TotalFirmAum", Attribution = "assetValue"},
            new XmlMap { Element = "totalFirmAum", Field = "FirmAumDetails", Attribution = "assetValue", ContentAs = ElementsContentAs.Array},
        };

        private static readonly XmlMap[] reportingShareClassMaps =
        {
            new XmlMap { Element = "totalAssetValue", Field = "TotalInvestmentInRsc"},
            new XmlMap { Element = "totalFundAumPercentage", Field = "PercentOfTotalAumInRsc"},
            new XmlMap { Element = "currency", Field = "CurrencyRsc"},
            new XmlMap { Element = "inceptionDate", Field = "InceptionDateRsc"},
            new XmlMap { Element = "managementFee", Field = "ManagementFeeRsc"},
            new XmlMap { Element = "performanceFee", Field = "PerformanceFeeRsc"},
        };

        private static readonly XmlMap[] fundAndInvestorDetailsMaps =
        {
            new XmlMap { Element = "fundName", Field = "FundName"},
            new XmlMap { Element = "date", Field = "Date"},
            new XmlMap { Element = "managerDetails"},
            new XmlMap { Element = "fundAumDetails"},
            new XmlMap { Element = "primaryInvestmentStrategy", Field = "PrimaryInvStrategy", ContentAs = ElementsContentAs.Array},
            new XmlMap { Element = "reportingShareClass"},
            //new XmlMap { Element = "performance", Field = "Performance"},
            new XmlMap { Element = "investorSize/topFiveLargestInvestors", Field = "TopInvestors", AttributionGroup = "aumPercentageAssetValue", ContentAs = ElementsContentAs.SubItems },
            new XmlMap { Element = "investorType", Field = "InvestorType", AttributionGroup = "aumPercentageAssetValue", ContentAs = ElementsContentAs.Array},
            new XmlMap { Element = "investorLiquidity", Field = "InvestorLiquidity", AttributionGroup = "aumPercentageAssetValue", ContentAs = ElementsContentAs.Array},
            new XmlMap { Element = "unencumberedCash", Field = "UnencumberedCash", Attribution = "aumPercentage"},
            new XmlMap { Element = "externalFundInvestment/cashManagementPurpose", Field = "InvForCashManagement", Attribution = "aumPercentage"},
            new XmlMap { Element = "externalFundInvestment/nonCashManagementPurpose", Field = "InvNonCashManagement", Attribution = "aumPercentage"},
        };

        private static readonly XmlMap[] operaMaps =
        {
            new XmlMap { Element = "fundAndInvestorDetails", Field = "FundAndInvestorDetails"},
            /*new XmlMap { Element = "equityExposure", Field = "EquityExposure"},
            new XmlMap { Element = "sovereignInterestRateExposure", Field = "SovereignIntRateExp"},
            new XmlMap { Element = "creditExposure", Field = "Credit"},
            new XmlMap { Element = "convertibleBondExposure", Field = "ConvertibleBondExposure"},
            new XmlMap { Element = "currencyExposure", Field = "CurrencyExposure"},
            new XmlMap { Element = "realAssetExposure", Field = "RealAssetsAndComExposure"},
            new XmlMap { Element = "valueAtRisk", Field = "VaR"},
            new XmlMap { Element = "sensitivity", Field = "Sensivity"},
            new XmlMap { Element = "stressTest", Field = "StressTest"},
            new XmlMap { Element = "counterpartyExposure", Field = "CounterParty"},
            new XmlMap { Element = "otherExposure", Field = "OtherExposure"},*/
        };
        
        private static readonly Dictionary<string, XmlMap[]> childrenElementsMaps = new Dictionary<string, XmlMap[]>
        {
            { "opera", operaMaps},
            { "fundAndInvestorDetails", fundAndInvestorDetailsMaps},

            { "managerDetails", managerDetailsMaps},
            { "fundAumDetails", fundAumDetailsMaps},
            { "reportingShareClass", reportingShareClassMaps},
        };

        public static XmlMap GetRoot()
        {
            var opera = new XmlMap { Element = "opera", Field = "RiskAggregationData" };
            opera.ContentMap.ChildrenElementsMapResolved = ResolveChildrenElements(opera);
            opera.ContentMap.AttributionsMapResolved = ResolveAttributions(opera);
            return opera;
        }

        private static XmlMap[] ResolveChildrenElements(XmlMap elementXmlMap)
        {
            XmlMap[] result = null;
            childrenElementsMaps.TryGetValue(elementXmlMap.Element, out result);
            return result;
        }

        private static XmlMap[] ResolveAttributions(XmlMap elementXmlMap)
        {
            if (elementXmlMap.Attribution != null)
            {
                return new []
                {
                    new XmlMap {  },
                };
            }

            XmlMap[] result = null;
            attributionsMaps.TryGetValue(elementXmlMap.AttributionGroup, out result);
            return result;
        }
    }
}