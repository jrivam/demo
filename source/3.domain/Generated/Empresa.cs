using library.Impl;
using library.Impl.Domain;
using library.Interface.Domain;
using System.Collections.Generic;
using System.Linq;

namespace domain.Model
{
    public partial class Empresa : IEntityState<entities.Model.Empresa, data.Model.Empresa>, IEntityLogic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
        protected data.Model.Empresa _data;
        public virtual data.Model.Empresa Data
        {
            get
            {
                return _data;
            }
            protected set
            {
                _data = value;
            }
        }

        protected readonly ILogic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa> _logic;

        public Empresa(ILogic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa> logic,
            data.Model.Empresa data)
        {
            _logic = logic;

            _data = data;
        }
        public Empresa(data.Model.Empresa data)
            : this(new Logic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>(new domain.Mapper.Empresa()),
                  data)
        {
        }
        public Empresa()
            : this(new data.Model.Empresa())
        {
        }

        public virtual bool Changed { get; set; }
        public virtual bool Deleted { get; set; }

        public virtual int? Id 
        {
            get { return Data?.Id; }
            set
            {
                if (Data?.Id != value)
                {
                    Data.Id = value;
                    Changed = true;

                    //if (Loaded)
                    //{
                    //    if (value == null)
                    //        Clear();

                    //    Sucursales?.ForEach(x => x.IdEmpresa = value);
                    //}
                }
            }
        }
        public virtual string RazonSocial { get { return Data?.RazonSocial; } set { if (Data?.RazonSocial != value) { Data.RazonSocial = value; Changed = true; } } }
        public virtual bool? Activo { get { return Data?.Activo; } set { if (Data?.Activo != value) { Data.Activo = value; Changed = true; } } }

        public virtual domain.Model.Sucursales Sucursales_Load(int maxdepth = 1, int top = 0)
        {
            if (this.Id != null)
            {
                var query = new domain.Query.Sucursal();
                query.Data["IdEmpresa"]?.Where(this.Id);

                Sucursales = new domain.Model.Sucursales().Load(query, maxdepth, top);
            }

            return _sucursales;
        }
        protected domain.Model.Sucursales _sucursales;
        public virtual domain.Model.Sucursales Sucursales
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

                    Data.Sucursales = _sucursales?.Datas;
                }
            }
        }

        public virtual domain.Model.Empresa Clear()
        {
            return _logic.Clear(this, Data);
        }

        public virtual (Result result, domain.Model.Empresa domain) Load(bool usedbcommand = false)
        {
            var load = _logic.Load(this, Data, usedbcommand);

            return load;
        }
        public virtual (Result result, domain.Model.Empresa domain) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            var save = _logic.Save(this, Data, useinsertdbcommand, useupdatedbcommand);

            SaveDependencies();

            return save;
        }
        public virtual (Result result, domain.Model.Empresa domain) Erase(bool usedbcommand = false)
        {
            EraseDependencies();

            var erase = _logic.Erase(this, Data, usedbcommand);

            return erase;
        }

        protected virtual void SaveDependencies()
        {
            _sucursales?.ToList()?.ForEach(i => { i.Save(); });
        }
        protected virtual void EraseDependencies()
        {
            _sucursales?.ToList()?.ForEach(i => { i.Erase(); });
        }
    }

    public partial class Empresas : List<domain.Model.Empresa>
    {
        public virtual data.Model.Empresas Datas
        {
            get
            {
                return new data.Model.Empresas().Load(this.Select(x => x.Data).Cast<data.Model.Empresa>());
            }
        }

        public Empresas()
        {
        }

        public virtual domain.Model.Empresas Load(domain.Query.Empresa query, int maxdepth = 1, int top = 0)
        {
            return Load(query.List(maxdepth, top).domains);
        }
        public virtual domain.Model.Empresas Load(IEnumerable<domain.Model.Empresa> list)
        {
            this.AddRange(list);

            return this;
        }
    }
}

namespace domain.Query
{
    public partial class Empresa : IQueryState<data.Query.Empresa>, IQueryLogic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
        public virtual data.Query.Empresa Data { get; protected set; }

        protected readonly ILogic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa> _logic;

        public Empresa(ILogic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa> logic,
            data.Query.Empresa data)
        {
            _logic = logic;

            Data = data;
        }
        public Empresa(data.Query.Empresa data)
            : this(new Logic<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>(new domain.Mapper.Empresa()),
                  data)
        {
        }
        public Empresa()
            : this(new data.Query.Empresa())
        {
        }

        public virtual (Result result, domain.Model.Empresa domain) Retrieve(int maxdepth = 1, domain.Model.Empresa domain = default(domain.Model.Empresa))
        {
            return _logic.Retrieve(Data, maxdepth, domain);
        }
        public virtual (Result result, IEnumerable<domain.Model.Empresa> domains) List(int maxdepth = 1, int top = 0, IList<domain.Model.Empresa> domains = null)
        {
            return _logic.List(Data, maxdepth, top, domains);
        }
    }
}

namespace domain.Mapper
{
    public partial class Empresa : MapperState<entities.Model.Empresa, data.Model.Empresa, domain.Model.Empresa>
    {
    }
}