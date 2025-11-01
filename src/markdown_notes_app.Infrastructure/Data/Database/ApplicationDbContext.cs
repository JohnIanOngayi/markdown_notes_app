using markdown_notes_app.Core.Entities;
using markdown_notes_app.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace markdown_notes_app.Infrastructure.Data.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<ISoftDelete>())
            {
                entry.Entity.UpdatedAt = DateTime.Now;

                if (entry.State == EntityState.Added) entry.Entity.CreatedAt = DateTime.Now;
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Automatically adding query filter to 
            // all LINQ queries that use Movie
            modelBuilder.Entity<ISoftDelete>()
                .HasQueryFilter(x => x.IsDeleted == false);
        }

        // Notes Table
        public DbSet<Note>? Notes { get; set; }
    }
}
