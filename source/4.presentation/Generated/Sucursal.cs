using Library.Impl;
using Library.Impl.Persistence.Sql;
using Library.Impl.Presentation;
using Library.Impl.Presentation.Query;
using Library.Impl.Presentation.Raiser;
using Library.Impl.Presentation.Table;
using Library.Interface.Presentation.Query;
using Library.Interface.Presentation.Raiser;
using Library.Interface.Presentation.Table;
using System;

namespace Presentation.Table
{
    public partial class Sucursal : AbstractTableModel<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public Sucursal(Business.Table.Sucursal domain, 
            IInteractiveTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal> interactive,           
            int maxdepth = 1)
            : base(domain, 
                  interactive, 
                  maxdepth)
        {
        }

        public Sucursal(Business.Table.Sucursal domain, 
            IRaiserInteractive<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal> raiser,             
             int maxdepth = 1)
            : this(domain, 
                  new InteractiveTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>(raiser),                    
                    maxdepth)
        {
        }
        public Sucursal(Business.Table.Sucursal domain, 
             int maxdepth = 1)
            : this(domain, 
                  new Presentation.Raiser.Sucursal(),                    
                    maxdepth)
        {
        }

        public Sucursal(Persistence.Table.Sucursal data,
            int maxdepth = 1)
            : this(new Business.Table.Sucursal(data),
                maxdepth)
        {
        }
        public Sucursal(Entities.Table.Sucursal entity,
            int maxdepth = 1)
            : this(new Persistence.Table.Sucursal(entity),
                maxdepth)
        {
        }
        public Sucursal(int maxdepth = 1)
            : this(new Entities.Table.Sucursal(),
                maxdepth)
        {
        }

        public virtual int? Id { get { return Domain?.Id; } set { if (Domain?.Id != value) { Domain.Id = value; OnPropertyChanged("Id"); } } }
        public virtual string Codigo { get { return Domain?.Codigo; } set { if (Domain?.Codigo != value) { Domain.Codigo = value; OnPropertyChanged("Codigo"); } } }
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

        protected Presentation.Table.Empresa _empresa;
        public virtual (Result result, Presentation.Table.Empresa domain) Empresa_Refresh(int maxdepth = 1, Presentation.Query.Empresa queryempresa = null)
        {
            var refresh = Domain.Empresa_Refresh(maxdepth, queryempresa?.Domain);

            Empresa = new Presentation.Table.Empresa(refresh.domain);

            return (refresh.result, _empresa);
        }
        public virtual Presentation.Table.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    if (Domain?.Empresa != null)
                    {
                        Empresa = new Presentation.Table.Empresa(Domain?.Empresa);
                    }
                    else
                    {
                        Empresa_Refresh();
                    }
                }

                return _empresa;
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

        protected Presentation.Table.Empresas _empresas;
        public virtual (Result result, Presentation.Table.Empresas presentation) Empresas_Refresh(int maxdepth = 1, int top = 0, Presentation.Query.Empresa queryempresa = null)
        {
            var refresh = Domain.Empresas_Refresh(maxdepth, top, queryempresa?.Domain);

            Empresas = new Presentation.Table.Empresas(refresh.domains);

            return (refresh.result, _empresas);
        }
        public virtual Presentation.Table.Empresas Empresas
        {
            get
            {
                return _empresas ?? Empresas_Refresh().presentation;
            }
            set
            {
                if (_empresas != value)
                {
                    _empresas = value;

                    Domain.Empresas = (Business.Table.Empresas)new Business.Table.Empresas().Load(_empresas?.Domains);

                    OnPropertyChanged("Empresas");
                }
            }
        }
    }

    public partial class Sucursales : ListModel<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public Sucursales(Business.Table.Sucursales domains)
            : base(domains, "Sucursales")
        {
        }
        public Sucursales()
            : base("Sucursales")
        {
        }
    }

    public partial class SucursalesQuery : ListModelQuery<Presentation.Query.Sucursal, Business.Query.Sucursal, Persistence.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public SucursalesQuery(Business.Table.Sucursales domains,
            Presentation.Query.Sucursal query, int maxdepth = 1, int top = 0)
            : base(domains, "Sucursales",
                query, maxdepth, top)
        {
        }
        public SucursalesQuery(Presentation.Query.Sucursal query, int maxdepth = 1, int top = 0)
            : base("Sucursales",
                query, maxdepth, top)
        {
        }

        public SucursalesQuery(Business.Table.Sucursales domains, int maxdepth = 1, int top = 0)
            : this(domains,
                new Presentation.Query.Sucursal(), maxdepth, top)
        {
        }
        public SucursalesQuery(int maxdepth = 1, int top = 0)
            : this(new Presentation.Query.Sucursal(), maxdepth, top)
        {
        }
    }
}

namespace Presentation.Query
{
    public partial class Sucursal : AbstractQueryModel<Presentation.Query.Sucursal, Business.Query.Sucursal, Persistence.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public Sucursal(Business.Query.Sucursal domain,
            IInteractiveQuery<Business.Query.Sucursal, Persistence.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal> interactive)
            : base(domain,
                  interactive)
        {
        }

        public Sucursal(Business.Query.Sucursal domain,
            IRaiserInteractive<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal> mapper)
            : this(domain,
                  new InteractiveQuery<Presentation.Query.Sucursal, Business.Query.Sucursal, Persistence.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>(mapper))
        {
        }

        public Sucursal(Business.Query.Sucursal domain)
            : this(domain,
                  new Presentation.Raiser.Sucursal())
        {
        }
        public Sucursal()
            : this(new Business.Query.Sucursal())
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                Domain.Id = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Codigo
        {
            set
            {
                Domain.Codigo = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Nombre
        {
            set
            {
                Domain.Nombre = (value.value, value.sign);
            }
        }
        public virtual (DateTime? value, WhereOperator? sign) Fecha
        {
            set
            {
                Domain.Fecha = (value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                Domain.Activo = (value.value, value.sign);
            }
        }
        public virtual (int? value, WhereOperator? sign) IdEmpresa
        {
            set
            {
                Domain.IdEmpresa = (value.value, value.sign);
            }
        }

        protected Presentation.Query.Empresa _empresa;
        public virtual Presentation.Query.Empresa Empresa(Presentation.Query.Empresa query = null)
        {
            return _empresa = query ?? _empresa ?? new Presentation.Query.Empresa(Domain?.Empresa());
        }
    }
}

namespace Presentation.Raiser
{
    public partial class Sucursal : BaseRaiserInteractive<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public override Presentation.Table.Sucursal Clear(Presentation.Table.Sucursal presentation, int maxdepth = 1, int depth = 0)
        {
            presentation = base.Clear(presentation, maxdepth, depth);

            depth++;
            if (depth < maxdepth)
            {
                presentation.Empresa = null;
            }

            return presentation;
        }
        public override Presentation.Table.Sucursal Raise(Presentation.Table.Sucursal presentation, int maxdepth = 1, int depth = 0)
        {
            presentation = base.Raise(presentation);

            depth++;
            if (depth < maxdepth)
            {
                presentation.OnPropertyChanged("Empresa");
            }

            return presentation;
        }
    }
}