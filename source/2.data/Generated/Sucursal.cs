using library.Extension;
using library.Impl;
using library.Impl.Data;
using library.Impl.Data.Mapper;
using library.Impl.Data.Query;
using library.Impl.Data.Sql;
using library.Impl.Data.Table;
using library.Interface.Data.Mapper;
using library.Interface.Data.Query;
using library.Interface.Data.Table;
using library.Interface.Entities.Reader;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace data.Model
{
    public partial class Sucursal : AbstractTableRepositoryMethods<entities.Model.Sucursal, data.Model.Sucursal>
    {
        public Sucursal(entities.Model.Sucursal entity,
            IRepositoryTable<entities.Model.Sucursal, data.Model.Sucursal> repository)
            : base(entity, 
                  repository, 
                  typeof(entities.Model.Sucursal).GetAttributeFromType<TableAttribute>()?.Name ?? "sucursal", "Sucursal")
        {
            Columns.Add(new TableColumn<int?>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id", true, true));
            Columns.Add(new TableColumn<string>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Nombre")?.Name ?? "nombre", "Nombre"));
            Columns.Add(new TableColumn<DateTime?>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Fecha")?.Name ?? "fecha", "Fecha"));
            Columns.Add(new TableColumn<bool?>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));

            Columns.Add(new TableColumn<int?>(typeof(entities.Model.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("IdEmpresa")?.Name ?? "id_empresa", "IdEmpresa"));
        }

        public Sucursal(ConnectionStringSettings connectionstringsettings,
            entities.Model.Sucursal entity,
            IReaderEntity<entities.Model.Sucursal> reader,
            IMapperRepository<entities.Model.Sucursal, data.Model.Sucursal> mapper)
            : this(entity,
                  new RepositoryTable<entities.Model.Sucursal, data.Model.Sucursal>(reader, mapper, connectionstringsettings))
        {
        }
        public Sucursal(ConnectionStringSettings connectionstringsettings,
            entities.Model.Sucursal entity)
            : this(connectionstringsettings,
                  entity,
                  new entities.Reader.Sucursal(),
                  new data.Mapper.Sucursal())
        {
        }

        public Sucursal(string appsettingsconnectionstringname, 
            entities.Model.Sucursal entity)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appsettingsconnectionstringname]], 
                  entity)
        {
            //SetProperties(entity, true);
        }

        public Sucursal()
            : this(new entities.Model.Sucursal())
        {
        }

        public override (Result result, data.Model.Sucursal data) SelectQuery(int maxdepth = 1, IQueryRepositoryMethods<entities.Model.Sucursal, data.Model.Sucursal> query = null)
        {
            var _query = (data.Query.Sucursal)query ?? new data.Query.Sucursal();

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

        protected data.Model.Empresa _empresa;
        public virtual data.Model.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    if (Entity?.Empresa != null)
                    {
                        Empresa = new data.Model.Empresa(Entity?.Empresa);
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

                    Entity.Empresa = _empresa?.Entity;
                }
            }
        }

        protected data.Model.Empresas _empresas;
        public virtual data.Model.Empresas Empresas
        {
            get
            {
                return _empresas ?? Empresas_Refresh().datas;
            }
            set
            {
                if (_empresas != value)
                {
                    _empresas = value;
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
        public Sucursales(entities.Model.Sucursales entities)
            : base(entities)
        {
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

        public Sucursal(string appsettingsconnectionstringname)
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