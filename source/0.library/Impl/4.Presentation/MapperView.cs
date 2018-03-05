using library.Interface.Business;
using library.Interface.Data;
using library.Interface.Domain;
using library.Interface.Presentation;
using System;
using System.Globalization;
using System.Reflection;

namespace library.Impl.Presentation
{
    public class MapperView<T, U, V, W> : IMapperView<T, U, V, W> where T : IEntity
                                            where U : IEntityTable<T>
                                            where V : IEntityState<T, U>
                                            where W : IEntityView<T, U, V>
    {
        public virtual W Clear(W presentation, int maxdepth = 1, int depth = 0)
        {
            return presentation;
        }

        public virtual W Map(W presentation, int maxdepth = 1, int depth = 0)
        {
            foreach (var column in presentation.Business.Data.Columns)
            {
                presentation.OnPropertyChanged(column.Reference);
            }

            return presentation;
        }
    }
}
