using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using WPF.MVVM.View;
using WPF.Service;
using WPF.Service.Implementation;

namespace WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<RegistrationView>();
                    services.AddTransient<IUserService, UserService>();
                })
                .Build();

            var mainWindow = host.Services.GetRequiredService<RegistrationView>();
            mainWindow.Show();
        }
    }

}
