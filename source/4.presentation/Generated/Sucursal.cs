using library.Impl.Domain.Table;
using library.Impl.Presentation;
using library.Impl.Presentation.Mapper;
using library.Impl.Presentation.Query;
using library.Impl.Presentation.Table;
using library.Interface.Presentation.Mapper;
using library.Interface.Presentation.Query;
using library.Interface.Presentation.Table;
using System;

namespace presentation.Model
{
    public partial class Sucursal : AbstractEntityInteractiveMethods<data.Query.Sucursal, domain.Query.Sucursal, presentation.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public virtual presentation.Query.Sucursal Query
        {
            get
            {
                return new presentation.Query.Sucursal();
            }
        }

        public Sucursal(domain.Model.Sucursal domain, 
            IInteractiveTable<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> interactive,
            int maxdepth = 1)
            : base(interactive, 
                  maxdepth)
        {
            Domain = domain;
        }

        public Sucursal(domain.Model.Sucursal domain, 
            IMapperInteractive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> mapper,
             int maxdepth = 1)
            : this(domain, 
                  new InteractiveTable<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>(mapper),                  
                  maxdepth)
        {
        }
        public Sucursal(domain.Model.Sucursal domain, 
             int maxdepth = 1)
            : this(domain, 
                    new presentation.Mapper.Sucursal(),                  
                    maxdepth)
        {
        }
        public Sucursal(int maxdepth = 1)
            : this(new domain.Model.Sucursal(),
                  maxdepth)
        {
        }

        public Sucursal(entities.Model.Sucursal entity,
            int maxdepth = 1)
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

        protected presentation.Model.Empresa _empresa;
        public virtual presentation.Model.Empresa Empresa
        {
            get
            {
                return _empresa ?? (Empresa = new presentation.Model.Empresa(Domain?.Empresa));
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
    }

    public partial class Sucursales : ListEntityInteractiveProperties<data.Query.Sucursal, domain.Query.Sucursal, presentation.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public Sucursales()
            : base()
        {
            AddCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<presentation.Model.Sucursal>(null, "SucursalAdd");
            }, delegate (object parameter) { return this != null; });
        }
        public Sucursales(domain.Model.Sucursales domains)
            : this()
        {
            Domains = domains;
        }
    }
}

namespace presentation.Query
{
    public partial class Sucursal : AbstractQueryInteractiveMethods<data.Query.Sucursal, domain.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public Sucursal(domain.Query.Sucursal domain,
            IInteractiveQuery<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> interactive)
            : base(interactive)
        {
            Domain = domain;
        }

        public Sucursal(domain.Query.Sucursal domain,
            IMapperInteractive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> mapper)
            : this(domain,
                  new InteractiveQuery<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>(mapper))
        {
        }
        public Sucursal(domain.Query.Sucursal domain)
            : this(domain,
                  new presentation.Mapper.Sucursal())
        {
        }
        public Sucursal()
            : this(new domain.Query.Sucursal())
        {
        }

        protected presentation.Query.Empresa _empresa;
        public virtual presentation.Query.Empresa Empresa(presentation.Query.Empresa query = null)
        {
            return _empresa = query ?? _empresa ?? new presentation.Query.Empresa(Domain?.Empresa());
        }
    }
}

namespace presentation.Mapper
{
    public partial class Sucursal : BaseMapperInteractive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
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