using library.Impl;
using library.Interface.Entities;

namespace library.Interface.Data
{
    public interface IEntityRepository<T, U> where T : IEntity
                                            where U : IEntityTable<T>
    {
        U Clear();

        (Result result, U data) Select();
        (Result result, U data) Insert();
        (Result result, U data) Update();
        (Result result, U data) Delete();
    }
}
