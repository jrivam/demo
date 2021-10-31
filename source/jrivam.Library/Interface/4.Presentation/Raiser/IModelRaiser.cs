using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Table;

namespace jrivam.Library.Interface.Presentation.Raiser
{
    public interface IModelRaiser
    {
        void Clear<T, U, V, W>(W model)
            where T : IEntity
            where U : ITableData<T, U>
            where V : ITableDomain<T, U, V>
            where W : ITableModel<T, U, V, W>;


        void Raise<T, U, V, W>(W model, int maxdepth = 1, int depth = 0)
            where T : IEntity
            where U : ITableData<T, U>
            where V : ITableDomain<T, U, V>
            where W : ITableModel<T, U, V, W>;
    }
}