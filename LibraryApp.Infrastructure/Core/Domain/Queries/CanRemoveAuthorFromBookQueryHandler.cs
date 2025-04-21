using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Books.Common;

namespace LibraryApp.Infrastructure.Core.Domain.Queries
{
    public class CanRemoveAuthorFromBookQueryHandler
    {
        private readonly IBookAuthorRepository _bookAuthorRepository;

        public CanRemoveAuthorFromBookQueryHandler(IBookAuthorRepository bookAuthorRepository)
        {
            _bookAuthorRepository = bookAuthorRepository;
        }

        public bool Handle(CanRemoveAuthorFromBookQuery query)
        {
            var existingRelation = _bookAuthorRepository.GetAuthorsForBook(query.BookId);
            return existingRelation.Any(a => a.Id == query.AuthorId);
        }
    }
}
