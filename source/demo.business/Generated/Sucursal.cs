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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace demo.Business.Table
{
    public partial class Sucursal : AbstractTableDomain<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal>
    {
        protected override void Init()
        {
            base.Init();

            //Validations.Add(("CodigoNotEmpty", new EmptyValidator(Data["Codigo"])));
            //Validations.Add(("CodigoUnique", new UniqueValidator<Entities.Table.Sucursal, Persistence.Table.Sucursal>(Data["Codigo"], new Persistence.Query.Sucursal())));
            //Validations.Add(("NombreNotEmpty", new EmptyValidator(Data["Nombre"])));
        }

        public Sucursal(ILogicTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal> logictable = null,
            Business.Query.Sucursal query = null,
            Persistence.Table.Sucursal data = null)
            : base(logictable ?? AutofacConfiguration.Container.Resolve<ILogicTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal>>(),
                  query ?? new Business.Query.Sucursal(),
                  data ?? new Persistence.Table.Sucursal())
        {
        }

        [Domain]
        public virtual int? Id { get { return Data?.Id; } set { if (Data?.Id != value) { Data.Id = value; Changed = true; } } }
        [Domain]
        public virtual string Codigo { get { return Data?.Codigo; } set { if (Data?.Codigo != value) { Data.Codigo = value; Changed = true; } } }
        [Domain]
        public virtual string Nombre { get { return Data?.Nombre; } set { if (Data?.Nombre != value) { Data.Nombre = value; Changed = true; } } }
        [Domain]
        public virtual DateTime? Fecha { get { return Data?.Fecha; } set { if (Data?.Fecha != value) { Data.Fecha = value; Changed = true; } } }
        [Domain]
        public virtual bool? Activo { get { return Data?.Activo; } set { if (Data?.Activo != value) { Data.Activo = value; Changed = true; } } }

        [Domain]
        public virtual int? IdEmpresa 
        {
            get
            {
                return Data?.IdEmpresa;
            }
            set
            {
                if (Data?.IdEmpresa != value)
                {
                    Data.IdEmpresa = value;

                    Changed = true;

                    Empresa = null;
                }
            }
        }
        protected Business.Table.Empresa _empresa;
        [Domain]
        public virtual Business.Table.Empresa Empresa
        {
            get
            {
                if (_empresa?.Id != this.IdEmpresa)
                {
                    Empresa = new Business.Table.Empresa(data: Data?.Empresa);
                }

                _empresa.Id = this.IdEmpresa;

                return _empresa;
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;
                }
            }
        }
    }

    public partial class Sucursales : ListDomain<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal>
    {
        public Sucursales(IListData<Entities.Table.Sucursal, Persistence.Table.Sucursal> datas)
            : base(datas)
        {
        }

        public Sucursales(ICollection<Entities.Table.Sucursal> entities = null)
           : this(new Persistence.Table.Sucursales(entities ?? new Collection<Entities.Table.Sucursal>()))
        {
        }
    }

    public partial class SucursalesQuery : ListDomainQuery<Business.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal>
    {
        public SucursalesQuery(IListData<Entities.Table.Sucursal, Persistence.Table.Sucursal> datas, 
            Business.Query.Sucursal query = null, 
            int maxdepth = 1)
            : base(datas, 
                  query ?? new Business.Query.Sucursal(), 
                  maxdepth)
        {
        }

        public SucursalesQuery(ICollection<Entities.Table.Sucursal> entities = null, 
            Business.Query.Sucursal query = null,
            int maxdepth = 1)
            : this(new Persistence.Table.Sucursales(entities ?? new Collection<Entities.Table.Sucursal>()), 
                  query ?? new Business.Query.Sucursal(),
                 maxdepth)
        {
        }
    }
}

namespace demo.Business.Query
{
    public partial class Sucursal : AbstractQueryDomain<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal>
    {
        public Sucursal(ILogicQuery<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal> logicquery = null,
            Persistence.Query.Sucursal data = null)
            : base(logicquery ?? AutofacConfiguration.Container.Resolve<ILogicQuery<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal>>(),
                  data ?? new Persistence.Query.Sucursal())
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                ((Persistence.Query.Sucursal)Data).Id = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Codigo
        {
            set
            {
                ((Persistence.Query.Sucursal)Data).Codigo = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Nombre
        {
            set
            {
                ((Persistence.Query.Sucursal)Data).Nombre = (value.value, value.sign);
            }
        }
        public virtual (DateTime? value, WhereOperator? sign) Fecha
        {
            set
            {
                ((Persistence.Query.Sucursal)Data).Fecha = (value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                ((Persistence.Query.Sucursal)Data).Activo = (value.value, value.sign);
            }
        }
        public virtual (int? value, WhereOperator? sign) IdEmpresa
        {
            set
            {
                ((Persistence.Query.Sucursal)Data).IdEmpresa = (value.value, value.sign);
            }
        }

        protected Business.Query.Empresa _empresa;
        public virtual Business.Query.Empresa Empresa(Business.Query.Empresa query = null)
        {
            return _empresa = query ?? _empresa ?? new Business.Query.Empresa(data: ((Persistence.Query.Sucursal)Data)?.Empresa());
        }
    }
}