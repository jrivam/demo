using library.Impl;
using library.Impl.Domain;
using library.Interface.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace domain.Model
{
    public partial class Sucursal : IEntityState<entities.Model.Sucursal, data.Model.Sucursal>, IEntityLogic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>
    {
        public virtual data.Model.Sucursal Data { get; set; } = new data.Model.Sucursal();

        protected readonly ILogic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal> _logic;

        public Sucursal(ILogic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal> logic)
        {
            _logic = logic;
        }
        public Sucursal()
            : this(new Logic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>(new domain.Mapper.Sucursal()))
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

        protected domain.Model.Empresa _empresa;
        public virtual domain.Model.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    Empresa = new domain.Model.Empresa() { Data = Data.Empresa };
                }

                return _empresa;
            }
            set { if (_empresa != value) { _empresa = value; } }
        }

        protected domain.Model.Empresas _empresas;
        public virtual domain.Model.Empresas Empresas
        {
            get
            {
                if (_empresas == null)
                {
                    var query = new domain.Query.Empresa();
                    query.Data["Activo"]?.Where(true);

                    _empresas = new domain.Model.Empresas().Load(query);
                }

                return _empresas;
            }
        }

        public virtual domain.Model.Sucursal Clear()
        {
            return _logic.Clear(this, Data);
        }
        public virtual (Result result, domain.Model.Sucursal domain) Load()
        {
            var load = _logic.Load(this, Data);

            return load;
        }
        public virtual (Result result, domain.Model.Sucursal domain) Save()
        {
            var save = _logic.Save(this, Data);

            SaveDependencies();

            return save;
        }
        public virtual (Result result, domain.Model.Sucursal domain) Erase()
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

    public partial class Sucursales : List<domain.Model.Sucursal>
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

        public virtual Sucursales Load(domain.Query.Sucursal query, int maxdepth = 1, int top = 0)
        {
            return Load(query.List(maxdepth, top).domains);
        }
        public virtual Sucursales Load(IEnumerable<domain.Model.Sucursal> list)
        {
            this.AddRange(list);

            return this;
        }
    }
}

namespace domain.Query
{
    public partial class Sucursal : IQueryState<data.Query.Sucursal>, IQueryLogic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>
    {
        public virtual data.Query.Sucursal Data { get; set; } = new data.Query.Sucursal();

        protected readonly ILogic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal> _logic;

        public Sucursal(ILogic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal> logic)
        {
            _logic = logic;
        }
        public Sucursal()
            : this(new Logic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>(new domain.Mapper.Sucursal()))
        {
        }

        private domain.Query.Empresa _empresa;
        public virtual domain.Query.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    _empresa = new domain.Query.Empresa();
                }

                return _empresa;
            }
            set { if (_empresa != value) { _empresa = value; } }
        }

        public virtual (Result result, domain.Model.Sucursal domain) Retrieve(int maxdepth = 1, domain.Model.Sucursal domain = default(domain.Model.Sucursal))
        {
            return _logic.Retrieve(Data, maxdepth, domain);
        }
        public virtual (Result result, IEnumerable<domain.Model.Sucursal> domains) List(int maxdepth = 1, int top = 0, IList<domain.Model.Sucursal> domains = null)
        {
            return _logic.List(Data, maxdepth, top, domains);
        }
    }
}

namespace domain.Mapper
{
    public partial class Sucursal : MapperState<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>
    {
        public override domain.Model.Sucursal Clear(domain.Model.Sucursal domain)
        {
            domain = base.Clear(domain);

            domain.Empresa = null;

            return domain;
        }
        public override domain.Model.Sucursal Map(domain.Model.Sucursal domain)
        {
            domain = base.Map(domain);

            return domain;
        }
    }
}