﻿using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Presentation;
using jrivam.Library.Impl.Presentation.Attributes;
using jrivam.Library.Impl.Presentation.Query;
using jrivam.Library.Impl.Presentation.Raiser;
using jrivam.Library.Impl.Presentation.Table;
using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Presentation.Query;
using jrivam.Library.Interface.Presentation.Table;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Presentation.Table
{
    public partial class Sucursal : AbstractTableModel<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public Sucursal(Business.Table.Sucursal domain,
            IInteractiveTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal> interactive = null,
            int maxdepth = 1)
            : base(domain,
                  interactive ?? new InteractiveTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>(new Presentation.Raiser.Sucursal()),
                maxdepth)
        {
        }

        public Sucursal(Persistence.Table.Sucursal data,
            IInteractiveTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal> interactive = null,
            int maxdepth = 1)
            : this(new Business.Table.Sucursal(data),
                  interactive ?? new InteractiveTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>(new Presentation.Raiser.Sucursal()),
                maxdepth)
        {
        }
        public Sucursal(Entities.Table.Sucursal entity,
            IInteractiveTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal> interactive = null,
            int maxdepth = 1)
            : this(new Persistence.Table.Sucursal(entity),
                  interactive ?? new InteractiveTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>(new Presentation.Raiser.Sucursal()),
                maxdepth)
        {
        }
        public Sucursal(IInteractiveTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal> interactive = null,
                int maxdepth = 1)
            : this(new Entities.Table.Sucursal(),
                  interactive ?? new InteractiveTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>(new Presentation.Raiser.Sucursal()),
                maxdepth)
        {
        }

        [Model]
        public virtual int? Id { get { return Domain?.Id; } set { if (Domain?.Id != value) { Domain.Id = value; OnPropertyChanged(nameof(Id)); } } }
        [Model]
        public virtual string Codigo { get { return Domain?.Codigo; } set { if (Domain?.Codigo != value) { Domain.Codigo = value; OnPropertyChanged(nameof(Codigo)); } } }
        [Model]
        public virtual string Nombre { get { return Domain?.Nombre; } set { if (Domain?.Nombre != value) { Domain.Nombre = value; OnPropertyChanged(nameof(Nombre)); } } }
        [Model]
        public virtual DateTime? Fecha { get { return Domain?.Fecha; } set { if (Domain?.Fecha != value) { Domain.Fecha = value; OnPropertyChanged(nameof(Fecha)); } } }
        [Model]
        public virtual bool? Activo { get { return Domain?.Activo; } set { if (Domain?.Activo != value) { Domain.Activo = value; OnPropertyChanged(nameof(Activo)); } } }

        [Model]
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
                    Domain.IdEmpresa = value;
                    OnPropertyChanged(nameof(IdEmpresa));

                    Empresa = null;
                }
            }
        }
        protected Presentation.Table.Empresa _empresa;
        [Model]
        public virtual Presentation.Table.Empresa Empresa
        {
            get
            {
                if (_empresa?.Id != this.IdEmpresa)
                {
                    Empresa = new Presentation.Table.Empresa(Domain?.Empresa);
                }

                _empresa.Id = this.IdEmpresa;

                return _empresa;
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;

                    OnPropertyChanged(nameof(Empresa));
                }
            }
        }
    }

    public partial class Sucursales : ListModel<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public Sucursales(IListDomain<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal> domains)
            : base(domains)
        {
        }

        public Sucursales(IListData<Entities.Table.Sucursal, Persistence.Table.Sucursal> datas)
           : this(new Business.Table.Sucursales(datas))
        {
        }
        public Sucursales(ICollection<Entities.Table.Sucursal> entities = null)
           : this(new Persistence.Table.Sucursales(entities ?? new Collection<Entities.Table.Sucursal>()))
        {
        }
    }

    public partial class SucursalesQuery : ListModelQuery<Presentation.Query.Sucursal, Business.Query.Sucursal, Persistence.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public SucursalesQuery(IListDomain<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal> domains, 
            Presentation.Query.Sucursal query = null, 
            int maxdepth = 1, int top = 0)
            : base(domains, 
                  query ?? new Presentation.Query.Sucursal(),
                  maxdepth, top)
        {
        }

        public SucursalesQuery(IListData<Entities.Table.Sucursal, Persistence.Table.Sucursal> datas, 
            Presentation.Query.Sucursal query = null,
            int maxdepth = 1)
            : this(new Business.Table.Sucursales(datas), 
                  query ?? new Presentation.Query.Sucursal(),
                 maxdepth)
        {
        }
        public SucursalesQuery(ICollection<Entities.Table.Sucursal> entities = null, 
            Presentation.Query.Sucursal query = null,
            int maxdepth = 1)
            : this(new Persistence.Table.Sucursales(entities ?? new Collection<Entities.Table.Sucursal>()), 
                  query ?? new Presentation.Query.Sucursal(),
                 maxdepth)
        {
        }
    }
}

namespace Presentation.Query
{
    public partial class Sucursal : AbstractQueryModel<Presentation.Query.Sucursal, Business.Query.Sucursal, Persistence.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public Sucursal(Business.Query.Sucursal domain = null,
            IInteractiveQuery<Business.Query.Sucursal, Persistence.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal> interactive = null)
            : base(domain ?? new Business.Query.Sucursal(),
                  interactive ?? new InteractiveQuery<Presentation.Query.Sucursal, Business.Query.Sucursal, Persistence.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>(new Presentation.Raiser.Sucursal()))
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                Domain.Id = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Codigo
        {
            set
            {
                Domain.Codigo = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Nombre
        {
            set
            {
                Domain.Nombre = (value.value, value.sign);
            }
        }
        public virtual (DateTime? value, WhereOperator? sign) Fecha
        {
            set
            {
                Domain.Fecha = (value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                Domain.Activo = (value.value, value.sign);
            }
        }
        public virtual (int? value, WhereOperator? sign) IdEmpresa
        {
            set
            {
                Domain.IdEmpresa = (value.value, value.sign);
            }
        }

        protected Presentation.Query.Empresa _empresa;
        public virtual Presentation.Query.Empresa Empresa(Presentation.Query.Empresa query = null)
        {
            return _empresa = query ?? _empresa ?? new Presentation.Query.Empresa(Domain?.Empresa());
        }
    }
}

namespace Presentation.Raiser
{
    public partial class Sucursal : BaseRaiser<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public override void Raise(Presentation.Table.Sucursal model, int maxdepth = 1, int depth = 0)
        {
            base.Raise(model);

            depth++;
            if (depth < maxdepth || maxdepth == 0)
            {
                new Presentation.Raiser.Empresa().Raise(model.Empresa, maxdepth, depth);
            }
        }
    }
}