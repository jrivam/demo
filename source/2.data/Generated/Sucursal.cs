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
    public partial class Sucursal : IEntityTable<domain.Model.Sucursal>, IEntityRepository<domain.Model.Sucursal, data.Model.Sucursal>
    {
        public partial class Mapper : MapperTable<domain.Model.Sucursal, data.Model.Sucursal>
        {
            public override data.Model.Sucursal CreateInstance(int maxdepth = 1, int depth = 0)
            {
                var instance = base.CreateInstance(maxdepth, depth);

                depth++;
                if (depth < maxdepth || maxdepth == 0)
                {
                    instance.Empresa = new data.Model.Empresa.Mapper().CreateInstance(maxdepth, depth);
                }

                return instance;
            }
            public override data.Model.Sucursal Clear(data.Model.Sucursal data, int maxdepth = 1, int depth = 0)
            {
                data.Domain.Id = null;
                data.Domain.Nombre = null;
                data.Domain.Fecha = null;
                data.Domain.Activo = null;
                data.Domain.IdEmpresa = null;

                depth++;
                if (depth < maxdepth || maxdepth == 0)
                {
                    data.Empresa = new data.Model.Empresa.Mapper().Clear(data.Empresa, maxdepth, depth);
                }
                else
                {
                    data.Empresa = null;
                }

                return data;
            }

            public override data.Model.Sucursal Map(data.Model.Sucursal data, int maxdepth = 1, int depth = 0)
            {
                data.Domain.Id = data["Id"].Value as int?;
                data.Domain.Nombre = data["Nombre"].Value as string;
                data.Domain.Activo = data["Activo"].Value as bool?;
                data.Domain.Fecha = data["Fecha"].Value as DateTime?;
                data.Domain.IdEmpresa = data["IdEmpresa"].Value as int?;

                depth++;
                if (depth < maxdepth || maxdepth == 0)
                {
                    data.Empresa = new data.Model.Empresa.Mapper().Map(data.Empresa, maxdepth, depth);
                }

                return data;
            }

            public override data.Model.Sucursal Read(data.Model.Sucursal data, IDataReader reader, IList<string> prefixname, string aliasseparator = ".", int maxdepth = 1, int depth = 0)
            {
                data = base.Read(data, reader, prefixname, aliasseparator, maxdepth, depth);

                depth++;
                if (depth < maxdepth || maxdepth == 0)
                {
                    data.Empresa = new data.Model.Empresa.Mapper().Read(data.Empresa, reader, prefixname, aliasseparator, maxdepth, depth);
                }

                return data;
            }
        }

        public virtual domain.Model.Sucursal Domain { get; set; } = new domain.Model.Sucursal();

        protected readonly IRepository<domain.Model.Sucursal, data.Model.Sucursal> _repository;

        public virtual string Name { get; private set; }
        public virtual string Reference { get; private set; }

        public Sucursal(IRepository<domain.Model.Sucursal, data.Model.Sucursal> repository, string name, string reference)
        {
            _repository = repository;

            Name = name;
            Reference = reference;

            Columns.Add(new EntityColumn<int?, domain.Model.Sucursal>(this, "id", "Id", true, true));
            Columns.Add(new EntityColumn<string, domain.Model.Sucursal>(this, "nombre", "Nombre"));
            Columns.Add(new EntityColumn<int?, domain.Model.Sucursal>(this, "id_empresa", "IdEmpresa"));
            Columns.Add(new EntityColumn<DateTime?, domain.Model.Sucursal>(this, "fecha", "Fecha"));
            Columns.Add(new EntityColumn<bool?, domain.Model.Sucursal>(this, "activo", "Activo"));
        }
        public Sucursal(string connectionstringname, string name, string reference)
            : this(new Repository<domain.Model.Sucursal, data.Model.Sucursal>(new data.Model.Sucursal.Mapper(), connectionstringname), name, reference)
        {
        }

        public virtual (string text, CommandType type, IList<DbParameter> parameters)? SelectDbCommand { get; set; }
        public virtual (string text, CommandType type, IList<DbParameter> parameters)? InsertDbCommand { get; set; }
        public virtual (string text, CommandType type, IList<DbParameter> parameters)? UpdateDbCommand { get; set; }
        public virtual (string text, CommandType type, IList<DbParameter> parameters)? DeleteDbCommand { get; set; }

        public virtual IEntityColumn<domain.Model.Sucursal> this[string reference]
        {
            get
            {
                return Columns.SingleOrDefault(x => x.Reference.ToLower() == reference.ToLower());
            }
        }
        public virtual IList<IEntityColumn<domain.Model.Sucursal>> Columns { get; set; } = new List<IEntityColumn<domain.Model.Sucursal>>();

        public virtual int? Id { get { return Domain?.Id; } set { if (Domain?.Id != value) { this["Id"].Value = Domain.Id = value; } } }
        public virtual string Nombre { get { return Domain?.Nombre; } set { if (Domain?.Nombre != value) { this["Nombre"].Value = Domain.Nombre = value; } } }
        public virtual bool? Activo { get { return Domain?.Activo; } set { if (Domain?.Activo != value) { this["Activo"].Value = Domain.Activo = value; } } }
        public virtual DateTime? Fecha { get { return Domain?.Fecha; } set { if (Domain?.Fecha != value) { this["Fecha"].Value = Domain.Fecha = value; } } }

        public virtual int? IdEmpresa
        {
            get
            {
                return Domain?.IdEmpresa;
            }
            set
            {
                if (Domain?.IdEmpresa != value)
                {
                    this["IdEmpresa"].Value = Domain.IdEmpresa = value;

                    Empresa = null;
                }
            }
        }

        protected data.Model.Empresa _empresa;
        public virtual data.Model.Empresa Empresa
        {
            get
            {
                if (_empresa == null && this.IdEmpresa != null)
                {
                    Empresa = new data.Model.Empresa() { Id = IdEmpresa }.Select().data;
                }

                return _empresa;
            }
            set { if (_empresa != value) { _empresa = value; } }
        }

        public virtual data.Model.Sucursal Clear()
        {
            return _repository.Clear(this);
        }
        public virtual (Result result, data.Model.Sucursal data) Select()
        {
            var select = _repository.Select(this);

            return select;
        }
        public virtual (Result result, data.Model.Sucursal data) Insert()
        {
            return _repository.Insert(this);
        }
        public virtual (Result result, data.Model.Sucursal data) Update()
        {
            return _repository.Update(this);
        }
        public virtual (Result result, data.Model.Sucursal data) Delete()
        {
            return _repository.Delete(this);
        }
    }

    public partial class Sucursales : List<data.Model.Sucursal>
    {
        public virtual IList<domain.Model.Sucursal> Domains
        {
            get
            {
                var list = new List<domain.Model.Sucursal>();
                this.ForEach(x => list.Add(x.Domain));
                return list;
            }
        }

        public Sucursales()
        {
        }

        public virtual Sucursales Load(data.Query.Sucursal query, int maxdepth = 1, int top = 0)
        {
            return Load(query.SelectMultiple(maxdepth, top).datas);
        }
        public virtual Sucursales Load(IEnumerable<data.Model.Sucursal> list)
        {
            this.AddRange(list);

            return this;
        }
    }
}

namespace data.Query
{
    public partial class Sucursal : IQueryTable, IQueryRepository<domain.Model.Sucursal, data.Model.Sucursal>
    {
        protected readonly IRepository<domain.Model.Sucursal, data.Model.Sucursal> _repository;

        public virtual string Name { get; private set; }
        public virtual string Reference { get; private set; }

        public Sucursal(IRepository<domain.Model.Sucursal, data.Model.Sucursal> repository, string name, string reference)
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
        public Sucursal(string connectionstringname, string name, string reference)
            : this(new Repository<domain.Model.Sucursal, data.Model.Sucursal>(new data.Model.Sucursal.Mapper(), connectionstringname), name, reference)
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

        public virtual (Result result, data.Model.Sucursal data) SelectSingle(int maxdepth = 1)
        {
            return _repository.SelectSingle(this, maxdepth);
        }
        public virtual (Result result, IEnumerable<data.Model.Sucursal> datas) SelectMultiple(int maxdepth = 1, int top = 0)
        {
            return _repository.SelectMultiple(this, maxdepth, top);
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