﻿using System;
using System.Runtime.Serialization;

namespace RaObjects.Objects
{
    [Serializable]
    [DataContract(Namespace = ServiceInfo.Namespace)]
    public class RaEntity : IName, IId
    {
        [DataMember]
        public RaId Id { get; set; }

        private string name;
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value?.Trim(); }
        }

        public RaEntity()
        {
            Id = new RaId();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}