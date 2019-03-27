using Library.Impl;
using Library.Impl.Data.Sql;
using Library.Impl.Presentation;
using Library.Impl.Presentation.Query;
using Library.Impl.Presentation.Raiser;
using Library.Impl.Presentation.Table;
using Library.Interface.Presentation.Query;
using Library.Interface.Presentation.Raiser;
using Library.Interface.Presentation.Table;
using System;

namespace presentation.Model
{
    public partial class Sucursal : AbstractTableModel<Entities.Table.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public Sucursal(domain.Model.Sucursal domain, 
            IInteractiveTable<Entities.Table.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> interactive,           
            int maxdepth = 1)
            : base(domain, 
                  interactive, 
                  maxdepth)
        {
        }

        public Sucursal(domain.Model.Sucursal domain, 
            IRaiserInteractive<Entities.Table.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> raiser,             
             int maxdepth = 1)
            : this(domain, 
                  new InteractiveTable<Entities.Table.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>(raiser),                    
                    maxdepth)
        {
        }
        public Sucursal(domain.Model.Sucursal domain, 
             int maxdepth = 1)
            : this(domain, 
                  new presentation.Raiser.Sucursal(),                    
                    maxdepth)
        {
        }

        public Sucursal(data.Model.Sucursal data,
            int maxdepth = 1)
            : this(new domain.Model.Sucursal(data),
                maxdepth)
        {
        }
        public Sucursal(Entities.Table.Sucursal entity,
            int maxdepth = 1)
            : this(new data.Model.Sucursal(entity),
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

        protected presentation.Model.Empresa _empresa;
        public virtual (Result result, presentation.Model.Empresa domain) Empresa_Refresh(int maxdepth = 1, presentation.Query.Sucursal query = null)
        {
            var refresh = Domain.Empresa_Refresh(maxdepth, query?.Domain);

            Empresa = new presentation.Model.Empresa(refresh.domain);

            return (refresh.result, _empresa);
        }
        public virtual presentation.Model.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    if (Domain?.Empresa != null)
                    {
                        Empresa = new presentation.Model.Empresa(Domain?.Empresa);
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

        protected presentation.Model.Empresas _empresas;
        public virtual (Result result, presentation.Model.Empresas presentation) Empresas_Refresh(int maxdepth = 1, int top = 0, presentation.Query.Empresa query = null)
        {
            var refresh = Domain.Empresas_Refresh(maxdepth, top, query?.Domain);

            Empresas = new presentation.Model.Empresas(refresh.domains);

            return (refresh.result, _empresas);
        }
        public virtual presentation.Model.Empresas Empresas
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

                    Domain.Empresas = (domain.Model.Empresas)new domain.Model.Empresas().Load(_empresas?.Domains);

                    OnPropertyChanged("Empresas");
                }
            }
        }
    }

    public partial class Sucursales : ListPresentation<Entities.Table.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public Sucursales(domain.Model.Sucursales domains)
            : base(domains, "Sucursales")
        {
        }
        public Sucursales()
            : base("Sucursales")
        {
        }
    }

    public partial class SucursalesQuery : ListPresentationQuery<presentation.Query.Sucursal, domain.Query.Sucursal, data.Query.Sucursal, Entities.Table.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public SucursalesQuery(domain.Model.Sucursales domains,
            presentation.Query.Sucursal query, int maxdepth = 1, int top = 0)
            : base(domains, "Sucursales",
                query, maxdepth, top)
        {
        }
        public SucursalesQuery(presentation.Query.Sucursal query, int maxdepth = 1, int top = 0)
            : base("Sucursales",
                query, maxdepth, top)
        {
        }

        public SucursalesQuery(domain.Model.Sucursales domains, int maxdepth = 1, int top = 0)
            : this(domains,
                new presentation.Query.Sucursal(), maxdepth, top)
        {
        }
        public SucursalesQuery(int maxdepth = 1, int top = 0)
            : this(new presentation.Query.Sucursal(), maxdepth, top)
        {
        }
    }
}

namespace presentation.Query
{
    public partial class Sucursal : AbstractQueryModel<presentation.Query.Sucursal, domain.Query.Sucursal, data.Query.Sucursal, Entities.Table.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public Sucursal(domain.Query.Sucursal domain,
            IInteractiveQuery<domain.Query.Sucursal, data.Query.Sucursal, Entities.Table.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> interactive)
            : base(domain,
                  interactive)
        {
        }

        public Sucursal(domain.Query.Sucursal domain,
            IRaiserInteractive<Entities.Table.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> mapper)
            : this(domain,
                  new InteractiveQuery<presentation.Query.Sucursal, domain.Query.Sucursal, data.Query.Sucursal, Entities.Table.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>(mapper))
        {
        }

        public Sucursal(domain.Query.Sucursal domain)
            : this(domain,
                  new presentation.Raiser.Sucursal())
        {
        }
        public Sucursal()
            : this(new domain.Query.Sucursal())
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

        protected presentation.Query.Empresa _empresa;
        public virtual presentation.Query.Empresa Empresa(presentation.Query.Empresa query = null)
        {
            return _empresa = query ?? _empresa ?? new presentation.Query.Empresa(Domain?.Empresa());
        }
    }
}

namespace presentation.Raiser
{
    public partial class Sucursal : BaseRaiserInteractive<Entities.Table.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public override presentation.Model.Sucursal Clear(presentation.Model.Sucursal presentation, int maxdepth = 1, int depth = 0)
        {
            presentation = base.Clear(presentation, maxdepth, depth);

            depth++;
            if (depth < maxdepth)
            {
                presentation.Empresa = null;
            }

            return presentation;
        }
        public override presentation.Model.Sucursal Raise(presentation.Model.Sucursal presentation, int maxdepth = 1, int depth = 0)
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