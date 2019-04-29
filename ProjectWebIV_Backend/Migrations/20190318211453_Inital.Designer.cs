﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectWebIV_Backend.Data;

namespace ProjectWebIV_Backend.Migrations
{
    [DbContext(typeof(PostContext))]
    [Migration("20190318211453_Inital")]
    partial class Inital
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjectWebIV_Backend.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Autheur")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("Created");

                    b.Property<int>("PostId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Comment");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Autheur = "Robbie Verdurme",
                            Created = new DateTime(2019, 3, 18, 22, 14, 53, 187, DateTimeKind.Local).AddTicks(4494),
                            PostId = 1,
                            Text = "Comment 1"
                        },
                        new
                        {
                            Id = 2,
                            Autheur = "Robbie Verdurme",
                            Created = new DateTime(2019, 3, 18, 22, 14, 53, 187, DateTimeKind.Local).AddTicks(5536),
                            PostId = 1,
                            Text = "Comment 2"
                        },
                        new
                        {
                            Id = 3,
                            Autheur = "Robbie Verdurme",
                            Created = new DateTime(2019, 3, 18, 22, 14, 53, 187, DateTimeKind.Local).AddTicks(5548),
                            PostId = 1,
                            Text = "Comment 3"
                        });
                });

            modelBuilder.Entity("ProjectWebIV_Backend.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2019, 3, 18, 22, 14, 53, 187, DateTimeKind.Local).AddTicks(2965),
                            Title = "Post 1"
                        },
                        new
                        {
                            Id = 2,
                            Created = new DateTime(2019, 3, 18, 22, 14, 53, 187, DateTimeKind.Local).AddTicks(2995),
                            Title = "Post 2"
                        });
                });

            modelBuilder.Entity("ProjectWebIV_Backend.Models.Comment", b =>
                {
                    b.HasOne("ProjectWebIV_Backend.Models.Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
