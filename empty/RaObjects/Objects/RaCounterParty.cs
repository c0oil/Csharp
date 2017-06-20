using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaObjects.Objects
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaCounterParty : RaIdNavigate, IAggregatedDataSource
    {
        [DataMember]
        public RaCounterPartyItem TradingAssets;
        [DataMember]
        public RaCounterPartyItem TreasureAssets;
        [DataMember]
        public RaCounterPartyItem OtherAssets;
        [DataMember]
        public RaCounterPartyItem TradingAssetsBySize;
        [DataMember]
        public RaCounterPartyItem TradingAssetsByGeography;
        [DataMember]
        public RaCounterPartyItem TreasureAssetsByGeography;
        [DataMember]
        public RaCounterPartyItem OtherAssetsByGeography;
        [DataMember]
        public RaCounterPartyItem TradingAssetsByLockup;
        [DataMember]
        public string PercentAggNetCreditCpExp;
        [DataMember]
        public string PercentFinancingUncoll;
        [DataMember]
        public bool IsAggregatedDataSource { get; set; }
        [DataMember]
        public bool IsCompletedData { get; set; }

        public RaCounterParty()
        {
            IsCompletedData = true;
        }

        protected override void OnDeserialization()
        {
            subItems = new List<IIdNavigate>
            {
                TradingAssets,
                TreasureAssets,
                OtherAssets,
                TradingAssetsBySize,
                TradingAssetsByGeography,
                TreasureAssetsByGeography,
                OtherAssetsByGeography,
                TradingAssetsByLockup,
            };
        }
    }
}