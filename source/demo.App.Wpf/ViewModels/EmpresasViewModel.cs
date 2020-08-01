﻿using Autofac;
using jrivam.Library;
using jrivam.Library.Impl.Presentation;

namespace demo.App.Wpf.ViewModels
{
    public class EmpresasViewModel : NotifyPropertyChanged
    {
        protected Presentation.Table.EmpresasReload _empresas;
        public Presentation.Table.EmpresasReload Empresas
        {
            get
            {
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

        public EmpresasViewModel(Presentation.Table.EmpresasReload empresas)
            : base()
        {
            Empresas = empresas;
            Empresas.Refresh();
        }

        public EmpresasViewModel()
            : this(AutofacConfiguration.Container.Resolve<Presentation.Table.EmpresasReload>())
        {
        }
    }
}
