﻿using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Raiser;
using jrivam.Library.Interface.Presentation.Table;

namespace jrivam.Library.Impl.Presentation.Raiser
{
    public class BaseRaiser<T, U, V, W> : IRaiser<T, U, V, W> 
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : ITableModel<T, U, V, W>
    {
        public virtual void Clear(W model)
        {
           //model.Validations = new Dictionary<string, string>();

            model.OnPropertyChanged("Validations");
        }

        public virtual void Raise(W model, int maxdepth = 1, int depth = 0)
        {
            foreach (var element in model.Elements)
            {
                model.OnPropertyChanged(element.Name);
            }
        }
    }
}