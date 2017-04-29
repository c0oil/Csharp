using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaStressTestItemD : RaEntity, ISubItems<RaStressTestItemD>, IIdNavigate
    {
        [DataMember]
        public double? PortfolioReturn;
        [DataMember]
        public double? PercentLongExposure;
        [DataMember]
        public double? PercentShortExposure;
        [DataMember]
        public DateTime? StartDate;

        [DataMember]
        public List<RaStressTestItemD> SubItems { get; set; }

        IEnumerable<IIdNavigate> IIdNavigate.SubItems => SubItems;

        public RaStressTestItemD()
        {
            SubItems = new List<RaStressTestItemD>();
        }
    }
}