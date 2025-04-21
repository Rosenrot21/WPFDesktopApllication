using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Books.Models;

namespace LibraryApp.Application.Domain.Books.Commands.SaveEdit
{
    public class SaveEditCommand
        {
            public Book SelectedBook { get; }
            public Book NewBook { get; }

            public SaveEditCommand(Book selectedBook, Book newBook)
            {
                SelectedBook = selectedBook;
                NewBook = newBook;
            }
        }
}
