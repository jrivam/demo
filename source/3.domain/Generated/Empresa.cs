using Library.Impl;
using Library.Impl.Data.Sql;
using Library.Impl.Domain;
using Library.Impl.Domain.Mapper;
using Library.Impl.Domain.Query;
using Library.Impl.Domain.Table;
using Library.Interface.Domain.Mapper;
using Library.Interface.Domain.Query;
using Library.Interface.Domain.Table;

namespace domain.Model
{
    public partial class Empresa : AbstractTableDomain<Entities.Table.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
        public Empresa(data.Model.Empresa data,
            ILogicTable<Entities.Table.Empresa, data.Model.Empresa, domain.Model.Empresa> logic)
            : base(data, 
                  logic)
        {
        }

        public Empresa(data.Model.Empresa data,
            IMapperLogic<Entities.Table.Empresa, data.Model.Empresa, domain.Model.Empresa> mapper)
            : this(data,
                  new LogicTable<Entities.Table.Empresa, data.Model.Empresa, domain.Model.Empresa>(mapper))
        {
        }
        public Empresa(data.Model.Empresa data)
            : this(data,
                  new domain.Mapper.Empresa())
        {
        }

        public Empresa(Entities.Table.Empresa entity)
            : this(new data.Model.Empresa(entity))
        {
        }
        public Empresa()
            : this(new Entities.Table.Empresa())
        {
        }

        public virtual int? Id { get { return Data?.Id; } set { if (Data?.Id != value) { Data.Id = value; Changed = true; } } }
        public virtual string Ruc { get { return Data?.Ruc; } set { if (Data?.Ruc != value) { Data.Ruc = value; Changed = true; } } }
        public virtual string RazonSocial { get { return Data?.RazonSocial; } set { if (Data?.RazonSocial != value) { Data.RazonSocial = value; Changed = true; } } }
        public virtual bool? Activo { get { return Data?.Activo; } set { if (Data?.Activo != value) { Data.Activo = value; Changed = true; } } }

        protected domain.Model.Sucursales _sucursales;
        public virtual (Result result, domain.Model.Sucursales domains) Sucursales_Refresh(int maxdepth = 1, int top = 0, domain.Query.Sucursal query = null)
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

    public partial class Empresas : ListDomain<Entities.Table.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
        public Empresas(data.Model.Empresas datas)
            : base(datas)
        {
        }
        public Empresas()
            : base()
        {
        }
    }
}

namespace domain.Query
{
    public partial class Empresa : AbstractQueryDomain<data.Query.Empresa, Entities.Table.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
        public Empresa(data.Query.Empresa data,
            ILogicQuery<data.Query.Empresa, Entities.Table.Empresa, data.Model.Empresa, domain.Model.Empresa> logic)
            : base(data, 
                  logic)
        {
        }

        public Empresa(data.Query.Empresa data,
            IMapperLogic<Entities.Table.Empresa, data.Model.Empresa, domain.Model.Empresa> mapper)
            : this(data, 
                  new LogicQuery<data.Query.Empresa, Entities.Table.Empresa, data.Model.Empresa, domain.Model.Empresa>(mapper))
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
    public partial class Empresa : BaseMapperLogic<Entities.Table.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
        public override domain.Model.Empresa Clear(domain.Model.Empresa domain, int maxdepth = 1, int depth = 0)
        {
            domain = base.Clear(domain, maxdepth, depth);

            domain.Sucursales = null;

            return domain;
        }
    }
}