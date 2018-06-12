using library.Interface.Data.Table;
using library.Interface.Entities;
using System.Collections.Generic;
using System.Data;

namespace library.Interface.Data.Mapper
{
    public interface IMapperRepository<T, U> 
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
    {
        U Read(U data, IDataReader reader, IList<string> prefixname, int maxdepth = 1, int depth = 0);

        U CreateInstance(int maxdepth = 1, int depth = 0);

        U Clear(U data, int maxdepth = 1, int depth = 0);
        U  Map(U data, int maxdepth = 1, int depth = 0);
    }
}