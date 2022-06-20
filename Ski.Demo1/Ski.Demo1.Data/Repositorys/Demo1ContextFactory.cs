using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ski.Demo1.Data.Repositorys
{
    internal class Demo1ContextFactory : IDesignTimeDbContextFactory<Demo1DbContext>
    {
        public Demo1DbContext CreateDbContext(string[] args)
        {
            var connectionString = args[0];
            var optionsBuilder = new DbContextOptionsBuilder<Demo1DbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            //optionsBuilder.UseSqlite(connectionString);

            return new Demo1DbContext(optionsBuilder.Options);
        }
    }
}
