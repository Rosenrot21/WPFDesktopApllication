using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Books.Common;

namespace LibraryApp.Infrastructure.Core.Domain.Queries
{
    public class CanAddAuthorToBookQueryHandler
    {
        private readonly IBookAuthorRepository _bookAuthorRepository;

        public CanAddAuthorToBookQueryHandler(IBookAuthorRepository bookAuthorRepository)
        {
            _bookAuthorRepository = bookAuthorRepository;
        }

        public bool Handle(CanAddAuthorToBookQuery query)
        {
            var existingRelation = _bookAuthorRepository.GetAuthorsForBook(query.BookId);
            return !existingRelation.Any(a => a.Id == query.AuthorId);
        }
    }
}
