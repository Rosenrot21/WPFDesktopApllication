using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Core.Domain.Authors.Models;

namespace LibraryApp.Persistence.EntityConfigurations
{
    public class AuthorEntityConfigurations : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            
            builder.ToTable("Authors");

            builder.HasKey(a => a.Id);

            
            builder.Property(a => a.Id)
                   .ValueGeneratedOnAdd(); 

            builder.Property(a => a.Name)
                   .IsRequired() 
                   .HasMaxLength(100); 

            
            builder.HasIndex(a => a.Name)
                   .IsUnique(); 
        }
    }
}
