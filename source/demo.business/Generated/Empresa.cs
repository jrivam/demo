﻿using jrivam.Library.Impl.Business;
using jrivam.Library.Impl.Business.Attributes;
using jrivam.Library.Impl.Business.Loader;
using jrivam.Library.Impl.Business.Query;
using jrivam.Library.Impl.Business.Table;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Interface.Business.Query;
using jrivam.Library.Interface.Business.Table;
using jrivam.Library.Interface.Persistence;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Business.Table
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

        public Empresa(Persistence.Table.Empresa data,
            ILogicTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa> logic = null)
            : base(data,
                  logic ?? new LogicTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>(new Business.Loader.Empresa()))
        {
        }

        public Empresa(Entities.Table.Empresa entity,
            ILogicTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa> logic = null)
            : this(new Persistence.Table.Empresa(entity),
                  logic ?? new LogicTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>(new Business.Loader.Empresa()))

        {
        }
        public Empresa(ILogicTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa> logic = null)
            : this(new Entities.Table.Empresa(),
                  logic ?? new LogicTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>(new Business.Loader.Empresa()))
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
                    Sucursales = new SucursalesQuery(Data?.Sucursales);
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

        //protected override Result SaveChildren()
        //{
        //    var savechildren = base.SaveChildren();

        //    if (savechildren.Success)
        //    {
        //        var saveall = _sucursales?.SaveAll();

        //        savechildren.Append(saveall);
        //    }

        //    return savechildren;
        //}
        //protected override Result EraseChildren()
        //{
        //    var erasechildren = base.EraseChildren();

        //    if (erasechildren.Success)
        //    {
        //        var eraseall = Sucursales?.Refresh().domains?.EraseAll();

        //        erasechildren.Append(eraseall);
        //    }

        //    return erasechildren;
        //}
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

    public partial class EmpresasQuery : ListDomainQuery<Business.Query.Empresa, Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>
    {
        public EmpresasQuery(IListData<Entities.Table.Empresa, Persistence.Table.Empresa> datas, 
            Business.Query.Empresa query = null, 
            int maxdepth = 1)
            : base(datas, 
                  query ?? new Business.Query.Empresa(), 
                  maxdepth)
        {
        }

        public EmpresasQuery(ICollection<Entities.Table.Empresa> entities = null, 
            Business.Query.Empresa query = null,
            int maxdepth = 1)
            : this(new Persistence.Table.Empresas(entities ?? new Collection<Entities.Table.Empresa>()), 
                  query ?? new Business.Query.Empresa(),
                 maxdepth)
        {
        }
    }
}

namespace Business.Query
{
    public partial class Empresa : AbstractQueryDomain<Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>
    {
        public Empresa(Persistence.Query.Empresa data = null,
            ILogicQuery<Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa> logic = null)
            : base(data ?? new Persistence.Query.Empresa(), 
                  logic ?? new LogicQuery<Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>(new Business.Loader.Empresa()))
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                Data.Id = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Ruc
        {
            set
            {
                Data.Ruc = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) RazonSocial
        {
            set
            {
                Data.RazonSocial = (value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                Data.Activo = (value.value, value.sign);
            }
        }

        protected Business.Query.Sucursal _sucursal;
        public virtual Business.Query.Sucursal Sucursal(Business.Query.Sucursal query = null)
        {
            return _sucursal = query ?? _sucursal ?? new Business.Query.Sucursal(Data?.Sucursal());
        }
    }
}

namespace Business.Loader
{
    public partial class Empresa : BaseLoader<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>
    {
        public override void Load(Business.Table.Empresa domain, int maxdepth = 1, int depth = 0)
        {
            base.Load(domain, maxdepth, depth);
        }
    }
}