using library.Impl;
using library.Impl.Business;
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
    public partial class Empresa : INotifyPropertyChanged, IEntityView<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa>, IEntityInteractive<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa, presentation.Model.Empresa>
    {
        public partial class Mapper : MapperView<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa, presentation.Model.Empresa>
        {
            public override presentation.Model.Empresa Map(presentation.Model.Empresa presentation, int maxdepth = 1, int depth = 0)
            {
                return base.Map(presentation, maxdepth, depth);
            }
        }

        public virtual business.Model.Empresa Business { get; set; } = new business.Model.Empresa();

        protected readonly IInteractive<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa, presentation.Model.Empresa> _interactive;

        public Empresa(IInteractive<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa, presentation.Model.Empresa> interactive)
        {
            _interactive = interactive;

            ClearCommand = new RelayCommand(delegate (object parameter) { Messenger.Default.Send<presentation.Model.Empresa>(Clear(), "EmpresaClear"); }, null);

            LoadCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, presentation.Model.Empresa entity) operation)>((CommandAction.Load, Load()), "EmpresaLoad");
            }, delegate (object parameter) { return Business.Data.Domain.Id != null && Business.Changed; });
            SaveCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, presentation.Model.Empresa entity) operation)>((CommandAction.Save, Save()), "EmpresaSave");
            }, delegate (object parameter) { return Business.Changed; });
            EraseCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(CommandAction action, (Result result, presentation.Model.Empresa entity) operation)>((CommandAction.Erase, Erase()), "EmpresaErase");
            }, delegate (object parameter) { return Business.Data.Domain.Id != null && !Business.Deleted; });

            EditCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(presentation.Model.Empresa oldvalue, presentation.Model.Empresa newvalue)>((this, this), "EmpresaEdit");
            }, delegate (object parameter) { return Business.Data.Domain.Id != null && !Business.Deleted; });
        }
        public Empresa()
            : this(new Interactive<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa, presentation.Model.Empresa>(new presentation.Model.Empresa.Mapper()))
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

        public virtual int? Id { get { return Business?.Id; } set { if (Business?.Id != value) { Business.Id = value; OnPropertyChanged("Id"); } } }
        public virtual string RazonSocial { get { return Business?.RazonSocial; } set { if (Business?.RazonSocial != value) { Business.RazonSocial = value; OnPropertyChanged("RazonSocial"); } } }
        public virtual bool? Activo { get { return Business?.Activo; } set { if (Business?.Activo != value) { Business.Activo = value; OnPropertyChanged("Activo"); } } }

        public virtual void Sucursales_Load(int maxdepth = 1, int top = 0)
        {
            var query = new presentation.Query.Sucursal();
            query.Business.Data["IdEmpresa"].Where(this.Id);

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

                 Business.Sucursales = Sucursales?.Businesses;

                 //OnPropertyChanged("Sucursales");
            }
        }

        public virtual presentation.Model.Empresa Clear()
        {
            return _interactive.Clear(this, Business);
        }
        public virtual (Result result, presentation.Model.Empresa presentation) Load(int maxdepth = 1)
        {
            var load = _interactive.Load(this, Business, maxdepth);

            return load;
        }
        public virtual (Result result, presentation.Model.Empresa presentation) Save()
        {
            var save = _interactive.Save(this, Business);

            SaveDependencies();

            return save;
        }
        public virtual (Result result, presentation.Model.Empresa presentation) Erase()
        {
            EraseDependencies();

            var erase = _interactive.Erase(this, Business);

            return erase;
        }

        protected virtual void SaveDependencies()
        {
            Sucursales?.ToList()?.ForEach(i => { ((presentation.Model.Sucursal)i).Save(); });
        }
        protected virtual void EraseDependencies()
        {
            Sucursales?.ToList()?.ForEach(i => { ((presentation.Model.Sucursal)i).Erase(); });
        }
    }

    public partial class Empresas : ObservableCollection<presentation.Model.Empresa>
    {
        public virtual ICommand AddCommand { get; set; }

        public virtual business.Model.Empresas Businesses
        {
            get
            {
                return new business.Model.Empresas().Load(this.Select(x => x.Business).Cast<business.Model.Empresa>());
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
            if (message.operation.presentation?.Business.Data.Domain.Id != null)
                this.Remove(message.operation.presentation);
        }

        public virtual void EmpresaAdd(presentation.Model.Empresa presentation)
        {
            if (presentation?.Business.Data.Domain.Id != null)
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
    public partial class Empresa : IQueryView<data.Query.Empresa, business.Query.Empresa>, IQueryInteractive<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa, presentation.Model.Empresa>
    {
        public virtual business.Query.Empresa Business { get; set; } = new business.Query.Empresa();

        protected readonly IInteractive<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa, presentation.Model.Empresa> _interactive;

        public Empresa(IInteractive<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa, presentation.Model.Empresa> interactive)
        {
            _interactive = interactive;
        }
        public Empresa()
            : this(new Interactive<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa, presentation.Model.Empresa>(new presentation.Model.Empresa.Mapper()))
        {
        }

        public virtual (Result result, presentation.Model.Empresa presentation) Retrieve(int maxdepth = 1)
        {
            return _interactive.Retrieve(Business, maxdepth);
        }
        public virtual (Result result, IEnumerable<presentation.Model.Empresa> presentations) List(int maxdepth = 1, int top = 0)
        {
            return _interactive.List(Business, maxdepth, top);
        }
    }
}