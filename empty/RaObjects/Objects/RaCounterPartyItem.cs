using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaObjects.Objects
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaCounterPartyItem : RaEntity, ISubItems<RaCounterPartyItem>, IIdNavigate
    {
        [DataMember]
        public string Number;
        [DataMember]
        public double? Equity;
        [DataMember]
        public double? Lmv;
        [DataMember]
        public double? Smv;
        [DataMember]
        public double? Cash;
        [DataMember]
        public double? OteMmt;
        [DataMember]
        public double? AvailableLiquidity;
        [DataMember]
        public double? RequiredMargin;
        [DataMember]
        public double? LongAumExposurePercent;
        [DataMember]
        public double? ShortAumExposurePercent;
        [DataMember]
        public List<RaCounterPartyItem> SubItems { get; set; }

        IEnumerable<IIdNavigate> IIdNavigate.SubItems => SubItems;

        public RaCounterPartyItem()
        {
            SubItems = new List<RaCounterPartyItem>();
        }
    }
}
