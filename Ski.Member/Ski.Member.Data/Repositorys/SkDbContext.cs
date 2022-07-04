using Microsoft.EntityFrameworkCore;

namespace Ski.Member.Data
{
    public class SkDbContext : DbContext
    {
        public SkDbContext()
        {
        }

        public SkDbContext(DbContextOptions<SkDbContext> options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        @"data source=(localdb)\MSSQLLocalDB;database=Demo1");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public virtual DbSet<Domain.Member> Member { get; set; }
    }
}