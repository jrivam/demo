using Autofac;
using demo.App.Wpf.ViewModels;
using jrivam.Library;
using jrivam.Library.Impl;
using jrivam.Library.Impl.Presentation;
using System;
using System.Windows;

namespace demo.App.Wpf.Views
{
    public partial class Empresa : Window
    {
        public EmpresaViewModel ViewModel
        {
            get
            {
                return (EmpresaViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }
        //public presentation.Model.Empresa ViewModel
        //{
        //    get { return (presentation.Model.Empresa)Resources["ViewModel"]; }
        //    set { Resources["ViewModel"] = value; }
        //}

        public Empresa(Presentation.Table.Empresa entity = null)
        {
            InitializeComponent();
            DataContext = new EmpresaViewModel(entity ?? AutofacConfiguration.Container.Resolve<Presentation.Table.Empresa>());

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Empresa entity) operation)>(this, EmpresaErase, nameof(EmpresaErase));

            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalSave, nameof(SucursalSave));
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalErase, nameof(SucursalErase));
            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalEdit, nameof(SucursalEdit));

            Messenger.Default.Register<(Result result, Presentation.Table.Sucursales list)>(this, SucursalesQueryRefresh, nameof(SucursalesQueryRefresh));
            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalesQueryAdd, nameof(SucursalesQueryAdd));
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, nameof(EmpresaErase));

            Messenger.Default.Unregister(this, nameof(SucursalSave));
            Messenger.Default.Unregister(this, nameof(SucursalErase));
            Messenger.Default.Unregister(this, nameof(SucursalEdit));

            Messenger.Default.Unregister(this, nameof(SucursalesQueryRefresh));
            Messenger.Default.Unregister(this, nameof(SucursalesQueryAdd));

            //ViewModel.Dispose();
        }

        public virtual void EmpresaErase((CommandAction action, (Result result, Presentation.Table.Empresa entity) operation) message)
        {
            this.Close();
        }

        public virtual void SucursalSave((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.Empresa.Sucursales.CommandSave(message);

            if (message.operation.result.Success)
                if (message.operation.entity.IdEmpresa != ViewModel.Empresa.Id)
                    ViewModel.Empresa.Sucursales.ItemRemove(message.operation.entity);
        }
        public virtual void SucursalErase((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.Empresa.Sucursales.CommandErase(message);
        }

        public virtual void SucursalesQueryRefresh((Result result, Presentation.Table.Sucursales list) message)
        {
            ViewModel.Empresa.Sucursales.CommandRefresh(message);
        }

        public virtual void SucursalesQueryAdd(Presentation.Table.Sucursal entity)
        {
            var view = new Views.Sucursal();
            view.ViewModel.Sucursal.IdEmpresa = ViewModel.Empresa.Id;
            view.ViewModel.Sucursal.Fecha = DateTime.Now.Date;
            view.ViewModel.Sucursal.Activo = true;
            view.ShowDialog();

            //view.ViewModel.Sucursal.Id != null && 
            if (view.ViewModel.Sucursal.IdEmpresa == ViewModel.Empresa.Id)
                ViewModel.Empresa.Sucursales.ItemAdd(view.ViewModel.Sucursal);
        }
        public virtual void SucursalEdit(Presentation.Table.Sucursal entity)
        {
            var view = new Views.Sucursal(entity);
            view.ShowDialog();

            if (view.ViewModel.Sucursal.IdEmpresa == ViewModel.Empresa.Id)
                ViewModel.Empresa.Sucursales.ItemEdit(entity, view.ViewModel.Sucursal);
            else if (!view.ViewModel.Sucursal.Domain.Changed)
                ViewModel.Empresa.Sucursales.ItemRemove(entity);
        }
    }
}
