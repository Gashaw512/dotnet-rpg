
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
        public DbSet<User> Users=>Set<User>();


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=dotnet-rpg;Trusted_Connection=True; TrustServerCertificate=true;");
            }
        }
    }
}