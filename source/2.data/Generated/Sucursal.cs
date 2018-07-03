using library.Extension;
using library.Impl;
using library.Impl.Data.Mapper;
using library.Impl.Data.Query;
using library.Impl.Data.Sql;
using library.Impl.Data.Sql.Factory;
using library.Impl.Data.Table;
using library.Interface.Data.Mapper;
using library.Interface.Data.Query;
using library.Interface.Data.Sql;
using library.Interface.Data.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;

namespace data.Model
{
    public partial class Sucursal : AbstractTableRepositoryMethods<entities.Model.Sucursal, data.Model.Sucursal>
    {
        public virtual data.Query.Sucursal Query
        {
            get
            {
                return new data.Query.Sucursal();
            }
        }

        public Sucursal(entities.Model.Sucursal entity, 
            IRepositoryTable<entities.Model.Sucursal, data.Model.Sucursal> repository)
            : base(repository, typeof(entities.Model.Sucursal).GetAttributeFromType<TableAttribute>()?.Name ?? "sucursal", "Sucursal")
        {
            Entity = entity;

            Columns.Add(new TableColumn<int?>(Description, typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id", true, true));
            Columns.Add(new TableColumn<string>(Description, typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Nombre")?.Name ?? "nombre", "Nombre"));
            Columns.Add(new TableColumn<DateTime?>(Description, typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Fecha")?.Name ?? "fecha", "Fecha"));
            Columns.Add(new TableColumn<bool?>(Description, typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));

            Columns.Add(new TableColumn<int?>(Description, typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("IdEmpresa")?.Name ?? "id_empresa", "IdEmpresa"));
        }

        public Sucursal(ConnectionStringSettings connectionstringsettings,
            entities.Model.Sucursal entity, 
            IMapperRepository<entities.Model.Sucursal, data.Model.Sucursal> mapper)
            : this(entity, 
                  new RepositoryTable<entities.Model.Sucursal, data.Model.Sucursal>(mapper, connectionstringsettings))
        {
        }
        public Sucursal(ConnectionStringSettings connectionstringsettings,
            entities.Model.Sucursal entity)
            : this(connectionstringsettings, 
                  entity, 
                  new data.Mapper.Sucursal(connectionstringsettings))
        {
        }
        public Sucursal(ConnectionStringSettings connectionstringsettings)
            : this(connectionstringsettings, 
                  new entities.Model.Sucursal())
        {
        }
        public Sucursal(string appconnectionstringname)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        public Sucursal(entities.Model.Sucursal entity, string appconnectionstringname)
            : this(appconnectionstringname)
        {
            SetProperties(entity);
        }

        public override (Result result, data.Model.Sucursal data) SelectQuery(int maxdepth = 1, IQueryRepositoryMethods<entities.Model.Sucursal, data.Model.Sucursal> query = null)
        {
            var _query = (data.Query.Sucursal)query ?? Query;

            _query.Id = (this.Id, WhereOperator.Equals);

            return _query.SelectSingle(maxdepth, this);
        }

        public virtual int? Id
        {
            get
            {
                return Entity?.Id;
            }
            set
            {
                if (Entity?.Id != value)
                {
                    this["Id"].Value = Entity.Id = value;
                }
            }
        }
        public virtual string Nombre { get { return Entity?.Nombre; } set { if (Entity?.Nombre != value) { this["Nombre"].Value = Entity.Nombre = value; } } }
        public virtual DateTime? Fecha { get { return Entity?.Fecha; } set { if (Entity?.Fecha != value) { this["Fecha"].Value = Entity.Fecha = value; } } }
        public virtual bool? Activo { get { return Entity?.Activo; } set { if (Entity?.Activo != value) { this["Activo"].Value = Entity.Activo = value; } } }

        public virtual int? IdEmpresa
        {
            get
            {
                return Entity?.IdEmpresa;
            }
            set
            {
                if (Entity?.IdEmpresa != value)
                {
                    this["IdEmpresa"].Value = Entity.IdEmpresa = value;

                    Empresa = null;
                }
            }
        }

        public data.Model.Empresa Empresa_Load(data.Query.Sucursal query = null)
        {
            if (Entity?.Empresa != null)
            {
                Empresa = new data.Model.Empresa(Entity?.Empresa);
            }
            else
            {
                if (this.IdEmpresa != null)
                {
                    var _query = query ?? Query;

                    _query.Empresa().Id = (this.IdEmpresa, WhereOperator.Equals);

                    Empresa = _query?.Empresa()?.SelectSingle().data;
                }
            }

            return _empresa;
        }
        protected data.Model.Empresa _empresa;
        public virtual data.Model.Empresa Empresa
        {
            get
            {
                return _empresa ?? Empresa_Load();
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;

                    Entity.Empresa = _empresa?.Entity;
                }
            }
        }
    }

    public partial class Sucursales : ListTableRepositoryProperties<data.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal>
    {
        public Sucursales()
            : base()
        {
        }
        public Sucursales(List<entities.Model.Sucursal> entities)
            : this()
        {
            Entities = entities;
        }
    }
}

namespace data.Query
{
    public partial class Sucursal : AbstractQueryRepositoryMethods<entities.Model.Sucursal, data.Model.Sucursal>
    {
        public virtual data.Query.Sucursal Query
        {
            get
            {
                return new data.Query.Sucursal();
            }
        }

        public override IList<(IQueryColumn internalkey, IQueryColumn externalkey)> GetJoins(int maxdepth = 1, int depth = 0)
        {
            var joins = new List<(IQueryColumn internalkey, IQueryColumn externalkey)>();

            if (_empresa != null || depth < maxdepth || maxdepth == 0)
            {
                joins.Add((this["IdEmpresa"], Empresa()["Id"]));
            }

            return joins;
        }

        public Sucursal(IRepositoryQuery<entities.Model.Sucursal, data.Model.Sucursal> repository)
            : base(repository, typeof(entities.Model.Sucursal).GetAttributeFromType<TableAttribute>()?.Name ?? "sucursal", "Sucursal")
        {
            Columns.Add(new QueryColumn<int?>(this, typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id"));
            Columns.Add(new QueryColumn<string>(this, typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Nombre")?.Name ?? "nombre", "Nombre"));
            Columns.Add(new QueryColumn<DateTime?>(this, typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Fecha")?.Name ?? "fecha", "Fecha"));
            Columns.Add(new QueryColumn<bool?>(this, typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));

            Columns.Add(new QueryColumn<int?>(this, typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("IdEmpresa")?.Name ?? "id_empresa", "IdEmpresa"));
        }

        public Sucursal(ISqlCreator creator, 
        IMapperRepository<entities.Model.Sucursal, data.Model.Sucursal> mapper,
        ISqlBuilderQuery builder)
            : this(new RepositoryQuery<entities.Model.Sucursal, data.Model.Sucursal>(creator, mapper, builder))
        {
        }
        public Sucursal(ConnectionStringSettings connectionstringsettings)
            : this(new SqlCreator(connectionstringsettings),
                  new data.Mapper.Sucursal(connectionstringsettings),
                  SqlBuilderQueryFactory.Create(connectionstringsettings))
        {
        }
        public Sucursal(string appconnectionstringname)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                this["Id"].Where(value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Nombre
        {
            set
            {
                this["Nombre"].Where(value.value, value.sign);
            }
        }
        public virtual (DateTime? value, WhereOperator? sign) Fecha
        {
            set
            {
                this["Fecha"].Where(value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                this["Activo"].Where(value.value, value.sign);
            }
        }

        public virtual (int? value, WhereOperator? sign) IdEmpresa
        {
            set
            {
                this["IdEmpresa"].Where(value.value, value.sign);
            }
        }

        protected data.Query.Empresa _empresa;
        public virtual data.Query.Empresa Empresa(data.Query.Empresa query = null)
        {
            return _empresa = query ?? _empresa ?? new data.Query.Empresa();
        }
    }
}

namespace data.Mapper
{
    public partial class Sucursal : BaseMapperTable<entities.Model.Sucursal, data.Model.Sucursal>
    {
        public Sucursal(ISqlSyntaxSign syntaxsign)
            : base(syntaxsign)
        {
        }
        public Sucursal(ConnectionStringSettings connectionstringsettings)
            : this(SqlSyntaxSignFactory.Create(connectionstringsettings))
        {
        }
        public Sucursal(string appconnectionstringname)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]])
        {
        }

        public override data.Model.Sucursal CreateInstance(int maxdepth = 1, int depth = 0)
        {
            var instance = base.CreateInstance(maxdepth, depth);

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                instance.Empresa = new data.Mapper.Empresa().CreateInstance(maxdepth, depth);
            }

            return instance;
        }
        public override data.Model.Sucursal Clear(data.Model.Sucursal data, int maxdepth = 1, int depth = 0)
        {
            data.Entity.Id = null;
            data.Entity.Nombre = null;
            data.Entity.Fecha = null;
            data.Entity.Activo = null;
            data.Entity.IdEmpresa = null;

            data.Entity.Empresa = null;

            return data;
        }

        public override data.Model.Sucursal Map(data.Model.Sucursal data, int maxdepth = 1, int depth = 0)
        {
            data.Entity.Id = data?["Id"]?.Value as int?;
            data.Entity.Nombre = data?["Nombre"]?.Value as string;
            data.Entity.Activo = data?["Activo"]?.Value as bool?;
            data.Entity.Fecha = data?["Fecha"]?.Value as DateTime?;
            data.Entity.IdEmpresa = data?["IdEmpresa"]?.Value as int?;

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                data.Empresa = new data.Mapper.Empresa().Map(data?.Empresa, maxdepth, depth);
            }

            return data;
        }

        public override data.Model.Sucursal Read(data.Model.Sucursal data, IDataReader reader, IList<string> prefixname, int maxdepth = 1, int depth = 0)
        {
            data = base.Read(data, reader, prefixname, maxdepth, depth);

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                data.Empresa = new data.Mapper.Empresa().Read(data?.Empresa, reader, prefixname, maxdepth, depth);
            }

            return data;
        }
    }
}