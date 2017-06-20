using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaObjects.Objects
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaRealAssetsAndComExposure : RaIdNavigate, IAggregatedDataSource
    {
        [DataMember]
        public RaExposureItem TotalExposure;
        [DataMember]
        public RaExposureItem PercentagePortfolio;
        [DataMember]
        public RaExposureItem AggregateNumberOfParentIssuers;
        [DataMember]
        public RaExposureItem Region;
        [DataMember]
        public RaExposureItem CommodityType;
        [DataMember]
        public RaExposureItem CommoditiesInstruments;
        [DataMember]
        public RaExposureItem RealEstate;
        [DataMember]
        public RaExposureItem Timberland;
        [DataMember]
        public RaExposureItem Infrastructure;
        [DataMember]
        public RaExposureItem Liquidity;
        [DataMember]
        public bool IsAggregatedDataSource { get; set; }
        [DataMember]
        public bool IsCompletedData { get; set; }

        public RaRealAssetsAndComExposure()
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
                Region,
                CommodityType,
                CommoditiesInstruments,
                RealEstate,
                Timberland,
                Infrastructure,
                Liquidity,
            };
        }
    }
}