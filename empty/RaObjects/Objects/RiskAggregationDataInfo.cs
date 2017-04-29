using System;
using System.Runtime.Serialization;

namespace RaObjects.Objects
{
    [DataContract]
    public class RiskAggregationDataInfo
    {
        [DataMember]
        private readonly RiskAggregationData value;
        [DataMember]
        private readonly DateTime date;

        public RiskAggregationDataInfo(RiskAggregationData value, DateTime date)
        {
            this.value = value;
            this.date = date;
        }

        public RiskAggregationData Value
        {
            get { return value; }
        }

        public DateTime Date
        {
            get { return date; }
        }
    }
}
