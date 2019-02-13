using library.Impl.Data.Sql;
using library.Impl.Presentation;
using library.Impl.Presentation.Query;
using library.Impl.Presentation.Raiser;
using library.Impl.Presentation.Table;
using library.Interface.Presentation.Query;
using library.Interface.Presentation.Raiser;
using library.Interface.Presentation.Table;
using System;

namespace presentation.Model
{
    public partial class Sucursal : AbstractTableInteractiveMethods<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public Sucursal(domain.Model.Sucursal domain, 
            IInteractiveTable<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> interactive,           
            int maxdepth = 1)
            : base(domain, 
                  interactive, 
                  maxdepth)
        {
        }

        public Sucursal(domain.Model.Sucursal domain, 
            IRaiserInteractive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> raiser,             
             int maxdepth = 1)
            : this(domain, 
                  new InteractiveTable<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>(raiser),                    
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

        public Sucursal(int maxdepth = 1)
            : this(new domain.Model.Sucursal(),
                  maxdepth)
        {
        }

        public Sucursal(data.Model.Sucursal data,
            int maxdepth = 1)
            : this(new domain.Model.Sucursal(data),
                maxdepth)
        {
        }
        public Sucursal(entities.Model.Sucursal entity,
            int maxdepth = 1)
            : this(new data.Model.Sucursal(entity),
                maxdepth)
        {
            SetProperties(entity, true);
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

        protected presentation.Model.Empresas _empresas;
        public virtual presentation.Model.Empresas Empresas
        {
            get
            {
                return _empresas ?? (Empresas = new presentation.Model.Empresas(Domain?.Empresas));
            }
            set
            {
                if (_empresas != value)
                {
                    _empresas = value;

                    Domain.Empresas = (domain.Model.Empresas)new domain.Model.Empresas().Load(_empresas?.Domains);
                }
            }
        }
    }

    public partial class Sucursales : ListPresentation<data.Query.Sucursal, domain.Query.Sucursal, presentation.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public Sucursales(domain.Model.Sucursales domains,
            presentation.Query.Sucursal query, int maxdepth = 1, int top = 0)
            : base(domains, "Sucursales",
                  query, maxdepth, top)
        {
        }

        public Sucursales(presentation.Query.Sucursal query, int maxdepth = 1, int top = 0)
            : this(new domain.Model.Sucursales(),
                  query, maxdepth, top)
        {

        }
    }
}

namespace presentation.Query
{
    public partial class Sucursal : AbstractQueryInteractiveMethods<data.Query.Sucursal, domain.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
    {
        public Sucursal(domain.Query.Sucursal domain,
            IInteractiveQuery<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> interactive)
            : base(domain,
                  interactive)
        {
        }

        public Sucursal(domain.Query.Sucursal domain,
            IRaiserInteractive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal> mapper)
            : this(domain,
                  new InteractiveQuery<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>(mapper))
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
    public partial class Sucursal : BaseRaiserInteractive<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal, presentation.Model.Sucursal>
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