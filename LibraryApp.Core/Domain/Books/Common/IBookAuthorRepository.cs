using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Authors.Models;
using LibraryApp.Core.Domain.Books.Models;

namespace LibraryApp.Core.Domain.Books.Common
{
    public interface IBookAuthorRepository
    {
        void Add(BookAuthor bookAuthor);
        void Delete(int bookId, int authorId);
        List<Author> GetAuthorsForBook(int bookId);
        List<Book> GetBooksForAuthor(int authorId); 
    }
}
