using System.Collections.Generic;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaOtherExposure : RaIdNavigate, IAggregatedDataSource
    {
        [DataMember]
        public RaExposureItem TotalInvestment;
        [DataMember]
        public RaExposureItem PercentagePortfolio;
        [DataMember]
        public RaExposureItem AggregateNumberOfPositions;
        [DataMember]
        public bool IsAggregatedDataSource { get; set; }
        [DataMember]
        public bool IsCompletedData { get; set; }

        public RaOtherExposure()
        {
            IsCompletedData = true;
        }

        protected override void OnDeserialization()
        {
            subItems = new List<IIdNavigate>
            {
                TotalInvestment,
                PercentagePortfolio,
                AggregateNumberOfPositions,
            };
        }
    }
}