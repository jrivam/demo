using Library.Impl.Persistence;
using System;

namespace Library.Interface.Persistence
{
    public interface IColumn
    {
        Type Type { get; }

        Description Description { get; }
    }
}
