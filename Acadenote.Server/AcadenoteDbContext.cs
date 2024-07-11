using Acadenode.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Acadenote.Server
{
    public class AcadenoteDbContext : DbContext
    {
        public AcadenoteDbContext(DbContextOptions<AcadenoteDbContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
