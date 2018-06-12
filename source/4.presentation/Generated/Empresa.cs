using library.Impl;
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
    public partial class Empresa : AbstractEntityInteractiveMethods<data.Query.Empresa, domain.Query.Empresa, presentation.Query.Empresa, entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
        public virtual presentation.Query.Empresa Query
        {
            get
            {
                return new presentation.Query.Empresa();
            }
        }

        public Empresa(domain.Model.Empresa domain,
            IInteractiveTable<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> interactive,            
            int maxdepth = 1)
            : base(interactive, 
                  maxdepth)
        {
            Domain = domain;
        }

        public Empresa(domain.Model.Empresa domain, 
            IMapperInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> mapper,
             int maxdepth = 1)
            : this(domain, 
                  new InteractiveTable<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>(mapper),                  
                  maxdepth)
        {
        }
        public Empresa(domain.Model.Empresa domain, 
             int maxdepth = 1)
            : this(domain, 
                    new presentation.Mapper.Empresa(),                  
                    maxdepth)
        {
        }
        public Empresa(int maxdepth = 1)
            : this(new domain.Model.Empresa(),
                  maxdepth)
        {
        }

        public Empresa(entities.Model.Empresa entity, 
            int maxdepth = 1)
            : this(maxdepth)
        {
            SetProperties(entity);
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

                    Domain.Sucursales = (domain.Model.Sucursales)new domain.Model.Sucursales().Load(_sucursales?.Domains);

                    OnPropertyChanged("Sucursales");
                }
            }
        }
    }

    public partial class Empresas : ListEntityInteractiveProperties<data.Query.Empresa, domain.Query.Empresa, presentation.Query.Empresa, entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
        public Empresas()
            : base()
        {
        }
        public Empresas(domain.Model.Empresas domains)
            : this()
        {
            Domains = domains;
        }

        public virtual void EmpresaLoad((CommandAction action, (Result result, presentation.Model.Empresa presentation) operation) message)
        {
            base.CommandLoad(message);
        }
        public virtual void EmpresaSave((CommandAction action, (Result result, presentation.Model.Empresa presentation) operation) message)
        {
            base.CommandSave(message);
        }
        public virtual void EmpresaErase((CommandAction action, (Result result, presentation.Model.Empresa presentation) operation) message)
        {
            base.CommandErase(message);
        }

        public virtual void EmpresaAdd(presentation.Model.Empresa presentation)
        {
            base.CommandAdd(presentation);
        }
        public virtual void EmpresaEdit((presentation.Model.Empresa oldvalue, presentation.Model.Empresa newvalue) message)
        {
            base.CommandEdit(message);
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
    }
}
