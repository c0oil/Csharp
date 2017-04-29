using System.Collections.Generic;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaDropDowns
    {
        [DataMember]
        public List<string> InvestmentStrategy;
        [DataMember]
        public List<string> AssetsClasses;
        [DataMember]
        public List<string> Instruments;
        [DataMember]
        public List<string> InvestmentStyle;
        [DataMember]
        public List<string> TradingStrategy;
        [DataMember]
        public List<string> MarketExposure;
        [DataMember]
        public List<string> HoldingPeriod;
        [DataMember]
        public List<string> AumMethod;
        [DataMember]
        public List<string> CounterpartyName;
        [DataMember]
        public List<string> CounterpartyType;
        [DataMember]
        public List<string> AccountType;
        [DataMember]
        public List<string> ExpPercentage;
        [DataMember]
        public List<string> VaRMethodology;
        [DataMember]
        public List<string> CounterpartyCount;

        public RaDropDowns()
        {
            InvestmentStrategy = new List<string>();
            AssetsClasses = new List<string>();
            Instruments = new List<string>();
            InvestmentStyle = new List<string>();
            TradingStrategy = new List<string>();
            MarketExposure = new List<string>();
            HoldingPeriod = new List<string>();
            AumMethod = new List<string>();
            CounterpartyName = new List<string>();
            CounterpartyType = new List<string>();
            AccountType = new List<string>();
            ExpPercentage = new List<string>();
            VaRMethodology = new List<string>();
            CounterpartyCount = new List<string>();
        }
    }
}
