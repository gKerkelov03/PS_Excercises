using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataLayer.Database;
using DataLayer.Repositories;
using DataLayer.Services;
using UI.Windows;
using UI.ViewModels;

namespace UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>(options =>
        {
            var solutionFolderName = "PS_Excercises";
            var databaseFileName = "database.db";
            var documentsFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var solutionFolder = System.IO.Path.Combine(documentsFolderPath, solutionFolderName);
            var databasePath = System.IO.Path.Combine(solutionFolder, databaseFileName);
            options.UseSqlite($"Data Source={databasePath}");
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<DataLayer.Logger>();
        services.AddTransient<LoginWindow>();
        services.AddTransient<MainWindow>();
        services.AddTransient<LogsWindow>();
        services.AddTransient<LoginViewModel>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<LogsViewModel>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var loginWindow = serviceProvider.GetRequiredService<LoginWindow>();
        loginWindow.Show();
    }
}