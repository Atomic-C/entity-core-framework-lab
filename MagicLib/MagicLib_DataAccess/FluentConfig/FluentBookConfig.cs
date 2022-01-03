using MagicLib_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLib_DataAccess.FluentConfig
{
    public class FluentBookConfig : IEntityTypeConfiguration<FluentBook>
    {
        public void Configure(EntityTypeBuilder<FluentBook> modelBuilder)
        {
            // Name Of Table

            // FluentBook
            modelBuilder.HasKey(b => b.Book_Id);
            modelBuilder.Property(b => b.Title).IsRequired();
            modelBuilder.Property(b => b.ISBN).IsRequired().HasMaxLength(15);
            modelBuilder.Property(b => b.Price).IsRequired();


            // One to one 1:1 relationship between FluentBook and FluentBookDetail
            modelBuilder.HasOne(b => b.FluentBookDetail).WithOne(b => b.FluentBook).HasForeignKey<FluentBook>("BookDetail_Id");


            // One to many 1:* relationship between FluentBook and FluentPublisher
            modelBuilder.HasOne(b => b.FluentPublisher).WithMany(b => b.FluentBook).HasForeignKey(b => b.Publisher_Id);


            // Many to many *:* relationship between FluentBook and FluentAuthor
            modelBuilder.HasMany(b => b.FluentAuthor)
                .WithMany(b => b.FluentBook)
                .UsingEntity(b => b.ToTable("FluentBookFluentAuthor"));
        }
    }
}
