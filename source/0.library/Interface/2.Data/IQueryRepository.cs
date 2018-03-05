using library.Impl;
using library.Interface.Domain;
using System.Collections.Generic;

namespace library.Interface.Data
{
    public interface IQueryRepository<T, U> where T : IEntity
                                            where U : IEntityTable<T>
    {
        (Result result, U data) SelectSingle(int maxdepth);
        (Result result, IEnumerable<U> datas) SelectMultiple(int maxdepth, int top);

        (Result result, int rows) Update(U data, int maxdepth);
        (Result result, int rows) Delete(int maxdepth);
    }
}
