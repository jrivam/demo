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
using System.Windows.Input;

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

        public virtual ICommand AddCommand { get; protected set; }

        public ListModel(IListDomain<T, U, V> domains = null, string name = null)
        {
            _domains = domains ?? new ListDomain<T, U, V>();
            _domains?.ToList()?.ForEach(x => this.Add(Presentation.HelperTableInteractive<T, U, V, W>.CreateModel(x)));

            Name = name ?? this.GetType().Name;

            AddCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<W>(null, $"{Name}Add");
            }, delegate (object parameter) { return this != null; });

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
        public virtual (Result result, W model) CommandSave((CommandAction action, (Result result, W model) operation) message)
        {
            return message.operation;
        }
        public virtual (Result result, W model) CommandErase((CommandAction action, (Result result, W model) operation) message)
        {
            if (message.operation.result.Success)
                ItemRemove(this.FirstOrDefault(x => x.Domain?.Data?.Entity?.Id == message.operation.model?.Domain?.Data?.Entity?.Id));

            return message.operation;
        }

        public virtual void ItemEdit(W oldmodel, W newmodel)
        {
            Domains.ItemEdit(oldmodel.Domain, newmodel.Domain);
        }

        public virtual bool ItemAdd(W model)
        {
            if (model != null)
            {
                if (Domains.ItemAdd(model.Domain))
                {
                    this.Add(model);

                    TotalRecords();
                    return true;
                }
            }

            return false;
        }

        public virtual bool ItemRemove(W model)
        {
            if (model != null)
            {
                if (Domains.ItemRemove(model.Domain))
                {
                    this.Remove(model);

                    TotalRecords();
                    return true;
                }
            }

            return false;
        }

        public virtual void TotalRecords()
        {
            Total = $"{(this.Count == 0 ? "No records" : $"Total records: {this.Count}")}";
        }
    }
}
