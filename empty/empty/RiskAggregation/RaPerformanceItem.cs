using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaPerformanceItem : RaEntity
    {
        [DataMember]
        public double? Month { get; set; }
        [DataMember]
        public double? Qtd { get; set; }
        [DataMember]
        public double? Ytd { get; set; }
        [DataMember]
        public double? Itd { get; set; }
    }
}
