﻿
namespace RaObjects.Objects
{
    public interface IAggregatedDataSource
    {
        bool IsAggregatedDataSource { get; }
        // TODO: Delete this property and all code with him
        bool IsCompletedData { get; }
    }
}