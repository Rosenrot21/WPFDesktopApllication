
using Microsoft.EntityFrameworkCore;
using LibraryApp.Core.Domain.Books.Common;
using LibraryApp.Core.Domain.Books.Models;
using LibraryApp.Persistence;

namespace LibraryApp.Infrastructure.Core.Domain.Books.Common
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public Book Find(int id)
        {
            var book = _context.Books.Find(id);
            return book ?? throw new InvalidOperationException($"Book with ID {id} not found.");
        }

        public bool ExistsByTitle(string title)
        {
            return _context.Books.Any(b => b.Title == title);
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
                throw new InvalidOperationException($"Книгу з ID {id} не знайдено.");

            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        public List<Book> GetAll()
        {
            return _context.Books.ToList();
        }
    }
}
