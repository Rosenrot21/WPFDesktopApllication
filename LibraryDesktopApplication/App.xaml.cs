using System.Configuration;
using System.Data;
using System.Windows;
using LibraryApp.Application.Domain.Books.Commands;
using LibraryApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryDesktopApplication;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public App()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<LibraryDbContext>(options =>
             options.UseNpgsql("Host=localhost;Port=5432;Database=Library;Username=postgres;Password=postgres"));

        // Регистрация ViewModel и View
        services.AddSingleton<MainWindow>();
        services.AddTransient<BookViewModel>();  // Внедряем ViewModel

        // Создание провайдера зависимостей
        ServiceProvider = services.BuildServiceProvider();
    }
}
