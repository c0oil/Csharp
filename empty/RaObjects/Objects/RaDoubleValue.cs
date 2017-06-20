using System.Runtime.Serialization;

namespace RaObjects.Objects
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaDoubleValue : RaEntity
    {
        [DataMember]
        public double? Value;
    }
}
