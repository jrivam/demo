﻿using jrivam.Library.Impl.Presentation;

namespace demo.App.Wpf.ViewModels
{
    public class EmpresaViewModel : NotifyPropertyChanged
    {
        protected Presentation.Table.Empresa _empresa;
        public Presentation.Table.Empresa Empresa
        {
            get
            {
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

        public EmpresaViewModel(Presentation.Table.Empresa empresa)
            : base()
        {
            Empresa = empresa;
        }

        public EmpresaViewModel()
            : this(new Presentation.Table.Empresa())
        {
        }
    }
}
