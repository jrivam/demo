using Autofac;
using jrivam.Library;
using jrivam.Library.Impl.Persistence;
using jrivam.Library.Impl.Persistence.Attributes;
using jrivam.Library.Impl.Persistence.Query;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Persistence.Table;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace demo.Persistence.Table
{
    public partial class Empresa : AbstractTableData<Entities.Table.Empresa, Persistence.Table.Empresa>
    {
        public Empresa(IRepositoryTableAsync<Entities.Table.Empresa, Persistence.Table.Empresa> repositorytable,
            Persistence.Query.Empresa query,
            Entities.Table.Empresa entity = null,
            string name = null, string dbname = null)
            : base(repositorytable,
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

        protected Persistence.Table.SucursalesReload _sucursales;
        [Data]
        public virtual Persistence.Table.SucursalesReload Sucursales
        {
            get
            {
                if (_sucursales == null)
                {
                    Sucursales = AutofacConfiguration.Container.Resolve<Persistence.Table.SucursalesReload>(new TypedParameter(typeof(ICollection<Entities.Table.Sucursal>), Entity?.Sucursales));
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

    public partial class EmpresasEdit : ListDataEdit<Entities.Table.Empresa, Persistence.Table.Empresa>
    {
        public EmpresasEdit(ICollection<Entities.Table.Empresa> entities = null)
            : base(entities ?? new Collection<Entities.Table.Empresa>())
        {
        }
    }

    public partial class EmpresasReload : ListDataReload<Persistence.Query.Empresa, Entities.Table.Empresa, Persistence.Table.Empresa>
    {
        public EmpresasReload(Persistence.Query.Empresa query,
            ICollection<Entities.Table.Empresa> entities = null, 
           int maxdepth = 1)
           : base(query, 
                 entities ?? new Collection<Entities.Table.Empresa>(),                  
                maxdepth)
        {
        }
    }
}

namespace demo.Persistence.Query
{
    public partial class Empresa : AbstractQueryData<Entities.Table.Empresa, Persistence.Table.Empresa>
    {
        public Empresa(IRepositoryQueryAsync<Entities.Table.Empresa, Persistence.Table.Empresa> repositoryquery,
            string name = null, string dbname = null)
            : base(repositoryquery,
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
        public virtual Persistence.Query.Sucursal Sucursal
        {
            get
            {
                if (_sucursal == null)
                {
                    Sucursal = AutofacConfiguration.Container.Resolve<Persistence.Query.Sucursal>();
                }

                return _sucursal;
            }
            set
            {
                if (_sucursal != value)
                {
                    _sucursal = value;
                }
            }
        }
    }
}
