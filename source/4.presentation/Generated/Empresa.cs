using library.Impl;
using library.Impl.Data.Sql;
using library.Impl.Presentation;
using library.Impl.Presentation.Query;
using library.Impl.Presentation.Raiser;
using library.Impl.Presentation.Table;
using library.Interface.Presentation.Query;
using library.Interface.Presentation.Raiser;
using library.Interface.Presentation.Table;

namespace presentation.Model
{
    public partial class Empresa : AbstractTableInteractiveMethods<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
        public Empresa(domain.Model.Empresa domain,
            IInteractiveTable<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> interactive,
            int maxdepth = 1)
            : base(domain, 
                  interactive, 
                  maxdepth)
        {
        }

        public Empresa(domain.Model.Empresa domain,
            IRaiserInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> raiser,
             int maxdepth = 1)
            : this(domain,
                  new InteractiveTable<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>(raiser),
                  maxdepth)
        {
        }
        public Empresa(domain.Model.Empresa domain, 
             int maxdepth = 1)
            : this(domain,
                  new presentation.Raiser.Empresa(),
                    maxdepth)
        {
        }


        public Empresa(data.Model.Empresa data,
            int maxdepth = 1)
            : this(new domain.Model.Empresa(data),
                maxdepth)
        {
        }
        public Empresa(entities.Model.Empresa entity,
            int maxdepth = 1)
            : this(new data.Model.Empresa(entity),
                maxdepth)
        {
        }

        public Empresa(int maxdepth = 1)
            : this(new entities.Model.Empresa(),
                  maxdepth)
        {
        }

        public virtual int? Id { get { return Domain?.Id; } set { if (Domain?.Id != value) { Domain.Id = value; OnPropertyChanged("Id"); } } }
        public virtual string RazonSocial { get { return Domain?.RazonSocial; } set { if (Domain?.RazonSocial != value) { Domain.RazonSocial = value; OnPropertyChanged("RazonSocial"); } } }
        public virtual bool? Activo { get { return Domain?.Activo; } set { if (Domain?.Activo != value) { Domain.Activo = value; OnPropertyChanged("Activo"); } } }

        protected presentation.Model.Sucursales _sucursales;
        public virtual (Result result, presentation.Model.Sucursales presentations) Sucursales_Refresh(int maxdepth = 1, int top = 0, presentation.Query.Empresa query = null)
        {
            var refresh = Domain.Sucursales_Refresh(maxdepth, top, query?.Domain);

            Sucursales = new presentation.Model.Sucursales(refresh.domains);

            return (refresh.result, _sucursales);
        }
        public virtual presentation.Model.Sucursales Sucursales
        {
            get
            {
                if (_sucursales == null)
                {
                    if (Domain?.Sucursales != null)
                    {
                        Sucursales = new presentation.Model.Sucursales(Domain?.Sucursales);
                    }
                    else
                    {
                        Sucursales_Refresh();
                    }
                }

                return _sucursales;
            }
            set
            {
                if (_sucursales != value)
                {
                    _sucursales = value;

                    Domain.Sucursales = (_sucursales != null) ? (domain.Model.Sucursales)new domain.Model.Sucursales().Load(_sucursales?.Domains) : null;
 
                    OnPropertyChanged("Sucursales");
                }
            }
        }
    }

    public partial class Empresas : ListPresentation<data.Query.Empresa, domain.Query.Empresa, presentation.Query.Empresa, entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
        public Empresas(domain.Model.Empresas domains,
            presentation.Query.Empresa query, int maxdepth = 1, int top = 0)
            : base(domains, "Empresas",
                  query, maxdepth, top)
        {
        }
        public Empresas(presentation.Query.Empresa query, int maxdepth = 1, int top = 0)
            : base("Empresas",
                  query, maxdepth, top)
        {
        }

        public Empresas(domain.Model.Empresas domains, int maxdepth = 1, int top = 0)
            : this(domains, 
                  new presentation.Query.Empresa(), maxdepth, top)
        {
        }
        public Empresas(int maxdepth = 1, int top = 0)
            : this(new presentation.Query.Empresa(), maxdepth, top)
        {
        }
    }
}

namespace presentation.Query
{
    public partial class Empresa : AbstractQueryInteractiveMethods<data.Query.Empresa, domain.Query.Empresa, entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
        public Empresa(domain.Query.Empresa domain,
            IInteractiveQuery<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> interactive)
            : base(domain,
                  interactive)
        {
        }

        public Empresa(domain.Query.Empresa domain,
            IRaiserInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa> mapper)
            : this(domain,
                  new InteractiveQuery<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>(mapper))
        {
        }

        public Empresa(domain.Query.Empresa domain)
            : this(domain,
                  new presentation.Raiser.Empresa())
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

namespace presentation.Raiser
{
    public partial class Empresa : BaseRaiserInteractive<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa, presentation.Model.Empresa>
    {
        public override presentation.Model.Empresa Clear(presentation.Model.Empresa presentation, int maxdepth = 1, int depth = 0)
        {
            presentation = base.Clear(presentation, maxdepth, depth);

            presentation.Sucursales = null;

            return presentation;
        }
    }
}
