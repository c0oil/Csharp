using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaObjects.Objects
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaConvertibleBondExposure : RaIdNavigate, IAggregatedDataSource
    {
        [DataMember]
        public RaExposureItemI TotalExposure;
        [DataMember]
        public RaExposureItemI PercentagePortfolio;
        [DataMember]
        public RaExposureItemI AggregateNumberOfParentIssuers;
        [DataMember]
        public RaExposureItemI Sectors;
        [DataMember]
        public RaExposureItemI Region;
        [DataMember]
        public RaExposureItemI Instrument;
        [DataMember]
        public RaExposureItemI Derivative;
        [DataMember]
        public RaExposureItemI CreditRating;
        [DataMember]
        public RaExposureItemI MaturityBuckets;
        [DataMember]
        public RaExposureItemI ConcentrationOfOwnership;
        [DataMember]
        public RaExposureItemI Liquidity;
        [DataMember]
        public bool IsAggregatedDataSource { get; set; }
        [DataMember]
        public bool IsCompletedData { get; set; }

        public RaConvertibleBondExposure()
        {
            IsCompletedData = true;
        }

        protected override void OnDeserialization()
        {
            subItems = new List<IIdNavigate>
            {
                TotalExposure,
                PercentagePortfolio,
                AggregateNumberOfParentIssuers,
                Sectors,
                Region,
                Instrument,
                Derivative,
                CreditRating,
                MaturityBuckets,
                ConcentrationOfOwnership,
                Liquidity,
            };
        }
    }
}