using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryApp.Core.Domain.Books.Common;

namespace LibraryApp.Application.Domain.Books.Queries
{
    public class CanAddBookQueryHandler
    {
        private readonly IBookRepository _bookRepository;

        public CanAddBookQueryHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public bool Handle(CanAddBookQuery query)
        {
            return !_bookRepository.ExistsByTitle(query.Title);
        }
    }
}
