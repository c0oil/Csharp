﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaObjects.Objects
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaInvestorBreakdown : RaEntity, ISubItems<RaInvestorBreakdown>, IIdNavigate
    {
        [DataMember]
        public double? PercentAge;
        [DataMember]
        public double? Amount;
        [DataMember]
        public List<RaInvestorBreakdown> SubItems { get; set; }

        IEnumerable<IIdNavigate> IIdNavigate.SubItems => SubItems;

        public RaInvestorBreakdown()
        {
            SubItems = new List<RaInvestorBreakdown>();
        }
    }
}
