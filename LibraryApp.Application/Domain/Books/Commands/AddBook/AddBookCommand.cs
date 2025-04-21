using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryApp.Core.Domain.Books.Models;

namespace LibraryApp.Application.Domain.Books.Commands.AddBook
{
    public class AddBookCommand
    {
        public Book Book { get; }

        public AddBookCommand(Book book)
        {
            Book = book;
        }
    }
}
