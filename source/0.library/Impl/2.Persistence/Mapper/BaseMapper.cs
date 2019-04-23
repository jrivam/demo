using Library.Interface.Entities;
using Library.Interface.Persistence.Mapper;
using Library.Interface.Persistence.Table;
using System;
using System.Globalization;
using System.Reflection;

namespace Library.Impl.Persistence.Mapper
{
    public class BaseMapper<T, U> : IMapper<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        public BaseMapper()
        {
        }

        public virtual U CreateInstance(T entity)
        {
            var instance = (U)Activator.CreateInstance(typeof(U),
                    BindingFlags.CreateInstance |
                    BindingFlags.Public |
                    BindingFlags.Instance |
                    BindingFlags.OptionalParamBinding, 
                    null, new object[] { entity }, 
                    CultureInfo.CurrentCulture);

            return instance;
        }


        public virtual U Clear(U data)
        {
            foreach (var column in data.Columns)
            {
                var name = column.Description.Reference;

                data[name].Value = data[name].DbValue = null;
            }

            return data;
        }

        public virtual U Map(U data, int maxdepth = 1, int depth = 0)
        {
            foreach (var column in data.Columns)
            {
                var name = column.Description.Reference;

                var prop = data.GetType().GetProperty(name);

                if (prop != null)
                {
                    data[name].Value = data[name].DbValue = prop.GetValue(data);
                }
            }

            return data;
        }
        public virtual U MapX(U data, int maxdepth = 1, int depth = 0)
        {
            return data;
        }
    }
}
