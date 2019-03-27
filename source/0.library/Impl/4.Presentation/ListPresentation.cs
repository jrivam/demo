using Library.Impl.Domain;
using Library.Interface.Data.Table;
using Library.Interface.Domain;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using Library.Interface.Presentation;
using Library.Interface.Presentation.Table;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace Library.Impl.Presentation
{
    public class ListPresentation<T, U, V, W> : ObservableCollection<W>, IListPresentation<T, U, V, W>, INotifyPropertyChanged, IStatus
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
    {
        public virtual IListDomain<T, U, V> Domains
        {
            get
            {
                return new ListDomain<T, U, V>().Load(this?.Select(x => x.Domain));
            }
            set
            {
                value?.ToList().ForEach(x => this.Add((W)Activator.CreateInstance(typeof(W),
                           BindingFlags.CreateInstance |
                           BindingFlags.Public |
                           BindingFlags.Instance |
                           BindingFlags.OptionalParamBinding, null,
                           new object[] { x },
                           CultureInfo.CurrentCulture)));
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

        public virtual string Name { get; protected set; }

        public virtual ICommand AddCommand { get; protected set; }

        public ListPresentation(IListDomain<T, U, V> domains, 
            string name)
        {
            Domains = domains;

            Name = name;

            AddCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<W>(null, $"{Name}Add");
            }, delegate (object parameter) { return this != null; });
        }
        public ListPresentation(string name)
            : this(new ListDomain<T, U, V>(),
                  name)
        {
        }

        public virtual IListPresentation<T, U, V, W> Load(IEnumerable<W> list)
        {
            if (list != null)
            {
                foreach (var item in list)
                    this?.CommandAdd(item);
            }

            return this;
        }
        
        public virtual (Result result, W presentation) CommandLoad((CommandAction action, (Result result, W presentation) operation) message)
        {
            return message.operation;
        }
        public virtual (Result result, W presentation) CommandSave((CommandAction action, (Result result, W presentation) operation) message)
        {
            return message.operation;
        }
        public virtual (Result result, W presentation) CommandErase((CommandAction action, (Result result, W presentation) operation) message)
        {
            if (message.operation.result.Success)
                if (message.operation.presentation?.Domain.Data.Entity.Id != null)
                    this.Remove(this.FirstOrDefault(x => x.Domain.Data.Entity.Id == message.operation.presentation?.Domain.Data.Entity.Id));

            TotalRecords();

            return message.operation;
        }

        public virtual void CommandEdit((W oldvalue, W newvalue) message)
        {
            if (this.Count > 0)
            {
                var i = this.IndexOf(message.oldvalue);
                if (i >= 0)
                    this[i] = message.newvalue;
            }
        }

        public virtual void CommandAdd(W presentation)
        {
            if (presentation.Domain?.Data?.Entity?.Id != null)
                this.Add(presentation);

            TotalRecords();
        }

        protected virtual void TotalRecords()
        {
            Status = (this.Count == 0) ? "No records found." : $"Total records: {this.Count}";
        }
    }
}
