using markdown_notes_app.Core.Entities;
using markdown_notes_app.Core.Interfaces.Repositories;
using markdown_notes_app.Infrastructure.Data.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace markdown_notes_app.Infrastructure.Repositories
{
    public class NoteRepository: RepositoryBase<Note>, INoteRepository
    {
        public NoteRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
        }
    }
}
