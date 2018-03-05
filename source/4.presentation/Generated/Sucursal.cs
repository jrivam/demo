using library.Impl;
using library.Impl.Business;
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
    public partial class Sucursal : INotifyPropertyChanged, IEntityView<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal>, IEntityInteractive<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal, presentation.Model.Sucursal>
    {
        public partial class Mapper : MapperView<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal, presentation.Model.Sucursal>
        {
            public override presentation.Model.Sucursal Clear(presentation.Model.Sucursal presentation, int maxdepth = 1, int depth = 0)
            {
                presentation = base.Clear(presentation, maxdepth, depth);

                depth++;
                if (depth < maxdepth || maxdepth == 0)
                {
                    presentation.Empresa = new presentation.Model.Empresa.Mapper().Clear(presentation.Empresa, maxdepth, depth);
                }
                else
                {
                    presentation.Empresa = null;
                }

                return presentation;
            }
            public override presentation.Model.Sucursal Map(presentation.Model.Sucursal presentation, int maxdepth = 1, int depth = 0)
            {
                presentation = base.Map(presentation, maxdepth, depth);

                presentation.OnPropertyChanged("Empresa");

                depth++;
                if (depth < maxdepth || maxdepth == 0)
                {
                    presentation.Empresa = new presentation.Model.Empresa.Mapper().Map(presentation.Empresa, maxdepth, depth);
                }

                return presentation;
            }
        }
        public virtual business.Model.Sucursal Business { get; set; } = new business.Model.Sucursal();

        protected readonly IInteractive<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal, presentation.Model.Sucursal> _interactive;

        public Sucursal(IInteractive<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal, presentation.Model.Sucursal> interactive)
        {
            _interactive = interactive;

            ClearCommand = new RelayCommand(delegate (object entity) { Messenger.Default.Send<presentation.Model.Sucursal>(Clear(), "SucursalClear"); }, null);

            LoadCommand = new RelayCommand(delegate (object parameter) 
            {
                Messenger.Default.Send<(CommandAction action, (Result result, presentation.Model.Sucursal entity) operation)>((CommandAction.Load, Load()), "SucursalLoad");
            }, delegate (object parameter) { return Business.Data.Domain.Id != null && Business.Changed; });
            SaveCommand = new RelayCommand(delegate (object parameter) 
            {
                Messenger.Default.Send<(CommandAction action, (Result result, presentation.Model.Sucursal entity) operation)>((CommandAction.Save, Save()), "SucursalSave");
            }, delegate (object parameter) { return Business.Changed; });
            EraseCommand = new RelayCommand(delegate (object parameter) 
            {
                Messenger.Default.Send<(CommandAction action, (Result result, presentation.Model.Sucursal entity) operation)>((CommandAction.Erase, Erase()), "SucursalErase");
            }, delegate (object parameter) { return Business.Data.Domain.Id != null && !Business.Deleted; });

            EditCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<(presentation.Model.Sucursal oldvalue, presentation.Model.Sucursal newvalue)>((this, this), "SucursalEdit");
            }, delegate (object parameter) { return Business.Data.Domain.Id != null && !Business.Deleted; });
        }
        public Sucursal()
            : this(new Interactive<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal, presentation.Model.Sucursal>(new presentation.Model.Sucursal.Mapper()))
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
        public virtual string Nombre { get { return Business?.Nombre; } set { if (Business?.Nombre != value) { Business.Nombre = value; OnPropertyChanged("Nombre"); } } }
        public virtual bool? Activo { get { return Business?.Activo; } set { if (Business?.Activo != value) { Business.Activo = value; OnPropertyChanged("Activo"); } } }
        public virtual DateTime? Fecha { get { return Business?.Fecha; } set { if (Business?.Fecha != value) { Business.Fecha = value; OnPropertyChanged("Fecha"); } } }

        public virtual int? IdEmpresa
        {
            get
            {
                return Business?.IdEmpresa;
            }
            set
            {
                if (Business?.IdEmpresa != value)
                {
                    Business.IdEmpresa = value;
                    OnPropertyChanged("IdEmpresa");

                    Empresa = null;
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
                    query.Business.Data["Activo"].Where(true);

                    _empresas = new presentation.Model.Empresas().Load(query);
                }

                return _empresas;
            }
        }

        protected presentation.Model.Empresa _empresa;
        public virtual presentation.Model.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    Empresa = new presentation.Model.Empresa() { Business = Business.Empresa };
                }

                return _empresa;
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;

                    //OnPropertyChanged("Empresa");
                }
            }
        }

        public virtual presentation.Model.Sucursal Clear()
        {
            return _interactive.Clear(this, Business);
        }
        public virtual (Result result, presentation.Model.Sucursal presentation) Load()
        {
            var load = _interactive.Load(this, Business);

            return load;
        }
        public virtual (Result result, presentation.Model.Sucursal presentation) Save()
        {
            var save = _interactive.Save(this, Business);

            SaveDependencies();

            return save;
        }
        public virtual (Result result, presentation.Model.Sucursal presentation) Erase()
        {
            EraseDependencies();

            var erase = _interactive.Erase(this, Business);

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

        public virtual business.Model.Sucursales Businesses
        {
            get
            {
                return new business.Model.Sucursales().Load(this.Select(x => x.Business).Cast<business.Model.Sucursal>());
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
            if (message.operation.presentation?.Business.Data.Domain.Id != null)
                this.Remove(message.operation.presentation);
        }

        public virtual void SucursalAdd(presentation.Model.Sucursal presentation)
        {
            if (presentation.Business.Data.Domain?.Id != null)
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
    public partial class Sucursal : IQueryView<data.Query.Sucursal, business.Query.Sucursal>, IQueryInteractive<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal, presentation.Model.Sucursal>
    {
        public virtual business.Query.Sucursal Business { get; set; } = new business.Query.Sucursal();

        protected readonly IInteractive<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal, presentation.Model.Sucursal> _interactive;

        public Sucursal(IInteractive<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal, presentation.Model.Sucursal> interactive)
        {
            _interactive = interactive;
        }
        public Sucursal()
            : this(new Interactive<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal, presentation.Model.Sucursal>(new presentation.Model.Sucursal.Mapper()))
        {
        }

        public virtual (Result result, presentation.Model.Sucursal presentation) Retrieve(int maxdepth = 1)
        {
            return _interactive.Retrieve(Business, maxdepth);
        }
        public virtual (Result result, IEnumerable<presentation.Model.Sucursal> presentations) List(int maxdepth = 1, int top = 0)
        {
            return _interactive.List(Business, maxdepth, top);
        }
    }
}