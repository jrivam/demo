using Library.Extension;
using Library.Impl.Persistence;
using Library.Impl.Persistence.Mapper;
using Library.Impl.Persistence.Query;
using Library.Impl.Persistence.Sql;
using Library.Impl.Persistence.Table;
using Library.Interface.Entities.Reader;
using Library.Interface.Persistence.Mapper;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace Persistence.Table
{
    public partial class Sucursal : AbstractTableData<Entities.Table.Sucursal, Persistence.Table.Sucursal>
    {
        public Sucursal(Entities.Table.Sucursal entity,
            IRepositoryTable<Entities.Table.Sucursal, Persistence.Table.Sucursal> repository)
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
            IReaderEntity<Entities.Table.Sucursal> reader, IMapperRepository<Entities.Table.Sucursal, Persistence.Table.Sucursal> mapper)
            : this(entity,
                  new RepositoryTable<Entities.Table.Sucursal, Persistence.Table.Sucursal>(reader, mapper, connectionstringsettings))
        {
        }
        public Sucursal(Entities.Table.Sucursal entity,
            string appsettingsconnectionstringname)
            : this(entity, 
                  ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appsettingsconnectionstringname]],                  
                  new Entities.Reader.Sucursal(), new Persistence.Mapper.Sucursal())
        {
        }

        public Sucursal()
            : this(new Entities.Table.Sucursal())
        {
        }

        public override IQueryData<Entities.Table.Sucursal, Persistence.Table.Sucursal> QuerySelect
        {
            get
            {
                var _query = new Persistence.Query.Sucursal();

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

        protected Persistence.Table.Empresa _empresa;
        public virtual Persistence.Table.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    if (Entity?.Empresa != null)
                    {
                        Empresa = new Persistence.Table.Empresa(Entity?.Empresa);
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

        protected Persistence.Table.Empresas _empresas;
        public virtual Persistence.Table.Empresas Empresas
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

    public partial class Sucursales : ListData<Entities.Table.Sucursal, Persistence.Table.Sucursal>
    {
        public Sucursales(Entities.Table.Sucursales entities)
            : base(entities)
        {
        }
        public Sucursales()
           : base()
        {
        }
    }
}

namespace Persistence.Query
{
    public partial class Sucursal : AbstractQueryData<Entities.Table.Sucursal, Persistence.Table.Sucursal>
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

        public Sucursal(IRepositoryQuery<Entities.Table.Sucursal, Persistence.Table.Sucursal> repository)
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
            IReaderEntity<Entities.Table.Sucursal> reader, IMapperRepository<Entities.Table.Sucursal, Persistence.Table.Sucursal> mapper)
            : this(new RepositoryQuery<Entities.Table.Sucursal, Persistence.Table.Sucursal>(reader, mapper, connectionstringsettings))
        {
        }

        public Sucursal(string appsettingsconnectionstringname)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appsettingsconnectionstringname]],
                  new Entities.Reader.Sucursal(), new Persistence.Mapper.Sucursal())
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

        protected Persistence.Query.Empresa _empresa;
        public virtual Persistence.Query.Empresa Empresa(Persistence.Query.Empresa query = null)
        {
            return _empresa = query ?? _empresa ?? new Persistence.Query.Empresa();
        }
    }
}

namespace Persistence.Mapper
{
    public partial class Sucursal : BaseMapperRepository<Entities.Table.Sucursal, Persistence.Table.Sucursal>
    {
        public Sucursal()
            : base()
        {
        }

        public override Persistence.Table.Sucursal Clear(Persistence.Table.Sucursal data, int maxdepth = 1, int depth = 0)
        {
            data["Id"].Value = data["Id"].DbValue = null;
            data["Codigo"].Value = data["Codigo"].DbValue = null;
            data["Nombre"].Value = data["Nombre"].DbValue = null;
            data["Activo"].Value = data["Activo"].DbValue = null;
            data["Fecha"].Value = data["Fecha"].DbValue = null;
            data["Fecha"].Value = data["Fecha"].DbValue = null;

            return data;
        }

        public override Persistence.Table.Sucursal Map(Persistence.Table.Sucursal data, int maxdepth = 1, int depth = 0)
        {
            data["Id"].Value = data["Id"].DbValue = data.Entity.Id;
            data["Codigo"].Value = data["Codigo"].DbValue = data.Entity.Codigo;
            data["Nombre"].Value = data["Nombre"].DbValue = data.Entity.Nombre;
            data["Activo"].Value = data["Activo"].DbValue = data.Entity.Activo;
            data["Fecha"].Value = data["Fecha"].DbValue = data.Entity.Fecha;
            data["IdEmpresa"].Value = data["IdEmpresa"].DbValue = data.Entity.IdEmpresa;

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                data.Empresa = new Persistence.Mapper.Empresa().Map(data.Empresa, maxdepth, depth);
            }

            return data;
        }
    }
}