using jrivam.Library.Impl.Persistence;
using System;

namespace jrivam.Library.Interface.Persistence
{
    public interface IColumn
    {
        Type Type { get; }

        Description Description { get; }
    }
}
