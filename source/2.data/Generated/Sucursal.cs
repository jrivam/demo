using Library.Extension;
using Library.Impl.Data;
using Library.Impl.Data.Mapper;
using Library.Impl.Data.Query;
using Library.Impl.Data.Sql;
using Library.Impl.Data.Table;
using Library.Interface.Data.Mapper;
using Library.Interface.Data.Query;
using Library.Interface.Data.Table;
using Library.Interface.Entities.Reader;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace data.Model
{
    public partial class Sucursal : AbstractTableData<Entities.Table.Sucursal, data.Model.Sucursal>
    {
        public Sucursal(Entities.Table.Sucursal entity,
            IRepositoryTable<Entities.Table.Sucursal, data.Model.Sucursal> repository)
            : base(entity, 
                  repository, 
                  typeof(Entities.Table.Sucursal).GetAttributeFromType<TableAttribute>()?.Name ?? "sucursal", "Sucursal")
        {
            Columns.Add(new ColumnTable<int?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id", isprimarykey: true, isidentity: true));
            Columns.Add(new ColumnTable<string>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Codigo")?.Name ?? "codigo", "Codigo", isunique: true));
            Columns.Add(new ColumnTable<string>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Nombre")?.Name ?? "nombre", "Nombre"));
            Columns.Add(new ColumnTable<DateTime?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Fecha")?.Name ?? "fecha", "Fecha"));
            Columns.Add(new ColumnTable<bool?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));

            Columns.Add(new ColumnTable<int?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("IdEmpresa")?.Name ?? "id_empresa", "IdEmpresa"));
        }

        public Sucursal(Entities.Table.Sucursal entity,
            ConnectionStringSettings connectionstringsettings,
            IReaderEntity<Entities.Table.Sucursal> reader, IMapperRepository<Entities.Table.Sucursal, data.Model.Sucursal> mapper)
            : this(entity,
                  new RepositoryTable<Entities.Table.Sucursal, data.Model.Sucursal>(reader, mapper, connectionstringsettings))
        {
        }
        public Sucursal(Entities.Table.Sucursal entity,
            string appsettingsconnectionstringname)
            : this(entity, 
                  ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appsettingsconnectionstringname]],                  
                  new Entities.Reader.Sucursal(), new data.Mapper.Sucursal())
        {
        }

        public Sucursal()
            : this(new Entities.Table.Sucursal())
        {
        }

        public override IQueryData<Entities.Table.Sucursal, data.Model.Sucursal> QuerySelect
        {
            get
            {
                var _query = new data.Query.Sucursal();

                _query.Id = (this.Id, WhereOperator.Equals);

                return _query;
            }
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
                    Columns["Id"].Value = Entity.Id = value;
                }
            }
        }
        public virtual string Codigo { get { return Entity?.Codigo; } set { if (Entity?.Codigo != value) { Columns["Codigo"].Value = Entity.Codigo = value; } } }
        public virtual string Nombre { get { return Entity?.Nombre; } set { if (Entity?.Nombre != value) { Columns["Nombre"].Value = Entity.Nombre = value; } } }
        public virtual DateTime? Fecha { get { return Entity?.Fecha; } set { if (Entity?.Fecha != value) { Columns["Fecha"].Value = Entity.Fecha = value; } } }
        public virtual bool? Activo { get { return Entity?.Activo; } set { if (Entity?.Activo != value) { Columns["Activo"].Value = Entity.Activo = value; } } }

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
                    Columns["IdEmpresa"].Value = Entity.IdEmpresa = value;

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

    public partial class Sucursales : ListData<Entities.Table.Sucursal, data.Model.Sucursal>
    {
        public Sucursales()
           : base()
        {
        }
        public Sucursales(Entities.Table.Sucursales entities)
            : base(entities)
        {
        }
    }
}

namespace data.Query
{
    public partial class Sucursal : AbstractQueryData<Entities.Table.Sucursal, data.Model.Sucursal>
    {
        public override IList<(IColumnQuery internalkey, IColumnQuery externalkey)> GetJoins(int maxdepth = 1, int depth = 0)
        {
            var joins = new List<(IColumnQuery internalkey, IColumnQuery externalkey)>();

            if (_empresa != null || depth < maxdepth || maxdepth == 0)
            {
                joins.Add((Columns["IdEmpresa"], Empresa()["Id"]));
            }

            return joins;
        }

        public Sucursal(IRepositoryQuery<Entities.Table.Sucursal, data.Model.Sucursal> repository)
            : base(repository, 
                  typeof(Entities.Table.Sucursal).GetAttributeFromType<TableAttribute>()?.Name ?? "sucursal", "Sucursal")
        {
            Columns.Add(new ColumnQuery<int?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id"));
            Columns.Add(new ColumnQuery<string>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Codigo")?.Name ?? "codigo", "Codigo"));
            Columns.Add(new ColumnQuery<string>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Nombre")?.Name ?? "nombre", "Nombre"));
            Columns.Add(new ColumnQuery<DateTime?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Fecha")?.Name ?? "fecha", "Fecha"));
            Columns.Add(new ColumnQuery<bool?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));

            Columns.Add(new ColumnQuery<int?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("IdEmpresa")?.Name ?? "id_empresa", "IdEmpresa"));
        }

        public Sucursal(ConnectionStringSettings connectionstringsettings,
            IReaderEntity<Entities.Table.Sucursal> reader, IMapperRepository<Entities.Table.Sucursal, data.Model.Sucursal> mapper)
            : this(new RepositoryQuery<Entities.Table.Sucursal, data.Model.Sucursal>(reader, mapper, connectionstringsettings))
        {
        }

        public Sucursal(string appsettingsconnectionstringname)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appsettingsconnectionstringname]],
                  new Entities.Reader.Sucursal(), new data.Mapper.Sucursal())
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                Columns["Id"].Where(value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Codigo
        {
            set
            {
                Columns["Codigo"].Where(value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Nombre
        {
            set
            {
                Columns["Nombre"].Where(value.value, value.sign);
            }
        }
        public virtual (DateTime? value, WhereOperator? sign) Fecha
        {
            set
            {
                Columns["Fecha"].Where(value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                Columns["Activo"].Where(value.value, value.sign);
            }
        }

        public virtual (int? value, WhereOperator? sign) IdEmpresa
        {
            set
            {
                Columns["IdEmpresa"].Where(value.value, value.sign);
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
    public partial class Sucursal : BaseMapperRepository<Entities.Table.Sucursal, data.Model.Sucursal>
    {
        public Sucursal()
            : base()
        {
        }

        public override data.Model.Sucursal Clear(data.Model.Sucursal data, int maxdepth = 1, int depth = 0)
        {
            data["Id"].DbValue = null;
            data["Codigo"].DbValue = null;
            data["Nombre"].DbValue = null;
            data["Activo"].DbValue = null;
            data["Fecha"].DbValue = null;
            data["IdEmpresa"].DbValue = null;

            return data;
        }

        public override data.Model.Sucursal Map(data.Model.Sucursal data, int maxdepth = 1, int depth = 0)
        {
            data["Id"].DbValue = data.Entity.Id;
            data["Codigo"].DbValue = data.Entity.Codigo;
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