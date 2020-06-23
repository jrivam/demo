﻿using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;
using jrivam.Library.Interface.Presentation.Query;
using jrivam.Library.Interface.Presentation.Table;
using System.Windows.Input;

namespace jrivam.Library.Impl.Presentation
{
    public class ListModelQuery<S, T, U, V, W> : ListModel<T, U, V, W>, IListModelQuery<S, T, U, V, W>, IListModelRefresh<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
        where S : IQueryModel<T, U, V, W>
    {
        public S Query { get; set; }
        protected int _maxdepth = 1;

        public virtual ICommand RefreshCommand { get; protected set; }

        public ListModelQuery(S query, 
            IListDomain<T, U, V> domains = null,            
            int maxdepth = 1, int top = 0,
            string name = null)
            : base(domains, name)
        {
            Query = query;
            _maxdepth = maxdepth;

            RefreshCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(Result result, IListModel<T, U, V, W> models)>(Refresh(top), $"{Name}Refresh");
            }, delegate (object parameter) { return true; });
        }

        public virtual (Result result, IListModel<T, U, V, W> models) Refresh(int top = 0)
        {
            var list = Query.List(_maxdepth, top);

            Status = Query.Status;

            if (list.result.Success)
            {
                this.ClearItems();

                var load = Load(list.models);

                return (list.result, load);
            }

            return (list.result, null);
        }

        public virtual (Result result, IListModel<T, U, V, W> models) CommandRefresh((Result result, IListModel<T, U, V, W> models) operation)
        {
            return operation;
        }
    }
}
