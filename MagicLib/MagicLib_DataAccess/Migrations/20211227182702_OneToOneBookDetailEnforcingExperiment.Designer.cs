﻿// <auto-generated />
using System;
using MagicLib_DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MagicLib_DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211227182702_OneToOneBookDetailEnforcingExperiment")]
    partial class OneToOneBookDetailEnforcingExperiment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("MagicLib_Model.Models.Author", b =>
                {
                    b.Property<int>("Author_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Author_Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("MagicLib_Model.Models.Book", b =>
                {
                    b.Property<int>("Book_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BookDetail_Id")
                        .HasColumnType("int");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Book_Id");

                    b.HasIndex("BookDetail_Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("MagicLib_Model.Models.BookDetail", b =>
                {
                    b.Property<int>("BookDetail_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("NumberOfCapters")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfPages")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("BookDetail_Id");

                    b.ToTable("BookDetails");
                });

            modelBuilder.Entity("MagicLib_Model.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("GenreName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Genre_name_column");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("GenreId");

                    b.ToTable("Genre_table");
                });

            modelBuilder.Entity("MagicLib_Model.Models.Publisher", b =>
                {
                    b.Property<int>("Publisher_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Location")
                        .HasColumnType("int");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.HasKey("Publisher_Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("MagicLib_Model.Models.Book", b =>
                {
                    b.HasOne("MagicLib_Model.Models.BookDetail", "BookDetail")
                        .WithMany()
                        .HasForeignKey("BookDetail_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
