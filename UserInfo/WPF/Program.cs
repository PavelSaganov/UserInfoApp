using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WPF.MVVM.View;
using WPF.Service;
using WPF.Service.Implementation;

namespace WPF
{
    public class Program
    {
        //[STAThread]
        //public static void Main()
        //{
        //    var host = Host.CreateDefaultBuilder()
        //        .ConfigureServices(services =>
        //        {
        //            services.AddSingleton<App>();
        //            services.AddSingleton<MainWindow>();
        //            services.AddTransient<IUserService, UserService>();
        //        })
        //        .Build();
        //    var app = host.Services.GetService<App>();
        //    app?.Run();
        //}
    }
}
