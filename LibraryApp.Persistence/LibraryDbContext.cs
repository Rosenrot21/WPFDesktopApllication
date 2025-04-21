using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Persistence.EntityConfigurations;
using LibraryApp.Core.Domain.Books.Models;
using LibraryApp.Core.Domain.Authors.Models;

namespace LibraryApp.Persistence;

public class LibraryDbContext : DbContext
{

    public static string LibraryDbSchema = "library";

    public static string LibraryMigrationHistory = "MigrationHistory";
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Library;Username=postgres;Password=postgres");
        }

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(LibraryDbSchema);
        modelBuilder.ApplyConfiguration(new BookEntityConfigurations());
        modelBuilder.ApplyConfiguration(new BookAuthorEntityConfigurations());
        modelBuilder.ApplyConfiguration(new AuthorEntityConfigurations());

    }
}