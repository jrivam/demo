using Library.Extension;
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
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace Persistence.Table
{
    public partial class Sucursal : AbstractTableData<Entities.Table.Sucursal, Persistence.Table.Sucursal>
    {
        protected override void Init()
        {
            base.Init();

            Columns.Add(new ColumnTable<int?>(this, nameof(Id), typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>(nameof(Id))?.Name ?? nameof(Id).ToUnderscoreCase().ToLower(), isprimarykey: true, isidentity: true));
            Columns.Add(new ColumnTable<string>(this, nameof(Codigo), typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>(nameof(Codigo))?.Name ?? nameof(Codigo).ToUnderscoreCase().ToLower()));
            Columns.Add(new ColumnTable<string>(this, nameof(Nombre), typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>(nameof(Nombre))?.Name ?? nameof(Nombre).ToUnderscoreCase().ToLower()));
            Columns.Add(new ColumnTable<DateTime?>(this, nameof(Fecha), typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>(nameof(Fecha))?.Name ?? nameof(Fecha).ToUnderscoreCase().ToLower()));
            Columns.Add(new ColumnTable<bool?>(this, nameof(Activo), typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>(nameof(Activo))?.Name ?? nameof(Activo).ToUnderscoreCase().ToLower()));
            Columns.Add(new ColumnTable<int?>(this, nameof(IdEmpresa), typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>(nameof(IdEmpresa))?.Name ?? nameof(IdEmpresa).ToUnderscoreCase().ToLower()));
        }

        public Sucursal(Entities.Table.Sucursal entity,
            IRepositoryTable<Entities.Table.Sucursal, Persistence.Table.Sucursal> repository,
            IQueryData<Entities.Table.Sucursal, Persistence.Table.Sucursal> query = null)
            : base(entity, 
                  repository,
                  query ?? new Persistence.Query.Sucursal(),
                  nameof(Sucursal), typeof(Entities.Table.Sucursal).GetAttributeFromType<TableAttribute>()?.Name ?? nameof(Sucursal).ToUnderscoreCase().ToLower())
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
        public virtual string Codigo { get { return Entity?.Codigo; } set { if (Entity?.Codigo != value) { Columns[nameof(Codigo)].Value = Entity.Codigo = value; } } }
        public virtual string Nombre { get { return Entity?.Nombre; } set { if (Entity?.Nombre != value) { Columns[nameof(Nombre)].Value = Entity.Nombre = value; } } }
        public virtual DateTime? Fecha { get { return Entity?.Fecha; } set { if (Entity?.Fecha != value) { Columns[nameof(Fecha)].Value = Entity.Fecha = value; } } }
        public virtual bool? Activo { get { return Entity?.Activo; } set { if (Entity?.Activo != value) { Columns[nameof(Activo)].Value = Entity.Activo = value; } } }

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
        public virtual Persistence.Table.Empresa Empresa
        {
            //get
            //{
            //    bool lazyload = false;

            //    if (_empresa == null)
            //    {
            //        if (Entity?.Empresa == null)
            //        {
            //            Entity.Empresa = new Entities.Table.Empresa();
            //            lazyload = true;
            //        }
            //        _empresa = new Persistence.Table.Empresa(Entity?.Empresa);
            //    }

            //    _empresa.Id = this.IdEmpresa;
            //    if (lazyload)
            //        _empresa.Select();

            //    return _empresa;
            //}
            get
            {
                if (_empresa?.Id != this.IdEmpresa)
                {
                    if (Entity?.Empresa == null)
                    {
                        Entity.Empresa = new Entities.Table.Empresa();
                    }
                    Empresa = new Persistence.Table.Empresa(Entity?.Empresa);

                    if (_empresa.Id == null && this.IdEmpresa != null)
                    {
                        _empresa.Id = this.IdEmpresa;
                        _empresa.Select();
                    }
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
        public Sucursales(ICollection<Entities.Table.Sucursal> entities)
            : base(entities)
        {
        }

        public Sucursales()
            : this(new Collection<Entities.Table.Sucursal>())
        {
        }
    }

    public partial class SucursalesQuery : ListDataQuery<Persistence.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal>
    {
        public SucursalesQuery(ICollection<Entities.Table.Sucursal> entities, 
            Persistence.Query.Sucursal query = null, 
            int maxdepth = 1)
            : base(entities, 
                  query ?? new Persistence.Query.Sucursal(), 
                  maxdepth)
        {
        }

        public SucursalesQuery(Persistence.Query.Sucursal query = null, 
            int maxdepth = 1)
            : this(new Collection<Entities.Table.Sucursal>(), 
                  query ?? new Persistence.Query.Sucursal(),
                  maxdepth)
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
                joins.Add((Columns[nameof(IdEmpresa)], Empresa()[nameof(Id)]));
            }

            return joins;
        }

        public override void Init()
        {
            Columns.Add(new ColumnQuery<int?>(this, nameof(Id), typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>(nameof(Id))?.Name ?? nameof(Id).ToUnderscoreCase().ToLower()));
            Columns.Add(new ColumnQuery<string>(this, nameof(Codigo), typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>(nameof(Codigo))?.Name ?? nameof(Codigo).ToUnderscoreCase().ToLower()));
            Columns.Add(new ColumnQuery<string>(this, nameof(Nombre), typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>(nameof(Nombre))?.Name ?? nameof(Nombre).ToUnderscoreCase().ToLower()));
            Columns.Add(new ColumnQuery<DateTime?>(this, nameof(Fecha), typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>(nameof(Fecha))?.Name ?? nameof(Fecha).ToUnderscoreCase().ToLower()));
            Columns.Add(new ColumnQuery<bool?>(this, nameof(Activo), typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>(nameof(Activo))?.Name ?? nameof(Activo).ToUnderscoreCase().ToLower()));

            Columns.Add(new ColumnQuery<int?>(this, nameof(IdEmpresa), typeof(Entities.Table.Sucursal).GetAttributeFromTypeProperty<ColumnAttribute>(nameof(IdEmpresa))?.Name ?? nameof(IdEmpresa).ToUnderscoreCase().ToLower()));
        }

        public Sucursal(IRepositoryQuery<Entities.Table.Sucursal, Persistence.Table.Sucursal> repository = null)
            : base(repository,
                  nameof(Sucursal), typeof(Entities.Table.Sucursal).GetAttributeFromType<TableAttribute>()?.Name ?? nameof(Sucursal).ToUnderscoreCase().ToLower())
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