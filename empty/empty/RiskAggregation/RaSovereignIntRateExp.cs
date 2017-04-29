using System.Collections.Generic;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaSovereignIntRateExp : RaIdNavigate, IAggregatedDataSource
    {
        [DataMember]
        public RaExposureItem TotalExposure;
        [DataMember]
        public RaExposureItem PercentagePortfolio;
        [DataMember]
        public RaExposureItem AggregateNumberOfPositions;
        [DataMember]
        public RaExposureItem Region;
        [DataMember]
        public RaExposureItem InstrumentsByMaturity;
        [DataMember]
        public RaExposureItem CreditRating;
        [DataMember]
        public RaExposureItem Liquidity;
        [DataMember]
        public bool IsAggregatedDataSource { get; set; }
        [DataMember]
        public bool IsCompletedData { get; set; }

        public RaSovereignIntRateExp()
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
                Region,
                InstrumentsByMaturity,
                CreditRating,
                Liquidity,
            };
        }
    }
}