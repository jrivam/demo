using library.Impl;
using library.Impl.Presentation;
using System;
using System.Windows;

namespace WpfApp.Views
{
    public partial class Empresa : Window
    {
        public presentation.Model.Empresa ViewModel
        {
            get
            {
                return (presentation.Model.Empresa)DataContext;
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
            Messenger.Default.Register<presentation.Model.Sucursal>(this, SucursalesAdd, "SucursalesAdd");
            Messenger.Default.Register<int>(this, SucursalesRefresh, "SucursalesRefresh");

            Messenger.Default.Register<presentation.Model.Sucursal>(this, SucursalEdit, "SucursalEdit");
            Messenger.Default.Register<(CommandAction action, (Result result, presentation.Model.Sucursal entity) operation)>(this, SucursalErase, "SucursalErase");

            Messenger.Default.Register<(CommandAction action, (Result result, presentation.Model.Empresa entity) operation)>(this, EmpresaErase, "EmpresaErase");

            SucursalesRefresh();
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

        public virtual void EmpresaErase((CommandAction action, (Result result, presentation.Model.Empresa entity) operation) message)
        {
            this.Close();
        }

        public virtual void SucursalesAdd(presentation.Model.Sucursal entity)
        {
            var view = new Views.Sucursal();

            view.ViewModel.IdEmpresa = ViewModel.Id;
            view.ViewModel.Fecha = DateTime.Now.Date;
            view.ViewModel.Activo = true;

            view.ShowDialog();

            if (view.ViewModel.IdEmpresa == ViewModel.Id)
                ViewModel.Sucursales.CommandAdd((presentation.Model.Sucursal)view.ViewModel);
        }
        public virtual void SucursalesRefresh(int top = 0)
        {
            var refresh = ViewModel.Sucursales_Refresh(top);

            ViewModel.Sucursales.CommandRefresh(refresh);
        }

        public virtual void SucursalEdit(presentation.Model.Sucursal entity)
        {
            var view = new Views.Sucursal();

            view.ViewModel = entity;

            var idempresa = entity.IdEmpresa;

            view.ShowDialog();

            if (view.ViewModel.Domain.Deleted || (!view.ViewModel.Domain.Changed && entity.IdEmpresa != idempresa))
            {
                ViewModel.Sucursales.Remove(entity);
            }
            else
            {
                ViewModel.Sucursales.CommandEdit((entity, view.ViewModel));
            }
        }
        public virtual void SucursalErase((CommandAction action, (Result result, presentation.Model.Sucursal entity) operation) message)
        {
            ViewModel.Sucursales.CommandErase(message);
        }
    }
}
