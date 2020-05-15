using Library.Impl.Business;
using Library.Interface.Business;
using Library.Interface.Business.Query;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation;
using Library.Interface.Presentation.Query;
using Library.Interface.Presentation.Table;
using System.Windows.Input;

namespace Library.Impl.Presentation
{
    public class ListModelQuery<Q, R, S, T, U, V, W> : ListModel<T, U, V, W>, IListModelRefresh<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>
        where Q : IQueryModel<R, S, T, U, V, W>
    {
        protected Q _query;
        protected int _maxdepth = 1;

        public virtual ICommand RefreshCommand { get; protected set; }

        public ListModelQuery(IListDomain<T, U, V> domains, 
            string name, 
            Q query, int maxdepth = 1, int top = 0)
            : base(domains, name)
        {
            _query = query;
            _maxdepth = maxdepth;

            RefreshCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(Result result, IListModel<T, U, V, W> models)>(Refresh(top), $"{Name}Refresh");
            }, delegate (object parameter) { return true; });
        }
        public ListModelQuery(string name,
            Q query, int maxdepth = 1, int top = 0)
            : this(new ListDomain<T, U, V>(), 
                  name, 
                  query, maxdepth, top)
        {
        }

        public virtual (Result result, IListModel<T, U, V, W> models) Refresh(int top = 0)
        {
            var list = _query.List(_maxdepth, top);

            Status = _query.Status;

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
