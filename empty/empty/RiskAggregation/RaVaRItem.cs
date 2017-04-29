using System.Collections.Generic;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaVaRItem : RaEntity, ISubItems<RaVaRItem>, IIdNavigate
    {
        [DataMember]
        public double? PercentExposure;
        [DataMember]
        public double? VaR;
        [DataMember]
        public double? CvaR;
        [DataMember]
        public List<RaVaRItem> SubItems { get; set; }

        IEnumerable<IIdNavigate> IIdNavigate.SubItems => SubItems;

        public RaVaRItem()
        {
            SubItems = new List<RaVaRItem>();
        }
    }
}
