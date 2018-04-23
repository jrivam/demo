using library.Impl.Data.Mapper;
using library.Impl.Data.Model;
using library.Impl.Data.Query;
using library.Impl.Data.Repository;
using library.Interface.Data.Model;
using library.Interface.Data.Query;
using System.Collections.Generic;
using System.Data;

namespace data.Model
{
    public partial class Empresa : AbstractEntityRepository<entities.Model.Empresa, data.Model.Empresa>
    {
        public Empresa(IRepositoryTable<entities.Model.Empresa, data.Model.Empresa> repository)
            : base(repository, "empresa", "Empresa")
        {
            Columns.Add(new EntityColumn<int?, entities.Model.Empresa>(this, "id", "Id", true, true));
            Columns.Add(new EntityColumn<string, entities.Model.Empresa>(this, "razon_social", "RazonSocial"));
            Columns.Add(new EntityColumn<bool?, entities.Model.Empresa>(this, "activo", "Activo"));

            InitDbCommands();
        }
        public Empresa(string connectionstringname)
            : this(new RepositoryTable<entities.Model.Empresa, data.Model.Empresa>(new data.Mapper.Empresa(), connectionstringname))
        {
        }
        public Empresa(entities.Model.Empresa entity, string connectionstringname)
            : this(connectionstringname)
        {
            SetProperties(entity);
        }

        public virtual int? Id
        {
            get { return Entity?.Id; }
            set
            {
                if (Entity?.Id != value)
                {
                    this["Id"].Value = Entity.Id = value;

                    _sucursales?.ForEach(x => x.IdEmpresa = value);
                }
            }
        }
        public virtual string RazonSocial { get { return Entity?.RazonSocial; } set { if (Entity?.RazonSocial != value) { this["RazonSocial"].Value = Entity.RazonSocial = value; } } }
        public virtual bool? Activo { get { return Entity?.Activo; } set { if (Entity?.Activo != value) { this["Activo"].Value = Entity.Activo = value; } } }

        public virtual data.Model.Sucursales Sucursales_Load(int maxdepth = 1, int top = 0)
        {
            if (this.Id != null)
            {
                var query = new data.Query.Sucursal();
                query["IdEmpresa"]?.Where(this.Id);

                Sucursales = new data.Model.Sucursales().Load(query, maxdepth, top);
            }

            return _sucursales;
        }
        protected data.Model.Sucursales _sucursales;
        public virtual data.Model.Sucursales Sucursales
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

                    Entity.Sucursales = _sucursales?.Entities;
                }
            }
        } 
    }

    public partial class Empresas : List<data.Model.Empresa>
    {
        public virtual IList<entities.Model.Empresa> Entities
        {
            get
            {
                var list = new List<entities.Model.Empresa>();
                this.ForEach(x => list.Add(x.Entity));
                return list;
            }
        }

        public Empresas()
        {
        }

        public virtual data.Model.Empresas Load(data.Query.Empresa query, int maxdepth = 1, int top = 0)
        {
            return Load(query.SelectMultiple(maxdepth, top).datas);
        }
        public virtual data.Model.Empresas Load(IEnumerable<data.Model.Empresa> list)
        {
            this.AddRange(list);

            return this;
        }
    }
}

namespace data.Query
{
    public partial class Empresa : AbstractQueryRepository<entities.Model.Empresa, data.Model.Empresa>
    {
        public Empresa(IRepositoryQuery<entities.Model.Empresa, data.Model.Empresa> repository)
            : base(repository, "empresa", "Empresa")
        {
            Columns.Add(new QueryColumn<int?>(this, "id", "Id"));
            Columns.Add(new QueryColumn<string>(this, "razon_social", "RazonSocial"));
            Columns.Add(new QueryColumn<bool?>(this, "activo", "Activo"));
        }
        public Empresa(string connectionstringname)
            : this(new RepositoryQuery<entities.Model.Empresa, data.Model.Empresa>(new data.Mapper.Empresa(), connectionstringname))
        {
        }
    }
}

namespace data.Mapper
{
    public partial class Empresa : AbstractMapperTable<entities.Model.Empresa, data.Model.Empresa>
    {
        public override data.Model.Empresa CreateInstance(int maxdepth = 1, int depth = 0)
        {
            return base.CreateInstance(maxdepth, depth);
        }

        public override data.Model.Empresa Clear(data.Model.Empresa data, int maxdepth = 1, int depth = 0)
        {
            data.Id = null;
            data.RazonSocial = null;
            data.Activo = null;

            data.Sucursales = null;

            return data;
        }
        public override data.Model.Empresa Map(data.Model.Empresa data, int maxdepth = 1, int depth = 0)
        {
            data.Entity.Id = data["Id"]?.Value as int?;
            data.Entity.RazonSocial = data["RazonSocial"]?.Value as string;
            data.Entity.Activo = data["Activo"]?.Value as bool?;

            return data;
        }

        public override data.Model.Empresa Read(data.Model.Empresa data, IDataReader reader, IList<string> prefixname, string aliasseparator = ".", int maxdepth = 1, int depth = 0)
        {
            return base.Read(data, reader, prefixname, aliasseparator, maxdepth, depth);
        }
    }
}
