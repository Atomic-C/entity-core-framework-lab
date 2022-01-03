using MagicLib_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLib_DataAccess.FluentConfig
{
    public class FluentAuthorConfig : IEntityTypeConfiguration<FluentAuthor>
    {
        public void Configure(EntityTypeBuilder<FluentAuthor> modelBuilder)
        {
            // FluentAuthor
            modelBuilder.HasKey(b => b.Author_Id);
            modelBuilder.Property(b => b.FirstName).IsRequired();
            modelBuilder.Property(b => b.LastName).IsRequired();
            modelBuilder.Ignore(b => b.FullName);
        }
    }
}
