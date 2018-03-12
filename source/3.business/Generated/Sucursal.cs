using library.Impl;
using library.Impl.Business;
using library.Interface.Business;
using System;
using System.Collections.Generic;
using System.Linq;

namespace business.Model
{
    public partial class Sucursal : IEntityState<domain.Model.Sucursal, data.Model.Sucursal>, IEntityLogic<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal>
    {
        public virtual data.Model.Sucursal Data { get; set; } = new data.Model.Sucursal();

        protected readonly ILogic<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal> _logic;

        public Sucursal(ILogic<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal> logic)
        {
            _logic = logic;
        }
        public Sucursal()
            : this(new Logic<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal>(new business.Mapper.Sucursal()))
        {
        }

        public virtual bool Loaded { get; set; }
        public virtual bool Changed { get; set; }
        public virtual bool Deleted { get; set; }

        public virtual int? Id { get { return Data?.Id; } set { if (Data?.Id != value) { Data.Id = value; Changed = true; } } }
        public virtual string Nombre { get { return Data?.Nombre; } set { if (Data?.Nombre != value) { Data.Nombre = value; Changed = true; } } }
        public virtual bool? Activo { get { return Data?.Activo; } set { if (Data?.Activo != value) { Data.Activo = value; Changed = true; } } }
        public virtual DateTime? Fecha { get { return Data?.Fecha; } set { if (Data?.Fecha != value) { Data.Fecha = value; Changed = true; } } }

        public virtual int? IdEmpresa
        {
            get { return Data?.IdEmpresa; }
            set
            {
                if (Data?.IdEmpresa != value)
                {
                    Data.IdEmpresa = value;
                    Changed = true;

                    Empresa = null;
                }
            }
        }

        protected business.Model.Empresa _empresa;
        public virtual business.Model.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    Empresa = new business.Model.Empresa() { Data = Data.Empresa };
                }

                return _empresa;
            }
            set { if (_empresa != value) { _empresa = value; } }
        }

        public virtual business.Model.Sucursal Clear()
        {
            return _logic.Clear(this, Data);
        }
        public virtual (Result result, business.Model.Sucursal business) Load()
        {
            var load = _logic.Load(this, Data);

            return load;
        }
        public virtual (Result result, business.Model.Sucursal business) Save()
        {
            var save = _logic.Save(this, Data);

            SaveDependencies();

            return save;
        }
        public virtual (Result result, business.Model.Sucursal business) Erase()
        {
            EraseDependencies();

            var erase = _logic.Erase(this, Data);

            return erase;
        }

        protected virtual void SaveDependencies()
        {
        }
        protected virtual void EraseDependencies()
        {
        }
    }

    public partial class Sucursales : List<business.Model.Sucursal>
    {
        public virtual data.Model.Sucursales Datas
        {
            get
            {
                return new data.Model.Sucursales().Load(this.Select(x => x.Data).Cast<data.Model.Sucursal>());
            }
        }

        public Sucursales()
        {
        }

        public virtual Sucursales Load(business.Query.Sucursal query, int maxdepth = 1, int top = 0)
        {
            return Load(query.List(maxdepth, top).businesses);
        }
        public virtual Sucursales Load(IEnumerable<business.Model.Sucursal> list)
        {
            this.AddRange(list);

            return this;
        }
    }
}

namespace business.Query
{
    public partial class Sucursal : IQueryState<data.Query.Sucursal>, IQueryLogic<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal>
    {
        public virtual data.Query.Sucursal Data { get; set; } = new data.Query.Sucursal();

        protected readonly ILogic<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal> _logic;

        public Sucursal(ILogic<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal> logic)
        {
            _logic = logic;
        }
        public Sucursal()
            : this(new Logic<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal>(new business.Mapper.Sucursal()))
        {
        }

        private business.Query.Empresa _empresa;
        public virtual business.Query.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    _empresa = new business.Query.Empresa();
                }

                return _empresa;
            }
            set { if (_empresa != value) { _empresa = value; } }
        }

        public virtual (Result result, business.Model.Sucursal business) Retrieve(int maxdepth = 1, business.Model.Sucursal business = default(business.Model.Sucursal))
        {
            return _logic.Retrieve(Data, maxdepth, business);
        }
        public virtual (Result result, IEnumerable<business.Model.Sucursal> businesses) List(int maxdepth = 1, int top = 0, IList<business.Model.Sucursal> businesses = null)
        {
            return _logic.List(Data, maxdepth, top, businesses);
        }
    }
}

namespace business.Mapper
{
    public partial class Sucursal : MapperState<domain.Model.Sucursal, data.Model.Sucursal, business.Model.Sucursal>
    {
        public override business.Model.Sucursal Clear(business.Model.Sucursal business, int maxdepth = 1, int depth = 0)
        {
            business = base.Clear(business, maxdepth, depth);

            //depth++;
            //if (depth < maxdepth || maxdepth == 0)
            //{
            //    business.Empresa = new business.Mapper.Empresa().Clear(business.Empresa, maxdepth, depth);
            //}
            //else
            //{
                business.Empresa = null;
            //}

            return business;
        }
        public override business.Model.Sucursal Map(business.Model.Sucursal business, int maxdepth = 1, int depth = 0)
        {
            business = base.Map(business, maxdepth, depth);

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                business.Empresa = new business.Mapper.Empresa().Map(business.Empresa, maxdepth, depth);
            }

            return business;
        }
    }
}