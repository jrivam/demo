using library.Impl.Domain.Mapper;
using library.Impl.Domain.Model;
using library.Impl.Domain.Query;
using library.Interface.Domain.Model;
using library.Interface.Domain.Query;
using System;
using System.Collections.Generic;
using System.Linq;

namespace domain.Model
{
    public partial class Sucursal : AbstractEntityLogic<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>
    {
        public Sucursal(ILogicState<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal> logic)
            : base(logic)
        {
        }
        public Sucursal()
            : this(new LogicState<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>(new domain.Mapper.Sucursal()))
        {
        }
        public Sucursal(data.Model.Sucursal data)
            : this()
        {
            Data = data;
        }
        public Sucursal(entities.Model.Sucursal entity)
            : this()
        {
            SetProperties(entity);
        }

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
    public partial class Sucursal : AbstractQueryLogic<data.Query.Sucursal, entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>
    {
        public Sucursal(ILogicQuery<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal> logic)
            : base(logic)
        {
        }
        public Sucursal()
            : this(new LogicQuery<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>(new domain.Mapper.Sucursal()))
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
    }
}

namespace domain.Mapper
{
    public partial class Sucursal : AbstractMapperState<entities.Model.Sucursal, data.Model.Sucursal, domain.Model.Sucursal>
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