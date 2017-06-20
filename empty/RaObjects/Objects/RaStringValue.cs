using System.Runtime.Serialization;

namespace RaObjects.Objects
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaStringValue : RaEntity
    {
        [DataMember]
        public string Value;
    }
}
