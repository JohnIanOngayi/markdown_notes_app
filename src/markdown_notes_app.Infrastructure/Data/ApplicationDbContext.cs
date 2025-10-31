using markdown_notes_app.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace markdown_notes_app.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            :base(options)
        {
        }

        // Notes Table
        public DbSet<Note>? Notes { get; set; }
    }
}
