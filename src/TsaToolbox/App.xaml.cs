using System.Windows;
using TsaToolbox.Models;
using TsaToolbox.Models.Setups;
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
            Setup setup = new Setup();

            MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(settigns, source, setup),
                Settings = settigns,
                Source = source,
                Setup = setup
            };

            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}
