using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Books.Models;

namespace LibraryApp.Core.Domain.Books.Common
{
    public interface IBookRepository
    {
        Book Find(int id);
        bool ExistsByTitle(string title);
        void Add(Book book);
        void Update(Book book);
        void Delete(int id);
        List<Book> GetAll();
    }
}
