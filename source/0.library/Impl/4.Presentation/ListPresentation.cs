﻿using Library.Impl.Domain;
using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Domain.Query;
using Library.Interface.Domain.Table;
using Library.Interface.Entities;
using Library.Interface.Presentation;
using Library.Interface.Presentation.Query;
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
    public class ListPresentation<Q, R, S, T, U, V, W> : ObservableCollection<W>, IListPresentation<Q, R, S, T, U, V, W>, INotifyPropertyChanged, IStatus
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
        where S : IQueryData<T, U>
        where R : IQueryDomain<S, T, U, V>
        where Q : IQueryModel<R, S, T, U, V, W>
    {
        public virtual ListDomain<T, U, V> Domains
        {
            get
            {
                return new ListDomain<T, U, V>().Load(this?.Select(x => x.Domain));
            }
            set
            {
                value?.ForEach(x => this.Add((W)Activator.CreateInstance(typeof(W),
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

        protected Q _query;
        protected int _maxdepth = 1;

        public virtual ICommand AddCommand { get; set; }
        public virtual ICommand RefreshCommand { get; set; }

        public ListPresentation(ListDomain<T, U, V> domains, 
            string name, 
            Q query, int maxdepth = 1, int top = 0)
        {
            Domains = domains;

            Name = name;

            _query = query;
            _maxdepth = maxdepth;

            AddCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<W>(null, $"{Name}Add");
            }, delegate (object parameter) { return this != null; });
            RefreshCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<int>(top, $"{Name}Refresh");
            }, delegate (object parameter) { return true; });
        }
        public ListPresentation(string name,
            Q query, int maxdepth = 1, int top = 0)
            : this(new ListDomain<T, U, V>(), 
                  name, 
                  query, maxdepth, top)
        {
        }

        public virtual (Result result, ListPresentation<Q, R, S, T, U, V, W> list) Refresh(int top = 0)
        {
            this.ClearItems();

            return LoadQuery(_query, _maxdepth, top);
        }

        public virtual (Result result, ListPresentation<Q, R, S, T, U, V, W> list) LoadQuery(Q query, int maxdepth = 1, int top = 0)
        {
            Status = "Loading...";
            var list = query.List(maxdepth, top);

            Status = (list.result.Success) ? string.Empty : String.Join("/", list.result.Messages.Where(x => x.category == ResultCategory.Error).ToArray()).Replace(Environment.NewLine, string.Empty); ;

            return (list.result, Load(list.presentations));
        }

        public virtual ListPresentation<Q, R, S, T, U, V, W> Load(IEnumerable<W> list)
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
            if (presentation.Domain.Data.Entity?.Id != null)
                this.Add(presentation);

            TotalRecords();
        }
        public virtual void CommandRefresh((Result result, ListPresentation<Q, R, S, T, U, V, W> presentations) operation)
        {
            TotalRecords();
        }

        protected virtual void TotalRecords()
        {
            Status = (this.Count == 0) ? "No records found." : $"Total records: {this.Count}";
        }
    }
}
