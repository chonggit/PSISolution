using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PSI.Administration;

namespace PSI.EntityFramework
{
    public class PSIDbContext : IdentityDbContext<User, Role, int>
    {
        public PSIDbContext() : base() { }

        public PSIDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(PSIDbContext).Assembly);
        }
    }
}
