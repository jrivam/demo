using library.Impl;
using library.Impl.Presentation;
using library.Interface.Presentation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Input;

namespace presentation.Model
{
    public partial class Empresa : INotifyPropertyChanged, IEntityView<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>, IEntityInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
        protected int _maxdepth;

        protected domain.Model.Empresa _domain;
        public virtual domain.Model.Empresa Domain
        {
            get
            {
                return _domain;
            }
            protected set
            {
                _domain = value;
            }
        }

        protected readonly IInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> _interactive;

        public Empresa(IInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> interactive,
            domain.Model.Empresa domain, 
            int maxdepth = 1)
        {
            _interactive = interactive;
            _maxdepth = maxdepth;

            _domain = domain;

            ClearCommand = new RelayCommand(delegate (object parameter) { Messenger.Default.Send<presentation.Model.Empresa>(Clear(), "EmpresaClear"); }, null);

            LoadCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, presentation.Model.Empresa entity) operation)>((CommandAction.Load, LoadIn(_maxdepth)), "EmpresaLoad");
            }, delegate (object parameter) { return Domain.Data.Entity.Id != null && Domain.Changed; });
            SaveCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, presentation.Model.Empresa entity) operation)>((CommandAction.Save, Save()), "EmpresaSave");
            }, delegate (object parameter) { return Domain.Changed; });
            EraseCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, presentation.Model.Empresa entity) operation)>((CommandAction.Erase, Erase()), "EmpresaErase");
            }, delegate (object parameter) { return Domain.Data.Entity.Id != null && !Domain.Deleted; });

            EditCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(presentation.Model.Empresa oldvalue, presentation.Model.Empresa newvalue)>((this, this), "EmpresaEdit");
            }, delegate (object parameter) { return Domain.Data.Entity.Id != null && !Domain.Deleted; });
        }
        public Empresa(domain.Model.Empresa domain, 
            int maxdepth = 1)
            : this(new Interactive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>(new presentation.Mapper.Empresa()),
                  domain,
                  maxdepth)
        {
        }
        public Empresa(int maxdepth = 1)
            : this(new domain.Model.Empresa(), 
                  maxdepth)
        {
        }
        public Empresa(entities.Model.Empresa entity, int maxdepth = 1)
            : this(maxdepth)
        {
            Id = entity.Id;
            RazonSocial = entity.RazonSocial;
            Activo = entity.Activo;
        }

        public virtual event PropertyChangedEventHandler PropertyChanged = delegate { };
        public virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual ICommand ClearCommand { get; set; }

        public virtual ICommand LoadCommand { get; set; }
        public virtual ICommand SaveCommand { get; set; }
        public virtual ICommand EraseCommand { get; set; }

        public virtual ICommand EditCommand { get; set; }

        public virtual int? Id { get { return Domain?.Id; } set { if (Domain?.Id != value) { Domain.Id = value; OnPropertyChanged("Id"); } } }
        public virtual string RazonSocial { get { return Domain?.RazonSocial; } set { if (Domain?.RazonSocial != value) { Domain.RazonSocial = value; OnPropertyChanged("RazonSocial"); } } }
        public virtual bool? Activo { get { return Domain?.Activo; } set { if (Domain?.Activo != value) { Domain.Activo = value; OnPropertyChanged("Activo"); } } }

        public virtual presentation.Model.Sucursales Sucursales_Load(int maxdepth = 1, int top = 0)
        {
            if (this.Id != null)
            {
                var query = new presentation.Query.Sucursal();
                query.Domain.Data["IdEmpresa"]?.Where(this.Id);

                Sucursales = new presentation.Model.Sucursales().Load(query, maxdepth, top);
            }

            return _sucursales;
        }
        protected presentation.Model.Sucursales _sucursales;
        public virtual presentation.Model.Sucursales Sucursales
        {
            get
            {
                return _sucursales ?? Sucursales_Load();
            }
            set
            {
                if (_sucursales != value)
                {
                    _sucursales = value;

                    Domain.Sucursales = _sucursales?.Domains;

                    OnPropertyChanged("Sucursales");
                }
            }
        }

        public virtual presentation.Model.Empresa Clear()
        {
            return _interactive.Clear(this, Domain);
        }

        public virtual (Result result, presentation.Model.Empresa presentation) LoadIn(int maxdepth = 1)
        {
            _maxdepth = maxdepth;

            var query = new presentation.Query.Empresa();
            query.Domain.Data["Id"]?.Where(this.Id);

            var load = query.Retrieve(maxdepth, this);

            return load;
        }
        public virtual (Result result, presentation.Model.Empresa presentation) Load(bool usedbcommand = false)
        {
            var load = _interactive.Load(this, Domain, usedbcommand);

            return load;
        }

        public virtual (Result result, presentation.Model.Empresa presentation) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            var save = _interactive.Save(this, Domain, useinsertdbcommand, useupdatedbcommand);

            SaveDependencies();

            return save;
        }
        public virtual (Result result, presentation.Model.Empresa presentation) Erase(bool usedbcommand = false)
        {
            EraseDependencies();

            var erase = _interactive.Erase(this, Domain, usedbcommand);

            return erase;
        }

        protected virtual void SaveDependencies()
        {
            _sucursales?.ToList()?.ForEach(i => { i.Save(); });
        }
        protected virtual void EraseDependencies()
        {
            _sucursales?.ToList()?.ForEach(i => { i.Erase(); });
        }
    }

    public partial class Empresas : ObservableCollection<presentation.Model.Empresa>
    {
        public virtual ICommand AddCommand { get; set; }

        public virtual domain.Model.Empresas Domains
        {
            get
            {
                return new domain.Model.Empresas().Load(this.Select(x => x.Domain).Cast<domain.Model.Empresa>());
            }
        }

        public Empresas()
        {
            AddCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<presentation.Model.Empresa>(null, "EmpresaAdd");
            }, delegate (object parameter) { return this != null; });
        }

        public virtual presentation.Model.Empresas Load(presentation.Query.Empresa query, int maxdepth = 1, int top = 0)
        {
            return Load(query.List(maxdepth, top).presentations);
        }
        public virtual presentation.Model.Empresas Load(IEnumerable<presentation.Model.Empresa> list)
        {
            list?.ToList().ForEach(i => Add(i));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

            return this;
        }

        public virtual void EmpresaLoad((CommandAction action, (Result result, presentation.Model.Empresa presentation) operation) message)
        {
        }
        public virtual void EmpresaSave((CommandAction action, (Result result, presentation.Model.Empresa presentation) operation) message)
        {
        }
        public virtual void EmpresaErase((CommandAction action, (Result result, presentation.Model.Empresa presentation) operation) message)
        {
            if (message.operation.presentation?.Domain.Data.Entity.Id != null)
                this.Remove(message.operation.presentation);
        }

        public virtual void EmpresaAdd(presentation.Model.Empresa presentation)
        {
            if (presentation?.Domain.Data.Entity.Id != null)
                this.Add(presentation);
        }
        public virtual void EmpresaEdit((presentation.Model.Empresa oldvalue, presentation.Model.Empresa newvalue) message)
        {
            if (this.Count > 0)
            {
                var i = this.IndexOf(message.oldvalue);
                if (i >= 0)
                    this[i] = message.newvalue;
            }
        }
    }
}

namespace presentation.Query
{
    public partial class Empresa : IQueryView<data.Query.Empresa, domain.Query.Empresa>, IQueryInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
        public virtual domain.Query.Empresa Domain { get; protected set; }

        protected readonly IInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> _interactive;

        public Empresa(IInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> interactive,
            domain.Query.Empresa domain)
        {
            _interactive = interactive;

            Domain = domain;
        }
        public Empresa(domain.Query.Empresa domain)
            : this(new Interactive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>(new presentation.Mapper.Empresa()),
                  domain)
        {
        }
        public Empresa()
            : this(new domain.Query.Empresa())
        {
        }

        public virtual (Result result, presentation.Model.Empresa presentation) Retrieve(int maxdepth = 1, presentation.Model.Empresa presentation = default(presentation.Model.Empresa))
        {
            return _interactive.Retrieve(Domain, maxdepth, presentation);
        }
        public virtual (Result result, IEnumerable<presentation.Model.Empresa> presentations) List(int maxdepth = 1, int top = 0, IList<presentation.Model.Empresa> presentations = null)
        {
            return _interactive.List(Domain, maxdepth, top, presentations);
        }
    }
}

namespace presentation.Mapper
{
    public partial class Empresa : MapperView<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
    }
}
