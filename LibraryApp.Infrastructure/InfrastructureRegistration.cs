using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Authors.Common;
using LibraryApp.Core.Domain.Books.Common;
using LibraryApp.Infrastructure.Core.Domain.Authors.Common;
using LibraryApp.Infrastructure.Core.Domain.Books.Common;
using LibraryApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.Infrastructure
{
    public static class ServiceConfiguration
    {
        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<LibraryDbContext>(options =>
                 options.UseNpgsql("Host=localhost;Port=5432;Database=Library;Username=postgres;Password=postgres"));
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookAuthorRepository, BookAuthorRepository>();

            return services.BuildServiceProvider();
        }
    }
}
