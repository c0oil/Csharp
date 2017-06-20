using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [Serializable]
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaExposureItem : RaEntity, IMovedSubItems<RaExposureItem>, IIdNavigate
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
        public Int64? NumberOfLongPositions;
        [DataMember]
        public Int64? NumberOfShortPositions;
        [DataMember]
        public List<RaExposureItem> SubItems { get; set; }

        public List<RaExposureItem> MovedSubItems { get; }

        IEnumerable<IIdNavigate> IIdNavigate.SubItems => SubItems;

        public RaExposureItem()
        {
            SubItems = new List<RaExposureItem>();
            MovedSubItems = new List<RaExposureItem>();
        }
    }
}
