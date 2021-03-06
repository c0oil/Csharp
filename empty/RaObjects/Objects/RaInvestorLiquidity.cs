﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaObjects.Objects
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaInvestorLiquidity : RaEntity, ISubItems<RaInvestorLiquidity>, IIdNavigate
    {
        [DataMember]
        public double? PercentWithoutPenalty;
        [DataMember]
        public double? PercentWithPenalty;
        [DataMember]
        public List<RaInvestorLiquidity> SubItems { get; set; }

        IEnumerable<IIdNavigate> IIdNavigate.SubItems => SubItems;

        public RaInvestorLiquidity()
        {
            SubItems = new List<RaInvestorLiquidity>();
        }
    }
}
