using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Authors.Common;

namespace LibraryApp.Application.Domain.Authors.Queries
{
    public class CanAddAuthorQueryHandler
    {
        private readonly IAuthorRepository _authorRepository;

        public CanAddAuthorQueryHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public bool Handle(CanAddAuthorQuery query)
        {
            return !_authorRepository.GetAll().Any(a => a.Name == query.Name);
        }
    }
}
