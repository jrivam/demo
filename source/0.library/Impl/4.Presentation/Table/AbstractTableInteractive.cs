﻿using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation;
using library.Interface.Presentation.Table;
using System.ComponentModel;
using System.Windows.Input;

namespace library.Impl.Presentation.Table
{
    public abstract class AbstractTableInteractive<T, U, V, W> : ITableInteractive<T, U, V>, INotifyPropertyChanged, IStatus
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>, new()
        where W : ITableInteractive<T, U, V>
    {
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
                OnPropertyChanged("Status");
            }
        } 

        public virtual V Domain { get; protected set; }

        public virtual ITableColumn this[string reference]
        {
            get
            {
                return Domain[reference];
            }
        }

        protected int _maxdepth;

        public virtual event PropertyChangedEventHandler PropertyChanged = delegate { };
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual ICommand LoadCommand { get; protected set; }
        public virtual ICommand SaveCommand { get; protected set; }
        public virtual ICommand EraseCommand { get; protected set; }

        public virtual ICommand EditCommand { get; protected set; }

        public AbstractTableInteractive(int maxdepth = 1)
        {
            _maxdepth = maxdepth;
        }
    }
}
