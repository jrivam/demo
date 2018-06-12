using library.Impl;
using library.Impl.Presentation;
using System;
using System.Windows;

namespace WpfApp.Views
{
    public partial class Sucursales : Window
    {
        public presentation.Model.Sucursales ViewModel
        {
            get
            {
                return (presentation.Model.Sucursales)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public Sucursales()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<(CommandAction action, (Result result, presentation.Model.Sucursal entity) operation)>(this, SucursalLoad, "SucursalLoad");
            Messenger.Default.Register<(CommandAction action, (Result result, presentation.Model.Sucursal entity) operation)>(this, SucursalSave, "SucursalSave");
            Messenger.Default.Register<(CommandAction action, (Result result, presentation.Model.Sucursal entity) operation)>(this, SucursalErase, "SucursalErase");

            Messenger.Default.Register<presentation.Model.Sucursal>(this, SucursalAdd, "SucursalAdd");
            Messenger.Default.Register<(presentation.Model.Sucursal oldvalue, presentation.Model.Sucursal newvalue)>(this, SucursalEdit, "SucursalEdit");
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, "SucursalLoad");
            Messenger.Default.Unregister(this, "SucursalSave");
            Messenger.Default.Unregister(this, "SucursalErase");

            Messenger.Default.Unregister(this, "SucursalAdd");
            Messenger.Default.Unregister(this, "SucursalEdit");
        }

        public virtual void SucursalLoad((CommandAction action, (Result result, presentation.Model.Sucursal entity) operation) message)
        {
            ViewModel.CommandLoad(message);
        }
        public virtual void SucursalSave((CommandAction action, (Result result, presentation.Model.Sucursal entity) operation) message)
        {
            ViewModel.CommandSave(message);
        }
        public virtual void SucursalErase((CommandAction action, (Result result, presentation.Model.Sucursal entity) operation) message)
        {
            ViewModel.CommandErase(message);
        }

        public virtual void SucursalAdd(presentation.Model.Sucursal entity)
        {
            var view = new Views.Sucursal();

            view.ViewModel.Fecha = DateTime.Now.Date;
            view.ViewModel.Activo = true;

            view.ShowDialog();

            ViewModel.CommandAdd((presentation.Model.Sucursal)view.ViewModel);
        }
        public virtual void SucursalEdit((presentation.Model.Sucursal oldvalue, presentation.Model.Sucursal newvalue) message)
        {
            var view = new Views.Sucursal();

            view.ViewModel = message.oldvalue;

            view.ShowDialog();

            if (view.ViewModel.Domain.Deleted)
                ViewModel.Remove(message.oldvalue);
            else
                ViewModel.CommandEdit((message.oldvalue, view.ViewModel));
        }
    }
}
