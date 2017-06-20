using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [Serializable]
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaExposureItemI : RaEntity, IMovedSubItems<RaExposureItemI>, IIdNavigate
    {
        [DataMember]
        public double? NonNettedLongAumExposure;
        [DataMember]
        public double? NonNettedShortAumExposure;
        [DataMember]
        public double? NettedLongAumExposure;
        [DataMember]
        public double? NettedShortAumExposure;
        [DataMember]
        public Int64? NumberOfLongIssuers;
        [DataMember]
        public Int64? NumberOfShortIssuers;
        [DataMember]
        public List<RaExposureItemI> SubItems { get; set; }

        public List<RaExposureItemI> MovedSubItems { get; }

        IEnumerable<IIdNavigate> IIdNavigate.SubItems => SubItems;

        public RaExposureItemI()
        {
            SubItems = new List<RaExposureItemI>();
            MovedSubItems = new List<RaExposureItemI>();
        }
    }
}
