using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Domain.Books.Queries
{
    public class CanRemoveAuthorFromBookQuery
    {
        public int BookId { get; }
        public int AuthorId { get; }

        public CanRemoveAuthorFromBookQuery(int bookId, int authorId)
        {
            BookId = bookId;
            AuthorId = authorId;
        }
    }
}
