using Microsoft.EntityFrameworkCore;

namespace PSI.EntityFramework
{
    public class PSIDbContext : DbContext
    {
        public PSIDbContext() : base() { }

        public PSIDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(PSIDbContext).Assembly);
        }
    }
}
