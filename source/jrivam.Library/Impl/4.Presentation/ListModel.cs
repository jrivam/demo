using jrivam.Library.Impl.Business;
using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace jrivam.Library.Impl.Presentation
{
    public class ListModel<T, U, V, W> : ConcurrentObservableCollection<W>, IListModel<T, U, V, W>, IStatus
        where T : IEntity
        where U : ITableData<T, U>
        where V : class, ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
    {
        private readonly object _syncLock = new object();

        protected IListDomain<T, U, V> _domains;
        public virtual IListDomain<T, U, V> Domains
        {
            get
            {
                return _domains;
            }
        }

        private string _status = string.Empty;
        public virtual string Status
        {
            get
            {
                return _status;
            }
            protected set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Status)));
                }
            }
        }

        private string _total = string.Empty;
        public virtual string Total
        {
            get
            {
                return _total;
            }
            protected set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Total)));
                }
            }
        }

        public virtual string Name { get; protected set; }

        protected virtual void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Total = $"{(this.Count == 0 ? "No records" : $"Total records: {this.Count}")}";
        }

        public ListModel(IListDomain<T, U, V> domains = null, string name = null)
        {
            base.CollectionChanged += OnCollectionChanged;
            this.Clear();

            _domains = domains ?? new ListDomain<T, U, V>();
            _domains?.ToList()?.ForEach(x => this.Add(Presentation.HelperTableInteractive<T, U, V, W>.CreateModel(x)));

            Name = name ?? this.GetType().Name;
        }

        public virtual IListModel<T, U, V, W> Load(IEnumerable<W> models, bool clear = false)
        {
            lock (_syncLock)
            {
                if (clear || models == null)
                {
                    Status = "Clearing...";
                    this.Clear();
                    Status = string.Empty;
                }

                if (models != null)
                {
                    Status = "Loading...";
                    this.AddRange(models);
                    Status = string.Empty;

                    //models.ToList()?.ForEach(x =>
                    //{
                    //    this.Add(x);
                    //}); //ObservableCollection.AddRange()

                }

                _domains?.Load(this.Select(x => x.Domain), true);
            }

            return this;
        }
        
        public virtual (Result result, W model) CommandLoad((CommandAction action, (Result result, W model) operation) message)
        {
            return message.operation;
        }
    }
}
