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
    [Migration("20171102150830_FigureDbInitialMigration")]
    partial class FigureDbInitialMigration
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

                    b.Property<string>("Aka");

                    b.Property<string>("Description");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Figures");
                });
        }
    }
}
