using System.Collections.Generic;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaCurrencyExposure : RaIdNavigate, IAggregatedDataSource
    {
        [DataMember]
        public RaExposureItem TotalExposure;
        [DataMember]
        public RaExposureItem PercentagePortfolio;
        [DataMember]
        public RaExposureItem AggregateNumberOfParentIssuers;
        [DataMember]
        public RaExposureItem RegionalCurrencies;
        [DataMember]
        public RaExposureItem Instruments;
        [DataMember]
        public RaExposureItem Liquidity;
        [DataMember]
        public bool IsAggregatedDataSource { get; set; }
        [DataMember]
        public bool IsCompletedData { get; set; }

        public RaCurrencyExposure()
        {
            IsCompletedData = true;
        }

        protected override void OnDeserialization()
        {
            subItems = new List<IIdNavigate>
            {
                TotalExposure,
                PercentagePortfolio,
                AggregateNumberOfParentIssuers,
                RegionalCurrencies,
                Instruments,
                Liquidity,
            };
        }
    }
}