using Autofac;
using jrivam.Library;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Presentation;
using jrivam.Library.Impl.Presentation.Attributes;
using jrivam.Library.Impl.Presentation.Query;
using jrivam.Library.Impl.Presentation.Table;
using jrivam.Library.Interface.Business;
using jrivam.Library.Interface.Persistence;
using jrivam.Library.Interface.Presentation.Query;
using jrivam.Library.Interface.Presentation.Table;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace demo.Presentation.Table
{
    public partial class Sucursal : AbstractTableModel<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public Sucursal(IInteractiveTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal> interactivetable = null, 
            Presentation.Query.Sucursal query = null, 
            Business.Table.Sucursal domain = null,
            int maxdepth = 1,
            string name = null)
            : base(interactivetable ?? AutofacConfiguration.Container.Resolve<IInteractiveTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>>(),
                  query ?? new Presentation.Query.Sucursal(),
                  domain ?? new Business.Table.Sucursal(),
                  maxdepth,
                  name)
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
                    Empresa = new Presentation.Table.Empresa(domain: Domain?.Empresa);
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

    public partial class SucursalesQuery : ListModelQuery<Presentation.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
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

namespace demo.Presentation.Query
{
    public partial class Sucursal : AbstractQueryModel<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public Sucursal(IInteractiveQuery<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal> interactivequery = null,
            Business.Query.Sucursal domain = null)
            : base(interactivequery ?? AutofacConfiguration.Container.Resolve<IInteractiveQuery<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>>(),
                  domain ?? new Business.Query.Sucursal())
        {
        }

        public virtual (int? value, WhereOperator? sign) Id
        {
            set
            {
                ((Business.Query.Sucursal)Domain).Id = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Codigo
        {
            set
            {
                ((Business.Query.Sucursal)Domain).Codigo = (value.value, value.sign);
            }
        }
        public virtual (string value, WhereOperator? sign) Nombre
        {
            set
            {
                ((Business.Query.Sucursal)Domain).Nombre = (value.value, value.sign);
            }
        }
        public virtual (DateTime? value, WhereOperator? sign) Fecha
        {
            set
            {
                ((Business.Query.Sucursal)Domain).Fecha = (value.value, value.sign);
            }
        }
        public virtual (bool? value, WhereOperator? sign) Activo
        {
            set
            {
                ((Business.Query.Sucursal)Domain).Activo = (value.value, value.sign);
            }
        }
        public virtual (int? value, WhereOperator? sign) IdEmpresa
        {
            set
            {
                ((Business.Query.Sucursal)Domain).IdEmpresa = (value.value, value.sign);
            }
        }

        protected Presentation.Query.Empresa _empresa;
        public virtual Presentation.Query.Empresa Empresa(Presentation.Query.Empresa query = null)
        {
            return _empresa = query ?? _empresa ?? new Presentation.Query.Empresa(domain: ((Business.Query.Sucursal)Domain)?.Empresa());
        }
    }
}