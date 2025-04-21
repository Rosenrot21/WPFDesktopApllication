using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Authors.Models;

namespace LibraryApp.Application.Domain.Authors.Commands.AddAuthor
{
    public class AddAuthorCommand
    {
        public Author Author { get; }

        public AddAuthorCommand(Author author)
        {
            Author = author;
        }
    }
}
