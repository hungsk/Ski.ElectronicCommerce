using Microsoft.EntityFrameworkCore;
using Ski.Member.Domain.Entities.MemberModels;

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

        public virtual DbSet<MemberModel> Member { get; set; }
    }
}