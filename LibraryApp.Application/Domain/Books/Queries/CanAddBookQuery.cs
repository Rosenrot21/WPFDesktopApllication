using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Domain.Books.Queries
{
    public class CanAddBookQuery
    {
        public string Title { get; }

        public CanAddBookQuery(string title)
        {
            Title = title;
        }
    }
}
