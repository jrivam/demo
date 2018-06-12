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
            Messenger.Default.Register<presentation.Model.Sucursal>(this, SucursalAdd, "SucursalAdd");
            Messenger.Default.Register<(presentation.Model.Sucursal oldvalue, presentation.Model.Sucursal newvalue)>(this, SucursalEdit, "SucursalEdit");
            Messenger.Default.Register<(CommandAction action, (Result result, presentation.Model.Sucursal entity) operation)>(this, SucursalErase, "SucursalErase");

            Messenger.Default.Register<(CommandAction action, (Result result, presentation.Model.Empresa entity) operation)>(this, EmpresaErase, "EmpresaErase");
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, "SucursalAdd");
            Messenger.Default.Unregister(this, "SucursalEdit");
            Messenger.Default.Unregister(this, "SucursalErase");

            Messenger.Default.Unregister(this, "EmpresaErase");

            //ViewModel.Dispose();
        }

        public virtual void EmpresaErase((CommandAction action, (Result result, presentation.Model.Empresa entity) operation) message)
        {
            this.Close();
        }

        public virtual void SucursalAdd(presentation.Model.Sucursal entity)
        {
            var view = new Views.Sucursal();

            view.ViewModel.IdEmpresa = ViewModel.Id;
            view.ViewModel.Fecha = DateTime.Now.Date;
            view.ViewModel.Activo = true;

            view.ShowDialog();

            if (view.ViewModel.IdEmpresa == ViewModel.Id)
                ViewModel.Sucursales.CommandAdd((presentation.Model.Sucursal)view.ViewModel);
        }
        public virtual void SucursalEdit((presentation.Model.Sucursal oldvalue, presentation.Model.Sucursal newvalue) message)
        {
            var view = new Views.Sucursal();

            view.ViewModel = message.oldvalue;

            view.ShowDialog();

            if (view.ViewModel.Domain.Deleted || message.oldvalue.IdEmpresa != message.newvalue.IdEmpresa)
            {
                ViewModel.Sucursales.Remove(message.oldvalue);
            }
            else
            {
                ViewModel.Sucursales.CommandEdit((message.oldvalue, view.ViewModel));
            }
        }
        public virtual void SucursalErase((CommandAction action, (Result result, presentation.Model.Sucursal entity) operation) message)
        {
            ViewModel.Sucursales.CommandErase(message);
        }
    }
}
