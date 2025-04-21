using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Application.Domain.Authors.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; }

        public DeleteAuthorCommand(int authorId)
        {
            AuthorId = authorId;
        }
    }
}
