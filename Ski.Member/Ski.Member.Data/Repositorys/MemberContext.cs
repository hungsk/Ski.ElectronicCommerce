using Microsoft.EntityFrameworkCore;
using Ski.Member.Domain.Entities;

namespace Ski.Member.Data
{

    public class MemberDbContext : DbContext
    {
        public MemberDbContext()
        {

        }
        public MemberDbContext(DbContextOptions<MemberDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //    optionsBuilder.UseSqlServer(
            //        @"data source=(localdb)\MSSQLLocalDB;database=Demo1");
            optionsBuilder.UseSqlite(
                "Data Source=../ski.member.data/Sqlite/Member.db");
        }

        public virtual DbSet<MemberModel> Member { get; set; }
    }
}