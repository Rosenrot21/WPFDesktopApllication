using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Domain.Books.Queries
{
    public class CanAddAuthorToBookQuery
    {
        public int BookId { get; }
        public int AuthorId { get; }

        public CanAddAuthorToBookQuery(int bookId, int authorId)
        {
            BookId = bookId;
            AuthorId = authorId;
        }
    }
}
