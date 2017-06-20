using System;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaIntValue : RaEntity
    {
        [DataMember]
        public Int64? Value;
    }
}
