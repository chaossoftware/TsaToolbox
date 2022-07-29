using System.Windows;
using TsaToolbox.Models;
using TsaToolbox.ViewModels;

namespace TsaToolbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Settings settigns = new Settings();
            DataSource source = new DataSource();

            MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(settigns, source),
                Settings = settigns,
                Source = source
            };

            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
