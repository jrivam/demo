using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Mapper;
using library.Interface.Presentation.Table;

namespace library.Impl.Presentation.Mapper
{
    public class BaseMapperInteractive<T, U, V, W> : IMapperInteractive<T, U, V, W> 
        where T : IEntity
        where U : ITableRepositoryProperties<T>
        where V : ITableLogicProperties<T, U>
        where W : ITableInteractiveProperties<T, U, V>
    {
        public virtual W Clear(W presentation, int maxdepth = 1, int depth = 0)
        {
            return presentation;
        }

        public virtual W Raise(W presentation)
        {
            foreach (var column in presentation.Domain.Data.Columns)
            {
                presentation.OnPropertyChanged(column.ColumnDescription.Reference);
            }

            return presentation;
        }
    }
}
