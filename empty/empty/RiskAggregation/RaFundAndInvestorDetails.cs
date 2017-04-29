using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaFundAndInvestorDetails : RaIdNavigate, IAggregatedDataSource
    {
        #region 1.1 Fund Name / Base Currency / Date
        [DataMember]
        public string FundName;
        [DataMember]
        public DateTime? Date;
        [DataMember]
        private string baseCurrency;
        public string BaseCurrency
        {
            get { return string.IsNullOrEmpty(baseCurrency)? "USD" : baseCurrency; }
            set { baseCurrency = value; }
        }
        #endregion

        #region 1.2 Manager Details
        [DataMember]
        public string InvestmentManagerName;
        [DataMember]
        public Int64? TotalFirmAum;
        [DataMember]
        public List<RaIntValue> FirmAumDetails;

        public static readonly List<string> ManagerDetailRows = new List<string>
            {
                "Investment Manager Name",
                "Total Firm Assets Under Management",
            };
        #endregion

        #region 1.3 Fund AUM Details
        [DataMember]
        public Int64? TotalAum;
        [DataMember]
        public string AumCalcMethod;
        [DataMember]
        public List<RaIntValue> AumDetails;

        public static readonly List<string> FundAUMDetailRows = new List<string>
            {
                "Total Assets Under Management",
                "Method Used to calculate AUM",
            };
        #endregion

        #region 1.4 Primary Investment Strategy (Drop Down Box)
        [DataMember]
        public List<RaStringValue> PrimaryInvStrategy;
        #endregion

        #region 1.5 Reporting Share Class (RSC) (if applicable)
        [DataMember]
        public Int64? TotalInvestmentInRsc;
        [DataMember]
        public double? PercentOfTotalAumInRsc;
        [DataMember]
        public string CurrencyRsc;
        [DataMember]
        public DateTime? InceptionDateRsc;
        [DataMember]
        public double? ManagementFeeRsc;
        [DataMember]
        public double? PerformanceFeeRsc;
        #endregion

        #region 1.6 Performance
        [DataMember]
        public List<RaPerformanceItem> Performance;
        [DataMember]
        public string HighWaterMark;
        #endregion

        #region 1.7 Investor Break Down
        [DataMember]
        public RaInvestorBreakdown TopInvestors;
        [DataMember]
        public List<RaInvestorBreakdown> InvestorType;
        #endregion

        #region 1.8 Investor Liquidity
        [DataMember]
        public List<RaInvestorLiquidity> InvestorLiquidity;
        #endregion

        #region 1.9 Unencumbered Cash
        [DataMember]
        public double? UnencumberedCash;
        #endregion

        #region 1.10 Investment in External Funds
        [DataMember]
        public double? InvForCashManagement;
        [DataMember]
        public double? InvNonCashManagement;
        #endregion

        [DataMember]
        public string ReportGeneratedBy;
        [DataMember]
        public DateTime? ReportGenerationDate;

        [DataMember]
        public List<string> Captions; 

        public RaFundAndInvestorDetails()
        {
            FirmAumDetails = new List<RaIntValue>();
            AumDetails = new List<RaIntValue>();
            PrimaryInvStrategy = new List<RaStringValue>();
            Performance = new List<RaPerformanceItem>();
            InvestorType = new List<RaInvestorBreakdown>();
            InvestorLiquidity = new List<RaInvestorLiquidity>();
            IsCompletedData = true;
        }

        [DataMember]
        public bool IsAggregatedDataSource { get; set; }
        [DataMember]
        public bool IsCompletedData { get; set; }

        protected override void OnDeserialization()
        {
            var wrapFundAumDetails = new WrapedRaEntityItems(Captions[1], "1.3",
                new List<WrapedRaEntityItems>
                {
                    new WrapedRaEntityItems(FundAUMDetailRows[0], "1.3.1"),
                    new WrapedRaEntityItems(FundAUMDetailRows[1], "1.3.2", AumDetails),
                });
            var wrapManagerDetails = new WrapedRaEntityItems(Captions[0], "1.2",
                new List<WrapedRaEntityItems>
                {
                    new WrapedRaEntityItems(ManagerDetailRows[0], "1.2.1"),
                    new WrapedRaEntityItems(ManagerDetailRows[1], "1.2.2", FirmAumDetails),
                });
            var wrapPrimaryInvStrategy = new WrapedRaEntityItems(Captions[2], "1.4", PrimaryInvStrategy);
            var wrapPerformance = new WrapedRaEntityItems(Captions[4], "1.6", Performance);
            var wrapInvestorBreakdown = new RaInvestorBreakdown { Name = Captions[5], Id = "1.7", SubItems = new[] { TopInvestors }.Concat(InvestorType).ToList() };
            var wrapInvestorLiquidity = new RaInvestorLiquidity { Name = Captions[7], Id = "1.8", SubItems = InvestorLiquidity };

            subItems = new List<IIdNavigate>
            {
                wrapFundAumDetails,
                wrapManagerDetails,
                wrapPerformance,
                wrapPrimaryInvStrategy,
                wrapInvestorBreakdown,
                wrapInvestorLiquidity,
            };
        }
    }
}