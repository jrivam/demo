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
        private int _maxdepth;

        public virtual domain.Model.Empresa Domain { get; set; } = new domain.Model.Empresa();

        protected readonly IInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> _interactive;

        public Empresa(IInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> interactive, int maxdepth)
        {
            _interactive = interactive;
            _maxdepth = maxdepth;

            ClearCommand = new RelayCommand(delegate (object parameter) { Messenger.Default.Send<presentation.Model.Empresa>(Clear(), "EmpresaClear"); }, null);

            LoadCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, presentation.Model.Empresa entity) operation)>((CommandAction.Load, Load(_maxdepth)), "EmpresaLoad");
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
            : this(new Interactive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>(new presentation.Mapper.Empresa()), maxdepth)
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

        public virtual int? Id
        {
            get { return Domain?.Id; }
            set
            {
                if (Domain?.Id != value)
                {
                    Domain.Id = value;
                    OnPropertyChanged("Id");

                    //if (value == null)
                    //    Clear();

                    //Sucursales?.ForEach(x => x.IdEmpresa = value);
                }
            }
        }
        public virtual string RazonSocial { get { return Domain?.RazonSocial; } set { if (Domain?.RazonSocial != value) { Domain.RazonSocial = value; OnPropertyChanged("RazonSocial"); } } }
        public virtual bool? Activo { get { return Domain?.Activo; } set { if (Domain?.Activo != value) { Domain.Activo = value; OnPropertyChanged("Activo"); } } }

        public virtual void Sucursales_Load(int maxdepth = 1, int top = 0)
        {
            var query = new presentation.Query.Sucursal();
            query.Domains.Data["IdEmpresa"]?.Where(this.Id);

            Sucursales_Load(query, maxdepth, top);
        }
        public virtual void Sucursales_Load(presentation.Query.Sucursal query, int maxdepth = 1, int top = 0)
        {
            Sucursales_Load(query.List(maxdepth, top).presentations.ToList());
        }
        public virtual void Sucursales_Load(IList<presentation.Model.Sucursal> list)
        {
            Sucursales = new presentation.Model.Sucursales().Load(list);
        }

        protected presentation.Model.Sucursales _sucursales;
        public virtual presentation.Model.Sucursales Sucursales
        {
            get
            {
                if (_sucursales == null && this.Id != null)
                {
                    Sucursales_Load();
                }

                return _sucursales;
            }
            set
            {
                _sucursales = value;

                 Domain.Sucursales = Sucursales?.Domains;

                 OnPropertyChanged("Sucursales");
            }
        }

        public virtual presentation.Model.Empresa Clear()
        {
            return _interactive.Clear(this, Domain);
        }
        public virtual (Result result, presentation.Model.Empresa presentation) Load()
        {
            var load = _interactive.Load(this, Domain);

            return load;
        }
        public virtual (Result result, presentation.Model.Empresa presentation) Load(int maxdepth = 1)
        {
            _maxdepth = maxdepth;

            var query = new presentation.Query.Empresa();
            query.Domains.Data["Id"]?.Where(this.Id);

            var load = query.Retrieve(maxdepth, this);

            return load;
        }
        public virtual (Result result, presentation.Model.Empresa presentation) Save()
        {
            var save = _interactive.Save(this, Domain);

            SaveDependencies();

            return save;
        }
        public virtual (Result result, presentation.Model.Empresa presentation) Erase()
        {
            EraseDependencies();

            var erase = _interactive.Erase(this, Domain);

            return erase;
        }

        protected virtual void SaveDependencies()
        {
            Sucursales?.ToList()?.ForEach(i => { i.Save(); });
        }
        protected virtual void EraseDependencies()
        {
            Sucursales?.ToList()?.ForEach(i => { i.Erase(); });
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

        public virtual Empresas Load(presentation.Query.Empresa query, int maxdepth = 1, int top = 0)
        {
            return Load(query.List(maxdepth, top).presentations);
        }
        public virtual Empresas Load(IEnumerable<presentation.Model.Empresa> list)
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
        public virtual domain.Query.Empresa Domains { get; set; } = new domain.Query.Empresa();

        protected readonly IInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> _interactive;

        public Empresa(IInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> interactive)
        {
            _interactive = interactive;
        }
        public Empresa()
            : this(new Interactive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>(new presentation.Mapper.Empresa()))
        {
        }

        public virtual (Result result, presentation.Model.Empresa presentation) Retrieve(int maxdepth = 1, presentation.Model.Empresa presentation = default(presentation.Model.Empresa))
        {
            return _interactive.Retrieve(Domains, maxdepth, presentation);
        }
        public virtual (Result result, IEnumerable<presentation.Model.Empresa> presentations) List(int maxdepth = 1, int top = 0, IList<presentation.Model.Empresa> presentations = null)
        {
            return _interactive.List(Domains, maxdepth, top, presentations);
        }
    }
}

namespace presentation.Mapper
{
    public partial class Empresa : MapperView<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
    }
}
