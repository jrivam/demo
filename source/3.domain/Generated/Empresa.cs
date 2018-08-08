﻿using library.Impl;
using library.Impl.Data.Sql;
using library.Impl.Domain;
using library.Impl.Domain.Mapper;
using library.Impl.Domain.Query;
using library.Impl.Domain.Table;
using library.Interface.Domain.Mapper;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;

namespace domain.Model
{
    public partial class Empresa : AbstractTableLogicMethods<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
        public Empresa(ILogicTable<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa> logic,
            data.Model.Empresa data)
            : base(logic)
        {
            Data = data;
        }

        public Empresa(data.Model.Empresa data,
            IMapperLogic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa> mapper)
            : this(new LogicTable<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>(mapper),
                  data)
        {
        }
        public Empresa(data.Model.Empresa data)
            : this(data,
                  new domain.Mapper.Empresa())
        {
        }
        public Empresa(entities.Model.Empresa entity)
            : this(new data.Model.Empresa(entity))
        {
        }
        public Empresa()
            : this(new data.Model.Empresa())
        {
        }

        public virtual int? Id { get { return Data?.Id; } set { if (Data?.Id != value) { Data.Id = value; Changed = true; } } }
        public virtual string RazonSocial { get { return Data?.RazonSocial; } set { if (Data?.RazonSocial != value) { Data.RazonSocial = value; Changed = true; } } }
        public virtual bool? Activo { get { return Data?.Activo; } set { if (Data?.Activo != value) { Data.Activo = value; Changed = true; } } }

        protected domain.Model.Sucursales _sucursales;
        public virtual domain.Model.Sucursales Sucursales
        {
            get
            {
                return _sucursales ?? (Sucursales = new domain.Model.Sucursales(Data?.Sucursales));
            }
            set
            {
                if (_sucursales != value)
                {
                    _sucursales = value;

                    Data.Sucursales = _sucursales != null ? (data.Model.Sucursales)new data.Model.Sucursales().Load(_sucursales?.Datas) : null;
                }
            }
        }

        protected override Result SaveChildren()
        {
            var savechildren = base.SaveChildren();

            if (savechildren.Success)
            {
                savechildren.Append(_sucursales?.Save());
            }

            return savechildren;
        }
        protected override Result EraseChildren()
        {
            var erasechildren = base.EraseChildren();

            if (this.Id != null)
            {
                if (erasechildren.Success)
                {
                    var _query = Data.Query;

                    _query.Sucursal().IdEmpresa = (this.Id, WhereOperator.Equals);

                    erasechildren.Append(_query?.Sucursal()?.Delete().result);
                }
            }

            return erasechildren;
        }
    }

    public partial class Empresas : ListDomain<data.Query.Empresa, domain.Query.Empresa, entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
        public Empresas()
            : base()
        {
        }
        public Empresas(data.Model.Empresas datas)
            : this()
        {
            Datas = datas;
        }
    }
}

namespace domain.Query
{
    public partial class Empresa : AbstractQueryLogicMethods<data.Query.Empresa, entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
        public Empresa(ILogicQuery<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa> logic,
            data.Query.Empresa data)
            : base(logic)
        {
            Data = data;
        }

        public Empresa(data.Query.Empresa data,
            IMapperLogic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa> mapper)
            : this(new LogicQuery<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>(mapper),
                  data)
        {
        }
        public Empresa(data.Query.Empresa data)
            : this(data, new domain.Mapper.Empresa())
        {
        }
        public Empresa()
            : this(new data.Query.Empresa())
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                Data.Id = (value.value, value.sign);
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

        protected domain.Query.Sucursal _sucursal;
        public virtual domain.Query.Sucursal Sucursal(domain.Query.Sucursal query = null)
        {
            return _sucursal = query ?? _sucursal ?? new domain.Query.Sucursal(Data?.Sucursal());
        }
    }
}

namespace domain.Mapper
{
    public partial class Empresa : BaseMapperLogic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
        public override domain.Model.Empresa Clear(domain.Model.Empresa domain, int maxdepth = 1, int depth = 0)
        {
            domain = base.Clear(domain, maxdepth, depth);

            domain.Sucursales = null;

            return domain;
        }
    }
}