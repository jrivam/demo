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
using System.Threading.Tasks;

namespace demo.Presentation.Table
{
    public partial class Empresa : AbstractTableModel<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public Empresa(IInteractiveTableAsync<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa> interactivetable,
            Business.Table.Empresa domain = null,
            int maxdepth = 1,
            string name = null)
            : base(interactivetable,
                  domain,
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

        protected Presentation.Table.SucursalesReload _sucursales;
        [Model]
        public virtual Presentation.Table.SucursalesReload Sucursales
        {
            get
            {
                if (_sucursales == null)
                {
                    Sucursales = AutofacConfiguration.Container.Resolve<Presentation.Table.SucursalesReload>(new TypedParameter(typeof(IListDomainReload<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal>), Domain?.Sucursales));

                    if (this.Id != null)
                    {
                        _sucursales.Query.IdEmpresa = (this.Id, WhereOperator.Equals);

                        Task.Run(async () => await _sucursales.RefreshAsync());
                        // _sucursales.RefreshAsync();
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

    public partial class EmpresasEdit : ListModelEdit<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public EmpresasEdit(IListDomainEditAsync<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa> domains)
            : base(domains)
        {
        }

        public EmpresasEdit(IListDataEdit<Entities.Table.Empresa, Persistence.Table.Empresa> datas)
           : this(new Business.Table.EmpresasEdit(datas))
        {
        }
        public EmpresasEdit(ICollection<Entities.Table.Empresa> entities = null)
           : this(new Persistence.Table.EmpresasEdit(entities ?? new Collection<Entities.Table.Empresa>()))
        {
        }
    }

    public partial class EmpresasReload : ListModelReload<Presentation.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public EmpresasReload(Presentation.Query.Empresa query,
            IListDomainEditAsync<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa> domains = null, 
            int maxdepth = 1, int top = 0)
            : base(query, 
                  domains,                   
                  maxdepth, top)
        {
        }
    }
}

namespace demo.Presentation.Query
{
    public partial class Empresa : AbstractQueryModel<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa>
    {
        public Empresa(IInteractiveQueryAsync<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa, Presentation.Table.Empresa> interactivequery,
            Business.Query.Empresa domain)
            : base(interactivequery,
                  domain)
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
        public virtual Presentation.Query.Sucursal Sucursal
        {
            get
            {
                if (_sucursal == null)
                {
                    Sucursal = AutofacConfiguration.Container.Resolve<Presentation.Query.Sucursal>(new TypedParameter(typeof(Persistence.Query.Sucursal), ((Persistence.Query.Empresa)Domain)?.Sucursal));
                }

                return _sucursal;
            }
            set
            {
                if (_sucursal != value)
                {
                    _sucursal = value;
                }
            }
        }
    }
}