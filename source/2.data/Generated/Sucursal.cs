using library.Impl;
using library.Impl.Data;
using library.Impl.Data.Repository;
using library.Impl.Data.Sql;
using library.Interface.Data;
using library.Interface.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

//using domainSucursal = domain.Model.Sucursal;
//using dataSucursal = data.Model.Sucursal;

namespace data.Model
{
    public partial class Sucursal : IEntityTable<entities.Model.Sucursal>, IEntityRepository<entities.Model.Sucursal, data.Model.Sucursal>
    {
        protected entities.Model.Sucursal _entity;
        public virtual entities.Model.Sucursal Entity
        {
            get
            {
                return _entity;
            }
            protected set
            {
                _entity = value;
            }
        }

        protected readonly IRepository<entities.Model.Sucursal, data.Model.Sucursal> _repository;

        public virtual string Name { get; private set; }
        public virtual string Reference { get; private set; }

        public Sucursal(IRepository<entities.Model.Sucursal, data.Model.Sucursal> repository,
            entities.Model.Sucursal entity, 
            string name, string reference)
        {
            _repository = repository;

            _entity = entity;

            Name = name;
            Reference = reference;

            Columns.Add(new EntityColumn<int?, entities.Model.Sucursal>(this, "id", "Id", true, true));
            Columns.Add(new EntityColumn<string, entities.Model.Sucursal>(this, "nombre", "Nombre"));
            Columns.Add(new EntityColumn<int?, entities.Model.Sucursal>(this, "id_empresa", "IdEmpresa"));
            Columns.Add(new EntityColumn<DateTime?, entities.Model.Sucursal>(this, "fecha", "Fecha"));
            Columns.Add(new EntityColumn<bool?, entities.Model.Sucursal>(this, "activo", "Activo"));
        }
        public Sucursal(string connectionstringname,
            entities.Model.Sucursal entity, 
            string name = "sucursal", string reference = "Sucursal")
            : this(new Repository<entities.Model.Sucursal, data.Model.Sucursal>(new data.Mapper.Sucursal(), connectionstringname),
                    entity,
                    name, reference)
        {
        }
        public Sucursal()
            : this(new entities.Model.Sucursal())
        {
        }

        public virtual bool UseDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? SelectDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? InsertDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? UpdateDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? DeleteDbCommand { get; set; }

        public virtual IEntityColumn<entities.Model.Sucursal> this[string reference]
        {
            get
            {
                return Columns.SingleOrDefault(x => x.Reference.ToLower() == reference.ToLower());
            }
        }
        public virtual IList<IEntityColumn<entities.Model.Sucursal>> Columns { get; set; } = new List<IEntityColumn<entities.Model.Sucursal>>();

        public virtual int? Id { get { return Entity?.Id; } set { if (Entity?.Id != value) { this["Id"].Value = Entity.Id = value; } } }
        public virtual string Nombre { get { return Entity?.Nombre; } set { if (Entity?.Nombre != value) { this["Nombre"].Value = Entity.Nombre = value; } } }
        public virtual bool? Activo { get { return Entity?.Activo; } set { if (Entity?.Activo != value) { this["Activo"].Value = Entity.Activo = value; } } }
        public virtual DateTime? Fecha { get { return Entity?.Fecha; } set { if (Entity?.Fecha != value) { this["Fecha"].Value = Entity.Fecha = value; } } }

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
                    this["IdEmpresa"].Value = Entity.IdEmpresa = value;

                    Empresa = null;
                }
            }
        }

        public data.Model.Empresa Empresa_Load()
        {
            if (this.IdEmpresa != null)
            {
                Empresa = new data.Model.Empresa() { Id = IdEmpresa }.Select().data;
            }

            return _empresa;
        }
        protected data.Model.Empresa _empresa;
        public virtual data.Model.Empresa Empresa
        {
            get
            {
                return _empresa ?? Empresa_Load();
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
                if (_empresas == null)
                {
                    var query = new data.Query.Empresa();
                    query["Activo"]?.Where(true);

                    Empresas = new data.Model.Empresas().Load(query);
                }

                return _empresas;
            }
            set
            {
                if (_empresas != value)
                {
                    _empresas = value;
                }
            }
        }

        public virtual data.Model.Sucursal Clear()
        {
            return _repository.Clear(this);
        }

        public virtual (Result result, data.Model.Sucursal data) Select(bool usedbcommand = false)
        {
            var select = _repository.Select(this, usedbcommand);

            return select;
        }
        public virtual (Result result, data.Model.Sucursal data) Insert(bool usedbcommand = false)
        {
            return _repository.Insert(this, usedbcommand);
        }
        public virtual (Result result, data.Model.Sucursal data) Update(bool usedbcommand = false)
        {
            return _repository.Update(this, usedbcommand);
        }
        public virtual (Result result, data.Model.Sucursal data) Delete(bool usedbcommand = false)
        {
            return _repository.Delete(this, usedbcommand);
        }
    }

    public partial class Sucursales : List<data.Model.Sucursal>
    {
        public virtual IList<entities.Model.Sucursal> Entities
        {
            get
            {
                var list = new List<entities.Model.Sucursal>();
                this.ForEach(x => list.Add(x.Entity));
                return list;
            }
        }

        public Sucursales()
        {
        }

        public virtual data.Model.Sucursales Load(data.Query.Sucursal query, int maxdepth = 1, int top = 0)
        {
            return Load(query.SelectMultiple(maxdepth, top).datas);
        }
        public virtual data.Model.Sucursales Load(IEnumerable<data.Model.Sucursal> list)
        {
            this.AddRange(list);

            return this;
        }
    }
}

