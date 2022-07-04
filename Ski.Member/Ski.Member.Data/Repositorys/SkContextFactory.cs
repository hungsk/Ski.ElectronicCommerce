using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ski.Member.Data.Repositorys
{
    internal class Demo1ContextFactory : IDesignTimeDbContextFactory<SkDbContext>
    {
        public SkDbContext CreateDbContext(string[] args)
        {
            var connectionString = args[0];
            var optionsBuilder = new DbContextOptionsBuilder<SkDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.UseSqlite(connectionString);

            return new SkDbContext(optionsBuilder.Options);
        }
    }
}
