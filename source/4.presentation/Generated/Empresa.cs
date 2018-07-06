using library.Impl.Data.Sql;
using library.Impl.Domain.Table;
using library.Impl.Presentation;
using library.Impl.Presentation.Mapper;
using library.Impl.Presentation.Query;
using library.Impl.Presentation.Table;
using library.Interface.Presentation.Mapper;
using library.Interface.Presentation.Query;
using library.Interface.Presentation.Table;

namespace presentation.Model
{
    public partial class Empresa : AbstractEntityInteractiveMethods<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
        public virtual presentation.Query.Empresa Query
        {
            get
            {
                return new presentation.Query.Empresa();
            }
        }

        public Empresa(IInteractiveTable<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> interactive,
            domain.Model.Empresa domain,
            int maxdepth = 1)
            : base(interactive, 
                  maxdepth)
        {
            Domain = domain;
        }

        public Empresa(IMapperInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> mapper,
             domain.Model.Empresa domain, 
             int maxdepth = 1)
            : this(new InteractiveTable<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>(mapper),
                  domain,
                  maxdepth)
        {
        }
        public Empresa(domain.Model.Empresa domain, 
             int maxdepth = 1)
            : this(new presentation.Mapper.Empresa(),
                    domain,
                    maxdepth)
        {
        }
        public Empresa(entities.Model.Empresa entity, 
            int maxdepth = 1)
            : this(maxdepth)
        {
            SetProperties(entity, true);
        }
        public Empresa(int maxdepth = 1)
            : this(new domain.Model.Empresa(),
                  maxdepth)
        {
        }

        public virtual int? Id { get { return Domain?.Id; } set { if (Domain?.Id != value) { Domain.Id = value; OnPropertyChanged("Id"); } } }
        public virtual string RazonSocial { get { return Domain?.RazonSocial; } set { if (Domain?.RazonSocial != value) { Domain.RazonSocial = value; OnPropertyChanged("RazonSocial"); } } }
        public virtual bool? Activo { get { return Domain?.Activo; } set { if (Domain?.Activo != value) { Domain.Activo = value; OnPropertyChanged("Activo"); } } }

        protected presentation.Model.Sucursales _sucursales;
        public virtual presentation.Model.Sucursales Sucursales
        {
            get
            {
                return _sucursales ?? (Sucursales = new presentation.Model.Sucursales(Domain?.Sucursales));
            }
            set
            {
                if (_sucursales != value)
                {
                    _sucursales = value;

                    Domain.Sucursales = _sucursales != null ? (domain.Model.Sucursales)new domain.Model.Sucursales().Load(_sucursales?.Domains) : null;

                    OnPropertyChanged("Sucursales");
                }
            }
        }
    }

    public partial class Empresas : ListTableInteractiveProperties<data.Query.Empresa, domain.Query.Empresa, presentation.Query.Empresa, entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
        public Empresas()
            : base()
        {
            AddCommand = new RelayCommand(delegate (object parameter)
            {
                Messenger.Default.Send<presentation.Model.Empresa>(null, "EmpresaAdd");
            }, delegate (object parameter) { return this != null; });
        }
        public Empresas(domain.Model.Empresas domains)
            : this()
        {
            Domains = domains;
        }
    }
}

namespace presentation.Query
{
    public partial class Empresa : AbstractQueryInteractiveMethods<data.Query.Empresa, domain.Query.Empresa, entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
        public Empresa(domain.Query.Empresa domain,
            IInteractiveQuery<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> interactive)
            : base(interactive)
        {
            Domain = domain;
        }

        public Empresa(domain.Query.Empresa domain,
            IMapperInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> mapper)
            : this(domain,
                  new InteractiveQuery<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>(mapper))
        {
        }
        public Empresa(domain.Query.Empresa domain)
            : this(domain,
                  new presentation.Mapper.Empresa())
        {
        }
        public Empresa()
            : this(new domain.Query.Empresa())
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                Domain.Id = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) RazonSocial
        {
            set
            {
                Domain.RazonSocial = (value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                Domain.Activo = (value.value, value.sign);
            }
        }

        protected presentation.Query.Sucursal _sucursal;
        public virtual presentation.Query.Sucursal Sucursal(presentation.Query.Sucursal query = null)
        {
            return _sucursal = query ?? _sucursal ?? new presentation.Query.Sucursal(Domain?.Sucursal());
        }
    }
}

namespace presentation.Mapper
{
    public partial class Empresa : BaseMapperInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
        public override presentation.Model.Empresa Clear(presentation.Model.Empresa presentation, int maxdepth = 1, int depth = 0)
        {
            presentation = base.Clear(presentation, maxdepth, depth);

            presentation.Sucursales = null;

            return presentation;
        }
    }
}
