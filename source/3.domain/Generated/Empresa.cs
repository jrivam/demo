using library.Impl.Domain.Mapper;
using library.Impl.Domain.Model;
using library.Impl.Domain.Query;
using library.Interface.Domain.Model;
using library.Interface.Domain.Query;
using System.Collections.Generic;
using System.Linq;

namespace domain.Model
{
    public partial class Empresa : AbstractEntityLogic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
        public Empresa(ILogicState<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa> logic)
            : base(logic)
        {
        }
        public Empresa()
            : this(new LogicState<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>(new domain.Mapper.Empresa()))
        {
        }
        public Empresa(data.Model.Empresa data)
            : this()
        {
            Data = data;
        }
        public Empresa(entities.Model.Empresa entity)
            : this()
        {
            SetProperties(entity);
        }

        public virtual int? Id { get { return Data?.Id; } set { if (Data?.Id != value) { Data.Id = value; Changed = true; } } }
        public virtual string RazonSocial { get { return Data?.RazonSocial; } set { if (Data?.RazonSocial != value) { Data.RazonSocial = value; Changed = true; } } }
        public virtual bool? Activo { get { return Data?.Activo; } set { if (Data?.Activo != value) { Data.Activo = value; Changed = true; } } }

        public virtual domain.Model.Sucursales Sucursales_Load(int maxdepth = 1, int top = 0)
        {
            if (this.Id != null)
            {
                var query = new domain.Query.Sucursal();
                query.Data["IdEmpresa"]?.Where(this.Id);

                Sucursales = new domain.Model.Sucursales().Load(query, maxdepth, top);
            }

            return _sucursales;
        }
        protected domain.Model.Sucursales _sucursales;
        public virtual domain.Model.Sucursales Sucursales
        {
            get
            {
                return _sucursales ?? Sucursales_Load();
            }
            set
            {
                if (_sucursales != value)
                {
                    _sucursales = value;

                    Data.Sucursales = _sucursales?.Datas;
                }
            }
        }

        protected override void SaveDependencies()
        {
            _sucursales?.ToList()?.ForEach(i => { i.Save(); });
        }
        protected override void EraseDependencies()
        {
            _sucursales?.ToList()?.ForEach(i => { i.Erase(); });
        }
    }

    public partial class Empresas : List<domain.Model.Empresa>
    {
        public virtual data.Model.Empresas Datas
        {
            get
            {
                return new data.Model.Empresas().Load(this.Select(x => x.Data).Cast<data.Model.Empresa>());
            }
        }

        public Empresas()
        {
        }

        public virtual domain.Model.Empresas Load(domain.Query.Empresa query, int maxdepth = 1, int top = 0)
        {
            return Load(query.List(maxdepth, top).domains);
        }
        public virtual domain.Model.Empresas Load(IEnumerable<domain.Model.Empresa> list)
        {
            this.AddRange(list);

            return this;
        }
    }
}

namespace domain.Query
{
    public partial class Empresa : AbstractQueryLogic<data.Query.Empresa, entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
        public Empresa(ILogicQuery<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa> logic)
            : base(logic)
        {
        }
        public Empresa()
            : this(new LogicQuery<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>(new domain.Mapper.Empresa()))
        {
        }
        public Empresa(data.Query.Empresa data)
            : this()
        {
            Data = data;
        }
    }
}

namespace domain.Mapper
{
    public partial class Empresa : AbstractMapperState<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
    }
}