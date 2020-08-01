using jrivam.Library.Extension;
using jrivam.Library.Impl.Persistence.Attributes;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Mapper;
using jrivam.Library.Interface.Persistence.Table;

namespace jrivam.Library.Impl.Persistence.Mapper
{
    public class DataMapper : IDataMapper
    {
        public virtual void Clear<T, U>(U data)
            where T : IEntity
            where U : ITableData<T, U>
        {
            foreach (var column in data.Columns)
            {
                data[column.Description.Name].DbValue = null;
            }
        }

        public virtual U Map<T, U>(U data, int maxdepth = 1, int depth = 0)
            where T : IEntity
            where U : ITableData<T, U>
        {
            foreach (var property in typeof(U).GetPropertiesFromType(isprimitive: true, isforeign: true, attributetypes: new System.Type[] { typeof(DataAttribute) }))
            {
                if (property.isprimitive)
                {
                    var entityproperty = typeof(T).GetPropertyFromType(property.info.Name);
                    data[property.info.Name].DbValue = entityproperty.GetValue(data.Entity);
                }

                if (property.isforeign)
                {
                    depth++;
                    if (depth < maxdepth || maxdepth == 0)
                    {
                        var foreign = property.info.GetValue(data);
                        if (foreign != null)
                        {
                            this.GetType()
                                    .GetMethod(nameof(Map))
                                    .MakeGenericMethod(property.info.PropertyType.BaseType.GetGenericArguments())
                                    .Invoke(this, new object[] { foreign, maxdepth, depth });
                        }
                    }
                }
            }

            return data;
        }
    }
}
