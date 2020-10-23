using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;
using jrivam.Library.Interface.Presentation.Query;
using jrivam.Library.Interface.Presentation.Table;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Input;

namespace jrivam.Library.Impl.Presentation
{
    public partial class ListModelReload<S, T, U, V, W> : ListModelEdit<T, U, V, W>, IListModelReloadAsync<T, U, V, W>, IListModelQuery<S, T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
        where S : IQueryModel<T, U, V, W>
    {
        public S Query { get; set; }
        protected int _maxdepth = 1;

        public virtual ICommand RefreshCommand { get; protected set; }

        public ListModelReload(S query, 
            IListDomainEdit<T, U, V> domains = null,            
            int maxdepth = 1, int top = 0,
            string name = null)
            : base(domains, name)
        {
            Query = query;
            _maxdepth = maxdepth;

            RefreshCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<Task<(Result result, IListModel<T, U, V, W> models)>>(RefreshAsync(top: top), $"{Name}Refresh");
            }, delegate (object parameter) { return true; });
        }

        public virtual async Task<(Result result, IListModel<T, U, V, W> models)> RefreshAsync(int top = 0,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var list = await Query.ListAsync(_maxdepth, top,
                connection,
                commandtimeout).ConfigureAwait(false);

            Status = Query.Status;

            if (list.result.Success)
            {
                var load = Load(list.models, true);

                return (list.result, load);
            }

            return (list.result, default(IListModel<T, U, V, W>));
        }

        public virtual (Result result, IListModel<T, U, V, W> models) CommandRefresh((Result result, IListModel<T, U, V, W> models) operation)
        {
            return operation;
        }
    }
}
