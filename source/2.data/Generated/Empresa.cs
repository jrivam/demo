using library.Extension;
using library.Impl;
using library.Impl.Data;
using library.Impl.Data.Mapper;
using library.Impl.Data.Query;
using library.Impl.Data.Repository;
using library.Impl.Data.Sql;
using library.Impl.Data.Sql.Factory;
using library.Impl.Data.Table;
using library.Interface.Data.Mapper;
using library.Interface.Data.Query;
using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Linq;

namespace data.Model
{
    public partial class Empresa : AbstractEntityRepositoryMethods<entities.Model.Empresa, data.Model.Empresa>
    {
        public virtual data.Query.Empresa Query
        {
            get
            {
                return new data.Query.Empresa();
            }
        }

        public Empresa(entities.Model.Empresa entity, 
            IRepositoryTable<entities.Model.Empresa, data.Model.Empresa> repository)
            : base(repository, typeof(entities.Model.Empresa).GetAttributeFromType<TableAttribute>()?.Name ?? "empresa", "Empresa")
        {
            Entity = entity;

            Columns.Add(new EntityColumn<int?>(Description, typeof(entities.Model.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id", true, true));
            Columns.Add(new EntityColumn<string>(Description, typeof(entities.Model.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("RazonSocial")?.Name ?? "razon_social", "RazonSocial"));
            Columns.Add(new EntityColumn<bool?>(Description, typeof(entities.Model.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));
        }

        public Empresa(ConnectionStringSettings connectionstringsettings, 
            entities.Model.Empresa entity, 
            IMapperRepository<entities.Model.Empresa, data.Model.Empresa> mapper)
            : this(entity, 
                  new RepositoryTable<entities.Model.Empresa, data.Model.Empresa>(mapper, connectionstringsettings))
        {
        }
        public Empresa(ConnectionStringSettings connectionstringsettings, 
            entities.Model.Empresa entity)
            : this(connectionstringsettings, 
                  entity, 
                  new data.Mapper.Empresa(connectionstringsettings))
        {
        }
        public Empresa(ConnectionStringSettings connectionstringsettings)
            : this(connectionstringsettings, 
                  new entities.Model.Empresa())
        {
        }
        public Empresa(string connectionstringname)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[connectionstringname]])
        {
        }

        public Empresa(entities.Model.Empresa entity, string connectionstringname)
            : this(connectionstringname)
        {
            SetProperties(entity);
        }

        public override (Result result, data.Model.Empresa data) SelectQuery(int maxdepth = 1, IQueryRepositoryMethods<entities.Model.Empresa, data.Model.Empresa> query = null)
        {
            var _query = (data.Query.Empresa)query ?? Query;

            _query.Id = (this.Id, WhereOperator.Equals);

            return _query.SelectSingle(maxdepth, this);
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

        public virtual data.Model.Sucursales Sucursales_Load(int maxdepth = 1, int top = 0, data.Query.Empresa query = null)
        {
            if (Entity?.Sucursales != null)
            {
                Sucursales = new data.Model.Sucursales(Entity?.Sucursales?.ToList());
            }
            else
            {
                if (this.Id != null)
                {
                    var _query = query ?? Query;

                    _query.Sucursal().IdEmpresa = (this.Id, WhereOperator.Equals);

                    Sucursales = (data.Model.Sucursales)new data.Model.Sucursales().Load(_query?.Sucursal(), maxdepth, top);
                }
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

    public partial class Empresas : ListEntityRepositoryProperties<data.Query.Empresa, entities.Model.Empresa, data.Model.Empresa>
    {
        public Empresas()
            : base()
        {
        }
        public Empresas(List<entities.Model.Empresa> entities)
            : this()
        {
            Entities = entities;
        }
    }
}

namespace data.Query
{
    public partial class Empresa : AbstractQueryRepositoryMethods<entities.Model.Empresa, data.Model.Empresa>
    {
        public Empresa(IRepositoryQuery<entities.Model.Empresa, data.Model.Empresa> repository)
            : base(repository, typeof(entities.Model.Empresa).GetAttributeFromType<TableAttribute>()?.Name ?? "empresa", "Empresa")
        {
            Columns.Add(new QueryColumn<int?>(this, typeof(entities.Model.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id"));
            Columns.Add(new QueryColumn<string>(this, typeof(entities.Model.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("RazonSocial")?.Name ?? "razon_social", "RazonSocial"));
            Columns.Add(new QueryColumn<bool?>(this, typeof(entities.Model.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));
        }
        public Empresa(ISqlCreator creator,
            IMapperRepository<entities.Model.Empresa, data.Model.Empresa> mapper,
            ISqlBuilderQuery builder)
            : this(new RepositoryQuery<entities.Model.Empresa, data.Model.Empresa>(creator, mapper, builder))
        {
        }
        public Empresa(ConnectionStringSettings connectionstringsettings)
            : this(new SqlCreator(connectionstringsettings),
                  new data.Mapper.Empresa(connectionstringsettings),
                  SqlBuilderQueryFactory.Create(connectionstringsettings))
        {
        }
        public Empresa(string connectionstringname)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[connectionstringname]])
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                this["Id"].Where(value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) RazonSocial
        {
            set
            {
                this["RazonSocial"].Where(value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                this["Activo"].Where(value.value, value.sign);
            }
        }

        protected data.Query.Sucursal _sucursal;
        public virtual data.Query.Sucursal Sucursal(data.Query.Sucursal query = null)
        {
            return _sucursal = query ?? _sucursal ?? new data.Query.Sucursal();
        }
    }
}

namespace data.Mapper
{
    public partial class Empresa : BaseMapperTable<entities.Model.Empresa, data.Model.Empresa>
    {
        public Empresa(ISqlSyntaxSign syntaxsign)
            : base(syntaxsign)
        {
        }
        public Empresa(ConnectionStringSettings connectionstringsettings)
            : this(SqlSyntaxSignFactory.Create(connectionstringsettings))
        {
        }
        public Empresa(string connectionstringname)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[connectionstringname]])
        {
        }

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
            data.Entity.Id = data?["Id"]?.Value as int?;
            data.Entity.RazonSocial = data?["RazonSocial"]?.Value as string;
            data.Entity.Activo = data?["Activo"]?.Value as bool?;

            return data;
        }

        public override data.Model.Empresa Read(data.Model.Empresa data, IDataReader reader, IList<string> prefixname, int maxdepth = 1, int depth = 0)
        {
            return base.Read(data, reader, prefixname, maxdepth, depth);
        }
    }
}
