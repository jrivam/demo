using library.Impl;
using library.Impl.Presentation;
using library.Impl.Presentation.Mapper;
using library.Impl.Presentation.Model;
using library.Impl.Presentation.Query;
using library.Interface.Presentation.Model;
using library.Interface.Presentation.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace presentation.Model
{
    public partial class Sucursal : AbstractEntityInteractive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public Sucursal(IInteractiveView<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> interactive,
            int maxdepth = 1)
            : base(interactive, maxdepth)
        {
            ClearCommand = new RelayCommand(delegate (object entity) { Messenger.Default.Send<presentation.Model.Sucursal>(Clear(), "SucursalClear"); }, null);

            LoadCommand = new RelayCommand(delegate (object parameter) 
            {
                Messenger.Default.Send<(CommandAction action, (Result result, presentation.Model.Sucursal entity) operation)>((CommandAction.Load, LoadIn(_maxdepth)), "SucursalLoad");
            }, delegate (object parameter) { return Domain.Data.Entity.Id != null && Domain.Changed; });

            SaveCommand = new RelayCommand(delegate (object parameter) 
            {
                Messenger.Default.Send<(CommandAction action, (Result result, presentation.Model.Sucursal entity) operation)>((CommandAction.Save, Save()), "SucursalSave");
            }, delegate (object parameter) { return Domain.Changed; });
            EraseCommand = new RelayCommand(delegate (object parameter) 
            {
                Messenger.Default.Send<(CommandAction action, (Result result, presentation.Model.Sucursal entity) operation)>((CommandAction.Erase, Erase()), "SucursalErase");
            }, delegate (object parameter) { return Domain.Data.Entity.Id != null && !Domain.Deleted; });

            EditCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(presentation.Model.Sucursal oldvalue, presentation.Model.Sucursal newvalue)>((this, this), "SucursalEdit");
            }, delegate (object parameter) { return Domain.Data.Entity.Id != null && !Domain.Deleted; });
        }
        public Sucursal(int maxdepth = 1)
            : this(new InteractiveView<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>(new presentation.Mapper.Sucursal()),
                  maxdepth)
        {
        }
        public Sucursal(domain.Model.Sucursal domain, int maxdepth = 1)
            : this(maxdepth)
        {
            Domain = domain;
        }
        public Sucursal(entities.Model.Sucursal entity, int maxdepth = 1)
            : this(maxdepth)
        {
            SetProperties(entity);
        }

        public virtual int? Id { get { return Domain?.Id; } set { if (Domain?.Id != value) { Domain.Id = value; OnPropertyChanged("Id"); } } }
        public virtual string Nombre { get { return Domain?.Nombre; } set { if (Domain?.Nombre != value) { Domain.Nombre = value; OnPropertyChanged("Nombre"); } } }
        public virtual DateTime? Fecha { get { return Domain?.Fecha; } set { if (Domain?.Fecha != value) { Domain.Fecha = value; OnPropertyChanged("Fecha"); } } }
        public virtual bool? Activo { get { return Domain?.Activo; } set { if (Domain?.Activo != value) { Domain.Activo = value; OnPropertyChanged("Activo"); } } }

        public virtual int? IdEmpresa
        {
            get
            {
                return Domain?.IdEmpresa;
            }
            set
            {
                if (Domain?.IdEmpresa != value)
                {
                    Domain.IdEmpresa = value;
                    OnPropertyChanged("IdEmpresa");

                    Empresa = null;
                }
            }
        }

        public presentation.Model.Empresa Empresa_Load()
        {
            if (this.IdEmpresa != null)
            {
                Empresa = new presentation.Model.Empresa(Domain.Empresa);
            }

            return _empresa;
        }
        protected presentation.Model.Empresa _empresa;
        public virtual presentation.Model.Empresa Empresa
        {
            get
            {
                return _empresa ?? Empresa_Load();
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;

                    Domain.Empresa = _empresa?.Domain;

                    OnPropertyChanged("Empresa");
                }
            }
        }

        protected presentation.Model.Empresas _empresas;
        public virtual presentation.Model.Empresas Empresas
        {
            get
            {
                if (_empresas == null)
                {
                    var query = new presentation.Query.Empresa();
                    query.Domain.Data["Activo"]?.Where(true);

                    Empresas = new presentation.Model.Empresas().Load(query);
                }

                return _empresas;
            }
            set
            {
                if (_empresas != value)
                {
                    _empresas = value;

                    Domain.Empresas = _empresas?.Domains;

                    OnPropertyChanged("Empresas");
                }
            }
        }

        public virtual (Result result, presentation.Model.Sucursal presentation) LoadIn(int maxdepth = 1)
        {
            _maxdepth = maxdepth;

            var query = new presentation.Query.Sucursal();
            query.Domain.Data["Id"]?.Where(this.Id);         

            var load = query.Retrieve(maxdepth, this);

            return load;
        }

        protected override void SaveDependencies()
        {
        }
        protected override void EraseDependencies()
        {
        }
    }

    public partial class Sucursales : ObservableCollection<presentation.Model.Sucursal>
    {
        public virtual ICommand AddCommand { get; set; }

        public virtual domain.Model.Sucursales Domains
        {
            get
            {
                return new domain.Model.Sucursales().Load(this.Select(x => x.Domain).Cast<domain.Model.Sucursal>());
            }
        }

        public Sucursales()
        {
            AddCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<presentation.Model.Sucursal>(null, "SucursalAdd");
            }, delegate (object parameter) { return this != null; });
        }
 
        public virtual presentation.Model.Sucursales Load(presentation.Query.Sucursal query, int maxdepth = 1, int top = 0)
        {
            return Load(query.List(maxdepth, top).presentations);
        }
        public virtual presentation.Model.Sucursales Load(IEnumerable<presentation.Model.Sucursal> list)
        {
            list?.ToList().ForEach(i => Add(i));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

            return this;
        }

        public virtual void SucursalLoad((CommandAction action, (Result result, presentation.Model.Sucursal presentation) operation) message)
        {
        }
        public virtual void SucursalSave((CommandAction action, (Result result, presentation.Model.Sucursal presentation) operation) message)
        {
        }
        public virtual void SucursalErase((CommandAction action, (Result result, presentation.Model.Sucursal presentation) operation) message)
        {
            if (message.operation.presentation?.Domain.Data.Entity.Id != null)
                this.Remove(message.operation.presentation);
        }

        public virtual void SucursalAdd(presentation.Model.Sucursal presentation)
        {
            if (presentation.Domain.Data.Entity?.Id != null)
                this.Add(presentation);
        }
        public virtual void SucursalEdit((presentation.Model.Sucursal oldvalue, presentation.Model.Sucursal newvalue) message)
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
    public partial class Sucursal : AbstractQueryInteractive<data.Query.Sucursal, domain.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public Sucursal(IInteractiveQuery<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> interactive)
            : base(interactive)
        {
        }
        public Sucursal()
            : this(new InteractiveQuery<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>(new presentation.Mapper.Sucursal()))
        {
        }
        public Sucursal(domain.Query.Sucursal domain)
            : this()
        {
            Domain = domain;
        }

        protected presentation.Query.Empresa _empresa;
        public virtual presentation.Query.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    Empresa = new presentation.Query.Empresa();
                }

                return _empresa;
            }
            set { if (_empresa != value) { _empresa = value; } }
        }
    }
}

namespace presentation.Mapper
{
    public partial class Sucursal : AbstractMapperView<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public override presentation.Model.Sucursal Clear(presentation.Model.Sucursal presentation)
        {
            presentation = base.Clear(presentation);

            presentation.Empresa = null;

            return presentation;
        }
        public override presentation.Model.Sucursal Map(presentation.Model.Sucursal presentation)
        {
            presentation = base.Map(presentation);

            presentation.OnPropertyChanged("Empresa");

            return presentation;
        }
    }
}