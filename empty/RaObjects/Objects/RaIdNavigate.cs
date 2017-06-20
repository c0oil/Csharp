using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RaObjects.Objects
{
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public abstract class RaIdNavigate : RaEntity, IIdNavigate, IDeserializationCallback
    {
        protected IEnumerable<IIdNavigate> subItems;
        public IEnumerable<IIdNavigate> SubItems => subItems;

        public void OnDeserialization(object sender)
        {
            OnDeserialization();
        }

        protected abstract void OnDeserialization();
    }
}