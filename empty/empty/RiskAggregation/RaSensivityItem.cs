using System.Collections.Generic;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaSensivityItem : RaEntity, IIdNavigate
    {
        [DataMember]
        public double? Beta;
        [DataMember]
        public double? Delta;
        [DataMember]
        public double? Gamma;
        [DataMember]
        public double? Vega;
        [DataMember]
        public double? Theta;
        [DataMember]
        public double? Cs01;
        [DataMember]
        public double? Dv01;

        IEnumerable<IIdNavigate> IIdNavigate.SubItems => null;

        public bool IsEmpty
        {
            get
            {
                return !Beta.HasValue &&
                       !Delta.HasValue &&
                       !Gamma.HasValue &&
                       !Vega.HasValue &&
                       !Theta.HasValue &&
                       !Cs01.HasValue &&
                       !Dv01.HasValue;
            }
        }
    }
}
