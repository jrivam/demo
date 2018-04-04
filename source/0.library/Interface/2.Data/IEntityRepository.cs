using library.Impl;
using library.Interface.Entities;

namespace library.Interface.Data
{
    public interface IEntityRepository<T, U> where T : IEntity
                                            where U : IEntityTable<T>
    {
        U Clear();

        (Result result, U data) Select(bool usedbcommand = false);
        (Result result, U data) Insert(bool usedbcommand = false);
        (Result result, U data) Update(bool usedbcommand = false);
        (Result result, U data) Delete(bool usedbcommand = false);
    }
}
