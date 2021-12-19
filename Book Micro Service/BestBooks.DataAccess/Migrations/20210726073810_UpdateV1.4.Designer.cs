﻿// <auto-generated />
using System;
using BestBooks.BookMicroservice.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BestBooks.BookMicroservice.DataAccess.Migrations
{
    [DbContext(typeof(BookDbContext))]
    [Migration("20210726073810_UpdateV1.4")]
    partial class UpdateV14
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BestBooks.BookMicroservice.DataAccess.DataModel.Book", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<DateTime>("DatePublished")
                        .HasColumnType("datetime");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("varchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("ID");

                    b.ToTable("Book");
                });
#pragma warning restore 612, 618
        }
    }
}