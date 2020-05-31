using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Presentation;
using jrivam.Library.Impl.Presentation.Attributes;
using jrivam.Library.Impl.Presentation.Query;
using jrivam.Library.Impl.Presentation.Raiser;
using jrivam.Library.Impl.Presentation.Table;
using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Presentation.Query;
using jrivam.Library.Interface.Presentation.Table;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace demo.Presentation.Table
{
    public partial class Empresa : AbstractTableModel<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public Empresa(IInteractiveTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa> interactive,
            Business.Table.Empresa domain = null,
            int maxdepth = 1,
            string name = null)
            : base(interactive,
                  domain,
                  maxdepth,
                  name)
        {
        }

        public Empresa(Business.Table.Empresa domain,
            int maxdepth = 1,
            string name = null)
            : this(new InteractiveTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>(new Presentation.Raiser.Empresa()),
                    domain: domain,
                    maxdepth: maxdepth,
                    name: name)
        {
        }
        public Empresa(Persistence.Table.Empresa data,
            int maxdepth = 1,
            string name = null)
            : this(new Business.Table.Empresa(data),
                maxdepth: maxdepth,
                name: name)
        {
        }
        public Empresa(Entities.Table.Empresa entity = null,
            int maxdepth = 1,
            string name = null)
            : this(new Persistence.Table.Empresa(entity),
                maxdepth: maxdepth,
                name: name)
        {
        }

        [Model]
        public virtual int? Id { get { return Domain?.Id; } set { if (Domain?.Id != value) { Domain.Id = value; ValidateProperty(value); OnPropertyChanged(nameof(Id)); } } }
        [Model]
        public virtual string Ruc { get { return Domain?.Ruc; } set { if (Domain?.Ruc != value) { Domain.Ruc = value; ValidateProperty(value); OnPropertyChanged(nameof(Ruc)); } } }
        [Model]
        public virtual string RazonSocial { get { return Domain?.RazonSocial; } set { if (Domain?.RazonSocial != value) { Domain.RazonSocial = value; ValidateProperty(value); OnPropertyChanged(nameof(RazonSocial)); } } }
        [Model]
        public virtual bool? Activo { get { return Domain?.Activo; } set { if (Domain?.Activo != value) { Domain.Activo = value; ValidateProperty(value); OnPropertyChanged(nameof(Activo)); } } }

        protected Presentation.Table.SucursalesQuery _sucursales;
        [Model]
        public virtual Presentation.Table.SucursalesQuery Sucursales
        {
            get
            {
                if (_sucursales == null)
                {
                    Sucursales = new SucursalesQuery(Domain?.Sucursales);

                    if (this.Id != null)
                    {
                        _sucursales.Query.IdEmpresa = (this.Id, WhereOperator.Equals);
                        _sucursales.Refresh();
                    }
                }
                else
                {
                    _sucursales.Query.IdEmpresa = (this.Id, WhereOperator.Equals);

                }

                return _sucursales;
            }
            set
            {
                if (_sucursales != value)
                {
                    _sucursales = value;

                    OnPropertyChanged(nameof(Sucursales));
                }
            }
        }
    }

    public partial class Empresas : ListModel<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public Empresas(IListDomain<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa> domains)
            : base(domains)
        {
        }

        public Empresas(IListData<Entities.Table.Empresa, Persistence.Table.Empresa> datas)
           : this(new Business.Table.Empresas(datas))
        {
        }
        public Empresas(ICollection<Entities.Table.Empresa> entities = null)
           : this(new Persistence.Table.Empresas(entities ?? new Collection<Entities.Table.Empresa>()))
        {
        }
    }

    public partial class EmpresasQuery : ListModelQuery<Presentation.Query.Empresa, Business.Query.Empresa, Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public EmpresasQuery(IListDomain<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa> domains, 
            Presentation.Query.Empresa query = null,             
            int maxdepth = 1, int top = 0)
            : base(domains, 
                  query ?? new Presentation.Query.Empresa(), 
                  maxdepth, top)
        {
        }

        public EmpresasQuery(IListData<Entities.Table.Empresa, Persistence.Table.Empresa> datas, 
            Presentation.Query.Empresa query = null,
            int maxdepth = 1)
            : this(new Business.Table.Empresas(datas), 
                  query ?? new Presentation.Query.Empresa(),
                 maxdepth)
        {
        }
        public EmpresasQuery(ICollection<Entities.Table.Empresa> entities = null, 
            Presentation.Query.Empresa query = null,
            int maxdepth = 1)
            : this(new Persistence.Table.Empresas(entities ?? new Collection<Entities.Table.Empresa>()), 
                  query ?? new Presentation.Query.Empresa(),
                 maxdepth)
        {
        }
    }
}

namespace demo.Presentation.Query
{
    public partial class Empresa : AbstractQueryModel<Presentation.Query.Empresa, Business.Query.Empresa, Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public Empresa(IInteractiveQuery<Business.Query.Empresa, Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa> interactive = null,
            Business.Query.Empresa domain = null)
            : base(interactive ?? new InteractiveQuery<Presentation.Query.Empresa, Business.Query.Empresa, Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>(new Presentation.Raiser.Empresa()),
                  domain ?? new Business.Query.Empresa())
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
            return _sucursal = query ?? _sucursal ?? new Presentation.Query.Sucursal(domain: Domain?.Sucursal());
        }
    }
}

namespace demo.Presentation.Raiser
{
    public partial class Empresa : BaseRaiser<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public override void Raise(Presentation.Table.Empresa model, int maxdepth = 1, int depth = 0)
        {
            base.Raise(model, maxdepth, depth);
        }
    }
}
