using Autofac;
using jrivam.Library;
using jrivam.Library.Impl.Business;
using jrivam.Library.Impl.Business.Attributes;
using jrivam.Library.Impl.Business.Query;
using jrivam.Library.Impl.Business.Table;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Persistence;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace demo.Business.Table
{
    public partial class Empresa : AbstractTableDomain<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>
    {
        protected override void Init()
        {
            base.Init();

            //Validations.Add(("RucNotEmpty", new EmptyValidator(Data["Ruc"])));
            //Validations.Add(("RucUnique", new UniqueValidator<Entities.Table.Empresa, Persistence.Table.Empresa>(Data["Ruc"], new Persistence.Query.Empresa())));
            //Validations.Add(("RazonSocialNotEmpty", new EmptyValidator(Data["RazonSocial"])));
        }

        public Empresa(ILogicTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa> logictable,
            Persistence.Table.Empresa data = null)
            : base(logictable,
                  data)
        {
        }

        [Domain]
        public virtual int? Id { get { return Data?.Id; } set { if (Data?.Id != value) { Data.Id = value; Changed = true; } } }
        [Domain]
        public virtual string Ruc { get { return Data?.Ruc; } set { if (Data?.Ruc != value) { Data.Ruc = value; Changed = true; } } }
        [Domain]
        public virtual string RazonSocial { get { return Data?.RazonSocial; } set { if (Data?.RazonSocial != value) { Data.RazonSocial = value; Changed = true; } } }
        [Domain]
        public virtual bool? Activo { get { return Data?.Activo; } set { if (Data?.Activo != value) { Data.Activo = value; Changed = true; } } }

        protected Business.Table.SucursalesQuery _sucursales;
        [Domain]
        public virtual Business.Table.SucursalesQuery Sucursales
        {
            get
            {
                if (_sucursales == null)
                {
                    Sucursales = AutofacConfiguration.Container.Resolve<Business.Table.SucursalesQuery>(new TypedParameter(typeof(Persistence.Table.SucursalesQuery), Data?.Sucursales));
                }

                _sucursales.Query.IdEmpresa = (this.Id, WhereOperator.Equals);

                return _sucursales;
            }
            set
            {
                if (_sucursales != value)
                {
                    _sucursales = value;
               }
            }
        }
    }

    public partial class Empresas : ListDomain<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>
    {
        public Empresas(IListData<Entities.Table.Empresa, Persistence.Table.Empresa> datas)
            : base(datas)
        {
        }

        public Empresas(ICollection<Entities.Table.Empresa> entities = null)
           : this(new Persistence.Table.Empresas(entities ?? new Collection<Entities.Table.Empresa>()))
        {
        }
    }

    public partial class EmpresasQuery : ListDomainQuery<Business.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>
    {
        public EmpresasQuery(Business.Query.Empresa query,
            IListData<Entities.Table.Empresa, Persistence.Table.Empresa> datas = null, 
            int maxdepth = 1)
            : base(query,
                  datas,
                  maxdepth)
        {
        }
    }
}

namespace demo.Business.Query
{
    public partial class Empresa : AbstractQueryDomain<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>
    {
        public Empresa(ILogicQuery<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa> logicquery,
            Persistence.Query.Empresa data)
            : base(logicquery,
                  data)
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                ((Persistence.Query.Empresa)Data).Id = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Ruc
        {
            set
            {
                ((Persistence.Query.Empresa)Data).Ruc = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) RazonSocial
        {
            set
            {
                ((Persistence.Query.Empresa)Data).RazonSocial = (value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                ((Persistence.Query.Empresa)Data).Activo = (value.value, value.sign);
            }
        }

        protected Business.Query.Sucursal _sucursal;
        public virtual Business.Query.Sucursal Sucursal(Business.Query.Sucursal query = null)
        {
            return _sucursal = query ?? _sucursal ?? AutofacConfiguration.Container.Resolve<Business.Query.Sucursal>(new TypedParameter(typeof(Persistence.Query.Sucursal), ((Persistence.Query.Empresa)Data)?.Sucursal()));
        }
    }
}
