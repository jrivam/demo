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
    public partial class Empresa : IEntityTable<domain.Model.Empresa>, IEntityRepository<domain.Model.Empresa, data.Model.Empresa>
    {
        public virtual domain.Model.Empresa Domain { get; set; } = new domain.Model.Empresa();

        protected readonly IRepository<domain.Model.Empresa, data.Model.Empresa> _repository;

        public virtual string Name { get; private set; }
        public virtual string Reference { get; private set; }

        public Empresa(IRepository<domain.Model.Empresa, data.Model.Empresa> repository, string name, string reference)
        {
            _repository = repository;

            Name = name;
            Reference = reference;

            Columns.Add(new EntityColumn<int?, domain.Model.Empresa>(this, "id", "Id", true, true));
            Columns.Add(new EntityColumn<string, domain.Model.Empresa>(this, "razon_social", "RazonSocial"));
            Columns.Add(new EntityColumn<bool?, domain.Model.Empresa>(this, "activo", "Activo"));
        }
        public Empresa(string connectionstringname, string name, string reference)
            : this(new Repository<domain.Model.Empresa, data.Model.Empresa>(new data.Mapper.Empresa(), connectionstringname), name, reference)
        {
        }

        public virtual (string text, CommandType type, IList<DbParameter> parameters)? SelectDbCommand { get; set; }
        public virtual (string text, CommandType type, IList<DbParameter> parameters)? InsertDbCommand { get; set; }
        public virtual (string text, CommandType type, IList<DbParameter> parameters)? UpdateDbCommand { get; set; }
        public virtual (string text, CommandType type, IList<DbParameter> parameters)? DeleteDbCommand { get; set; }

        public virtual IEntityColumn<domain.Model.Empresa> this[string reference]
        {
            get
            {
                return Columns.SingleOrDefault(x => x.Reference.ToLower() == reference.ToLower());
            }
        }
        public virtual IList<IEntityColumn<domain.Model.Empresa>> Columns { get; set; } = new List<IEntityColumn<domain.Model.Empresa>>();

        public virtual int? Id
        {
            get { return Domain?.Id; }
            set
            {
                if (Domain?.Id != value)
                {
                    this["Id"].Value = Domain.Id = value;

                    //if (value == null)
                    //    Clear();

                    //Sucursales?.ForEach(x => x.IdEmpresa = value);
                }
            }
        }
        public virtual string RazonSocial { get { return Domain?.RazonSocial; } set { if (Domain?.RazonSocial != value) { this["RazonSocial"].Value = Domain.RazonSocial = value; } } }
        public virtual bool? Activo { get { return Domain?.Activo; } set { if (Domain?.Activo != value) { this["Activo"].Value = Domain.Activo = value; } } }

        public virtual void Sucursales_Load(int maxdepth = 1, int top = 0)
        {
            var query = new data.Query.Sucursal();
            query["IdEmpresa"].Where(this.Id);

            Sucursales_Load(query, maxdepth, top);
        }
        public virtual void Sucursales_Load(data.Query.Sucursal query, int maxdepth = 1, int top = 0)
        {
            Sucursales_Load(query.SelectMultiple(maxdepth, top).datas.ToList());
        }
        public virtual void Sucursales_Load(IList<data.Model.Sucursal> list)
        {
            Sucursales = new data.Model.Sucursales().Load(list);
        }

        protected data.Model.Sucursales _sucursales;
        public virtual data.Model.Sucursales Sucursales
        {
            get
            {
                if (_sucursales == null && this.Id != null)
                {
                    Sucursales_Load();
                }

                return _sucursales;
            }
            set
            {
                _sucursales = value;

                Domain.Sucursales = Sucursales?.Domains;
            }
        } 

        public virtual data.Model.Empresa Clear()
        {
            return _repository.Clear(this);
        }
        public virtual (Result result, data.Model.Empresa data) Select()
        {
            var select = _repository.Select(this);

            return select;
        }
        public virtual (Result result, data.Model.Empresa data) Insert()
        {
            return _repository.Insert(this);
        }
        public virtual (Result result, data.Model.Empresa data) Update()
        {
            return _repository.Update(this);
        }
        public virtual (Result result, data.Model.Empresa data) Delete()
        {
            return _repository.Delete(this);
        }
    }

    public partial class Empresas : List<data.Model.Empresa>
    {
        public virtual IList<domain.Model.Empresa> Domains
        {
            get
            {
                var list = new List<domain.Model.Empresa>();
                this.ForEach(x => list.Add(x.Domain));
                return list;
            }
        }

        public Empresas()
        {
        }

        public virtual Empresas Load(data.Query.Empresa query, int maxdepth = 1, int top = 0)
        {
            return Load(query.SelectMultiple(maxdepth, top).datas);
        }
        public virtual Empresas Load(IEnumerable<data.Model.Empresa> list)
        {
            this.AddRange(list);

            return this;
        }
    }
}

namespace data.Query
{
    public partial class Empresa : IQueryTable, IQueryRepository<domain.Model.Empresa, data.Model.Empresa>
    {
        protected readonly IRepository<domain.Model.Empresa, data.Model.Empresa> _repository;

        public virtual string Name { get; private set; }
        public virtual string Reference { get; private set; }

        public Empresa(IRepository<domain.Model.Empresa, data.Model.Empresa> repository, string name, string reference)
        {
            _repository = repository;

            Name = name;
            Reference = reference;

            Columns.Add(new QueryColumn<int?>(this, "id", "Id"));
            Columns.Add(new QueryColumn<string>(this, "razon_social", "RazonSocial"));
            Columns.Add(new QueryColumn<bool?>(this, "activo", "Activo"));
        }
        public Empresa(string connectionstringname, string name, string reference)
            : this(new Repository<domain.Model.Empresa, data.Model.Empresa>(new data.Mapper.Empresa(), connectionstringname), name, reference)
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
    public partial class Empresa : MapperTable<domain.Model.Empresa, data.Model.Empresa>
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
            data.Domain.Id = data["Id"].Value as int?;
            data.Domain.RazonSocial = data["RazonSocial"].Value as string;
            data.Domain.Activo = data["Activo"].Value as bool?;

            return data;
        }

        public override data.Model.Empresa Read(data.Model.Empresa data, IDataReader reader, IList<string> prefixname, string aliasseparator = ".", int maxdepth = 1, int depth = 0)
        {
            return base.Read(data, reader, prefixname, aliasseparator, maxdepth, depth);
        }
    }
}
