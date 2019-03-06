using library.Impl.Data.Definition;
using System;

namespace library.Interface.Data
{
    public interface IColumn
    {
        Type Type { get; }

        Description Description { get; }
    }
}
