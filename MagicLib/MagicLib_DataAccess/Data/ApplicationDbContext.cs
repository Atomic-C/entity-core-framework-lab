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
       public DbSet<BookDetail> BookDetails { get; set; } 
       public DbSet<FluentBookDetail> FluentBookDetail { get; set; } 
       public DbSet<FluentBook> FluentBooks { get; set; }
       public DbSet<FluentAuthor> FluentAuthors { get; set; }
       public DbSet<FluentPublisher> FluentPublishers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Here we configure fluent API stuff

            // Category table name and column name for testing purposes
            modelBuilder.Entity<Category>().ToTable("Fluent_Category");
            modelBuilder.Entity<Category>().Property(b => b.Name).HasColumnName("CategoryName");

            //// FluentBookDetail
            //modelBuilder.Entity<FluentBookDetail>().HasKey(b => b.BookDetail_Id); // This sets the PK of BookDetail to BookDetail_ID 
            //modelBuilder.Entity<FluentBookDetail>().Property(b => b.NumberOfCapters).IsRequired(); // Sets REQUIRED

            //// FluentBook
            //modelBuilder.Entity<FluentBook>().HasKey(b => b.Book_Id);
            //modelBuilder.Entity<FluentBook>().Property(b => b.Title).IsRequired();
            //modelBuilder.Entity<FluentBook>().Property(b => b.ISBN).IsRequired().HasMaxLength(15);
            //modelBuilder.Entity<FluentBook>().Property(b => b.Price).IsRequired();
            //// One to one 1:1 relationship between FluentBook and FluentBookDetail
            //modelBuilder.Entity<FluentBook>().HasOne(b => b.FluentBookDetail).WithOne(b => b.FluentBook).HasForeignKey<FluentBook>("BookDetail_Id");
            //// One to many 1:* relationship between FluentBook and FluentPublisher
            //modelBuilder.Entity<FluentBook>().HasOne(b => b.FluentPublisher).WithMany(b => b.FluentBook).HasForeignKey(b=>b.Publisher_Id);
            //// Many to many *:* relationship between FluentBook and FluentAuthor
            //modelBuilder.Entity<FluentBook>().HasMany(b => b.FluentAuthor)
            //    .WithMany(b => b.FluentBook)
            //    .UsingEntity(b => b.ToTable("FluentBookFluentAuthor"));

            // To achieve many to many here I've read the following sources by this order:
            // https://www.rasikag.com/posts/ef-core-5-many-to-many-relationship
            // https://dev.to/ruben_j/many-to-many-relations-in-entity-framework-core-3-1-and-5-59ei
            // Saw this channel and could not find map function in ICollection :| 
            // https://www.youtube.com/watch?v=GjmnHwCScRA&list=PLUU6Q8aGTpKV4bfIPiqGANF2OpbFbE0vl&index=23
            // This arabic video which I understand nothing of, allowed me to see the pattern:
            // https://youtu.be/PhTVKbApH0k?t=368 At this right time PostTagsTest as an example.
            // This allowed me to skip Bruhen Patel's method of creating a third entity explicitely to achieve my end goal of a many to many relationship, by connecting two one to many relationships.
            // Side question if I ever have an answer, why didn't map work though?
            // https://www.xsprogram.com/content/creating-many-to-many-relationships-using-fluent-api-in-entity-framework.html

            //// FluentAuthor
            //modelBuilder.Entity<FluentAuthor>().HasKey(b => b.Author_Id);
            //modelBuilder.Entity<FluentAuthor>().Property(b => b.FirstName).IsRequired();
            //modelBuilder.Entity<FluentAuthor>().Property(b => b.LastName).IsRequired();
            //modelBuilder.Entity<FluentAuthor>().Ignore(b => b.FullName);

            //// FluentPublisher
            //modelBuilder.Entity<FluentPublisher>().HasKey(b => b.Publisher_Id);
            //modelBuilder.Entity<FluentPublisher>().Property(b => b.Name).IsRequired();
            //modelBuilder.Entity<FluentPublisher>().Property(b => b.Location).IsRequired();

            // Changes below were made after most models were comented above:



        }
    }
}
