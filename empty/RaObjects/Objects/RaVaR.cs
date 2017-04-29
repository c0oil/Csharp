using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaObjects.Objects
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaVaR : RaIdNavigate, IAggregatedDataSource
    {
        [DataMember]
        public string MethodologyCaption;
        [DataMember]
        public RaStringValue VaRMethodology;
        [DataMember]
        public RaStringValue MethodologyDetails;
        [DataMember]
        public RaDoubleValue DecayFactor;
        [DataMember]
        public RaIntValue LookbackPeriod;
        /*public List<RaVaRItem> AssetClass;
        public List<RaVaRItem> Region;
        public List<RaVaRItem> Sector;*/
        [DataMember]
        public RaVaRItem AssetClass;
        [DataMember]
        public RaVaRItem Region;
        [DataMember]
        public RaVaRItem Sector;
        [DataMember]
        public RaDoubleValue LongExposureIncluded;
        [DataMember]
        public RaDoubleValue ShortExposureIncluded;
        [DataMember]
        public bool IsAggregatedDataSource { get; set; }
        [DataMember]
        public bool IsCompletedData { get; set; }

        public RaVaR()
        {
            IsCompletedData = true;
        }
        
        protected override void OnDeserialization()
        {
            subItems = new List<IIdNavigate>
            {
                AssetClass,
                Region,
                Sector,
            };
        }
    }
}