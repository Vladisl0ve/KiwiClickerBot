using KiwiClickerBot.ViewModels;
using KiwiClickerBot.Views;
using System.Windows;

namespace KiwiClickerBot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindowView window = new MainWindowView();
            MainWindowViewModel mainWindowViewModel= new MainWindowViewModel();
            window.DataContext= mainWindowViewModel;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
        }
    }
}
