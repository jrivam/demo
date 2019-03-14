using library.Interface.Data.Table;
using library.Interface.Domain.Table;
using library.Interface.Entities;
using library.Interface.Presentation;
using library.Interface.Presentation.Table;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;
using System;

namespace library.Impl.Presentation.Table
{
    public abstract class AbstractTableInteractive<T, U, V, W> : ITableInteractive<T, U, V>, INotifyPropertyChanged, IStatus
        where T : IEntity
        where U : ITableRepository, ITableEntity<T>
        where V : ITableLogic<T, U>, new()
        where W : ITableInteractive<T, U, V>
    {
        public virtual void OnStatusChange(string status)
        {
            _status = status;
            PropertyChanged(this, new PropertyChangedEventArgs("Status"));
        }

        public virtual event PropertyChangedEventHandler PropertyChanged = delegate { };
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            if (Domain.Changed)
            {
                OnStatusChange("Editing");
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
                if (_status != value)
                {
                    OnStatusChange(value);
                }
            }
        }

        public Dictionary<string, string> Validations { get; } = new Dictionary<string, string>();
        public string Validation
        {
            get
            {
                return String.Join("/", Validations.Select(x => x.Value).ToArray<string>()).Replace(Environment.NewLine, string.Empty);
            }
        }

        public virtual V Domain { get; set; }

        public virtual ITableColumn this[string reference]
        {
            get
            {
                return Domain[reference];
            }
        }

        protected int _maxdepth;

        public virtual ICommand LoadCommand { get; protected set; }
        public virtual ICommand SaveCommand { get; protected set; }
        public virtual ICommand EraseCommand { get; protected set; }

        public virtual ICommand EditCommand { get; protected set; }

        public AbstractTableInteractive(V domain, int maxdepth = 1)
        {
            Domain = domain;

            _maxdepth = maxdepth;
        }
    }
}
