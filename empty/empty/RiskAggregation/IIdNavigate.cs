using System.Collections.Generic;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
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