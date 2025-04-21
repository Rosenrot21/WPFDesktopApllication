using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Core.Domain.Books.Models;

namespace LibraryApp.Persistence.EntityConfigurations
{
    public class BookAuthorEntityConfigurations : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            
            builder.HasKey(x => new { x.BookId, x.AuthorId });

            
            builder.HasOne(ba => ba.Book)
                   .WithMany()
                   .HasForeignKey(ba => ba.BookId);

           
            builder.HasOne(ba => ba.Author)
                   .WithMany()
                   .HasForeignKey(ba => ba.AuthorId);

            
            builder.ToTable("BookAuthors");
        }
    }
}
