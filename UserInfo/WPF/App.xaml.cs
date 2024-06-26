﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using WPF.MVVM.View;
using WPF.MVVM.ViewModel;
using WPF.Service;
using WPF.Service.Implementation;

namespace WPF
{
    public partial class App : Application
    {
        public App()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();

                    ConfigureViews(services);
                    ConfigureViewModels(services);
                    ConfigureServices(services);
                })
                .Build();

            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureViewModels(IServiceCollection services)
        {
            services.AddTransient<MainViewModel>();
            services.AddTransient<AuthViewModel>();
            services.AddTransient<RegistrationViewModel>();
        }

        private static void ConfigureViews(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddTransient<RegistrationView>();
            services.AddTransient<AuthView>();

            ConfigureViewFactories(services);
        }

        private static void ConfigureViewFactories(IServiceCollection services)
        {
            services.AddTransient<Func<RegistrationView>>(serviceProvider
                                    => serviceProvider.GetRequiredService<RegistrationView>);
            services.AddTransient<Func<AuthView>>(serviceProvider
                => serviceProvider.GetRequiredService<AuthView>);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();
        }
    }

}
