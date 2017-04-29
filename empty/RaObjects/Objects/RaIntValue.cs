using System;
using System.Runtime.Serialization;

namespace RaObjects.Objects
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaIntValue : RaEntity
    {
        [DataMember]
        public Int64? Value;
    }
}
