using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ski.Member.Data.Repositorys
{
    internal class ShinkongDbContextFactory : IDesignTimeDbContextFactory<ShinkongDbContext>
    {
        public ShinkongDbContext CreateDbContext(string[] args)
        {
            var connectionString = args[0];
            var optionsBuilder = new DbContextOptionsBuilder<ShinkongDbContext>();
            //optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.UseSqlite(connectionString);

            return new ShinkongDbContext(optionsBuilder.Options);
        }
    }
}
