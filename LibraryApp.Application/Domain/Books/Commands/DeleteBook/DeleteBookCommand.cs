using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Books.Models;

namespace LibraryApp.Application.Domain.Books.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        public Book Book { get; }

        public DeleteBookCommand(Book book)
        {
            Book = book;
        }
    }
}
