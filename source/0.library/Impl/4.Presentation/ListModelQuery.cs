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
                Messenger.Default.Send<int>(top, $"{Name}Refresh");
            }, delegate (object parameter) { return true; });

            TotalRecords();
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
            this.ClearItems();

            var list = _query.List(_maxdepth, top);

            var load = Load(list.models, _query.Status);

            return (list.result, load);
        }

        public virtual void CommandRefresh((Result result, IListModel<T, U, V, W> models) operation)
        {
        }
    }
}
