using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace LibraryApp.Persistence.LibraryDb
{
    public static class LibraryDbContextRegistration
    {

        private const string ConnectionStringName = "Library";
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString(ConnectionStringName)
                                  ?? throw new AggregateException($"Connection string: '{ConnectionStringName}' is not found in configurations.");

            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseNpgsql(
                  connectionString,
                  npgsqlOptions =>
                  {
                      npgsqlOptions.MigrationsHistoryTable(
                            LibraryDbContext.LibraryMigrationHistory,
                            LibraryDbContext.LibraryDbSchema);
                  });
            });
        }
    }
}
