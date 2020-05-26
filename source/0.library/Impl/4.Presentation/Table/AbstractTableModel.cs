using Library.Extension;
using Library.Interface.Business.Table;
using Library.Interface.Entities;
using Library.Interface.Persistence.Table;
using Library.Interface.Presentation;
using Library.Interface.Presentation.Table;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Library.Impl.Presentation.Table
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

        public virtual IElement this[string name]
        {
            get
            {
                return Elements[name];
            }
        }
        public virtual ListElements<IElement> Elements { get; set; } = new ListElements<IElement>();

        protected virtual void Init()
        {
            Elements.Clear();

            foreach (var property in typeof(T).GetPropertiesFromType(isprimitive: true, iscollection: true, isforeign: true))
            {
                Elements.Add(new Element(property.info.Name));
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

        protected readonly IInteractiveTable<T, U, V, W> _interactive;
        protected readonly int _maxdepth;

        public AbstractTableModel(V domain, IInteractiveTable<T, U, V, W> interactive,
            int maxdepth = 1,
            string name = null)
        {
            _interactive = interactive;
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

            Domain = domain;

            Init();
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
