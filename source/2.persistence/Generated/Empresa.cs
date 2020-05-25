using Library.Extension;
using Library.Impl.Persistence;
using Library.Impl.Persistence.Mapper;
using Library.Impl.Persistence.Query;
using Library.Impl.Persistence.Sql;
using Library.Impl.Persistence.Sql.Factory;
using Library.Impl.Persistence.Table;
using Library.Interface.Persistence.Query;
using Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace Persistence.Table
{
    public partial class Empresa : AbstractTableData<Entities.Table.Empresa, Persistence.Table.Empresa>
    {
        public Empresa(IRepositoryTable<Entities.Table.Empresa, Persistence.Table.Empresa> repository,
            Entities.Table.Empresa entity = null, 
            IQueryData<Entities.Table.Empresa, Persistence.Table.Empresa> query = null)
            : base(repository,
                  query ?? new Persistence.Query.Empresa(),
                  entity ?? new Entities.Table.Empresa(),                  
                  nameof(Empresa), typeof(Entities.Table.Empresa).GetAttributeFromType<TableAttribute>()?.Name ?? nameof(Empresa).ToUnderscoreCase().ToLower())
        {
        }

        public Empresa(ConnectionStringSettings connectionstringsettings,
            Entities.Table.Empresa entity = null)
            : this(new RepositoryTable<Entities.Table.Empresa, Persistence.Table.Empresa>(new Entities.Reader.Empresa(SqlSyntaxSignFactory.Create(connectionstringsettings)), new Persistence.Mapper.Empresa(), connectionstringsettings),
                  entity ?? new Entities.Table.Empresa())
        {
        }
        public Empresa(string appsettingsconnectionstringname,
            Entities.Table.Empresa entity = null)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appsettingsconnectionstringname]],
                  entity ?? new Entities.Table.Empresa())
        {
        }

        public virtual int? Id
        {
            get { return Entity?.Id; }
            set
            {
                if (Entity?.Id != value)
                {
                    Columns[nameof(Id)].Value = Entity.Id = value;

                    Sucursales?.ForEach(x => x.IdEmpresa = value);
                }
            }
        }
        public virtual string Ruc { get { return Entity?.Ruc; } set { if (Entity?.Ruc != value) { Columns[nameof(Ruc)].Value = Entity.Ruc = value; } } }
        public virtual string RazonSocial { get { return Entity?.RazonSocial; } set { if (Entity?.RazonSocial != value) { Columns[nameof(RazonSocial)].Value = Entity.RazonSocial = value; } } }
        public virtual bool? Activo { get { return Entity?.Activo; } set { if (Entity?.Activo != value) { Columns[nameof(Activo)].Value = Entity.Activo = value; } } }

        protected Persistence.Table.SucursalesQuery _sucursales;
        public virtual Persistence.Table.SucursalesQuery Sucursales
        {
            get
            {
                if (_sucursales == null)
                {
                    if (Entity?.Sucursales == null)
                    {
                        Entity.Sucursales = new Collection<Entities.Table.Sucursal>();
                    }
                    Sucursales = new SucursalesQuery(Entity?.Sucursales);
                }

                _sucursales.Query.IdEmpresa = (this.Id, WhereOperator.Equals);

                return _sucursales;
            }
            set
            {
                if (_sucursales != value)
                {
                    _sucursales = value;
                }
            }
        }
    }

    public partial class Empresas : ListData<Entities.Table.Empresa, Persistence.Table.Empresa>
    {
        public Empresas(ICollection<Entities.Table.Empresa> entities = null)
            : base(entities ?? new Collection<Entities.Table.Empresa>())
        {
        }
    }

    public partial class EmpresasQuery : ListDataQuery<Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa>
    {
        public EmpresasQuery(ICollection<Entities.Table.Empresa> entities = null, 
            Persistence.Query.Empresa query = null, 
           int maxdepth = 1)
           : base(entities ?? new Collection<Entities.Table.Empresa>(), 
                 query ?? new Persistence.Query.Empresa(), 
                maxdepth)
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
                  nameof(Empresa), typeof(Entities.Table.Empresa).GetAttributeFromType<TableAttribute>()?.Name ?? nameof(Empresa).ToUnderscoreCase().ToLower())
        {
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
                Columns[nameof(Id)].Where(value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Ruc
        {
            set
            {
                Columns[nameof(Ruc)].Where(value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) RazonSocial
        {
            set
            {
                Columns[nameof(RazonSocial)].Where(value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                Columns[nameof(Activo)].Where(value.value, value.sign);
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
        public override void Map(Persistence.Table.Empresa data, int maxdepth = 1, int depth = 0)
        {
            base.Map(data, maxdepth, depth);
        }
    }
}
