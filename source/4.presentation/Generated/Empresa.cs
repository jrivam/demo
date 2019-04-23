using Library.Impl;
using Library.Impl.Persistence.Sql;
using Library.Impl.Presentation;
using Library.Impl.Presentation.Query;
using Library.Impl.Presentation.Raiser;
using Library.Impl.Presentation.Table;
using Library.Interface.Presentation.Query;
using Library.Interface.Presentation.Table;

namespace Presentation.Table
{
    public partial class Empresa : AbstractTableModel<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public Empresa(Business.Table.Empresa domain,
            IInteractiveTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa> interactive,
            int maxdepth = 1)
            : base(domain, 
                  interactive, 
                  maxdepth)
        {
        }

        public Empresa(Business.Table.Empresa domain,
             int maxdepth = 1)
            : this(domain,
                  new InteractiveTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>(new Presentation.Raiser.Empresa()),
                  maxdepth)
        {
        }

        public Empresa(Persistence.Table.Empresa data,
            int maxdepth = 1)
            : this(new Business.Table.Empresa(data),
                maxdepth)
        {
        }
        public Empresa(Entities.Table.Empresa entity,
            int maxdepth = 1)
            : this(new Persistence.Table.Empresa(entity),
                maxdepth)
        {
        }
        public Empresa(int maxdepth = 1)
            : this(new Entities.Table.Empresa(),
                  maxdepth)
        {
        }

        public virtual int? Id { get { return Domain?.Id; } set { if (Domain?.Id != value) { Domain.Id = value; OnPropertyChanged("Id"); } } }
        public virtual string Ruc { get { return Domain?.Ruc; } set { if (Domain?.Ruc != value) { Domain.Ruc = value; OnPropertyChanged("Ruc"); } } }
        public virtual string RazonSocial { get { return Domain?.RazonSocial; } set { if (Domain?.RazonSocial != value) { Domain.RazonSocial = value; OnPropertyChanged("RazonSocial"); } } }
        public virtual bool? Activo { get { return Domain?.Activo; } set { if (Domain?.Activo != value) { Domain.Activo = value; OnPropertyChanged("Activo"); } } }

        protected Presentation.Table.SucursalesQuery _sucursales;
        public virtual (Result result, Presentation.Table.SucursalesQuery models) Sucursales_Refresh(int maxdepth = 1, int top = 0, Presentation.Query.Sucursal query = null)
        {
            var refresh = Domain.Sucursales_Refresh(maxdepth, top, query?.Domain);

            Sucursales = new Presentation.Table.SucursalesQuery(refresh.domains);

            return (refresh.result, _sucursales);
        }
        public virtual Presentation.Table.SucursalesQuery Sucursales
        {
            get
            {
                if (_sucursales == null)
                {
                    if (Domain?.Sucursales != null)
                    {
                        Sucursales = new Presentation.Table.SucursalesQuery(Domain?.Sucursales);
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

                    Domain.Sucursales = (_sucursales != null) ? (Business.Table.Sucursales)new Business.Table.Sucursales().Load(_sucursales?.Domains) : null;
 
                    OnPropertyChanged("Sucursales");
                }
            }
        }
    }

    public partial class Empresas : ListModel<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public Empresas(Business.Table.Empresas domains)
            : base(domains, "Empresas")
        {
        }
        public Empresas()
            : base("Empresas")
        {
        }
    }

    public partial class EmpresasQuery : ListModelQuery<Presentation.Query.Empresa, Business.Query.Empresa, Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public EmpresasQuery(Business.Table.Empresas domains,
            Presentation.Query.Empresa query, int maxdepth = 1, int top = 0)
            : base(domains, "Empresas",
                  query, maxdepth, top)
        {
        }
        public EmpresasQuery(Presentation.Query.Empresa query, int maxdepth = 1, int top = 0)
            : base("Empresas",
                  query, maxdepth, top)
        {
        }

        public EmpresasQuery(Business.Table.Empresas domains, int maxdepth = 1, int top = 0)
            : this(domains, 
                  new Presentation.Query.Empresa(), maxdepth, top)
        {
        }
        public EmpresasQuery(int maxdepth = 1, int top = 0)
            : this(new Presentation.Query.Empresa(), maxdepth, top)
        {
        }
    }
}

namespace Presentation.Query
{
    public partial class Empresa : AbstractQueryModel<Presentation.Query.Empresa, Business.Query.Empresa, Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public Empresa(Business.Query.Empresa domain,
            IInteractiveQuery<Business.Query.Empresa, Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa> interactive)
            : base(domain,
                  interactive)
        {
        }

        public Empresa(Business.Query.Empresa domain)
            : this(domain,
                  new InteractiveQuery<Presentation.Query.Empresa, Business.Query.Empresa, Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>(new Presentation.Raiser.Empresa()))
        {
        }

        public Empresa()
            : this(new Business.Query.Empresa())
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                Domain.Id = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Ruc
        {
            set
            {
                Domain.Ruc = (value.value, value.sign);
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

        protected Presentation.Query.Sucursal _sucursal;
        public virtual Presentation.Query.Sucursal Sucursal(Presentation.Query.Sucursal query = null)
        {
            return _sucursal = query ?? _sucursal ?? new Presentation.Query.Sucursal(Domain?.Sucursal());
        }
    }
}

namespace Presentation.Raiser
{
    public partial class Empresa : BaseRaiser<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public override Presentation.Table.Empresa Clear(Presentation.Table.Empresa model)
        {
            model = base.Clear(model);

            model.Sucursales = null;

            return model;
        }
        public override Presentation.Table.Empresa Raise(Presentation.Table.Empresa model, int maxdepth = 1, int depth = 0)
        {
            model = base.Raise(model, maxdepth, depth);

            return model;
        }
    }
}
