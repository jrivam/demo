using library.Impl;
using library.Interface.Domain;

namespace library.Interface.Data
{
    public interface IEntityRepository<T, U> where T : IEntity
                                            where U : IEntityTable<T>
    {
        U Clear();

        (Result result, U data) Select(int maxdepth = 1);
        (Result result, U data) Insert();
        (Result result, U data) Update();
        (Result result, U data) Delete();
    }
}
