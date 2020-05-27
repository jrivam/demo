﻿using demo.App.Wpf.ViewModels;
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
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalLoad, "SucursalLoad");
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalSave, "SucursalSave");
            Messenger.Default.Register<(CommandAction action, (Result result, Presentation.Table.Sucursal entity) operation)>(this, SucursalErase, "SucursalErase");
            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalEdit, "SucursalEdit");

            Messenger.Default.Register<(Result result, Presentation.Table.Sucursales list)>(this, SucursalesRefresh, "SucursalesQueryRefresh");
            Messenger.Default.Register<Presentation.Table.Sucursal>(this, SucursalesAdd, "SucursalesQueryAdd");
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, "SucursalLoad");
            Messenger.Default.Unregister(this, "SucursalSave");
            Messenger.Default.Unregister(this, "SucursalErase");
            Messenger.Default.Unregister(this, "SucursalEdit");

            Messenger.Default.Unregister(this, "SucursalesQueryRefresh");
            Messenger.Default.Unregister(this, "SucursalesQueryAdd");
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

        public virtual void SucursalesRefresh((Result result, Presentation.Table.Sucursales list) message)
        {
            ViewModel.SucursalesQuery.CommandRefresh(message);
        }

        public virtual void SucursalesAdd(Presentation.Table.Sucursal entity)
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