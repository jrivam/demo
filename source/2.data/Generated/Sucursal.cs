using library.Impl.Data.Mapper;
using library.Impl.Data.Model;
using library.Impl.Data.Query;
using library.Impl.Data.Repository;
using library.Interface.Data.Model;
using library.Interface.Data.Query;
using System;
using System.Collections.Generic;
using System.Data;

namespace data.Model
{
    public partial class Sucursal : AbstractEntityRepository<entities.Model.Sucursal, data.Model.Sucursal>
    {
        public Sucursal(IRepositoryTable<entities.Model.Sucursal, data.Model.Sucursal> repository)
            : base(repository, "sucursal", "Sucursal")
        {
            Columns.Add(new EntityColumn<int?, entities.Model.Sucursal>(this, "id", "Id", true, true));
            Columns.Add(new EntityColumn<string, entities.Model.Sucursal>(this, "nombre", "Nombre"));
            Columns.Add(new EntityColumn<int?, entities.Model.Sucursal>(this, "id_empresa", "IdEmpresa"));
            Columns.Add(new EntityColumn<DateTime?, entities.Model.Sucursal>(this, "fecha", "Fecha"));
            Columns.Add(new EntityColumn<bool?, entities.Model.Sucursal>(this, "activo", "Activo"));
        }
        public Sucursal(string connectionstringname)
            : this(new RepositoryTable<entities.Model.Sucursal, Sucursal>(new data.Mapper.Sucursal(), connectionstringname))
        {
        }
        public Sucursal(entities.Model.Sucursal entity, string connectionstringname)
            : this(connectionstringname)
        {
            SetProperties(entity);
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
                    this["Id"].Value = Entity.Id = value;
                }
            }
        }
        public virtual string Nombre { get { return Entity?.Nombre; } set { if (Entity?.Nombre != value) { this["Nombre"].Value = Entity.Nombre = value; } } }
        public virtual DateTime? Fecha { get { return Entity?.Fecha; } set { if (Entity?.Fecha != value) { this["Fecha"].Value = Entity.Fecha = value; } } }
        public virtual bool? Activo { get { return Entity?.Activo; } set { if (Entity?.Activo != value) { this["Activo"].Value = Entity.Activo = value; } } }

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
    }

    //public partial class Sucursales : List<data.Model.Sucursal>
    //{
    //    public virtual IList<entities.Model.Sucursal> Entities
    //    {
    //        get
    //        {
    //            var list = new List<entities.Model.Sucursal>();
    //            this.ForEach(x => list.Add(x.Entity));
    //            return list;
    //        }
    //    }

    //    public Sucursales()
    //    {
    //    }

    //    public virtual data.Model.Sucursales Load(data.Query.Sucursal query, int maxdepth = 1, int top = 0)
    //    {
    //        return Load(query.SelectMultiple(maxdepth, top).datas);
    //    }
    //    public virtual data.Model.Sucursales Load(IEnumerable<data.Model.Sucursal> list)
    //    {
    //        this.AddRange(list);

    //        return this;
    //    }
    //}
    public partial class Sucursales : ListEntityTable<data.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal>
    {
    }
}

namespace data.Query
{
    public partial class Sucursal : AbstractQueryRepository<entities.Model.Sucursal, data.Model.Sucursal>
    {
        public Sucursal(IRepositoryQuery<entities.Model.Sucursal, data.Model.Sucursal> repository)
            : base(repository, "sucursal", "Sucursal")
        {
            Columns.Add(new QueryColumn<int?>(this, "id", "Id"));
            Columns.Add(new QueryColumn<string>(this, "nombre", "Nombre"));
            Columns.Add(new QueryColumn<int?>(this, "id_empresa", "IdEmpresa"));
            Columns.Add(new QueryColumn<DateTime?>(this, "fecha", "Fecha"));
            Columns.Add(new QueryColumn<bool?>(this, "activo", "Activo"));

            Joins.Add((this["IdEmpresa"], Empresa["Id"]));
        }

        public Sucursal(string connectionstringname)
            : this(new RepositoryQuery<entities.Model.Sucursal, data.Model.Sucursal>(new data.Mapper.Sucursal(), connectionstringname))
        {
        }

        protected data.Query.Empresa _empresa;
        public virtual data.Query.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    Empresa = new data.Query.Empresa();
                }

                return _empresa;
            }
            set { if (_empresa != value) { _empresa = value; } }
        }
    }
}

namespace data.Mapper
{
    public partial class Sucursal : AbstractMapperTable<entities.Model.Sucursal, data.Model.Sucursal>
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