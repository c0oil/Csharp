using System.Collections.Generic;
using System.Linq;

namespace RaObjects.Objects
{
    public class WrapedRaEntityItems : IIdNavigate
    {
        private readonly RaEntity raEntity;

        public RaId Id
        {
            get { return raEntity.Id; }
            set { raEntity.Id = value; }
        }

        public string Name
        {
            get { return raEntity.Name; }
            set { raEntity.Name = value; }
        }

        public IEnumerable<IIdNavigate> SubItems { get; set; }

        public WrapedRaEntityItems(RaEntity raEntity)
        {
            this.raEntity = raEntity;
            SubItems = null;
        }
        
        public WrapedRaEntityItems(string name, string id, IEnumerable<RaEntity> items)
        {
            raEntity = new RaEntity { Id = id, Name = name };
            SubItems = items != null ? new List<IIdNavigate>(items.Select(x => new WrapedRaEntityItems(x))) : null;
        }
        
        public WrapedRaEntityItems(string name, string id, IEnumerable<WrapedRaEntityItems> items = null)
        {
            raEntity = new RaEntity { Id = id, Name = name };
            SubItems = items != null ? new List<IIdNavigate>(items) : null;
        }

        public override string ToString()
        {
            return raEntity.ToString();
        }
    }
}