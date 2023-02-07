
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DbContext> options) : base(options)
        {

        }
        public DataContext() : base()
        {
        }

        public DbSet<Character> Characters => Set<Character>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("SERVER=Server\\Instance;DATABASE=Database;UID=user;PWD=password;");
            }
        }
    }
}