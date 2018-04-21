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
        protected readonly ILogic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal> _logic;

        public virtual data.Model.Sucursal Data { get; protected set; } = new data.Model.Sucursal();

        public Sucursal(ILogic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal> logic)
        {
            _logic = logic;
        }
        public Sucursal()
            : this(new Logic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>(new domain.Mapper.Sucursal()))
        {
        }
        public Sucursal(data.Model.Sucursal data)
            : this()
        {
            Data = data;
        }
        public Sucursal(entities.Model.Sucursal entity)
            : this(new data.Model.Sucursal(entity))
        {
        }

        public virtual bool Changed { get; set; }
        public virtual bool Deleted { get; set; }

        public virtual int? Id { get { return Data?.Id; } set { if (Data?.Id != value) { Data.Id = value; Changed = true; } } }
        public virtual string Nombre { get { return Data?.Nombre; } set { if (Data?.Nombre != value) { Data.Nombre = value; Changed = true; } } }
        public virtual DateTime? Fecha { get { return Data?.Fecha; } set { if (Data?.Fecha != value) { Data.Fecha = value; Changed = true; } } }
        public virtual bool? Activo { get { return Data?.Activo; } set { if (Data?.Activo != value) { Data.Activo = value; Changed = true; } } }

        public virtual int? IdEmpresa
        {
            get
            {
                return Data?.IdEmpresa;
            }
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

        public domain.Model.Empresa Empresa_Load()
        {
            if (this.IdEmpresa != null)
            {
                Empresa = new domain.Model.Empresa(Data.Empresa);
            }

            return _empresa;
        }
        protected domain.Model.Empresa _empresa;
        public virtual domain.Model.Empresa Empresa
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

                    Data.Empresa = _empresa?.Data;
                }
            }
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

                    Empresas = new domain.Model.Empresas().Load(query);
                }

                return _empresas;
            }
            set
            {
                if (_empresas != value)
                {
                    _empresas = value;

                    Data.Empresas = _empresas?.Datas;
                }
            }
        }

        public virtual domain.Model.Sucursal Clear()
        {
            return _logic.Clear(this, Data);
        }

        public virtual (Result result, domain.Model.Sucursal domain) Load(bool usedbcommand = false)
        {
            var load = _logic.Load(this, Data, usedbcommand);

            return load;
        }
        public virtual (Result result, domain.Model.Sucursal domain) Save(bool useinsertdbcommand = false, bool useupdatedbcommand = false)
        {
            var save = _logic.Save(this, Data, useinsertdbcommand, useupdatedbcommand);

            SaveDependencies();

            return save;
        }
        public virtual (Result result, domain.Model.Sucursal domain) Erase(bool usedbcommand = false)
        {
            EraseDependencies();

            var erase = _logic.Erase(this, Data, usedbcommand);

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

        public virtual domain.Model.Sucursales Load(domain.Query.Sucursal query, int maxdepth = 1, int top = 0)
        {
            return Load(query.List(maxdepth, top).domains);
        }
        public virtual domain.Model.Sucursales Load(IEnumerable<domain.Model.Sucursal> list)
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
        protected readonly ILogic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal> _logic;

        public virtual data.Query.Sucursal Data { get; protected set; } = new data.Query.Sucursal();

        public Sucursal(ILogic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal> logic)
        {
            _logic = logic;
        }
        public Sucursal()
            : this(new Logic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>(new domain.Mapper.Sucursal()))
        {
        }
        public Sucursal(data.Query.Sucursal data)
            : this()
        {
            Data = data;
        }

        protected domain.Query.Empresa _empresa;
        public virtual domain.Query.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    Empresa = new domain.Query.Empresa();
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