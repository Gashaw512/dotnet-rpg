global using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace dotnet_rpg.Data
{
    public class DataContext: DbContext
    {
       public DataContext(DbContextOptions<DbContext> options ): base(options)
       {
        
       } 
       public DbSet<Character> Characters =>Set<Character>();
    }
}