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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Linq;

namespace data.Model
{
    public partial class Empresa : AbstractTableRepositoryMethods<entities.Model.Empresa, data.Model.Empresa>
    {
        public Empresa(entities.Model.Empresa entity,
            IRepositoryTable<entities.Model.Empresa, data.Model.Empresa> repository)
            : base(entity, 
                  repository, 
                  typeof(entities.Model.Empresa).GetAttributeFromType<TableAttribute>()?.Name ?? "empresa", "Empresa")
        {
            Columns.Add(new TableColumn<int?>(typeof(entities.Model.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id", true, true));
            Columns.Add(new TableColumn<string>(typeof(entities.Model.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("RazonSocial")?.Name ?? "razon_social", "RazonSocial"));
            Columns.Add(new TableColumn<bool?>(typeof(entities.Model.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));
        }

        public Empresa(ConnectionStringSettings connectionstringsettings,
            entities.Model.Empresa entity,
            IReaderEntity<entities.Model.Empresa> reader,
            IMapperRepository<entities.Model.Empresa, data.Model.Empresa> mapper)
            : this(entity,
                  new RepositoryTable<entities.Model.Empresa, data.Model.Empresa>(reader, mapper, connectionstringsettings))
        {
        }
        public Empresa(ConnectionStringSettings connectionstringsettings, 
            entities.Model.Empresa entity)
            : this(connectionstringsettings,
                  entity,
                  new entities.Reader.Empresa(),
                  new data.Mapper.Empresa())
        {
        }

        public Empresa(string appsettingsconnectionstringname,
            entities.Model.Empresa entity)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appsettingsconnectionstringname]],
                entity)
        {
            //SetProperties(entity, true);
        }

        public Empresa()
            : this(new entities.Model.Empresa())
        {
        }

        public override (Result result, data.Model.Empresa data) SelectQuery(int maxdepth = 1, IQueryRepositoryMethods<entities.Model.Empresa, data.Model.Empresa> query = null)
        {
            var _query = (data.Query.Empresa)query ?? new data.Query.Empresa();

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
                Sucursales = new data.Model.Sucursales(new entities.Model.Sucursales(Entity?.Sucursales?.ToList()));
            }
            else
            {
                if (this.Id != null)
                {
                    var _query = query ?? new data.Query.Empresa();

                    _query.Sucursal().IdEmpresa = (this.Id, WhereOperator.Equals);

                    Sucursales = (data.Model.Sucursales)new data.Model.Sucursales().Load(_query?.Sucursal(), maxdepth, top).list;
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

    public partial class Empresas : ListData<data.Query.Empresa, entities.Model.Empresa, data.Model.Empresa>
    {
        public Empresas(ListEntity<entities.Model.Empresa> entities)
            : base(entities)
        {
        }
        public Empresas()
            : this(new entities.Model.Empresas())
        {
        }
    }
}

namespace data.Query
{
    public partial class Empresa : AbstractQueryRepositoryMethods<data.Query.Empresa, entities.Model.Empresa, data.Model.Empresa>
    {
        public Empresa(IRepositoryQuery<data.Query.Empresa, entities.Model.Empresa, data.Model.Empresa> repository)
            : base(repository, 
                  typeof(entities.Model.Empresa).GetAttributeFromType<TableAttribute>()?.Name ?? "empresa", "Empresa")
        {
            Columns.Add(new QueryColumn<int?>(typeof(entities.Model.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id"));
            Columns.Add(new QueryColumn<string>(typeof(entities.Model.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("RazonSocial")?.Name ?? "razon_social", "RazonSocial"));
            Columns.Add(new QueryColumn<bool?>(typeof(entities.Model.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));
        }

        public Empresa(ConnectionStringSettings connectionstringsettings,
            IReaderEntity<entities.Model.Empresa> reader,
            IMapperRepository<entities.Model.Empresa, data.Model.Empresa> mapper)
            : this(new RepositoryQuery<data.Query.Empresa, entities.Model.Empresa, data.Model.Empresa>(reader, mapper, connectionstringsettings))
        {
        }
        public Empresa(ConnectionStringSettings connectionstringsettings)
            : this(connectionstringsettings,
                  new entities.Reader.Empresa(),
                  new data.Mapper.Empresa())
        {
        }

        public Empresa(string appsettingsconnectionstringname)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appsettingsconnectionstringname]])
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

namespace entities.Model
{
    public partial class Empresas : ListEntity<entities.Model.Empresa>
    {
        public Empresas()
            : base()
        {
        }
        public Empresas(List<entities.Model.Empresa> entities)
            : base(entities)
        {
        }
    }
}

namespace entities.Reader
{
    public partial class Empresa : BaseReaderEntity<entities.Model.Empresa>
    {
        public Empresa()
            : base()
        {
        }

        public override entities.Model.Empresa Clear(entities.Model.Empresa entity, int maxdepth = 1, int depth = 0)
        {
            entity.Id = null;
            entity.RazonSocial = null;
            entity.Activo = null;

            entity.Sucursales = null;

            return entity;
        }

        public override entities.Model.Empresa Read(entities.Model.Empresa entity, IDataReader reader, IList<string> prefixname, string columnseparator, int maxdepth = 1, int depth = 0)
        {
            prefixname.Add("Empresa");

            var prefix = string.Join(columnseparator, prefixname);
            prefix += (prefix == string.Empty ? prefix : columnseparator);

            entity.Id = reader[$"{prefix}Id"] as int?;
            entity.RazonSocial = reader[$"{prefix}RazonSocial"] as string;
            entity.Activo = reader[$"{prefix}Activo"] as bool?;

            return entity;
        }
    }
}

namespace data.Mapper
{
    public partial class Empresa : BaseMapperRepository<entities.Model.Empresa, data.Model.Empresa>
    {
        public Empresa()
            : base()
        {
        }

        public override data.Model.Empresa Clear(data.Model.Empresa data, int maxdepth = 1, int depth = 0)
        {
            data["Id"].DbValue = null;
            data["RazonSocial"].DbValue = null;
            data["Activo"].DbValue = null;

            return data;
        }
        public override data.Model.Empresa Map(data.Model.Empresa data, int maxdepth = 1, int depth = 0)
        {
            data["Id"].DbValue = data.Entity.Id;
            data["RazonSocial"].DbValue = data.Entity.RazonSocial;
            data["Activo"].DbValue = data.Entity.Activo;

            return data;
        }
    }
}
