using System.Collections.Generic;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaEquityExposure : RaIdNavigate, IAggregatedDataSource
    {
        [DataMember]
        public RaExposureItem TotalExposure;
        [DataMember]
        public RaExposureItem PercentagePortfolio;
        [DataMember]
        public RaExposureItem AggregateNumberOfPositions;
        [DataMember]
        public RaExposureItem Sectors;
        [DataMember]
        public RaExposureItem Region;
        [DataMember]
        public RaExposureItem Instruments;
        [DataMember]
        public RaExposureItem MarketCapitalExposure;
        [DataMember]
        public RaExposureItem Liquidity;
        [DataMember]
        public bool IsAggregatedDataSource { get; set; }
        [DataMember]
        public bool IsCompletedData { get; set; }

        public RaEquityExposure()
        {
            IsCompletedData = true;
        }

        protected override void OnDeserialization()
        {
            subItems = new List<IIdNavigate>
            {
                TotalExposure,
                PercentagePortfolio,
                AggregateNumberOfPositions,
                Sectors,
                Region,
                Instruments,
                MarketCapitalExposure,
                Liquidity,
            };
        }
    }
}