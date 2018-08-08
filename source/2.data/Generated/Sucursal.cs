using library.Extension;
using library.Impl;
using library.Impl.Data;
using library.Impl.Data.Mapper;
using library.Impl.Data.Query;
using library.Impl.Data.Sql;
using library.Impl.Data.Table;
using library.Impl.Entities;
using library.Impl.Entities.Reader;
using library.Interface.Data.Mapper;
using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Entities.Reader;
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

        public Sucursal(IRepositoryTable<entities.Model.Sucursal, data.Model.Sucursal> repository,
            entities.Model.Sucursal entity)
            : base(repository, 
                  typeof(entities.Model.Sucursal).GetAttributeFromType<TableAttribute>()?.Name ?? "sucursal", "Sucursal")
        {
            Entity = entity;

            Columns.Add(new TableColumn<int?>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id", true, true));
            Columns.Add(new TableColumn<string>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Nombre")?.Name ?? "nombre", "Nombre"));
            Columns.Add(new TableColumn<DateTime?>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Fecha")?.Name ?? "fecha", "Fecha"));
            Columns.Add(new TableColumn<bool?>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));

            Columns.Add(new TableColumn<int?>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("IdEmpresa")?.Name ?? "id_empresa", "IdEmpresa"));
        }

        public Sucursal(ConnectionStringSettings connectionstringsettings,
            IReaderEntity<entities.Model.Sucursal> reader,
            IMapperRepository<entities.Model.Sucursal, data.Model.Sucursal> mapper,
            entities.Model.Sucursal entity)
            : this(new RepositoryTable<entities.Model.Sucursal, data.Model.Sucursal>(reader, mapper, connectionstringsettings),
                  entity)
        {
        }
        public Sucursal(ConnectionStringSettings connectionstringsettings,
            entities.Model.Sucursal entity)
            : this(connectionstringsettings, 
                  new entities.Reader.Sucursal(),
                  new data.Mapper.Sucursal(),
                  entity)
        {
        }
        public Sucursal(ConnectionStringSettings connectionstringsettings)
            : this(connectionstringsettings, 
                  new entities.Model.Sucursal())
        {
        }
        public Sucursal(string appconnectionstringname, entities.Model.Sucursal entity)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appconnectionstringname]],
                  entity)
        {
        }
        public Sucursal(string appconnectionstringname)
            : this(appconnectionstringname,
                new entities.Model.Sucursal())
        {
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

    public partial class Sucursales : ListData<data.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal>
    {
        public Sucursales()
            : base()
        {
        }
        public Sucursales(ListEntity<entities.Model.Sucursal> entities)
            : this()
        {
            Entities = entities;
        }
    }
}

namespace data.Query
{
    public partial class Sucursal : AbstractQueryRepositoryMethods<data.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal>
    {
        public override IList<(IQueryRepository internaltable, IQueryColumn internalkey, IQueryRepository externaltable, IQueryColumn externalkey)> GetJoins(int maxdepth = 1, int depth = 0)
        {
            var joins = new List<(IQueryRepository internaltable, IQueryColumn internalkey, IQueryRepository externaltable, IQueryColumn externalkey)>();

            if (_empresa != null || depth < maxdepth || maxdepth == 0)
            {
                joins.Add((this, this["IdEmpresa"], Empresa(), Empresa()["Id"]));
            }

            return joins;
        }

        public Sucursal(IRepositoryQuery<data.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal> repository)
            : base(repository, 
                  typeof(entities.Model.Sucursal).GetAttributeFromType<TableAttribute>()?.Name ?? "sucursal", "Sucursal")
        {
            Columns.Add(new QueryColumn<int?>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id"));
            Columns.Add(new QueryColumn<string>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Nombre")?.Name ?? "nombre", "Nombre"));
            Columns.Add(new QueryColumn<DateTime?>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Fecha")?.Name ?? "fecha", "Fecha"));
            Columns.Add(new QueryColumn<bool?>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));

            Columns.Add(new QueryColumn<int?>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("IdEmpresa")?.Name ?? "id_empresa", "IdEmpresa"));
        }

        public Sucursal(ConnectionStringSettings connectionstringsettings,
            IReaderEntity<entities.Model.Sucursal> reader,
            IMapperRepository<entities.Model.Sucursal, data.Model.Sucursal> mapper)
            : this(new RepositoryQuery<data.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal>(reader, mapper, connectionstringsettings))
        {
        }
        public Sucursal(ConnectionStringSettings connectionstringsettings)
            : this(connectionstringsettings,
                  new entities.Reader.Sucursal(),
                  new data.Mapper.Sucursal())
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

namespace entities.Model
{
    public partial class Sucursales : ListEntity<entities.Model.Sucursal>
    {
        public Sucursales()
            : base()
        {
        }
        public Sucursales(List<entities.Model.Sucursal> list)
            : this()
        {
            List = list;
        }
    }
}

namespace entities.Reader
{
    public partial class Sucursal : BaseReaderEntity<entities.Model.Sucursal>
    {
        public Sucursal()
            : base()
        {
        }

        public override entities.Model.Sucursal CreateInstance()
        {
            return base.CreateInstance();
        }

        public override entities.Model.Sucursal Clear(entities.Model.Sucursal entity, int maxdepth = 1, int depth = 0)
        {
            entity.Id = null;
            entity.Nombre = null;
            entity.Fecha = null;
            entity.Activo = null;
            entity.IdEmpresa = null;

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                entity.Empresa = null;
            }

            return entity;
        }

        public override entities.Model.Sucursal Read(entities.Model.Sucursal entity, IDataReader reader, IList<string> prefixname, string columnseparator, int maxdepth = 1, int depth = 0)
        {
            prefixname.Add("Sucursal");

            var prefix = string.Join(columnseparator, prefixname);
            prefix += (prefix == string.Empty ? prefix : columnseparator);

            entity.Id = reader[$"{prefix}Id"] as int?;
            entity.Nombre = reader[$"{prefix}Nombre"] as string;
            entity.Fecha = reader[$"{prefix}Fecha"] as DateTime?;
            entity.Activo = reader[$"{prefix}Activo"] as bool?;

            entity.IdEmpresa = reader[$"{prefix}IdEmpresa"] as int?;

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                entity.Empresa = new entities.Reader.Empresa().Read(new entities.Model.Empresa(), reader, new List<string>(prefixname), columnseparator, maxdepth, depth);
            }

            return entity;
        }
    }
}


namespace data.Mapper
{
    public partial class Sucursal : BaseMapperRepository<entities.Model.Sucursal, data.Model.Sucursal>
    {
        public Sucursal()
            : base()
        {
        }

        public override data.Model.Sucursal Clear(data.Model.Sucursal data, int maxdepth = 1, int depth = 0)
        {
            data["Id"].DbValue = null;
            data["Nombre"].DbValue = null;
            data["Activo"].DbValue = null;
            data["Fecha"].DbValue = null;
            data["IdEmpresa"].DbValue = null;

            return data;
        }

        public override data.Model.Sucursal Map(data.Model.Sucursal data, int maxdepth = 1, int depth = 0)
        {
            data["Id"].DbValue = data.Entity.Id;
            data["Nombre"].DbValue = data.Entity.Nombre;
            data["Activo"].DbValue = data.Entity.Activo;
            data["Fecha"].DbValue = data.Entity.Fecha;
            data["IdEmpresa"].DbValue = data.Entity.IdEmpresa;

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                data.Empresa = new data.Mapper.Empresa().Map(data.Empresa, maxdepth, depth);
            }

            return data;
        }
    }
}