using jrivam.Library.Extension;
using jrivam.Library.Impl.Presentation.Attributes;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Raiser;
using jrivam.Library.Interface.Presentation.Table;

namespace jrivam.Library.Impl.Presentation.Raiser
{
    public class ModelRaiser : IModelRaiser        
    {
        public virtual void Clear<T, U, V, W>(W model)
            where T : IEntity
            where U : ITableData<T, U>
            where V : ITableDomain<T, U, V>
            where W : ITableModel<T, U, V, W>
        {
            //model.Validations = new Dictionary<string, string>();

            model.OnPropertyChanged("Validations");
        }

        public virtual void Raise<T, U, V, W>(W model, int maxdepth = 1, int depth = 0)
            where T : IEntity
            where U : ITableData<T, U>
            where V : ITableDomain<T, U, V>
            where W : ITableModel<T, U, V, W>
        {
            foreach (var property in typeof(W).GetPropertiesFromType(isprimitive: true, isforeign: true, attributetypes: new System.Type[] { typeof(ModelAttribute) }))
            {
                if (property.isprimitive)
                {
                    model.OnPropertyChanged(property.info.Name);
                }

                if (property.isforeign)
                {
                    depth++;
                    if (depth < maxdepth || maxdepth == 0)
                    {
                        var foreign = property.info.GetValue(model);
                        if (foreign != null)
                        {
                            this.GetType()
                                    .GetMethod(nameof(Raise))
                                    .MakeGenericMethod(property.info.PropertyType.BaseType.GetGenericArguments())
                                    .Invoke(this, new object[] { foreign, maxdepth, depth });
                        }
                    }
                }
            }
        }
    }
}
