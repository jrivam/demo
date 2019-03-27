using Library.Impl;
using Library.Impl.Presentation;
using System;
using System.Windows;

namespace WpfApp.Views
{
    public partial class Sucursales : Window
    {
        public Presentation.Table.SucursalesQuery ViewModel
        {
            get
            {
                return (Presentation.Table.SucursalesQuery)DataContext;
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
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalLoad, "SucursalLoad");
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalSave, "SucursalSave");
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalErase, "SucursalErase");

            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalEdit, "SucursalEdit");

            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalesAdd, "SucursalesAdd");
            Messenger.Default.Register<int>(this, SucursalesRefresh, "SucursalesRefresh");

            SucursalesRefresh();
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, "SucursalLoad");
            Messenger.Default.Unregister(this, "SucursalSave");
            Messenger.Default.Unregister(this, "SucursalErase");

            Messenger.Default.Unregister(this, "SucursalEdit");

            Messenger.Default.Unregister(this, "SucursalesAdd");
            Messenger.Default.Unregister(this, "SucursalesRefresh");
        }

        public virtual void SucursalLoad((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.CommandLoad(message);
        }
        public virtual void SucursalSave((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.CommandSave(message);
        }
        public virtual void SucursalErase((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.CommandErase(message);
        }

        public virtual void SucursalEdit(Presentation.Table.Sucursal entity)
        {
            var view = new Views.Sucursal();

            view.ViewModel = entity;

            view.ShowDialog();

            if (view.ViewModel.Domain.Deleted)
                ViewModel.Remove(entity);
            else
                ViewModel.CommandEdit((entity, view.ViewModel));
        }

        public virtual void SucursalesAdd(Presentation.Table.Sucursal entity)
        {
            var view = new Views.Sucursal();

            view.ViewModel.Fecha = DateTime.Now.Date;
            view.ViewModel.Activo = true;

            view.ShowDialog();

            ViewModel.CommandAdd((Presentation.Table.Sucursal)view.ViewModel);
        }
        public virtual void SucursalesRefresh(int top = 0)
        {
            var refresh = ViewModel.Refresh(top);

            ViewModel.CommandRefresh(refresh);
        }
    }
}
