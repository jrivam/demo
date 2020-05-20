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
        public Empresa(Presentation.Table.Empresa entity)
            : this()
        {
            ViewModel.Empresa = entity;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Empresa entity) operation)>(this, EmpresaErase, "EmpresaErase");

            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalSave, "SucursalSave");
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalErase, "SucursalErase");
            
            Messenger.Default.Register<(Result result, Presentation.Table.Sucursales list)>(this, SucursalesRefresh, "SucursalesRefresh");

            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalesAdd, "SucursalesAdd");
            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalEdit, "SucursalEdit");
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, "EmpresaErase");

            Messenger.Default.Unregister(this, "SucursalSave");
            Messenger.Default.Unregister(this, "SucursalErase");

            Messenger.Default.Unregister(this, "SucursalesRefresh");

            Messenger.Default.Unregister(this, "SucursalesAdd");
            Messenger.Default.Unregister(this, "SucursalEdit");

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

        public virtual void SucursalesRefresh((Result result, Presentation.Table.Sucursales list) message)
        {
            ViewModel.Empresa.Sucursales.CommandRefresh(message);
        }

        public virtual void SucursalesAdd(Presentation.Table.Sucursal entity)
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
