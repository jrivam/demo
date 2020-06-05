using Autofac;
using jrivam.Library;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Presentation;
using jrivam.Library.Impl.Presentation.Attributes;
using jrivam.Library.Impl.Presentation.Query;
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
        public Empresa(IInteractiveTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa> interactivetable = null,
            Presentation.Query.Empresa query = null, 
            Business.Table.Empresa domain = null,
            int maxdepth = 1,
            string name = null)
            : base(interactivetable ?? AutofacConfiguration.Container.Resolve<IInteractiveTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>>(),
                  query ?? new Presentation.Query.Empresa(),
                  domain ?? new Business.Table.Empresa(),
                  maxdepth,
                  name)
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

    public partial class EmpresasQuery : ListModelQuery<Presentation.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
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
    public partial class Empresa : AbstractQueryModel<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public Empresa(IInteractiveQuery<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa> interactivequery = null,
            Business.Query.Empresa domain = null)
            : base(interactivequery ?? AutofacConfiguration.Container.Resolve<IInteractiveQuery<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>>(),
                  domain ?? new Business.Query.Empresa())
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                ((Business.Query.Empresa)Domain).Id = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Ruc
        {
            set
            {
                ((Business.Query.Empresa)Domain).Ruc = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) RazonSocial
        {
            set
            {
                ((Business.Query.Empresa)Domain).RazonSocial = (value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                ((Business.Query.Empresa)Domain).Activo = (value.value, value.sign);
            }
        }

        protected Presentation.Query.Sucursal _sucursal;
        public virtual Presentation.Query.Sucursal Sucursal(Presentation.Query.Sucursal query = null)
        {
            return _sucursal = query ?? _sucursal ?? new Presentation.Query.Sucursal(domain: ((Business.Query.Empresa)Domain)?.Sucursal());
        }
    }
}