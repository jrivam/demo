using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation.Mapper;
using library.Interface.Presentation.Table;

namespace library.Impl.Presentation.Mapper
{
    public class BaseMapperInteractive<T, U, V, W> : IMapperInteractive<T, U, V, W> 
        where T : IEntity
        where U : IEntityRepositoryProperties<T>
        where V : IEntityLogicProperties<T, U>
        where W : IEntityInteractiveProperties<T, U, V>
    {
        public virtual W Clear(W presentation)
        {
            return presentation;
        }

        public virtual W Map(W presentation)
        {
            foreach (var column in presentation.Domain.Data.Columns)
            {
                presentation.OnPropertyChanged(column.ColumnDescription.Reference);
            }

            return presentation;
        }
    }
}
