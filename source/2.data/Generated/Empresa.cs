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
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;

namespace data.Model
{
    public partial class Empresa : AbstractTableData<Entities.Table.Empresa, data.Model.Empresa>
    {
        public Empresa(Entities.Table.Empresa entity,
            IRepositoryTable<Entities.Table.Empresa, data.Model.Empresa> repository)
            : base(entity,
                  repository,
                  typeof(Entities.Table.Empresa).GetAttributeFromType<TableAttribute>()?.Name ?? "empresa", "Empresa")
        {
            Columns.Add(new ColumnTable<int?>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id", isprimarykey: true, isidentity: true));
            Columns.Add(new ColumnTable<string>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Ruc")?.Name ?? "ruc", "Ruc", isunique: true));
            Columns.Add(new ColumnTable<string>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("RazonSocial")?.Name ?? "razon_social", "RazonSocial"));
            Columns.Add(new ColumnTable<bool?>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));
        }

        public Empresa(Entities.Table.Empresa entity,
            ConnectionStringSettings connectionstringsettings,
            IReaderEntity<Entities.Table.Empresa> reader, IMapperRepository<Entities.Table.Empresa, data.Model.Empresa> mapper)
            : this(entity,
                  new RepositoryTable<Entities.Table.Empresa, data.Model.Empresa>(reader, mapper, connectionstringsettings))
        {
        }
        public Empresa(Entities.Table.Empresa entity,
            string appsettingsconnectionstringname)
            : this(entity,
                  ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appsettingsconnectionstringname]],
                  new Entities.Reader.Empresa(), new data.Mapper.Empresa())
        {
        }

        public Empresa()
            : this(new Entities.Table.Empresa())
        {
        }

        public override IQueryData<Entities.Table.Empresa, data.Model.Empresa> QuerySelect
        {
            get
            {
                var _query = new data.Query.Empresa();

                _query.Id = (this.Id, WhereOperator.Equals);

                return _query;
            }
        }

        public virtual int? Id
        {
            get { return Entity?.Id; }
            set
            {
                if (Entity?.Id != value)
                {
                    Columns["Id"].Value = Entity.Id = value;

                    _sucursales?.ForEach(x => x.IdEmpresa = value);
                }
            }
        }
        public virtual string Ruc { get { return Entity?.Ruc; } set { if (Entity?.Ruc != value) { Columns["Ruc"].Value = Entity.Ruc = value; } } }
        public virtual string RazonSocial { get { return Entity?.RazonSocial; } set { if (Entity?.RazonSocial != value) { Columns["RazonSocial"].Value = Entity.RazonSocial = value; } } }
        public virtual bool? Activo { get { return Entity?.Activo; } set { if (Entity?.Activo != value) { Columns["Activo"].Value = Entity.Activo = value; } } }

        protected data.Model.Sucursales _sucursales;
        public virtual data.Model.Sucursales Sucursales
        {
            get
            {
                if (_sucursales == null)
                {
                    if (Entity?.Sucursales != null)
                    {
                        Sucursales = new data.Model.Sucursales(new Entities.Table.Sucursales(Entity?.Sucursales?.ToList()));
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

    public partial class Empresas : ListData<Entities.Table.Empresa, data.Model.Empresa>
    {
        public Empresas()
           : base()
        {
        }
        public Empresas(Entities.Table.Empresas entities)
            : base(entities)
        {
        }
    }
}

namespace data.Query
{
    public partial class Empresa : AbstractQueryData<Entities.Table.Empresa, data.Model.Empresa>
    {
        public Empresa(IRepositoryQuery<Entities.Table.Empresa, data.Model.Empresa> repository)
            : base(repository, 
                  typeof(Entities.Table.Empresa).GetAttributeFromType<TableAttribute>()?.Name ?? "empresa", "Empresa")
        {
            Columns.Add(new ColumnQuery<int?>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id"));
            Columns.Add(new ColumnQuery<string>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Ruc")?.Name ?? "ruc", "Ruc"));
            Columns.Add(new ColumnQuery<string>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("RazonSocial")?.Name ?? "razon_social", "RazonSocial"));
            Columns.Add(new ColumnQuery<bool?>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));
        }

        public Empresa(ConnectionStringSettings connectionstringsettings,
            IReaderEntity<Entities.Table.Empresa> reader, IMapperRepository<Entities.Table.Empresa, data.Model.Empresa> mapper)
            : this(new RepositoryQuery<Entities.Table.Empresa, data.Model.Empresa>(reader, mapper, connectionstringsettings))
        {
        }

        public Empresa(string appsettingsconnectionstringname)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appsettingsconnectionstringname]],
                  new Entities.Reader.Empresa(), new data.Mapper.Empresa())
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                Columns["Id"].Where(value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Ruc
        {
            set
            {
                Columns["Ruc"].Where(value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) RazonSocial
        {
            set
            {
                Columns["RazonSocial"].Where(value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                Columns["Activo"].Where(value.value, value.sign);
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
    public partial class Empresa : BaseMapperRepository<Entities.Table.Empresa, data.Model.Empresa>
    {
        public Empresa()
            : base()
        {
        }

        public override data.Model.Empresa Clear(data.Model.Empresa data, int maxdepth = 1, int depth = 0)
        {
            data["Id"].DbValue = null;
            data["Ruc"].DbValue = null;
            data["RazonSocial"].DbValue = null;
            data["Activo"].DbValue = null;

            return data;
        }
        public override data.Model.Empresa Map(data.Model.Empresa data, int maxdepth = 1, int depth = 0)
        {
            data["Id"].DbValue = data.Entity.Id;
            data["Ruc"].DbValue = data.Entity.Ruc;
            data["RazonSocial"].DbValue = data.Entity.RazonSocial;
            data["Activo"].DbValue = data.Entity.Activo;

            return data;
        }
    }
}
