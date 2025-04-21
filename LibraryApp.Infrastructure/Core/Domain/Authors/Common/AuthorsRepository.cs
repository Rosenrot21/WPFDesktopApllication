using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Authors.Common;
using LibraryApp.Core.Domain.Authors.Models;
using LibraryApp.Persistence;

namespace LibraryApp.Infrastructure.Core.Domain.Authors.Common
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public Author Find(int id)
        {
            var author = _context.Authors.Find(id);
            return author ?? throw new InvalidOperationException($"Автор з {id} не знайден.");
        }

        public void Add(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public List<Author> GetAll()
        {
            return _context.Authors.ToList();
        }

        public void Delete(int id)
        {
            var author = _context.Authors.Find(id);
            if (author == null)
                throw new InvalidOperationException($"Author з {id} не знайден.");

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
