using library.Impl;
using library.Impl.Presentation;
using library.Interface.Presentation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace presentation.Model
{
    public partial class Sucursal : INotifyPropertyChanged, IEntityView<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>, IEntityInteractive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        protected int _maxdepth;

        protected domain.Model.Sucursal _domain;
        public virtual domain.Model.Sucursal Domain
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

        protected readonly IInteractive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> _interactive;

        public Sucursal(IInteractive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> interactive,
            domain.Model.Sucursal domain,
            int maxdepth = 1)
        {
            _interactive = interactive;
            _maxdepth = maxdepth;

            Domain = domain;

            ClearCommand = new RelayCommand(delegate (object entity) { Messenger.Default.Send<presentation.Model.Sucursal>(Clear(), "SucursalClear"); }, null);

            LoadCommand = new RelayCommand(delegate (object parameter) 
            {
                Messenger.Default.Send<(CommandAction action, (Result result, presentation.Model.Sucursal entity) operation)>((CommandAction.Load, Load(_maxdepth)), "SucursalLoad");
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
        public Sucursal(domain.Model.Sucursal domain, 
            int maxdepth = 1)
            : this(new Interactive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>(new presentation.Mapper.Sucursal()),
                  domain,
                  maxdepth)
        {
        }
        public Sucursal(int maxdepth = 1)
            : this(new domain.Model.Sucursal(), 
                  maxdepth)
        {
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
        public virtual string Nombre { get { return Domain?.Nombre; } set { if (Domain?.Nombre != value) { Domain.Nombre = value; OnPropertyChanged("Nombre"); } } }
        public virtual bool? Activo { get { return Domain?.Activo; } set { if (Domain?.Activo != value) { Domain.Activo = value; OnPropertyChanged("Activo"); } } }
        public virtual DateTime? Fecha { get { return Domain?.Fecha; } set { if (Domain?.Fecha != value) { Domain.Fecha = value; OnPropertyChanged("Fecha"); } } }

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

        protected presentation.Model.Empresa _empresa;
        public virtual presentation.Model.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    Empresa = new presentation.Model.Empresa(Domain.Empresa);
                }

                return _empresa;
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;

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

                    _empresas = new presentation.Model.Empresas().Load(query);
                }

                return _empresas;
            }
        }

        public virtual presentation.Model.Sucursal Clear()
        {
            return _interactive.Clear(this, Domain);
        }
        public virtual (Result result, presentation.Model.Sucursal presentation) Load()
        {
            var load = _interactive.Load(this, Domain);

            return load;
        }
        public virtual (Result result, presentation.Model.Sucursal presentation) Load(int maxdepth = 1)
        {
            _maxdepth = maxdepth;

            var query = new presentation.Query.Sucursal();
            query.Domain.Data["Id"]?.Where(this.Id);         

            var load = query.Retrieve(maxdepth, this);

            return load;
        }
        public virtual (Result result, presentation.Model.Sucursal presentation) Save()
        {
            var save = _interactive.Save(this, Domain);

            SaveDependencies();

            return save;
        }
        public virtual (Result result, presentation.Model.Sucursal presentation) Erase()
        {
            EraseDependencies();

            var erase = _interactive.Erase(this, Domain);

            return erase;
        }

        protected virtual void SaveDependencies()
        {
        }
        protected virtual void EraseDependencies()
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
 
        public virtual Sucursales Load(presentation.Query.Sucursal query, int maxdepth = 1, int top = 0)
        {
            return Load(query.List(maxdepth, top).presentations);
        }
        public virtual Sucursales Load(IEnumerable<presentation.Model.Sucursal> list)
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
    public partial class Sucursal : IQueryView<data.Query.Sucursal, domain.Query.Sucursal>, IQueryInteractive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public virtual domain.Query.Sucursal Domain { get; protected set; }

        protected readonly IInteractive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> _interactive;

        public Sucursal(IInteractive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> interactive,
            domain.Query.Sucursal domain)
        {
            _interactive = interactive;

            Domain = domain;
        }
        public Sucursal(domain.Query.Sucursal domain)
            : this(new Interactive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>(new presentation.Mapper.Sucursal()),
                  domain)
        {
        }
        public Sucursal()
            : this(new domain.Query.Sucursal())
        {
        }

        public virtual (Result result, presentation.Model.Sucursal presentation) Retrieve(int maxdepth = 1, presentation.Model.Sucursal presentation = default(presentation.Model.Sucursal))
        {
            return _interactive.Retrieve(Domain, maxdepth, presentation);
        }
        public virtual (Result result, IEnumerable<presentation.Model.Sucursal> presentations) List(int maxdepth = 1, int top = 0, IList<presentation.Model.Sucursal> presentations = null)
        {
            return _interactive.List(Domain, maxdepth, top, presentations);
        }
    }
}

namespace presentation.Mapper
{
    public partial class Sucursal : MapperView<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
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