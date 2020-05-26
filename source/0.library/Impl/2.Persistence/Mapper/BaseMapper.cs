using Library.Extension;
using Library.Interface.Entities;
using Library.Interface.Persistence.Mapper;
using Library.Interface.Persistence.Table;

namespace Library.Impl.Persistence.Mapper
{
    public class BaseMapper<T, U> : IMapper<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        public BaseMapper()
        {
        }

        public virtual void Clear(U data)
        {
            foreach (var column in data.Columns)
            {
                data[column.Description.Name].DbValue = null;
            }
        }

        public virtual void Map(U data, int maxdepth = 1, int depth = 0)
        {
            foreach (var property in typeof(T).GetPropertiesFromType(isprimitive: true))
            {
                data[property.info.Name].DbValue = property.info.GetValue(data.Entity);
            }
        }
    }
}