namespace data.Query
{
    public partial class Sucursal : IQueryTable, IQueryRepository<entities.Model.Sucursal, data.Model.Sucursal>
    {
        protected readonly IRepository<entities.Model.Sucursal, data.Model.Sucursal> _repository;

        public virtual string Name { get; private set; }
        public virtual string Reference { get; private set; }

        public Sucursal(IRepository<entities.Model.Sucursal, data.Model.Sucursal> repository,
            string name, string reference)
        {
            _repository = repository;

            Name = name;
            Reference = reference;

            Columns.Add(new QueryColumn<int?>(this, "id", "Id"));
            Columns.Add(new QueryColumn<string>(this, "nombre", "Nombre"));
            Columns.Add(new QueryColumn<int?>(this, "id_empresa", "IdEmpresa"));
            Columns.Add(new QueryColumn<DateTime?>(this, "fecha", "Fecha"));
            Columns.Add(new QueryColumn<bool?>(this, "activo", "Activo"));
        }
        public Sucursal(string connectionstringname,
            string name = "sucursal", string reference = "Sucursal")
            : this(new Repository<entities.Model.Sucursal, data.Model.Sucursal>(new data.Mapper.Sucursal(), connectionstringname), 
                  name, reference)
        {
        }

        public virtual IList<(IQueryColumn column, OrderDirection flow)> Orders { get; set; } = new List<(IQueryColumn, OrderDirection)>();

        public virtual IQueryColumn this[string reference]
        {
            get
            {
                return Columns.SingleOrDefault(x => x.Reference.ToLower() == reference.ToLower());
            }
        }
        public virtual IList<IQueryColumn> Columns { get; set; } = new List<IQueryColumn>();

        public virtual IList<(IQueryColumn internalkey, IQueryColumn externalkey)> Joins { get => new List<(IQueryColumn, IQueryColumn)>() { (this["IdEmpresa"], Empresa["Id"]) }; }

        protected data.Query.Empresa _empresa;
        public virtual data.Query.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                    Empresa = new data.Query.Empresa();

                return _empresa;
            }
            set { if (_empresa != value) { _empresa = value; } }
        }

        public virtual (Result result, data.Model.Sucursal data) SelectSingle(int maxdepth = 1, data.Model.Sucursal data = default(data.Model.Sucursal))
        {
            return _repository.SelectSingle(this, maxdepth, data);
        }
        public virtual (Result result, IEnumerable<data.Model.Sucursal> datas) SelectMultiple(int maxdepth = 1, int top = 0, IList<data.Model.Sucursal> datas = null)
        {
            return _repository.SelectMultiple(this, maxdepth, top, datas);
        }

        public virtual (Result result, int rows) Update(data.Model.Sucursal entity, int maxdepth = 1)
        {
            return _repository.Update(entity, this, maxdepth);
        }
        public virtual (Result result, int rows) Delete(int maxdepth = 1)
        {
            return _repository.Delete(this, maxdepth);
        }
    }
}

namespace data.Mapper
{
    public partial class Sucursal : MapperTable<entities.Model.Sucursal, data.Model.Sucursal>
    {
        public override data.Model.Sucursal CreateInstance(int maxdepth = 1, int depth = 0)
        {
            var instance = base.CreateInstance(maxdepth, depth);

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                instance.Empresa = new data.Mapper.Empresa().CreateInstance(maxdepth, depth);
            }

            return instance;
        }
        public override data.Model.Sucursal Clear(data.Model.Sucursal data, int maxdepth = 1, int depth = 0)
        {
            data.Entity.Id = null;
            data.Entity.Nombre = null;
            data.Entity.Fecha = null;
            data.Entity.Activo = null;
            data.Entity.IdEmpresa = null;

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                data.Empresa = new data.Mapper.Empresa().Clear(data.Empresa, maxdepth, depth);
            }
            else
            {
                data.Empresa = null;
            }

            return data;
        }

        public override data.Model.Sucursal Map(data.Model.Sucursal data, int maxdepth = 1, int depth = 0)
        {
            data.Entity.Id = data["Id"]?.Value as int?;
            data.Entity.Nombre = data["Nombre"]?.Value as string;
            data.Entity.Activo = data["Activo"]?.Value as bool?;
            data.Entity.Fecha = data["Fecha"]?.Value as DateTime?;
            data.Entity.IdEmpresa = data["IdEmpresa"]?.Value as int?;

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                data.Empresa = new data.Mapper.Empresa().Map(data.Empresa, maxdepth, depth);
            }

            return data;
        }

        public override data.Model.Sucursal Read(data.Model.Sucursal data, IDataReader reader, IList<string> prefixname, string aliasseparator = ".", int maxdepth = 1, int depth = 0)
        {
            data = base.Read(data, reader, prefixname, aliasseparator, maxdepth, depth);

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                data.Empresa = new data.Mapper.Empresa().Read(data.Empresa, reader, prefixname, aliasseparator, maxdepth, depth);
            }

            return data;
        }
    }
}