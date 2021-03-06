﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaObjects.Objects
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaStressTestItem : RaEntity, ISubItems<RaStressTestItem>, IIdNavigate
    {
        [DataMember]
        public double? PortfolioReturn;
        [DataMember]
        public double? PercentLongExposure;
        [DataMember]
        public double? PercentShortExposure;
        [DataMember]
        public List<RaStressTestItem> SubItems { get; set; }

        IEnumerable<IIdNavigate> IIdNavigate.SubItems => SubItems;

        public RaStressTestItem()
        {
            SubItems = new List<RaStressTestItem>();
        }
    }
}
