using library.Impl;
using library.Impl.Presentation;
using library.Impl.Presentation.Mapper;
using library.Impl.Presentation.Model;
using library.Impl.Presentation.Query;
using library.Interface.Presentation.Model;
using library.Interface.Presentation.Query;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Windows.Input;

namespace presentation.Model
{
    public partial class Empresa : AbstractEntityInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
        public Empresa(IInteractiveView<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> interactive,
            int maxdepth = 1)
            : base(interactive, maxdepth)
        {
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
        public Empresa(int maxdepth = 1)
            : this(new InteractiveView<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>(new presentation.Mapper.Empresa()),
                  maxdepth)
        {
        }
        public Empresa(domain.Model.Empresa domain, int maxdepth = 1)
            : this(maxdepth)
        {
            Domain = domain;
        }
        public Empresa(entities.Model.Empresa entity, int maxdepth = 1)
            : this(maxdepth)
        {
            SetProperties(entity);
        }


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

        public virtual (Result result, presentation.Model.Empresa presentation) LoadIn(int maxdepth = 1)
        {
            _maxdepth = maxdepth;

            var query = new presentation.Query.Empresa();
            query.Domain.Data["Id"]?.Where(this.Id);

            var load = query.Retrieve(maxdepth, this);

            return load;
        }

        protected override void SaveDependencies()
        {
            _sucursales?.ToList()?.ForEach(i => { i.Save(); });
        }
        protected override void EraseDependencies()
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
    public partial class Empresa : AbstractQueryInteractive<data.Query.Empresa, domain.Query.Empresa, entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
        public Empresa(IInteractiveQuery<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> interactive)
            : base(interactive)
        {
        }
        public Empresa()
            : this(new InteractiveQuery<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>(new presentation.Mapper.Empresa()))
        {
        }
        public Empresa(domain.Query.Empresa domain)
            : this()
        {
            Domain = domain;
        }
    }
}

namespace presentation.Mapper
{
    public partial class Empresa : AbstractMapperView<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
    }
}
