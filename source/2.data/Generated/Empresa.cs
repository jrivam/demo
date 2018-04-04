using library.Impl;
using library.Impl.Data;
using library.Impl.Data.Repository;
using library.Impl.Data.Sql;
using library.Interface.Data;
using library.Interface.Data.Repository;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace data.Model
{
    public partial class Empresa : IEntityTable<entities.Model.Empresa>, IEntityRepository<entities.Model.Empresa, data.Model.Empresa>
    {
        protected entities.Model.Empresa _entity;
        public virtual entities.Model.Empresa Entity
        {
            get
            {
                return _entity;
            }
            protected set
            {
                _entity = value;

                //var props = _entity.GetType().GetProperties();
                //foreach (var prop in props)
                //{
                //    this[prop.Name].Value = prop.GetValue(_entity, null);
                //}
            }
        }

        protected readonly IRepository<entities.Model.Empresa, data.Model.Empresa> _repository;

        public virtual string Name { get; private set; }
        public virtual string Reference { get; private set; }

        public Empresa(IRepository<entities.Model.Empresa, data.Model.Empresa> repository,
            entities.Model.Empresa entity,
            string name, string reference)
        {
            _repository = repository;

            Entity = entity;

            Name = name;
            Reference = reference;

            Columns.Add(new EntityColumn<int?, entities.Model.Empresa>(this, "id", "Id", true, true));
            Columns.Add(new EntityColumn<string, entities.Model.Empresa>(this, "razon_social", "RazonSocial"));
            Columns.Add(new EntityColumn<bool?, entities.Model.Empresa>(this, "activo", "Activo"));
        }
        public Empresa(string connectionstringname,
            entities.Model.Empresa entity,
            string name = "empresa", string reference = "Empresa")
            : this(new Repository<entities.Model.Empresa, data.Model.Empresa>(new data.Mapper.Empresa(), connectionstringname),
                  entity,
                  name, reference)
        {
        }
        public Empresa()
            : this(new entities.Model.Empresa())
        {
        }

        public virtual bool UseDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? SelectDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? InsertDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? UpdateDbCommand { get; set; }
        public virtual (bool usedbcommand, (string text, CommandType type, IList<DbParameter> parameters) dbcommand)? DeleteDbCommand { get; set; }

        public virtual IEntityColumn<entities.Model.Empresa> this[string reference]
        {
            get
            {
                return Columns.SingleOrDefault(x => x.Reference.ToLower() == reference.ToLower());
            }
        }
        public virtual IList<IEntityColumn<entities.Model.Empresa>> Columns { get; set; } = new List<IEntityColumn<entities.Model.Empresa>>();

        public virtual int? Id
        {
            get { return Entity?.Id; }
            set
            {
                if (Entity?.Id != value)
                {
                    this["Id"].Value = Entity.Id = value;

                    //if (value == null)
                    //    Clear();

                    //Sucursales?.ForEach(x => x.IdEmpresa = value);
                }
            }
        }
        public virtual string RazonSocial { get { return Entity?.RazonSocial; } set { if (Entity?.RazonSocial != value) { this["RazonSocial"].Value = Entity.RazonSocial = value; } } }
        public virtual bool? Activo { get { return Entity?.Activo; } set { if (Entity?.Activo != value) { this["Activo"].Value = Entity.Activo = value; } } }

        public virtual data.Model.Sucursales Sucursales_Load(int maxdepth = 1, int top = 0)
        {
            if (this.Id != null)
            {
                var query = new data.Query.Sucursal();
                query["IdEmpresa"]?.Where(this.Id);

                Sucursales = new data.Model.Sucursales().Load(query, maxdepth, top);
            }

            return _sucursales;
        }
        protected data.Model.Sucursales _sucursales;
        public virtual data.Model.Sucursales Sucursales
        {
            get
            {
                return _sucursales ?? Sucursales_Load();
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

        public virtual data.Model.Empresa Clear()
        {
            return _repository.Clear(this);
        }

        public virtual (Result result, data.Model.Empresa data) Select(bool usedbcommand = false)
        {
            var select = _repository.Select(this, usedbcommand);

            return select;
        }
        public virtual (Result result, data.Model.Empresa data) Insert(bool usedbcommand = false)
        {
            return _repository.Insert(this, usedbcommand);
        }
        public virtual (Result result, data.Model.Empresa data) Update(bool usedbcommand = false)
        {
            return _repository.Update(this, usedbcommand);
        }
        public virtual (Result result, data.Model.Empresa data) Delete(bool usedbcommand = false)
        {
            return _repository.Delete(this, usedbcommand);
        }
    }

    public partial class Empresas : List<data.Model.Empresa>
    {
        public virtual IList<entities.Model.Empresa> Entities
        {
            get
            {
                var list = new List<entities.Model.Empresa>();
                this.ForEach(x => list.Add(x.Entity));
                return list;
            }
        }

        public Empresas()
        {
        }

        public virtual data.Model.Empresas Load(data.Query.Empresa query, int maxdepth = 1, int top = 0)
        {
            return Load(query.SelectMultiple(maxdepth, top).datas);
        }
        public virtual data.Model.Empresas Load(IEnumerable<data.Model.Empresa> list)
        {
            this.AddRange(list);

            return this;
        }
    }
}

namespace data.Query
{
    public partial class Empresa : IQueryTable, IQueryRepository<entities.Model.Empresa, data.Model.Empresa>
    {
        protected readonly IRepository<entities.Model.Empresa, data.Model.Empresa> _repository;

        public virtual string Name { get; private set; }
        public virtual string Reference { get; private set; }

        public Empresa(IRepository<entities.Model.Empresa, data.Model.Empresa> repository,
            string name, string reference)
        {
            _repository = repository;

            Name = name;
            Reference = reference;

            Columns.Add(new QueryColumn<int?>(this, "id", "Id"));
            Columns.Add(new QueryColumn<string>(this, "razon_social", "RazonSocial"));
            Columns.Add(new QueryColumn<bool?>(this, "activo", "Activo"));
        }
        public Empresa(string connectionstringname,
            string name = "empresa", string reference = "Empresa")
            : this(new Repository<entities.Model.Empresa, data.Model.Empresa>(new data.Mapper.Empresa(), connectionstringname), 
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

        public virtual IList<(IQueryColumn internalkey, IQueryColumn externalkey)> Joins { get; } = new List<(IQueryColumn, IQueryColumn)>();

        public virtual (Result result, data.Model.Empresa data) SelectSingle(int maxdepth = 1, data.Model.Empresa data = default(data.Model.Empresa))
        {
            return _repository.SelectSingle(this, maxdepth, data);
        }
        public virtual (Result result, IEnumerable<data.Model.Empresa> datas) SelectMultiple(int maxdepth = 1, int top = 0, IList<data.Model.Empresa> datas = null)
        {
            return _repository.SelectMultiple(this, maxdepth, top, datas);
        }

        public virtual (Result result, int rows) Update(data.Model.Empresa entity, int maxdepth = 1)
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
    public partial class Empresa : MapperTable<entities.Model.Empresa, data.Model.Empresa>
    {
        public override data.Model.Empresa CreateInstance(int maxdepth = 1, int depth = 0)
        {
            return base.CreateInstance(maxdepth, depth);
        }

        public override data.Model.Empresa Clear(data.Model.Empresa data, int maxdepth = 1, int depth = 0)
        {
            data.Id = null;
            data.RazonSocial = null;
            data.Activo = null;

            data.Sucursales = null;

            return data;
        }
        public override data.Model.Empresa Map(data.Model.Empresa data, int maxdepth = 1, int depth = 0)
        {
            data.Entity.Id = data["Id"].Value as int?;
            data.Entity.RazonSocial = data["RazonSocial"].Value as string;
            data.Entity.Activo = data["Activo"].Value as bool?;

            return data;
        }

        public override data.Model.Empresa Read(data.Model.Empresa data, IDataReader reader, IList<string> prefixname, string aliasseparator = ".", int maxdepth = 1, int depth = 0)
        {
            return base.Read(data, reader, prefixname, aliasseparator, maxdepth, depth);
        }
    }
}
