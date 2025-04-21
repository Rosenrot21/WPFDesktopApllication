using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Authors.Models;
using LibraryApp.Core.Domain.Books.Models;

namespace LibraryApp.Application.Domain.Books.Commands.RemoveAuthorFromBook
{
    public class RemoveAuthorFromBookCommand
    {
        public Book Book { get; }
        public Author Author { get; }

        public RemoveAuthorFromBookCommand(Book book, Author author)
        {
            Book = book;
            Author = author;
        }
    }
}
