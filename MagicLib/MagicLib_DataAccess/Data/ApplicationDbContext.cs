using MagicLib_Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLib_DataAccess.Data
{
   public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
       public DbSet<Category> Categories { get; set; } // This is how EF Core knows Category is a model. It creates it in DB if it doesn't exist
       public DbSet<Genre> Genres { get; set; } 
       public DbSet<Book> Books { get; set; } 
       public DbSet<Author> Authors { get; set; } 
       public DbSet<Publisher> Publishers { get; set; } 
    }
}
