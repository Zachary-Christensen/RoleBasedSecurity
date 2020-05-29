﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RoleBasedSecurityApp.Models;

namespace RoleBasedSecurityApp.Migrations
{
    [DbContext(typeof(BugTrackerContext))]
    [Migration("20200526201929_AddProjectCounterM2M")]
    partial class AddProjectCounterM2M
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RoleBasedSecurityApp.Models.Counter", b =>
                {
                    b.Property<int>("CounterID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.HasKey("CounterID");

                    b.ToTable("Counters");
                });

            modelBuilder.Entity("RoleBasedSecurityApp.Models.Project", b =>
                {
                    b.Property<int>("ProjectID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("RoleBasedSecurityApp.Models.ProjectCounter", b =>
                {
                    b.Property<int>("ProjectID")
                        .HasColumnType("int");

                    b.Property<int>("CounterID")
                        .HasColumnType("int");

                    b.HasKey("ProjectID", "CounterID");

                    b.HasAlternateKey("CounterID", "ProjectID");

                    b.ToTable("ProjectCounters");
                });

            modelBuilder.Entity("RoleBasedSecurityApp.Models.ProjectCounter", b =>
                {
                    b.HasOne("RoleBasedSecurityApp.Models.Counter", "Counter")
                        .WithMany("ProjectCounters")
                        .HasForeignKey("CounterID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RoleBasedSecurityApp.Models.Project", "Project")
                        .WithMany("ProjectCounters")
                        .HasForeignKey("ProjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
