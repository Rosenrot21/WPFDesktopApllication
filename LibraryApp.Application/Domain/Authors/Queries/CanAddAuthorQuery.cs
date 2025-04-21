using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Domain.Authors.Queries
{
    public class CanAddAuthorQuery
    {
        public string Name { get; }

        public CanAddAuthorQuery(string name)
        {
            Name = name;
        }
    }
}
