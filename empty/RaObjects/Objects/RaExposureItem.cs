using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaObjects.Objects
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
        public string Test;
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
