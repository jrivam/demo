using Library.Impl.Business;
using Library.Interface.Business;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation;
using Library.Interface.Presentation.Table;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Library.Impl.Presentation
{
    public class ListModel<T, U, V, W> : ObservableCollection<W>, IListModel<T, U, V, W>, IStatus
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
    {
        protected IListDomain<T, U, V> _domains;
        public virtual IListDomain<T, U, V> Domains
        {
            get
            {
                return _domains;
            }
            set
            {
                if (_domains != value)
                {
                    _domains = value;
                    _domains?.ToList().ForEach(x => this.Add(Presentation.HelperInteractive<T, U, V, W>.CreateInstance(x)));
                }
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

        public ListModel(IListDomain<T, U, V> domains, 
            string name)
        {
            Domains = domains;

            Name = name;

            AddCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<W>(null, $"{Name}Add");
            }, delegate (object parameter) { return this != null; });

            TotalRecords();
        }
        public ListModel(string name)
            : this(new ListDomain<T, U, V>(),
                  name)
        {
        }

        public virtual IListModel<T, U, V, W> Load(IEnumerable<W> list)
        {
            if (list != null)
            {
                list.ToList()?.ForEach(x => this?.Add(x));
                _domains = new ListDomain<T, U, V>().Load(this?.Select(x => x.Domain));

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
            if (Domains.ItemAdd(model.Domain))
            {
                this.Add(model);

                TotalRecords();
                return true;
            }

            return false;
        }

        public virtual bool ItemRemove(W model)
        {
            if (Domains.ItemRemove(model.Domain))
            {
                this.Remove(model);

                TotalRecords();
                return true;
            }

            return false;
        }

        public virtual void TotalRecords()
        {
            Total = $"{(this.Count == 0 ? "No records" : $"Total records: {this.Count}")}";
        }
    }
}
