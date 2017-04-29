using System.Collections.Generic;

namespace RaObjects.Objects
{
    public interface IId
    {
        RaId Id { get; set; }
    }

    public interface IIdNavigate : IId, IName
    {
        IEnumerable<IIdNavigate> SubItems { get; }
    }
}