﻿using Autofac;
using jrivam.Library;
using jrivam.Library.Impl.Persistence.Sql;
using jrivam.Library.Impl.Presentation;

namespace demo.App.Wpf.ViewModels
{
    public class SucursalesViewModel : NotifyPropertyChanged
    {
        protected Presentation.Table.SucursalesQuery _sucursalesquery;
        public Presentation.Table.SucursalesQuery SucursalesQuery
        {
            get
            {
                return _sucursalesquery;
            }
            set
            {
                if (_sucursalesquery != value)
                {
                    _sucursalesquery = value;
                    OnPropertyChanged(nameof(SucursalesQuery));
                }
            }
        }

        protected Presentation.Table.EmpresasQuery _empresas;
        public Presentation.Table.EmpresasQuery Empresas
        {
            get
            {
                if (_empresas == null)
                {
                    Empresas = AutofacConfiguration.Container.Resolve<Presentation.Table.EmpresasQuery>();
                    _empresas.Query.Activo = (true, WhereOperator.Equals);

                    _empresas.Refresh();
                }

                return _empresas;
            }
            set
            {
                if (_empresas != value)
                {
                    _empresas = value;
                    OnPropertyChanged(nameof(Empresas));
                }
            }
        }

        public SucursalesViewModel(Presentation.Table.SucursalesQuery sucursalesquery)
            : base()
        {
            SucursalesQuery = sucursalesquery;
            SucursalesQuery.Refresh();
        }

        public SucursalesViewModel()
            : this(AutofacConfiguration.Container.Resolve<Presentation.Table.SucursalesQuery>(new NamedParameter("maxdepth", 2)))
        {
        }
    }
}
