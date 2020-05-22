using Library.Impl.Business;
using Library.Impl.Business.Loader;
using Library.Impl.Business.Query;
using Library.Impl.Business.Table;
using Library.Impl.Persistence.Sql;
using Library.Interface.Business.Query;
using Library.Interface.Business.Table;
using Library.Interface.Persistence;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Business.Table
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
        public virtual Business.Table.Empresa Empresa
        {
            get
            {
                if (_empresa?.Id != this.IdEmpresa)
                {
                    Empresa = new Business.Table.Empresa(Data?.Empresa);
                }

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

        public Sucursales(ICollection<Entities.Table.Sucursal> entities)
           : this(new Persistence.Table.Sucursales(entities))
        {
        }
        public Sucursales()
            : this(new Collection<Entities.Table.Sucursal>())
        {
        }
    }

    public partial class SucursalesQuery : ListDomainQuery<Business.Query.Sucursal, Persistence.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal>
    {
        public SucursalesQuery(IListData<Entities.Table.Sucursal, Persistence.Table.Sucursal> datas, Business.Query.Sucursal query, 
            int maxdepth = 1)
            : base(datas, query, 
                  maxdepth)
        {
        }

        public SucursalesQuery(ICollection<Entities.Table.Sucursal> entities, Business.Query.Sucursal query = null,
            int maxdepth = 1)
            : this(new Persistence.Table.Sucursales(entities), query ?? new Business.Query.Sucursal(),
                 maxdepth)
        {
        }
        public SucursalesQuery(Business.Query.Sucursal query = null, int maxdepth = 1)
            : this(new Collection<Entities.Table.Sucursal>(), query ?? new Business.Query.Sucursal(),
                  maxdepth)
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
        public override Business.Table.Sucursal Load(Business.Table.Sucursal domain, int maxdepth = 1, int depth = 0)
        {
            domain = base.Load(domain, maxdepth, depth);

            return domain;
        }
    }
}