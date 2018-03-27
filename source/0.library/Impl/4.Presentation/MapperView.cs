using library.Interface.Data;
using library.Interface.Domain;
using library.Interface.Entities;
using library.Interface.Presentation;

namespace library.Impl.Presentation
{
    public class MapperView<T, U, V, W> : IMapperView<T, U, V, W> where T : IEntity
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
