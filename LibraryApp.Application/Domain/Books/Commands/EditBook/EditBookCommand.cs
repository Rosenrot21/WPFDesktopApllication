using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Books.Models;

namespace LibraryApp.Application.Domain.Books.Commands.EditBook
{
    public class EditBookCommand
    {
        public Book SelectedBook { get; }

        public EditBookCommand(Book selectedBook)
        {
            SelectedBook = selectedBook;
        }
    }
}
