using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LibraryApp.Core.Domain.Authors.Models;
using LibraryApp.Core.Domain.Books.Common;
using LibraryApp.Core.Domain.Books.Models;
using LibraryApp.Persistence;

namespace LibraryApp.Infrastructure.Core.Domain.Books.Common
{
    public class BookAuthorRepository : IBookAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public BookAuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public void Add(BookAuthor bookAuthor)
        {
            _context.BookAuthors.Add(bookAuthor);
            _context.SaveChanges();
        }

        public void Delete(int bookId, int authorId)
        {
            var bookAuthor = _context.BookAuthors
                .FirstOrDefault(ba => ba.BookId == bookId && ba.AuthorId == authorId);
            if (bookAuthor == null)
                throw new InvalidOperationException($"Вiдношення ID {bookId} книги та ID {authorId} автора не знайдено.");

            _context.BookAuthors.Remove(bookAuthor);
            _context.SaveChanges();
        }

        public List<Author> GetAuthorsForBook(int bookId)
        {
            var authorIds = _context.BookAuthors
                .Where(ba => ba.BookId == bookId)
                .Select(ba => ba.AuthorId)
                .ToList();

            var authors = _context.Authors
                .Where(a => authorIds.Contains(a.Id))
                .ToList();

            return authors;
        }

        public List<Book> GetBooksForAuthor(int authorId)
        {
            var bookIds = _context.BookAuthors
                .Where(ba => ba.AuthorId == authorId)
                .Select(ba => ba.BookId)
                .ToList();

            var books = _context.Books
                .Where(b => bookIds.Contains(b.Id))
                .ToList();

            return books;
        }
    }
}
