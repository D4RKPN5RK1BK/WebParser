﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebPareser.Data;

#nullable disable

namespace WebParser.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.1");

            modelBuilder.Entity("WebParser.Models.Document", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Extention")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LegasyURL")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PageId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PageId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("WebParser.Models.Page", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Created")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLegasy")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastModified")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LegasyContent")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Meta")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PageGroupId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentPageId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PageGroupId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("WebParser.Models.PageGroup", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("PageGroups");
                });

            modelBuilder.Entity("WebParser.Models.Document", b =>
                {
                    b.HasOne("WebParser.Models.Page", "Page")
                        .WithMany("PageFiles")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Page");
                });

            modelBuilder.Entity("WebParser.Models.Page", b =>
                {
                    b.HasOne("WebParser.Models.PageGroup", "PageGroup")
                        .WithMany("Pages")
                        .HasForeignKey("PageGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PageGroup");
                });

            modelBuilder.Entity("WebParser.Models.Page", b =>
                {
                    b.Navigation("PageFiles");
                });

            modelBuilder.Entity("WebParser.Models.PageGroup", b =>
                {
                    b.Navigation("Pages");
                });
#pragma warning restore 612, 618
        }
    }
}
