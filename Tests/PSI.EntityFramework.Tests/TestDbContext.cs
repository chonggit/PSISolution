using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSI.EntityFramework;

namespace PSI.EntityFramework.Tests
{

    public class TestDbContext : PSIDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=PSISolution.db");
        }
    }
}