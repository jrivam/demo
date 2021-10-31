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
        public Sucursal(IInteractiveTable<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal> interactivetable, 
            Business.Table.Sucursal domain = null,
            int maxdepth = 1,
            string name = null)
            : base(interactivetable,
                  domain,
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
                    Empresa = AutofacConfiguration.Container.Resolve<Presentation.Table.Empresa>(new TypedParameter(typeof(Business.Table.Empresa), Domain?.Empresa));
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

    public partial class SucursalesEdit : ListModelEdit<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public SucursalesEdit(IListDomainEdit<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal> domains)
            : base(domains)
        {
        }

        public SucursalesEdit(IListDataEdit<Entities.Table.Sucursal, Persistence.Table.Sucursal> datas)
           : this(new Business.Table.SucursalesEdit(datas))
        {
        }
        public SucursalesEdit(ICollection<Entities.Table.Sucursal> entities = null)
           : this(new Persistence.Table.SucursalesEdit(entities ?? new Collection<Entities.Table.Sucursal>()))
        {
        }
    }

    public partial class SucursalesReload : ListModelReload<Presentation.Query.Sucursal, Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public SucursalesReload(Presentation.Query.Sucursal query,
            IListDomainEdit<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal> domains = null,
            int maxdepth = 1, int top = 0)
            : base(query, 
                  domains,                  
                  maxdepth, top)
        {
        }
    }
}

namespace demo.Presentation.Query
{
    public partial class Sucursal : AbstractQueryModel<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal>
    {
        public Sucursal(IInteractiveQuery<Entities.Table.Sucursal, Persistence.Table.Sucursal, Business.Table.Sucursal, Presentation.Table.Sucursal> interactivequery,
            Business.Query.Sucursal domain)
            : base(interactivequery,
                  domain)
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
        public virtual Presentation.Query.Empresa Empresa
        {
            get
            {
                if (_empresa == null)
                {
                    Empresa = AutofacConfiguration.Container.Resolve<Presentation.Query.Empresa>(new TypedParameter(typeof(Persistence.Query.Empresa), ((Persistence.Query.Sucursal)Domain)?.Empresa));
                }

                return _empresa;
            }
            set
            {
                if (_empresa != value)
                {
                    _empresa = value;
                }
            }
        }
    }
}