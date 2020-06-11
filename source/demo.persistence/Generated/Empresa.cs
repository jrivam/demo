using Autofac;
using jrivam.Library;
using jrivam.Library.Impl.Persistence;
using jrivam.Library.Impl.Persistence.Attributes;
using jrivam.Library.Impl.Persistence.Query;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Persistence.Sql.Factory;
using jrivam.Library.Impl.Persistence.Table;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Sql.Builder;
using jrivam.Library.Interface.Persistence.Sql.Providers;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;

namespace demo.Persistence.Table
{
    public partial class Empresa : AbstractTableData<Entities.Table.Empresa, Persistence.Table.Empresa>
    {
        public Empresa(IRepositoryTable<Entities.Table.Empresa, Persistence.Table.Empresa> repositorytable, 
            Persistence.Query.Empresa query = null,
            Entities.Table.Empresa entity = null,
            string name = null, string dbname = null)
            : base(repositorytable,
                  query ?? new Persistence.Query.Empresa(),
                  entity ?? new Entities.Table.Empresa(),
                  name, dbname)
        {
        }

        public Empresa(ConnectionStringSettings connectionstringsettings,
            Persistence.Query.Empresa query = null,
            Entities.Table.Empresa entity = null,
            string name = null, string dbname = null)
            : this(//HelperTableRepository<Entities.Table.Empresa, Persistence.Table.Empresa>.GetRepositoryTable(connectionstringsettings),
                  AutofacConfiguration.Container.Resolve<IRepositoryTable<Entities.Table.Empresa, Persistence.Table.Empresa>>(
                        new TypedParameter(typeof(IRepository), AutofacConfiguration.Container.Resolve<IRepository>(
                                new TypedParameter(typeof(ConnectionStringSettings), connectionstringsettings))),
                        new TypedParameter(typeof(ISqlBuilderTable), AutofacConfiguration.Container.Resolve<ISqlBuilderTable>(new TypedParameter(typeof(ISqlSyntaxSign), SqlSyntaxSignFactory.Create(connectionstringsettings.ProviderName)))),
                        new TypedParameter(typeof(ISqlCommandBuilder), SqlCommandBuilderFactory.Create(connectionstringsettings.ProviderName)
                        )
                    ),
                  query,
                  entity,
                  name, dbname)
        {
        }

        [Data]
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
        [Data]
        public virtual string Ruc { get { return Entity?.Ruc; } set { if (Entity?.Ruc != value) { Columns[nameof(Ruc)].Value = Entity.Ruc = value; } } }
        [Data]
        public virtual string RazonSocial { get { return Entity?.RazonSocial; } set { if (Entity?.RazonSocial != value) { Columns[nameof(RazonSocial)].Value = Entity.RazonSocial = value; } } }
        [Data]
        public virtual bool? Activo { get { return Entity?.Activo; } set { if (Entity?.Activo != value) { Columns[nameof(Activo)].Value = Entity.Activo = value; } } }

        protected Persistence.Table.SucursalesQuery _sucursales;
        [Data]
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

namespace demo.Persistence.Query
{
    public partial class Empresa : AbstractQueryData<Entities.Table.Empresa, Persistence.Table.Empresa>
    {
        public Empresa(IRepositoryQuery<Entities.Table.Empresa, Persistence.Table.Empresa> repositoryquery,
            string name = null, string dbname = null)
            : base(repositoryquery,
                  name, dbname)
        {
        }

        public Empresa(ConnectionStringSettings connectionstringsettings,
            string name = null, string dbname = null)
            : base(AutofacConfiguration.Container.Resolve<IRepositoryQuery<Entities.Table.Empresa, Persistence.Table.Empresa>>(
                        new TypedParameter(typeof(IRepository), AutofacConfiguration.Container.Resolve<IRepository>(
                                new TypedParameter(typeof(ConnectionStringSettings), connectionstringsettings))),
                        new TypedParameter(typeof(ISqlBuilderQuery), AutofacConfiguration.Container.Resolve<ISqlBuilderQuery>(new TypedParameter(typeof(ISqlSyntaxSign), SqlSyntaxSignFactory.Create(connectionstringsettings.ProviderName)))),
                        new TypedParameter(typeof(ISqlCommandBuilder), SqlCommandBuilderFactory.Create(connectionstringsettings.ProviderName))
                    ),
                  name, dbname)
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
