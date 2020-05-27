using jrivam.Library.Impl.Persistence;
using jrivam.Library.Impl.Persistence.Attributes;
using jrivam.Library.Impl.Persistence.Mapper;
using jrivam.Library.Impl.Persistence.Query;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Persistence.Sql.Factory;
using jrivam.Library.Impl.Persistence.Table;
using jrivam.Library.Interface.Persistence.Query;
using jrivam.Library.Interface.Persistence.Table;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;

namespace demo.Persistence.Table
{
    public partial class Sucursal : AbstractTableData<Entities.Table.Sucursal, Persistence.Table.Sucursal>
    {
        public Sucursal(IRepositoryTable<Entities.Table.Sucursal, Persistence.Table.Sucursal> repository,
            Entities.Table.Sucursal entity = null,
            IQueryData<Entities.Table.Sucursal, Persistence.Table.Sucursal> query = null)
            : base(repository,
                  query ?? new Persistence.Query.Sucursal(),
                  entity ?? new Entities.Table.Sucursal())
        {
        }

        public Sucursal(ConnectionStringSettings connectionstringsettings,
            Entities.Table.Sucursal entity = null)
            : this(new RepositoryTable<Entities.Table.Sucursal, Persistence.Table.Sucursal>(new Entities.Reader.Sucursal(SqlSyntaxSignFactory.Create(connectionstringsettings)), new Persistence.Mapper.Sucursal(), connectionstringsettings),
                  entity ?? new Entities.Table.Sucursal())
        {
        }
        public Sucursal(string appsettingsconnectionstringname,
            Entities.Table.Sucursal entity = null)
            : this(ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appsettingsconnectionstringname]],
                  entity ?? new Entities.Table.Sucursal())
        {
        }

        [Data]
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
                    Columns[nameof(Id)].Value = Entity.Id = value;
                }
            }
        }
        [Data]
        public virtual string Codigo { get { return Entity?.Codigo; } set { if (Entity?.Codigo != value) { Columns[nameof(Codigo)].Value = Entity.Codigo = value; } } }
        [Data]
        public virtual string Nombre { get { return Entity?.Nombre; } set { if (Entity?.Nombre != value) { Columns[nameof(Nombre)].Value = Entity.Nombre = value; } } }
        [Data]
        public virtual DateTime? Fecha { get { return Entity?.Fecha; } set { if (Entity?.Fecha != value) { Columns[nameof(Fecha)].Value = Entity.Fecha = value; } } }
        [Data]
        public virtual bool? Activo { get { return Entity?.Activo; } set { if (Entity?.Activo != value) { Columns[nameof(Activo)].Value = Entity.Activo = value; } } }
        
        [Data]
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
                    Columns[nameof(IdEmpresa)].Value = Entity.IdEmpresa = value;

                    Entity.Empresa = null;
                    Empresa = null;
                }
            }
        }
        protected Persistence.Table.Empresa _empresa;
        [Data]
        public virtual Persistence.Table.Empresa Empresa
        {
            get
            {
                if (_empresa?.Id != this.IdEmpresa)
                {
                    if (Entity?.Empresa == null)
                    {
                        Entity.Empresa = new Entities.Table.Empresa();
                    }
                    Empresa = new Persistence.Table.Empresa(Entity?.Empresa);
                    //Empresa = new Persistence.Table.Empresa(Entity?.Empresa ??= new Entities.Table.Empresa());

                    if (_empresa.Id == null && this.IdEmpresa != null)
                    {
                        _empresa.Id = this.IdEmpresa;
                        _empresa.Select();
                    }
                }
                else
                {
                    _empresa.Id = this.IdEmpresa;
                }

                return _empresa;
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;
                }
            }
        }
    }

    public partial class Sucursales : ListData<Entities.Table.Sucursal, Persistence.Table.Sucursal>
    {
        public Sucursales(ICollection<Entities.Table.Sucursal> entities = null)
            : base(entities ?? new Collection<Entities.Table.Sucursal>())
        {
        }
    }

    public partial class SucursalesQuery : ListDataQuery<Persistence.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal>
    {
        public SucursalesQuery(ICollection<Entities.Table.Sucursal> entities = null, 
            Persistence.Query.Sucursal query = null, 
            int maxdepth = 1)
            : base(entities ?? new Collection<Entities.Table.Sucursal>(), 
                  query ?? new Persistence.Query.Sucursal(), 
                  maxdepth)
        {
        }
    }
}

namespace demo.Persistence.Query
{
    public partial class Sucursal : AbstractQueryData<Entities.Table.Sucursal, Persistence.Table.Sucursal>
    {
        public override IList<(IColumnQuery internalkey, IColumnQuery externalkey)> GetJoins(int maxdepth = 1, int depth = 0)
        {
            var joins = new List<(IColumnQuery internalkey, IColumnQuery externalkey)>();

            if (_empresa != null || depth < maxdepth || maxdepth == 0)
            {
                joins.Add((Columns[nameof(IdEmpresa)], Empresa()[nameof(Id)]));
            }

            return joins;
        }

        public Sucursal(IRepositoryQuery<Entities.Table.Sucursal, Persistence.Table.Sucursal> repository)
            : base(repository)
        {
        }

        public Sucursal(ConnectionStringSettings connectionstringsettings)
            : this(new RepositoryQuery<Entities.Table.Sucursal, Persistence.Table.Sucursal>(new Entities.Reader.Sucursal(SqlSyntaxSignFactory.Create(connectionstringsettings)), new Persistence.Mapper.Sucursal(), connectionstringsettings))
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
                Columns[nameof(Id)].Where(value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Codigo
        {
            set
            {
                Columns[nameof(Codigo)].Where(value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Nombre
        {
            set
            {
                Columns[nameof(Nombre)].Where(value.value, value.sign);
            }
        }
        public virtual (DateTime? value, WhereOperator? sign) Fecha
        {
            set
            {
                Columns[nameof(Fecha)].Where(value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                Columns[nameof(Activo)].Where(value.value, value.sign);
            }
        }

        public virtual (int? value, WhereOperator? sign) IdEmpresa
        {
            set
            {
                Columns[nameof(IdEmpresa)].Where(value.value, value.sign);
            }
        }

        protected Persistence.Query.Empresa _empresa;
        public virtual Persistence.Query.Empresa Empresa(Persistence.Query.Empresa query = null)
        {
            return _empresa = query ?? _empresa ?? new Persistence.Query.Empresa();
        }
    }
}

namespace demo.Persistence.Mapper
{
    public partial class Sucursal : BaseMapper<Entities.Table.Sucursal, Persistence.Table.Sucursal>
    {
        public override void Map(Persistence.Table.Sucursal data, int maxdepth = 1, int depth = 0)
        {
            base.Map(data, maxdepth, depth);

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                new Persistence.Mapper.Empresa().Map(data.Empresa, maxdepth, depth);
            }
        }
    }
}