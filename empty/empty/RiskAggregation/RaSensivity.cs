using System.Collections.Generic;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaSensivity : RaIdNavigate, IAggregatedDataSource
    {
        [DataMember]
        public RaDoubleValue LongExposureIncluded;
        [DataMember]
        public RaDoubleValue ShortExposureIncluded;
        [DataMember]
        public string SensivityCaption;
        [DataMember]
        public List<RaSensivityItem> Sensivities;

        public RaSensivity()
        {
            Sensivities = new List<RaSensivityItem>();
            IsCompletedData = true;
        }

        [DataMember]
        public bool IsAggregatedDataSource { get; set; }
        [DataMember]
        public bool IsCompletedData { get; set; }
        
        protected override void OnDeserialization()
        {
            subItems = new List<IIdNavigate>
            {
                new WrapedRaEntityItems(SensivityCaption, "9.8", Sensivities)
            };
        }
    }
}