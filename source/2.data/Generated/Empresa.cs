﻿using library.Extension;
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
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
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

        protected data.Model.Sucursales _sucursales;
        public virtual data.Model.Sucursales Sucursales
        {
            get
            {
                if (_sucursales == null)
                {
                    if (Entity?.Sucursales != null)
                    {
                        Sucursales = new data.Model.Sucursales(new entities.Model.Sucursales(Entity?.Sucursales?.ToList()));
                    }
                    else
                    {
                        Sucursales_Refresh();
                    }
                }

                return _sucursales;
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
        public Empresas()
           : base()
        {
        }
        public Empresas(entities.Model.Empresas entities)
            : base(entities)
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
