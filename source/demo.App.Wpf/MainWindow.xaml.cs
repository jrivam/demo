using Autofac;
using jrivam.Library;
using System.Windows;

namespace demo.App.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AutofacConfiguration.BuildContainer(DependencyInstaller.RegisterServices(new ContainerBuilder()));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Empresas_Click(object sender, RoutedEventArgs e)
        {
            var view = new Views.Empresas();
            view.Show();
        }

        private void Sucursales_Click(object sender, RoutedEventArgs e)
        {
            var view = new Views.Sucursales();
            view.Show();
        }
    }
}
