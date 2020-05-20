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

        public virtual U Clear(U data)
        {
            foreach (var column in data.Columns)
            {
                var name = column.Description.Name;

                data[name].DbValue = null;
            }

            return data;
        }

        public virtual U Map(U data, int maxdepth = 1, int depth = 0)
        {
            foreach (var column in Persistence.HelperTableRepository<T, U>.GetPropertiesValue(data))
            {
                data[column.name].DbValue = column.value;
            }

            return data;
        }
        public virtual U MapX(U data, int maxdepth = 1, int depth = 0)
        {
            return data;
        }
    }
}
