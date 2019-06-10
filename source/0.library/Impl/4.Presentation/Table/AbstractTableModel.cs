using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation.Table;
using System.Collections.Generic;
using System.Windows.Input;

namespace Library.Impl.Presentation.Table
{
    public abstract class AbstractTableModel<T, U, V, W> : NotifyPropertyChanged, ITableModel<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>, new()
        where W : class, ITableModel<T, U, V, W>
    {
        public virtual T Entity
        {
            get
            {
                return Domain.Entity;
            }
            set
            {
                Domain.Entity = value;

                OnPropertyChanged("Entity");
            }
        }

        protected V _domain;
        public virtual V Domain
        {
            get
            {
                return _domain;
            }
            set
            {
                _domain = value;
            }
        }

        public override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (Domain.Changed)
            {
                OnStatusChange("Editing");
            }
        }

        public virtual void OnStatusChange(string status)
        {
            if (_status != status)
            {
                _status = status;
                base.OnPropertyChanged("Status");
            }
        }

        private string _status = string.Empty;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    OnStatusChange(value);
                }
            }
        }

        public Dictionary<string, string> Validations { get; set; } = new Dictionary<string, string>();
        public bool Validate(string key, string value)
        {
            var contains = Validations.ContainsKey(key);

            if ((contains && Validations[key] != value) || (!contains && value != string.Empty))
            {
                Validations[key] = value;

                OnPropertyChanged("Validations");

                return true;
            }

            return false;
        }

        public virtual IColumnTable this[string reference]
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

        protected readonly IInteractiveTable<T, U, V, W> _interactive;

        public AbstractTableModel(V domain, IInteractiveTable<T, U, V, W> interactive,
            int maxdepth = 1)
        {
            _interactive = interactive;
            _maxdepth = maxdepth;

            LoadCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W model) operation)>((CommandAction.Load, LoadQuery(_maxdepth)), $"{Domain.Data.Description.Reference}Load");
            }, delegate (object parameter) { return this.Domain.Data.Entity.Id != null && this.Domain.Changed; });
            SaveCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W model) operation)>((CommandAction.Save, Save()), $"{Domain.Data.Description.Reference}Save");
            }, delegate (object parameter) { return this.Domain.Changed; });
            EraseCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W model) operation)>((CommandAction.Erase, Erase()), $"{Domain.Data.Description.Reference}Erase");
            }, delegate (object parameter) { return this.Domain.Data.Entity.Id != null && !this.Domain.Deleted; });

            EditCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<W>(this as W, $"{Domain.Data.Description.Reference}Edit");
            }, delegate (object parameter) { return this.Domain.Data.Entity.Id != null && !this.Domain.Deleted; });

            Domain = domain;
        }

        public string CheckIsUnique()
        {
            var checkisunique = Domain?.Data?.CheckIsUnique();

            return checkisunique?.result.FilteredAsTextSelected<string>("/", x => x.category == ResultCategory.Error, y => y.message);
        }
        public string CheckIsRequiredColumn(string columnname)
        {
            var checkisrequiredcolumn = Domain?.Data?.CheckIsEmptyColumn(columnname);

            return checkisrequiredcolumn?.result.FilteredAsTextSelected<string>("/", x => x.category == ResultCategory.Error, y => y.message);
        }

        public virtual (Result result, W model) Load(bool usedbcommand = false)
        {
            var load = _interactive.Load(this as W, usedbcommand);

            return load;
        }
        public virtual (Result result, W model) LoadQuery(int maxdepth = 1)
        {
            var loadquery = _interactive.LoadQuery(this as W, maxdepth);

            return loadquery;
        }

        public virtual (Result result, W model) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            var save = _interactive.Save(this as W, useinsertdbcommand, useupdatedbcommand);

            return save;
        }
        public virtual (Result result, W model) Erase(bool usedbcommand = false)
        {
            var erase = _interactive.Erase(this as W, usedbcommand);

            return erase;
        }
    }
}
