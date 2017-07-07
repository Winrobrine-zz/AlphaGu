using System.Windows;

namespace AlphaGu
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            new MainWindow().Show();
        }
    }
}
