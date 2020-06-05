using demo.App.Wpf.ViewModels;
using jrivam.Library.Impl;
using jrivam.Library.Impl.Presentation;
using System;
using System.Windows;

namespace demo.App.Wpf.Views
{
    public partial class Sucursales : Window
    {
        public SucursalesViewModel ViewModel
        {
            get
            {
                return (SucursalesViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public Sucursales()
        {
            InitializeComponent();
            DataContext = new SucursalesViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalLoad, nameof(SucursalLoad));
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalSave, nameof(SucursalSave));
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalErase, nameof(SucursalErase));
            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalEdit, nameof(SucursalEdit));

            Messenger.Default.Register<(Result result, Presentation.Table.Sucursales list)>(this, SucursalesQueryRefresh, nameof(SucursalesQueryRefresh));
            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalesQueryAdd, nameof(SucursalesQueryAdd));
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, nameof(SucursalLoad));
            Messenger.Default.Unregister(this, nameof(SucursalSave));
            Messenger.Default.Unregister(this, nameof(SucursalErase));
            Messenger.Default.Unregister(this, nameof(SucursalEdit));

            Messenger.Default.Unregister(this, nameof(SucursalesQueryRefresh));
            Messenger.Default.Unregister(this, nameof(SucursalesQueryAdd));
        }

        public virtual void SucursalLoad((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.SucursalesQuery.CommandLoad(message);
        }
        public virtual void SucursalSave((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.SucursalesQuery.CommandSave(message);
        }
        public virtual void SucursalErase((CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation) message)
        {
            ViewModel.SucursalesQuery.CommandErase(message);
        }

        public virtual void SucursalesQueryRefresh((Result result, Presentation.Table.Sucursales list) message)
        {
            ViewModel.SucursalesQuery.CommandRefresh(message);
        }

        public virtual void SucursalesQueryAdd(Presentation.Table.Sucursal entity)
        {
            var view = new Views.Sucursal();
            view.ViewModel.Sucursal.Fecha = DateTime.Now.Date;
            view.ViewModel.Sucursal.Activo = true;
            view.ShowDialog();

            ViewModel.SucursalesQuery.ItemAdd(view.ViewModel.Sucursal);
        }
        public virtual void SucursalEdit(Presentation.Table.Sucursal entity)
        {
            var view = new Views.Sucursal(entity);
            view.ShowDialog();

            ViewModel.SucursalesQuery.ItemEdit(entity, view.ViewModel.Sucursal);
        }
    }
}
