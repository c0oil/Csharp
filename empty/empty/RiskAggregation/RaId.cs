using System;
using System.Runtime.Serialization;
using AlternativeSoft.Common;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    [Serializable]
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaId
    {
        [DataMember]
        public string Id { get; set; }

        public RaId() : this(null) { }
        public RaId(string id)
        {
            Id = id ?? string.Empty;
        }

        public bool Contain(RaId id)
        {
            return Equals(id) || id.Id.StartsWith($"{Id}.");
        }

        public bool IsEmpty()
        {
            return string.IsNullOrEmpty(Id);
        }

        public RaId GetPrevId()
        {
            int dot = Id.LastIndexOf(".");
            return dot > 0 ? Id.Substring(0, dot) : null;
        }

        public bool Equals(RaId other)
        {
            return other != null && Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            RaId p = obj as RaId;
            if (p == null)
            {
                return false;
            }
            
            return Id == p.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id;
        }

        public static implicit operator RaId(string id)
        {
            return new RaId(id);
        }
    }
}