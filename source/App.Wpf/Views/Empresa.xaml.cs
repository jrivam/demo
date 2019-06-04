using Library.Impl;
using Library.Impl.Presentation;
using System;
using System.Windows;
using WpfApp.ViewModels;

namespace WpfApp.Views
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

        public Empresa()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalesAdd, "SucursalesAdd");
            Messenger.Default.Register<int>(this, SucursalesRefresh, "SucursalesRefresh");

            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalEdit, "SucursalEdit");
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalErase, "SucursalErase");

            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Empresa entity) operation)>(this, EmpresaErase, "EmpresaErase");

            //SucursalesRefresh();
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, "SucursalesAdd");
            Messenger.Default.Unregister(this, "SucursalesRefresh");

            Messenger.Default.Unregister(this, "SucursalEdit");
            Messenger.Default.Unregister(this, "SucursalErase");

            Messenger.Default.Unregister(this, "EmpresaErase");

            //ViewModel.Dispose();
        }

        public virtual void EmpresaErase((CommandAction action, (Result result, Presentation.Table.Empresa entity) operation) message)
        {
            this.Close();
        }

        public virtual void SucursalesAdd(Presentation.Table.Sucursal entity)
        {
            var view = new Views.Sucursal();

            view.ViewModel.Sucursal.IdEmpresa = ViewModel.Empresa.Id;
            view.ViewModel.Sucursal.Fecha = DateTime.Now.Date;
            view.ViewModel.Sucursal.Activo = true;

            view.ShowDialog();

            if (view.ViewModel.Sucursal.IdEmpresa == ViewModel.Empresa.Id)
                ViewModel.Empresa.Sucursales.CommandAdd(view.ViewModel.Sucursal);
        }
        public virtual void SucursalesRefresh(int top = 0)
        {
            var refresh = ViewModel.Empresa.Sucursales_Refresh(top);

            ViewModel.Empresa.Sucursales.CommandRefresh(refresh);
        }

        public virtual void SucursalEdit(Presentation.Table.Sucursal entity)
        {
            var view = new Views.Sucursal();

            view.ViewModel.Sucursal = entity;

            var idempresa = entity.IdEmpresa;

            view.ShowDialog();

            if (view.ViewModel.Sucursal.Domain.Deleted || (!view.ViewModel.Sucursal.Domain.Changed && entity.IdEmpresa != idempresa))
            {
                ViewModel.Empresa.Sucursales.Remove(entity);
            }
            else
            {
                ViewModel.Empresa.Sucursales.CommandEdit((entity, view.ViewModel.Sucursal));
            }
        }
        public virtual void SucursalErase((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.Empresa.Sucursales.CommandErase(message);
        }
    }
}
