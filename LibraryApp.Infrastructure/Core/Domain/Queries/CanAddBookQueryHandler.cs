using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryApp.Persistence;

namespace LibraryApp.Infrastructure.Core.Domain.Queries
{
    public class CanAddBookQueryHandler
    {
        private readonly LibraryDbContext _context;

        public CanAddBookQueryHandler(LibraryDbContext context)
        {
            _context = context;
        }

        public bool Handle(CanAddBookQuery query)
        {
            return !_context.Books.Any(b => b.Title == query.Title);
        }
    }
}
