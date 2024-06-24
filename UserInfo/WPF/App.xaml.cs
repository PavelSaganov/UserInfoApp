using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
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

                    ConfigureViews(services);
                    ConfigureServices(services);
                })
                .Build();

            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private static void ConfigureViews(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<RegistrationView>();
            services.AddSingleton<AuthView>();

            ConfigureViewFactories(services);
        }

        private static void ConfigureViewFactories(IServiceCollection services)
        {
            services.AddSingleton<Func<RegistrationView>>(serviceProvider
                                    => serviceProvider.GetRequiredService<RegistrationView>);
            services.AddSingleton<Func<AuthView>>(serviceProvider
                => serviceProvider.GetRequiredService<AuthView>);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }
    }

}
