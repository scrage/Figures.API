using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Figures.API.Entities;
using Figures.API.Models;

namespace Figures.API.Migrations
{
    [DbContext(typeof(FigureContext))]
    [Migration("20171102170423_FigureDbExpandDtosMigration")]
    partial class FigureDbExpandDtosMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Figures.API.Entities.Figure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alias")
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("FigureType");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Gender");

                    b.Property<bool>("IsLastNameFirst");

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50);

                    b.Property<string>("Title")
                        .HasMaxLength(25);

                    b.Property<string>("UniquelyDisplayedFullName")
                        .HasMaxLength(152);

                    b.HasKey("Id");

                    b.ToTable("Figures");
                });
        }
    }
}
