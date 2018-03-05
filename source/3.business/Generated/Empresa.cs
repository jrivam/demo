﻿using library.Impl;
using library.Impl.Business;
using library.Interface.Business;
using System.Collections.Generic;
using System.Linq;

namespace business.Model
{
    public partial class Empresa : IEntityState<domain.Model.Empresa, data.Model.Empresa>, IEntityLogic<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa>
    {
        public partial class Mapper : MapperState<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa>
        {
            public override business.Model.Empresa Map(business.Model.Empresa business, int maxdepth = 1, int depth = 0)
            {
                return base.Map(business, maxdepth, depth);
            }
        }

        public virtual data.Model.Empresa Data { get; set; } = new data.Model.Empresa();

        protected readonly ILogic<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa> _logic;

        public Empresa(ILogic<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa> logic)
        {
            _logic = logic;
        }
        public Empresa()
            : this(new Logic<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa>(new business.Model.Empresa.Mapper()))
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

                    if (Loaded)
                    {
                        if (value == null)
                            Clear();

                        //Sucursales.IdEmpresa = value;
                    }
                }
            }
        }
        public virtual string RazonSocial { get { return Data?.RazonSocial; } set { if (Data?.RazonSocial != value) { Data.RazonSocial = value; Changed = true; } } }
        public virtual bool? Activo { get { return Data?.Activo; } set { if (Data?.Activo != value) { Data.Activo = value; Changed = true; } } }

        public virtual void Sucursales_Load()
        {
            var query = new business.Query.Sucursal();
            query.Data["IdEmpresa"].Where(this.Id);

            Sucursales_Load(query);
        }
        public virtual void Sucursales_Load(business.Query.Sucursal query)
        {
            Sucursales_Load(query.List(1, 0).businesses.ToList());
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

        public virtual Empresas Load(business.Query.Empresa query)
        {
            return Load(query.List(1, 0).businesses);
        }
        public virtual Empresas Load(IEnumerable<business.Model.Empresa> list)
        {
            list?.ToList().ForEach(i => Add(i));

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
            : this(new Logic<domain.Model.Empresa, data.Model.Empresa, business.Model.Empresa>(new business.Model.Empresa.Mapper()))
        {
        }

        public virtual (Result result, business.Model.Empresa business) Retrieve(int maxdepth = 1)
        {
            return _logic.Retrieve(Data, maxdepth);
        }
        public virtual (Result result, IEnumerable<business.Model.Empresa> businesses) List(int maxdepth = 1, int top = 0)
        {
            return _logic.List(Data, maxdepth, top);
        }
    }
}