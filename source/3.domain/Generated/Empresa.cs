using library.Impl;
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
        public Empresa(data.Model.Empresa data,
            ILogicTable<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa> logic)
            : base(data, 
                  logic)
        {
        }

        public Empresa(data.Model.Empresa data,
            IMapperLogic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa> mapper)
            : this(data,
                  new LogicTable<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>(mapper))
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
            : this(new entities.Model.Empresa())
        {
        }

        public virtual int? Id { get { return Data?.Id; } set { if (Data?.Id != value) { Data.Id = value; Changed = true; } } }
        public virtual string Ruc { get { return Data?.Ruc; } set { if (Data?.Ruc != value) { Data.Ruc = value; Changed = true; } } }
        public virtual string RazonSocial { get { return Data?.RazonSocial; } set { if (Data?.RazonSocial != value) { Data.RazonSocial = value; Changed = true; } } }
        public virtual bool? Activo { get { return Data?.Activo; } set { if (Data?.Activo != value) { Data.Activo = value; Changed = true; } } }

        protected domain.Model.Sucursales _sucursales;
        public virtual (Result result, domain.Model.Sucursales domains) Sucursales_Refresh(int maxdepth = 1, int top = 0, domain.Query.Empresa query = null)
        {
            var refresh = Data.Sucursales_Refresh(maxdepth, top, query?.Data);

            Sucursales = new domain.Model.Sucursales(refresh.datas);

            return (refresh.result, _sucursales);
        }
        public virtual domain.Model.Sucursales Sucursales
        {
            get
            {
                if (_sucursales == null)
                {
                    if (Data?.Sucursales != null)
                    {
                        Sucursales = new domain.Model.Sucursales(Data?.Sucursales);
                    }
                    else
                    {
                        Sucursales_Refresh();
                    }
                }

                return _sucursales;
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
                var saveall = _sucursales?.SaveAll();

                savechildren.Append(saveall);
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
                    var eraseall = Sucursales?.EraseAll();

                    erasechildren.Append(eraseall);
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
            : base(datas)
        {
        }
    }
}

namespace domain.Query
{
    public partial class Empresa : AbstractQueryLogicMethods<data.Query.Empresa, entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
        public Empresa(data.Query.Empresa data,
            ILogicQuery<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa> logic)
            : base(data, 
                  logic)
        {
        }

        public Empresa(data.Query.Empresa data,
            IMapperLogic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa> mapper)
            : this(data, 
                  new LogicQuery<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>(mapper))
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