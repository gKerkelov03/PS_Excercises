using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataLayer.Database;
using DataLayer.Repositories;
using DataLayer.Services;
using UI.Windows;
using UI.ViewModels;
using System.IO;
using DataLayer;

namespace UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Configure DbContext
        var solutionFolderName = "PS_Excercises";
        var databaseFileName = "database.db";
        var documentsFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        var solutionFolder = Path.Combine(documentsFolderPath, solutionFolderName);
        var databasePath = Path.Combine(solutionFolder, databaseFileName);

        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite($"Data Source={databasePath}"));

        // Register repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IDisciplineRepository, DisciplineRepository>();

        // Register services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDisciplineService, DisciplineService>();
        services.AddScoped<Logger>();

        // Register ViewModels
        services.AddTransient<LoginViewModel>();
        services.AddTransient<MainViewModel>();
        services.AddTransient<LogsViewModel>();
        services.AddTransient<DisciplineManagementViewModel>();

        // Register Windows
        services.AddTransient<LoginWindow>();
        services.AddTransient<MainWindow>();
        services.AddTransient<LogsWindow>();
        services.AddTransient<DisciplineManagementWindow>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Ensure database is created and migrations are applied
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            dbContext.Database.EnsureCreated();
            
            // Seed the database with our custom seeder
            DatabaseSeeder.SeedData(dbContext);
        }

        var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
        loginWindow.Show();
    }
}