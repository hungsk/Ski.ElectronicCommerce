using Microsoft.EntityFrameworkCore;
using Ski.Member.Domain.Entities;

namespace Ski.Member.Data
{
    public class ShinkongDbContext : DbContext
    {
        public ShinkongDbContext()
        {
        }

        public ShinkongDbContext(DbContextOptions<ShinkongDbContext> options) : base(options)
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

        public virtual DbSet<MemberModel> Member { get; set; }
    }
}