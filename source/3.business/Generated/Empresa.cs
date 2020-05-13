using Library.Impl;
using Library.Impl.Business;
using Library.Impl.Business.Loader;
using Library.Impl.Business.Query;
using Library.Impl.Business.Table;
using Library.Impl.Persistence.Sql;
using Library.Interface.Business.Query;
using Library.Interface.Business.Table;

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
            ILogicTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa> logic)
            : base(data, 
                  logic)
        {
        }

        public Empresa(Persistence.Table.Empresa data)
            : this(data,
                  new LogicTable<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>(new Business.Mapper.Empresa()))
        {
        }

        public Empresa(Entities.Table.Empresa entity)
            : this(new Persistence.Table.Empresa(entity))
        {
        }
        public Empresa()
            : this(new Entities.Table.Empresa())
        {
        }

        public override Persistence.Table.Empresa Data
        {
            get
            {
                return base.Data;
            }
            set
            {
                 base.Data = value;

                 _sucursales = null;
            }
        }

        public virtual int? Id { get { return Data?.Id; } set { if (Data?.Id != value) { Data.Id = value; Changed = true; } } }
        public virtual string Ruc { get { return Data?.Ruc; } set { if (Data?.Ruc != value) { Data.Ruc = value; Changed = true; } } }
        public virtual string RazonSocial { get { return Data?.RazonSocial; } set { if (Data?.RazonSocial != value) { Data.RazonSocial = value; Changed = true; } } }
        public virtual bool? Activo { get { return Data?.Activo; } set { if (Data?.Activo != value) { Data.Activo = value; Changed = true; } } }

        protected Business.Table.Sucursales _sucursales;
        public virtual Business.Table.Sucursales Sucursales
        {
            get
            {
                if (_sucursales == null)
                {
                    if (Data?.Sucursales != null)
                    {
                        Sucursales = new Business.Table.Sucursales(Data?.Sucursales);
                    }
                    else
                    {
                        Sucursales_Refresh();
                    }
                }

                return _sucursales;
            }
            protected set
            {
                if (_sucursales != value)
                {
                    _sucursales = value;

                    //Data.Sucursales = _sucursales != null ? (Persistence.Table.Sucursales)new Persistence.Table.Sucursales().Load(_sucursales?.Datas) : null;
                }
            }
        }
        public virtual (Result result, Business.Table.Sucursales domains) Sucursales_Refresh(int maxdepth = 1, int top = 0, Business.Query.Sucursal query = null)
        {
            var refresh = Data.Sucursales_Refresh(maxdepth, top, query?.Data);

            if (refresh.datas != null)
                Sucursales = new Business.Table.Sucursales(refresh.datas);

            return (refresh.result, _sucursales);
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

            if (erasechildren.Success)
            {
                var eraseall = Sucursales?.EraseAll();

                erasechildren.Append(eraseall);
            }

            return erasechildren;
        }
    }

    public partial class Empresas : ListDomain<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>
    {
        public Empresas(Persistence.Table.Empresas datas)
            : base(datas)
        {
        }

        public Empresas()
            : this(new Persistence.Table.Empresas())
        {
        }
    }
}

namespace Business.Query
{
    public partial class Empresa : AbstractQueryDomain<Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>
    {
        public Empresa(Persistence.Query.Empresa data,
            ILogicQuery<Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa> logic)
            : base(data, 
                  logic)
        {
        }

        public Empresa(Persistence.Query.Empresa data)
            : this(data, 
                  new LogicQuery<Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>(new Business.Mapper.Empresa()))
        {
        }

        public Empresa()
            : this(new Persistence.Query.Empresa())
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

namespace Business.Mapper
{
    public partial class Empresa : BaseLoader<Entities.Table.Empresa, Persistence.Table.Empresa, Business.Table.Empresa>
    {
        public override Business.Table.Empresa Load(Business.Table.Empresa domain, int maxdepth = 1, int depth = 0)
        {
            domain = base.Load(domain, maxdepth, depth);

            return domain;
        }
    }
}