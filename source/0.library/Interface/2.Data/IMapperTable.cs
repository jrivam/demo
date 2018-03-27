using library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Data
{
    public interface IMapperTable<T, U> where T : IEntity
                                       where U : IEntityTable<T>
    {
        U Read(U data, IDataReader reader, IList<string> prefixname, string aliasseparator, int maxdepth = 1, int depth = 0);

        U CreateInstance(int maxdepth = 1, int depth = 0);

        U Clear(U data, int maxdepth = 1, int depth = 0);
        U  Map(U data, int maxdepth = 1, int depth = 0);
    }
}