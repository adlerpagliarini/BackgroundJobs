﻿// <auto-generated />
using System;
using BackgroundJobs.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackgroundJobs.Migrations
{
    [DbContext(typeof(ModelFlowContext))]
    [Migration("20200111233106_Model_Nodes")]
    partial class Model_Nodes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackgroundJobs.Models.Model", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id_Model")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ModelFlow");
                });

            modelBuilder.Entity("BackgroundJobs.Models.Node", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id_Node")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Input")
                        .HasColumnType("bigint");

                    b.Property<Guid>("ModelId")
                        .HasColumnName("Id_Model")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ModelId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Operation")
                        .HasColumnType("int");

                    b.Property<long?>("Output")
                        .HasColumnType("bigint");

                    b.Property<Guid?>("ParentId")
                        .HasColumnName("Id_Parent")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.HasIndex("ModelId1")
                        .IsUnique()
                        .HasFilter("[ModelId1] IS NOT NULL");

                    b.HasIndex("ParentId");

                    b.ToTable("NodeFlow");
                });

            modelBuilder.Entity("BackgroundJobs.Models.Node", b =>
                {
                    b.HasOne("BackgroundJobs.Models.Model", null)
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BackgroundJobs.Models.Model", null)
                        .WithOne("RootNode")
                        .HasForeignKey("BackgroundJobs.Models.Node", "ModelId1");

                    b.HasOne("BackgroundJobs.Models.Node", "ParentReference")
                        .WithMany("LinkedNodes")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
