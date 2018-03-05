using library.Impl;
using library.Impl.Presentation;
using System.Windows;

namespace WpfApp.Views
{
    /// <summary>
    /// Interaction logic for SucursalView.xaml
    /// </summary>
    public partial class Sucursal : Window
    {
        public presentation.Model.Sucursal ViewModel
        {
            get
            {
                return (presentation.Model.Sucursal)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public Sucursal()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Register<(CommandAction action, (Result result, presentation.Model.Sucursal entity) operation)>(this, SucursalErase, "SucursalErase");

        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Unregister(this, "SucursalErase");
        }

        public virtual void SucursalErase((CommandAction action, (Result result, presentation.Model.Sucursal entity) operation) message)
        {
            this.Close();
        }
    }
}
