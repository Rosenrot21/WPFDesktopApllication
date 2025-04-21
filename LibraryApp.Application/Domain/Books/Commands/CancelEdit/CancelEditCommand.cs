using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Books.Models;

namespace LibraryApp.Application.Domain.Books.Commands.CancelEdit
{
    public class CancelEditCommand
    {
        public Book SelectedBook { get; }

        public CancelEditCommand(Book selectedBook)
        {
            SelectedBook = selectedBook;
        }
    }
}
