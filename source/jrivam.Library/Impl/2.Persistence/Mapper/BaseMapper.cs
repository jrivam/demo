using jrivam.Library.Extension;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Mapper;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Impl.Persistence.Mapper
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
