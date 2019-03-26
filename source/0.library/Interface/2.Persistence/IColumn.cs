using Library.Impl.Data;
using System;

namespace Library.Interface.Data
{
    public interface IColumn
    {
        Type Type { get; }

        Description Description { get; }
    }
}
