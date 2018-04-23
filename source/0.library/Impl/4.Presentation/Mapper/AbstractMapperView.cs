using library.Interface.Data.Model;
using library.Interface.Domain.Model;
using library.Interface.Entities;
using library.Interface.Presentation.Mapper;
using library.Interface.Presentation.Model;

namespace library.Impl.Presentation.Mapper
{
    public class AbstractMapperView<T, U, V, W> : IMapperView<T, U, V, W> 
        where T : IEntity
        where U : IEntityTable<T>
        where V : IEntityState<T, U>
        where W : IEntityView<T, U, V>
    {
        public virtual W Clear(W presentation)
        {
            return presentation;
        }

        public virtual W Map(W presentation)
        {
            foreach (var column in presentation.Domain.Data.Columns)
            {
                presentation.OnPropertyChanged(column.Reference);
            }

            return presentation;
        }
    }
}
