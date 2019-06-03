using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation.Raiser;
using Library.Interface.Presentation.Table;
using System.Collections.Generic;

namespace Library.Impl.Presentation.Raiser
{
    public class BaseRaiser<T, U, V, W> : IRaiser<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        public virtual W Clear(W model)
        {
            model.Validations = new Dictionary<string, string>();

            model.OnPropertyChanged("Validations");

            return model;
        }

        public virtual W Raise(W model, int maxdepth = 1, int depth = 0)
        {
            foreach (var column in model.Domain.Data.Columns)
            {
                model.OnPropertyChanged(column.Description.Reference);
            }

            return model;
        }
        public virtual W RaiseX(W model, int maxdepth = 1, int depth = 0)
        {
            return model;
        }
    }
}
