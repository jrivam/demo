using Library.Extension;
using Library.Impl;
using Library.Impl.Persistence;
using Library.Impl.Persistence.Mapper;
using Library.Impl.Persistence.Query;
using Library.Impl.Persistence.Sql;
using Library.Impl.Persistence.Sql.Factory;
using Library.Impl.Persistence.Table;
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
        public override void Init()
        {
            Columns.Add(new ColumnTable<int?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id", isprimarykey: true, isidentity: true));
            Columns.Add(new ColumnTable<string>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Codigo")?.Name ?? "codigo", "Codigo", isunique: true));
            Columns.Add(new ColumnTable<string>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Nombre")?.Name ?? "nombre", "Nombre"));
            Columns.Add(new ColumnTable<DateTime?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Fecha")?.Name ?? "fecha", "Fecha"));
            Columns.Add(new ColumnTable<bool?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));

            Columns.Add(new ColumnTable<int?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("IdEmpresa")?.Name ?? "id_empresa", "IdEmpresa"));
        }

        public Sucursal(Entities.Table.Sucursal entity,
            IRepositoryTable<Entities.Table.Sucursal, Persistence.Table.Sucursal> repository,
            IQueryData<Entities.Table.Sucursal, Persistence.Table.Sucursal> query)
            : base(entity, 
                  repository,
                  query,
                  typeof(Entities.Table.Sucursal).GetAttributeFromType<TableAttribute>()?.Name ?? "sucursal", "Sucursal")
        {
        }

        public Sucursal(Entities.Table.Sucursal entity,
            IRepositoryTable<Entities.Table.Sucursal, Persistence.Table.Sucursal> repository)
            : this(entity,
                    repository,
                    new Persistence.Query.Sucursal())
        {
        }
        public Sucursal(Entities.Table.Sucursal entity,
            ConnectionStringSettings connectionstringsettings)
            : this(entity,
                  new RepositoryTable<Entities.Table.Sucursal, Persistence.Table.Sucursal>(new Entities.Reader.Sucursal(SqlSyntaxSignFactory.Create(connectionstringsettings)), new Persistence.Mapper.Sucursal(), connectionstringsettings))
        {
        }
        public Sucursal(Entities.Table.Sucursal entity,
            string appsettingsconnectionstringname)
            : this(entity, 
                  ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings[appsettingsconnectionstringname]])
        {
        }

        public Sucursal()
            : this(new Entities.Table.Sucursal())
        {
        }

        public override Entities.Table.Sucursal Entity
        {
            get
            {
                return base.Entity;
            }
            set
            {
                base.Entity = value;

                _empresa = null;
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
        public virtual (Result result, Persistence.Table.Empresa data) Empresa_Refresh(int maxdepth = 1, Persistence.Query.Empresa query = null)
        {
            if (this.IdEmpresa != null)
            {
                var queryselect = query ?? new Persistence.Query.Empresa();

                queryselect.Id = (this.IdEmpresa, WhereOperator.Equals);

                var selectsingle = queryselect?.SelectSingle(maxdepth);

                Empresa = selectsingle?.data;

                return (selectsingle?.result, _empresa);
            }

            return (new Result() { Messages = new List<(ResultCategory, string, string)>() { (ResultCategory.Error, "Empresa_Refresh", $"IdEmpresa in {this.Description.Name} cannot be null") } }, null);
        }
    }

    public partial class Sucursales : ListData<Entities.Table.Sucursal, Persistence.Table.Sucursal>
    {
        public Sucursales(Entities.Table.Sucursales entities)
            : base(entities)
        {
        }

        public Sucursales()
           : this(new Entities.Table.Sucursales())
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

        public override void Init()
        {
            Columns.Add(new ColumnQuery<int?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Id")?.Name ?? "id", "Id"));
            Columns.Add(new ColumnQuery<string>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Codigo")?.Name ?? "codigo", "Codigo"));
            Columns.Add(new ColumnQuery<string>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Nombre")?.Name ?? "nombre", "Nombre"));
            Columns.Add(new ColumnQuery<DateTime?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Fecha")?.Name ?? "fecha", "Fecha"));
            Columns.Add(new ColumnQuery<bool?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("Activo")?.Name ?? "activo", "Activo"));

            Columns.Add(new ColumnQuery<int?>(this, typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>("IdEmpresa")?.Name ?? "id_empresa", "IdEmpresa"));
        }

        public Sucursal(IRepositoryQuery<Entities.Table.Sucursal, Persistence.Table.Sucursal> repository)
            : base(repository, 
                  typeof(Entities.Table.Sucursal).GetAttributeFromType<TableAttribute>()?.Name ?? "sucursal", "Sucursal")
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
    public partial class Sucursal : BaseMapper<Entities.Table.Sucursal, Persistence.Table.Sucursal>
    {
        public override Persistence.Table.Sucursal Map(Persistence.Table.Sucursal data, int maxdepth = 1, int depth = 0)
        {
            data = base.Map(data, maxdepth, depth);

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                data.Empresa = new Persistence.Mapper.Empresa().Map(data.Empresa, maxdepth, depth);
            }

            return data;
        }
    }
}