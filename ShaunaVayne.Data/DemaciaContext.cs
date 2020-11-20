using Microsoft.EntityFrameworkCore;
using ShaunaVayne.Models;

namespace ShaunaVayne.Data
{
    public class DemaciaContext: DbContext
    {
        public DemaciaContext(DbContextOptions<DemaciaContext> options)
            : base(options)
        {
            
        }

        public DbSet<Test> Tests { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
