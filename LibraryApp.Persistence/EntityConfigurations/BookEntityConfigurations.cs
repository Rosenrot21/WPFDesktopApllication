using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Core.Domain.Books.Models;

namespace LibraryApp.Persistence.EntityConfigurations
{
    public class BookEntityConfigurations : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(b => b.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(b => b.Year)
                   .IsRequired();
        }
    }
}
