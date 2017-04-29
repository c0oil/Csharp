using System.Collections.Generic;

namespace AlternativeSoft.BusinessObjects.RiskAggregation
{
    public interface IMovedSubItems<T> : ISubItems<T>
    {
        List<T> MovedSubItems { get; }
    }

    public interface ISubItems<T> : IName, IId
    {
        List<T> SubItems { get; }
    }

    public interface IName
    {
        string Name { get; set; }
    }
}
