using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Authors.Models;
using LibraryApp.Core.Domain.Books.Models;

namespace LibraryApp.Application.Domain.Books.Commands.AddAuthorToBookCommand
{
    public class AddAuthorToBookCommand
    {
        public Book Book { get; }
        public Author Author { get; }

        public AddAuthorToBookCommand(Book book, Author author)
        {
            Book = book;
            Author = author;
        }
    }
}
