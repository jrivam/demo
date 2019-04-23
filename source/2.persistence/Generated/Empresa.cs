using Library.Extension;
using Library.Impl.Persistence;
using Library.Impl.Persistence.Mapper;
using Library.Impl.Persistence.Query;
using Library.Impl.Persistence.Sql;
using Library.Impl.Persistence.Sql.Factory;
using Library.Impl.Persistence.Table;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;

namespace Persistence.Table
{
    public partial class Empresa : AbstractTableData<Entities.Table.Empresa, Persistence.Table.Empresa>
    {
        public Empresa(Entities.Table.Empresa entity,
            IQueryData<Entities.Table.Empresa, Persistence.Table.Empresa> query,
            IRepositoryTable<Entities.Table.Empresa, Persistence.Table.Empresa> repository)
            : base(entity,
                  query,
                  repository,
                  typeof(Entities.Table.Empresa).GetAttributeFromType<TableAttribute>()?.Name ?? "empresa", "Empresa")
        {
            Columns.Add(new ColumnTable<int?>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id", isprimarykey: true, isidentity: true));
            Columns.Add(new ColumnTable<string>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Ruc")?.Name ?? "ruc", "Ruc", isunique: true));
            Columns.Add(new ColumnTable<string>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("RazonSocial")?.Name ?? "razon_social", "RazonSocial"));
            Columns.Add(new ColumnTable<bool?>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));
        }

        public Empresa(Entities.Table.Empresa entity,
            ConnectionStringSettings connectionstringsettings)
            : this(entity,
                    new Persistence.Query.Empresa(),
                    new RepositoryTable<Entities.Table.Empresa, Persistence.Table.Empresa>(new Entities.Reader.Empresa(SqlSyntaxSignFactory.Create(connectionstringsettings)), new Persistence.Mapper.Empresa(), connectionstringsettings))
        {
        }
        public Empresa(Entities.Table.Empresa entity,
            string appsettingsconnectionstringname)
            : this(entity,
                  ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appsettingsconnectionstringname]])
        {
        }

        public Empresa()
            : this(new Entities.Table.Empresa())
        {
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

        protected Persistence.Table.Sucursales _sucursales;
        public virtual Persistence.Table.Sucursales Sucursales
        {
            get
            {
                if (_sucursales == null)
                {
                    if (Entity?.Sucursales != null)
                    {
                        Sucursales = new Persistence.Table.Sucursales(new Entities.Table.Sucursales(Entity?.Sucursales?.ToList()));
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

    public partial class Empresas : ListData<Entities.Table.Empresa, Persistence.Table.Empresa>
    {
        public Empresas(Entities.Table.Empresas entities)
            : base(entities)
        {
        }
        public Empresas()
            : base()
        {
        }
    }
}

namespace Persistence.Query
{
    public partial class Empresa : AbstractQueryData<Entities.Table.Empresa, Persistence.Table.Empresa>
    {
        public Empresa(IRepositoryQuery<Entities.Table.Empresa, Persistence.Table.Empresa> repository)
            : base(repository, 
                  typeof(Entities.Table.Empresa).GetAttributeFromType<TableAttribute>()?.Name ?? "empresa", "Empresa")
        {
            Columns.Add(new ColumnQuery<int?>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id"));
            Columns.Add(new ColumnQuery<string>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Ruc")?.Name ?? "ruc", "Ruc"));
            Columns.Add(new ColumnQuery<string>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("RazonSocial")?.Name ?? "razon_social", "RazonSocial"));
            Columns.Add(new ColumnQuery<bool?>(this, typeof(Entities.Table.Empresa).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));
        }

        public Empresa(ConnectionStringSettings connectionstringsettings)
            : this(new RepositoryQuery<Entities.Table.Empresa, Persistence.Table.Empresa>(new Entities.Reader.Empresa(SqlSyntaxSignFactory.Create(connectionstringsettings)), new Persistence.Mapper.Empresa(), connectionstringsettings))
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

        protected Persistence.Query.Sucursal _sucursal;
        public virtual Persistence.Query.Sucursal Sucursal(Persistence.Query.Sucursal query = null)
        {
            return _sucursal = query ?? _sucursal ?? new Persistence.Query.Sucursal();
        }
    }
}

namespace Persistence.Mapper
{
    public partial class Empresa : BaseMapper<Entities.Table.Empresa, Persistence.Table.Empresa>
    {
        public Empresa()
            : base()
        {
        }

        public override Persistence.Table.Empresa Clear(Persistence.Table.Empresa data)
        {
            data = base.Clear(data);

            return data;
        }
        public override Persistence.Table.Empresa Map(Persistence.Table.Empresa data, int maxdepth = 1, int depth = 0)
        {
            data = base.Map(data, maxdepth, depth);

            return data;
        }
    }
}
