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
    public class FluentBookDetailsConfig : IEntityTypeConfiguration<FluentBookDetail>
    {
        public void Configure(EntityTypeBuilder<FluentBookDetail> modelBuilder)
        {
            // Name Of Table

            // FluentBookDetail
            modelBuilder.HasKey(b => b.BookDetail_Id); // This sets the PK of BookDetail to BookDetail_ID 
            modelBuilder.Property(b => b.NumberOfCapters).IsRequired(); // Sets REQUIRED
        }
    }
}
