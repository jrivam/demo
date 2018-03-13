using library.Impl;
using library.Impl.Business;
using library.Interface.Business;
using System.Collections.Generic;
using System.Linq;

namespace business.Model
{
    public partial class Empresa : IEntityState<domain.Model.Empresa, data.Model.Empresa>, IEntityLogic<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa>
    {
        public virtual data.Model.Empresa Data { get; set; } = new data.Model.Empresa();

        protected readonly ILogic<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa> _logic;

        public Empresa(ILogic<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa> logic)
        {
            _logic = logic;
        }
        public Empresa()
            : this(new Logic<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa>(new business.Mapper.Empresa()))
        {
        }

        public virtual bool Loaded { get; set; }
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

        public virtual void Sucursales_Load(int maxdepth = 1, int top = 0)
        {
            var query = new business.Query.Sucursal();
            query.Data["IdEmpresa"].Where(this.Id);

            Sucursales_Load(query, maxdepth, top);
        }
        public virtual void Sucursales_Load(business.Query.Sucursal query, int maxdepth = 1, int top = 0)
        {
            Sucursales_Load(query.List(maxdepth, top).businesses.ToList());
        }
        public virtual void Sucursales_Load(IList<business.Model.Sucursal> list)
        {
            Sucursales = new business.Model.Sucursales().Load(list);
        }

        protected business.Model.Sucursales _sucursales;
        public virtual business.Model.Sucursales Sucursales
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

                Data.Sucursales = Sucursales?.Datas;
            }
        }

        public virtual business.Model.Empresa Clear()
        {
            return _logic.Clear(this, Data);
        }
        public virtual (Result result, business.Model.Empresa business) Load()
        {
            var load = _logic.Load(this, Data);

            return load;
        }
        public virtual (Result result, business.Model.Empresa business) Save()
        {
            var save = _logic.Save(this, Data);

            SaveDependencies();

            return save;
        }
        public virtual (Result result, business.Model.Empresa business) Erase()
        {
            EraseDependencies();

            var erase = _logic.Erase(this, Data);

            return erase;
        }

        protected virtual void SaveDependencies()
        {
            Sucursales?.ToList()?.ForEach(i => { i.Save(); });
        }
        protected virtual void EraseDependencies()
        {
            Sucursales?.ToList()?.ForEach(i => { i.Erase(); });
        }
    }

    public partial class Empresas : List<business.Model.Empresa>
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

        public virtual Empresas Load(business.Query.Empresa query, int maxdepth = 1, int top = 0)
        {
            return Load(query.List(maxdepth, top).businesses);
        }
        public virtual Empresas Load(IEnumerable<business.Model.Empresa> list)
        {
            this.AddRange(list);

            return this;
        }
    }
}

namespace business.Query
{
    public partial class Empresa : IQueryState<data.Query.Empresa>, IQueryLogic<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa>
    {
        public virtual data.Query.Empresa Data { get; set; } = new data.Query.Empresa();

        protected readonly ILogic<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa> _logic;

        public Empresa(ILogic<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa> logic)
        {
            _logic = logic;
        }
        public Empresa()
            : this(new Logic<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa>(new business.Mapper.Empresa()))
        {
        }

        public virtual (Result result, business.Model.Empresa business) Retrieve(int maxdepth = 1, business.Model.Empresa business = default(business.Model.Empresa))
        {
            return _logic.Retrieve(Data, maxdepth, business);
        }
        public virtual (Result result, IEnumerable<business.Model.Empresa> businesses) List(int maxdepth = 1, int top = 0, IList<business.Model.Empresa> businesses = null)
        {
            return _logic.List(Data, maxdepth, top, businesses);
        }
    }
}

namespace business.Mapper
{
    public partial class Empresa : MapperState<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa>
    {
    }
}