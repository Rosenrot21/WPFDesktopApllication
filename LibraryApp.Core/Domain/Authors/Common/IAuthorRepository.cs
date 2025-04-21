using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Core.Domain.Authors.Models;

namespace LibraryApp.Core.Domain.Authors.Common
{
    public interface IAuthorRepository
    {
        Author Find(int id);
        void Add(Author author);
        List<Author> GetAll();
        void Delete(int id); 
    }
}
