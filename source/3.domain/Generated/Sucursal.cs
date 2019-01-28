using library.Impl.Data.Sql;
using library.Impl.Domain;
using library.Impl.Domain.Mapper;
using library.Impl.Domain.Query;
using library.Impl.Domain.Table;
using library.Interface.Domain.Mapper;
using library.Interface.Domain.Query;
using library.Interface.Domain.Table;
using System;

namespace domain.Model
{
    public partial class Sucursal : AbstractTableLogicMethods<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>
    {
        public Sucursal(data.Model.Sucursal data,
            ILogicTable<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal> logic)
            : base(data, 
                  logic)
        {
        }

        public Sucursal(data.Model.Sucursal data,
            IMapperLogic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal> mapper)
            : this(data,
                  new LogicTable<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>(mapper))
        {
        }
        public Sucursal(data.Model.Sucursal data)
            : this(data,
                  new domain.Mapper.Sucursal())
        {
        }
        public Sucursal(entities.Model.Sucursal entity)
            : this(new data.Model.Sucursal(entity))
        {
        }
        public Sucursal()
            : this(new data.Model.Sucursal())
        {
        }

        public virtual int? Id { get { return Data?.Id; } set { if (Data?.Id != value) { Data.Id = value; Changed = true; } } }
        public virtual string Nombre { get { return Data?.Nombre; } set { if (Data?.Nombre != value) { Data.Nombre = value; Changed = true; } } }
        public virtual DateTime? Fecha { get { return Data?.Fecha; } set { if (Data?.Fecha != value) { Data.Fecha = value; Changed = true; } } }
        public virtual bool? Activo { get { return Data?.Activo; } set { if (Data?.Activo != value) { Data.Activo = value; Changed = true; } } }

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

        protected domain.Model.Empresa _empresa;
        public virtual domain.Model.Empresa Empresa
        {
            get
            {
                return _empresa ?? (Empresa = new domain.Model.Empresa(Data?.Empresa));
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;

                    Data.Empresa = _empresa?.Data;
                }
            }
        }
    }

    public partial class Sucursales : ListDomain<data.Query.Sucursal, domain.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>
    {
        public Sucursales(data.Model.Sucursales datas)
            : base(datas)
        {
        }
        public Sucursales()
            : this(new data.Model.Sucursales())
        {

        }
    }
}

namespace domain.Query
{
    public partial class Sucursal : AbstractQueryLogicMethods<data.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>
    {
        public Sucursal(data.Query.Sucursal data,
            ILogicQuery<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal> logic)
            : base(data, 
                  logic)
        {
        }

        public Sucursal(data.Query.Sucursal data,
            IMapperLogic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal> mapper)
            : this(data,
                  new LogicQuery<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>(mapper))
        {
        }
        public Sucursal(data.Query.Sucursal data)
            : this(data, new domain.Mapper.Sucursal())
        {
        }
        public Sucursal()
            : this(new data.Query.Sucursal())
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                Data.Id = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Nombre
        {
            set
            {
                Data.Nombre = (value.value, value.sign);
            }
        }
        public virtual (DateTime? value, WhereOperator? sign) Fecha
        {
            set
            {
                Data.Fecha = (value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                Data.Activo = (value.value, value.sign);
            }
        }
        public virtual (int? value, WhereOperator? sign) IdEmpresa
        {
            set
            {
                Data.IdEmpresa = (value.value, value.sign);
            }
        }

        protected domain.Query.Empresa _empresa;
        public virtual domain.Query.Empresa Empresa(domain.Query.Empresa query = null)
        {
            return _empresa = query ?? _empresa ?? new domain.Query.Empresa(Data?.Empresa());
        }
    }
}

namespace domain.Mapper
{
    public partial class Sucursal : BaseMapperLogic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>
    {
        public override domain.Model.Sucursal Clear(domain.Model.Sucursal domain, int maxdepth = 1, int depth = 0)
        {
            domain = base.Clear(domain, maxdepth, depth);

            depth++;
            if (depth >= maxdepth)
            {
                domain.Empresa = null;
            }

            return domain;
        }
    }
}