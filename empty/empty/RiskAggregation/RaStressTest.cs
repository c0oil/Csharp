using System.Collections.Generic;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaStressTest : RaIdNavigate, IAggregatedDataSource
    {
        [DataMember]
        public RaStressTestItem Scenarios;
        [DataMember]
        public RaStressTestItem HistoricalStress;
        [DataMember]
        public RaStressTestItemD HistoricalPortfolioStress;
        [DataMember]
        public bool IsAggregatedDataSource { get; set; }
        [DataMember]
        public bool IsCompletedData { get; set; }

        public RaStressTest()
        {
            IsCompletedData = true;
        }

        protected override void OnDeserialization()
        {
            subItems = new List<IIdNavigate>
            {
                Scenarios,
                HistoricalStress,
                HistoricalPortfolioStress,
            };
        }
    }
}