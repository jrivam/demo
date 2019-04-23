using Library.Impl;
using Library.Impl.Domain;
using Library.Impl.Domain.Loader;
using Library.Impl.Domain.Query;
using Library.Impl.Domain.Table;
using Library.Impl.Persistence.Sql;
using Library.Interface.Business.Query;
using Library.Interface.Business.Table;
using System;

namespace Business.Table
{
    public partial class Sucursal : AbstractTableDomain<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal>
    {
        public Sucursal(Persistence.Table.Sucursal data,
            ILogicTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal> logic)
            : base(data, 
                  logic)
        {
        }

        public Sucursal(Persistence.Table.Sucursal data)
            : this(data,
                  new LogicTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal>(new Business.Mapper.Sucursal()))
        {
        }

        public Sucursal(Entities.Table.Sucursal entity)
            : this(new Persistence.Table.Sucursal(entity))
        {
        }
        public Sucursal()
            : this(new Entities.Table.Sucursal())
        {
        }

        public virtual int? Id { get { return Data?.Id; } set { if (Data?.Id != value) { Data.Id = value; Changed = true; } } }
        public virtual string Codigo { get { return Data?.Codigo; } set { if (Data?.Codigo != value) { Data.Codigo = value; Changed = true; } } }
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

        protected Business.Table.Empresa _empresa;
        public virtual (Result result, Business.Table.Empresa domain) Empresa_Refresh(int maxdepth = 1, Business.Query.Empresa queryempresa = null)
        {
            var refresh = Data.Empresa_Refresh(maxdepth, queryempresa?.Data);

            Empresa = new Business.Table.Empresa(refresh.data);

            return (refresh.result, _empresa);
        }
        public virtual Business.Table.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    if (Data?.Empresa != null)
                    {
                        Empresa = new Business.Table.Empresa(Data?.Empresa);
                    }
                    else
                    {
                        Empresa_Refresh();
                    }
                }

                return _empresa;
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

        protected Business.Table.Empresas _empresas;
        public virtual (Result result, Business.Table.Empresas domains) Empresas_Refresh(int maxdepth = 1, int top = 0, Business.Query.Empresa queryempresa = null)
        {
            var refresh = Data.Empresas_Refresh(maxdepth, top, queryempresa?.Data);

            Empresas = new Business.Table.Empresas(refresh.datas);

            return (refresh.result, _empresas);
        }
        public virtual Business.Table.Empresas Empresas
        {
            get
            {
                return _empresas ?? Empresas_Refresh().domains;
            }
            set
            {
                if (_empresas != value)
                {
                    _empresas = value;

                    Data.Empresas = (Persistence.Table.Empresas)new Persistence.Table.Empresas().Load(_empresas?.Datas);
                }
            }
        }
    }

    public partial class Sucursales : ListDomain<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal>
    {
        public Sucursales(Persistence.Table.Sucursales datas)
            : base(datas)
        {
        }
        public Sucursales()
            : base()
        {
        }
    }
}

namespace Business.Query
{
    public partial class Sucursal : AbstractQueryDomain<Persistence.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal>
    {
        public Sucursal(Persistence.Query.Sucursal data,
            ILogicQuery<Persistence.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal> logic)
            : base(data, 
                  logic)
        {
        }

        public Sucursal(Persistence.Query.Sucursal data)
            : this(data,
                  new LogicQuery<Persistence.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal>(new Business.Mapper.Sucursal()))
        {
        }

        public Sucursal()
            : this(new Persistence.Query.Sucursal())
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                Data.Id = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Codigo
        {
            set
            {
                Data.Codigo = (value.value, value.sign);
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

        protected Business.Query.Empresa _empresa;
        public virtual Business.Query.Empresa Empresa(Business.Query.Empresa query = null)
        {
            return _empresa = query ?? _empresa ?? new Business.Query.Empresa(Data?.Empresa());
        }
    }
}

namespace Business.Mapper
{
    public partial class Sucursal : BaseLoader<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal>
    {
        public override Business.Table.Sucursal Clear(Business.Table.Sucursal domain)
        {
            domain = base.Clear(domain);

            domain.Empresa = null;

            return domain;
        }
        public override Business.Table.Sucursal Load(Business.Table.Sucursal domain, int maxdepth = 1, int depth = 0)
        {
            return base.Load(domain, maxdepth, depth);
        }
    }
}