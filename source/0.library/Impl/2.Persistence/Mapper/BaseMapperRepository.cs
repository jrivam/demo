using Library.Interface.Data.Mapper;
using Library.Interface.Data.Table;
using Library.Interface.Entities;
using System;
using System.Globalization;
using System.Reflection;

namespace Library.Impl.Data.Mapper
{
    public class BaseMapperRepository<T, U> : IMapperRepository<T, U> 
        where T : IEntity
        where U : ITableData<T, U>
    {
        public BaseMapperRepository()
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


        public virtual U Clear(U data, int maxdepth = 1, int depth = 0)
        {
            return data;
        }
        public virtual U Map(U data, int maxdepth = 1, int depth = 0)
        {
            return data;
        }

        public virtual U Extra(U data, int maxdepth = 1, int depth = 0)
        {
            return data;
        }
    }
}
