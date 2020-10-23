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
using System.Threading.Tasks;
using System.Windows.Input;

namespace jrivam.Library.Impl.Presentation.Table
{
    public abstract partial class AbstractTableModel<T, U, V, W> : TableModelValidation, ITableModel<T, U, V, W>
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

        protected readonly IInteractiveTableAsync<T, U, V, W> _interactivetableasync;

        public AbstractTableModel(IInteractiveTableAsync<T, U, V, W> interactivetableasync,
            V domain = default(V), 
            int maxdepth = 1,
            string name = null)
        {
            _interactivetableasync = interactivetableasync;

            if (domain == null)
                _domain = HelperTableLogic<T, U, V>.CreateDomain(HelperTableRepository<T, U>.CreateData(HelperEntities<T>.CreateEntity()));
            else
                _domain = domain;

            _maxdepth = maxdepth;

            Name = name ?? typeof(T).Name;

            LoadCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, Task<(Result result, W model)> operation)>((CommandAction.Load, LoadQueryAsync(maxdepth: _maxdepth)), $"{Name}Load");
            }, delegate (object parameter) { return this.Domain.Data.Entity.Id != null && this.Domain.Changed; });
            SaveCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, Task<(Result result, W model)> operation)>((CommandAction.Save, SaveAsync()), $"{Name}Save");
            }, delegate (object parameter) { return this.Domain.Changed; });
            EraseCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, Task<(Result result, W model)> operation)>((CommandAction.Erase, EraseAsync()), $"{Name}Erase");
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

        public virtual async Task<(Result result, W model)> LoadQueryAsync(int maxdepth = 1,
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var loadquery = await _interactivetableasync.LoadQueryAsync(this as W,
                maxdepth,
                connection,
                commandtimeout).ConfigureAwait(false);
            return loadquery;
        }

        public virtual async Task<(Result result, W model)> LoadAsync(
            IDbConnection connection = null,
            int? commandtimeout = null)
        {
            var load = await _interactivetableasync.LoadAsync(this as W,
                connection,
                commandtimeout).ConfigureAwait(false);

            return load;
        }

        public virtual async Task<(Result result, W model)> SaveAsync(
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            var save = await _interactivetableasync.SaveAsync(this as W,
                connection, transaction,
                commandtimeout).ConfigureAwait(false);

            return save;
        }

        public virtual async Task<(Result result, W model)> EraseAsync(
            IDbConnection connection = null, IDbTransaction transaction = null,
            int? commandtimeout = null)
        {
            var erase = await _interactivetableasync.EraseAsync(this as W,
                connection, transaction,
                commandtimeout).ConfigureAwait(false);

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
