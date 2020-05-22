using Library.Impl.Persistence.Sql;
using Library.Impl.Presentation;
using Library.Impl.Presentation.Query;
using Library.Impl.Presentation.Raiser;
using Library.Impl.Presentation.Table;
using Library.Interface.Business;
using Library.Interface.Persistence;
using Library.Interface.Presentation.Query;
using Library.Interface.Presentation.Table;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Presentation.Table
{
    public partial class Empresa : AbstractTableModel<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        protected override void Init()
        {
            base.Init();

            Elements.Add(new Element(nameof(Id)));
            Elements.Add(new Element(nameof(Ruc)));
            Elements.Add(new Element(nameof(RazonSocial)));
            Elements.Add(new Element(nameof(Activo)));
            Elements.Add(new Element(nameof(Sucursales)));
        }

        public Empresa(Business.Table.Empresa domain,
            IInteractiveTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa> interactive,
            int maxdepth = 1)
            : base(nameof(Empresa),
                  domain, 
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

        //public override Business.Table.Empresa Domain
        //{
        //    get
        //    {
        //        return base.Domain;
        //    }
        //    set
        //    {
        //        base.Domain = value;

        //        _sucursales = null;
        //    }
        //}

        public virtual int? Id 
        { 
            get 
            { 
                return Domain?.Id; 
            } 
            set 
            { 
                if (Domain?.Id != value) 
                { 
                    Domain.Id = value;

                    ValidateProperty(value); 
                    OnPropertyChanged(nameof(Id)); 
                } 
            } 
        }
        public virtual string Ruc { get { return Domain?.Ruc; } set { if (Domain?.Ruc != value) { Domain.Ruc = value; ValidateProperty(value); OnPropertyChanged(nameof(Ruc)); } } }
        public virtual string RazonSocial
        {
            get
            {
                return Domain?.RazonSocial;
            }
            set
            {
                if (Domain?.RazonSocial != value)
                {
                    Domain.RazonSocial = value;
                    ValidateProperty(value);
                    OnPropertyChanged(nameof(RazonSocial));
                }
            }
        }
        public virtual bool? Activo { get { return Domain?.Activo; } set { if (Domain?.Activo != value) { Domain.Activo = value; ValidateProperty(value); OnPropertyChanged(nameof(Activo)); } } }

        protected Presentation.Table.SucursalesQuery _sucursales;
        public virtual Presentation.Table.SucursalesQuery Sucursales
        {
            get
            {
                if (_sucursales == null)
                {
                    if (this.Id != null)
                    {
                        Sucursales = new SucursalesQuery(Domain.Sucursales, new Presentation.Query.Sucursal());
                        _sucursales.Query.IdEmpresa = (this.Id, WhereOperator.Equals);

                        _sucursales.Refresh();
                    }
                }

                return _sucursales;
            }
            set
            {
                if (_sucursales != value)
                {
                    _sucursales = value;

                    //Domain.Sucursales = (_sucursales != null) ? (Business.Table.Sucursales)new Business.Table.Sucursales().Load(_sucursales?.Domains) : null;
 
                    OnPropertyChanged(nameof(Sucursales));
                }
            }
        }
    }

    public partial class Empresas : ListModel<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public Empresas(IListDomain<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa> domains)
            : base(nameof(Empresas), domains)
        {
        }

        public Empresas(IListData<Entities.Table.Empresa, Persistence.Table.Empresa> datas)
           : this(new Business.Table.Empresas(datas))
        {
        }
        public Empresas(ICollection<Entities.Table.Empresa> entities)
           : this(new Persistence.Table.Empresas(entities))
        {
        }
        public Empresas()
            : this(new Collection<Entities.Table.Empresa>())
        {
        }
    }

    public partial class EmpresasQuery : ListModelQuery<Presentation.Query.Empresa, Business.Query.Empresa, Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public EmpresasQuery(IListDomain<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa> domains, Presentation.Query.Empresa query,             
            int maxdepth = 1, int top = 0)
            : base(nameof(Empresas),
                  domains, query, 
                  maxdepth, top)
        {
        }

        public EmpresasQuery(IListData<Entities.Table.Empresa, Persistence.Table.Empresa> datas, Presentation.Query.Empresa query = null,
            int maxdepth = 1)
            : this(new Business.Table.Empresas(datas), query ?? new Presentation.Query.Empresa(),
                 maxdepth)
        {
        }
        public EmpresasQuery(ICollection<Entities.Table.Empresa> entities, Presentation.Query.Empresa query = null,
            int maxdepth = 1)
            : this(new Persistence.Table.Empresas(entities), query ?? new Presentation.Query.Empresa(),
                 maxdepth)
        {
        }
        public EmpresasQuery(Presentation.Query.Empresa query = null, int maxdepth = 1)
            : this(new Collection<Entities.Table.Empresa>(), query ?? new Presentation.Query.Empresa(),
                  maxdepth)
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
        public override Presentation.Table.Empresa Raise(Presentation.Table.Empresa model, int maxdepth = 1, int depth = 0)
        {
            model = base.Raise(model, maxdepth, depth);

            //model.OnPropertyChanged("Sucursales");

            return model;
        }
    }
}
