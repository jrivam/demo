using jrivam.Library.Impl.Business;
using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace jrivam.Library.Impl.Presentation
{
    public class ListModel<T, U, V, W> : ObservableCollection<W>, IListModel<T, U, V, W>, IStatus
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
    {
        protected IListDomain<T, U, V> _domains;
        public virtual IListDomain<T, U, V> Domains
        {
            get
            {
                return _domains;
            }
        }

        private string _status = string.Empty;
        public string Status
        {
            get
            {
                return _status;
            }
            protected set
            {
                _status = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Status"));
            }
        }

        private string _total = string.Empty;
        public string Total
        {
            get
            {
                return _total;
            }
            protected set
            {
                _total = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Total"));
            }
        }

        public virtual string Name { get; protected set; }

        public ListModel(IListDomain<T, U, V> domains = null, string name = null)
        {
            _domains = domains ?? new ListDomain<T, U, V>();
            _domains?.ToList()?.ForEach(x => this.Add(Presentation.HelperTableInteractive<T, U, V, W>.CreateModel(x)));

            Name = name ?? this.GetType().Name;

            TotalRecords();
        }

        public virtual IListModel<T, U, V, W> Load(IEnumerable<W> models)
        {
            if (models != null)
            {
                models.ToList()?.ForEach(x => this?.Add(x));

                _domains?.Clear();
                _domains?.Load(this.Select(x => x.Domain));

                TotalRecords();
            }

            return this;
        }
        
        public virtual (Result result, W model) CommandLoad((CommandAction action, (Result result, W model) operation) message)
        {
            return message.operation;
        }

        public virtual void TotalRecords()
        {
            Total = $"{(this.Count == 0 ? "No records" : $"Total records: {this.Count}")}";
        }
    }
}
