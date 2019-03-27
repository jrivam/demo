using Library.Impl.Domain;
using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Domain;
using Library.Interface.Domain.Query;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using Library.Interface.Presentation;
using Library.Interface.Presentation.Query;
using Library.Interface.Presentation.Table;
using System.ComponentModel;
using System.Windows.Input;

namespace Library.Impl.Presentation
{
    public class ListPresentationQuery<Q, R, S, T, U, V, W> : ListPresentation<T, U, V, W>, IListPresentationRefresh<T, U, V, W>, INotifyPropertyChanged, IStatus
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

        public ListPresentationQuery(IListDomain<T, U, V> domains, 
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
        }
        public ListPresentationQuery(string name,
            Q query, int maxdepth = 1, int top = 0)
            : this(new ListDomain<T, U, V>(), 
                  name, 
                  query, maxdepth, top)
        {
        }

        public virtual (Result result, IListPresentation<T, U, V, W> list) Refresh(int top = 0)
        {
            this.ClearItems();

            var list = _query.List(_maxdepth, top);

            var load = Load(list.presentations);

            return (list.result, load);
        }

        public virtual void CommandRefresh((Result result, IListPresentation<T, U, V, W> presentations) operation)
        {
            TotalRecords();
        }
    }
}
