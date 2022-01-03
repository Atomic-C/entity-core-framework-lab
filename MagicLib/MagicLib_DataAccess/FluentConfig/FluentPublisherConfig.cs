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
   public class FluentPublisherConfig : IEntityTypeConfiguration<FluentPublisher>
    {
        public void Configure (EntityTypeBuilder<FluentPublisher> modelBuilder)
        {
            // FluentPublisher
            modelBuilder.HasKey(b => b.Publisher_Id);
            modelBuilder.Property(b => b.Name).IsRequired();
            modelBuilder.Property(b => b.Location).IsRequired();
        }
    }
}
