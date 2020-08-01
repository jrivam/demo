using jrivam.Library.Impl.Business;
using jrivam.Library.Impl.Entities;
using jrivam.Library.Impl.Persistence;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Entities;
using jrivam.Library.Interface.Persistence.Table;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace jrivam.Library.Impl.Presentation.Table
{
    public abstract class AbstractTableModel<T, U, V, W> : TableModelValidation, ITableModel<T, U, V, W>
        where T : IEntity
        where U : ITableData<T, U>
        where V : ITableDomain<T, U, V>
        where W : class, ITableModel<T, U, V, W>
    {
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

        public virtual string Name { get; protected set; }

        public override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (Domain.Changed)
            {
                OnStatusChange("Editing");
            }
        }

        protected string _status = string.Empty;
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
        public virtual void OnStatusChange(string status)
        {
            if (_status != status)
            {
                _status = status;
                base.OnPropertyChanged("Status");
            }
        }

        public virtual ICommand LoadCommand { get; protected set; }
        public virtual ICommand SaveCommand { get; protected set; }
        public virtual ICommand EraseCommand { get; protected set; }

        public virtual ICommand EditCommand { get; protected set; }

        protected readonly int _maxdepth;

        protected readonly IInteractiveTable<T, U, V, W> _interactivetable;

        public AbstractTableModel(IInteractiveTable<T, U, V, W> interactivetable,
            V domain = default(V), 
            int maxdepth = 1,
            string name = null)
        {
            _interactivetable = interactivetable;

            if (domain == null)
                _domain = HelperTableLogic<T, U, V>.CreateDomain(HelperTableRepository<T, U>.CreateData(HelperEntities<T>.CreateEntity()));
            else
                _domain = domain;

            _maxdepth = maxdepth;

            Name = name ?? typeof(T).Name;

            LoadCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W model) operation)>((CommandAction.Load, LoadQuery(_maxdepth)), $"{Name}Load");
            }, delegate (object parameter) { return this.Domain.Data.Entity.Id != null && this.Domain.Changed; });
            SaveCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W model) operation)>((CommandAction.Save, Save()), $"{Name}Save");
            }, delegate (object parameter) { return this.Domain.Changed; });
            EraseCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, W model) operation)>((CommandAction.Erase, Erase()), $"{Name}Erase");
            }, delegate (object parameter) { return this.Domain.Data.Entity.Id != null && !this.Domain.Deleted; });

            EditCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<W>(this as W, $"{Name}Edit");
            }, delegate (object parameter) { return this.Domain.Data.Entity.Id != null && !this.Domain.Deleted; });

            Init();
        }

        protected virtual void Init()
        {
        }

        public virtual (Result result, W model) LoadQuery(int maxdepth = 1,
            IDbConnection connection = null)
        {
            var loadquery = _interactivetable.LoadQuery(this as W, maxdepth, 
                connection);
            return loadquery;
        }
        public virtual (Result result, W model) Load(bool usedbcommand = false,
            IDbConnection connection = null)
        {
            var load = _interactivetable.Load(this as W, usedbcommand,
                connection);

            return load;
        }

        public virtual (Result result, W model) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var save = _interactivetable.Save(this as W, useinsertdbcommand, useupdatedbcommand,
                connection, transaction);

            return save;
        }
        public virtual (Result result, W model) Erase(bool usedbcommand = false,
            IDbConnection connection = null, IDbTransaction transaction = null)
        {
            var erase = _interactivetable.Erase(this as W, usedbcommand,
                connection, transaction);

            return erase;
        }

        public void ValidateProperty(object value, [CallerMemberName] string propertyName = null)
        {
            var validationContext = new ValidationContext(Domain.Data.Entity, null, null);
            validationContext.MemberName = propertyName;
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(value, validationContext, validationResults);

            //clear previous _errors from tested property  
            if (_errors.ContainsKey(propertyName))
                _errors.Remove(propertyName);
            OnErrorsChanged(propertyName);

            var resultsByPropNames = from res in validationResults
                                     from mname in res.MemberNames
                                     group res by mname into g
                                     select g;

            foreach (var prop in resultsByPropNames)
            {
                var messages = prop.Select(r => r.ErrorMessage).ToList();
                _errors.Add(prop.Key, messages);
                OnErrorsChanged(prop.Key);
            }
        }
    }
}
