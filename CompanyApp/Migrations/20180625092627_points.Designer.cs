﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using CompanyApp.Data;
using System;

namespace CompanyApp.Migrations
{
    [DbContext(typeof(CompanyDbContext))]
    [Migration("20180625092627_points")]
    partial class points
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CompanyApp.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("CompanyApp.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DepartmentId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("CompanyApp.Models.EmployeeProject", b =>
                {
                    b.Property<int>("EmployeeId");

                    b.Property<int>("ProjectId");

                    b.HasKey("EmployeeId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("EmployeeProject");
                });

            modelBuilder.Entity("CompanyApp.Models.Point", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EmployeeId");

                    b.Property<bool>("IsAward");

                    b.Property<int?>("ReceivedFromId");

                    b.Property<DateTime>("Timestamp");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ReceivedFromId");

                    b.ToTable("Point");
                });

            modelBuilder.Entity("CompanyApp.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("CompanyApp.Models.Employee", b =>
                {
                    b.HasOne("CompanyApp.Models.Department", "Department")
                        .WithMany("Employee")
                        .HasForeignKey("DepartmentId");
                });

            modelBuilder.Entity("CompanyApp.Models.EmployeeProject", b =>
                {
                    b.HasOne("CompanyApp.Models.Employee", "Employee")
                        .WithMany("EmployeeProject")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CompanyApp.Models.Project", "Project")
                        .WithMany("ProjectEmployee")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CompanyApp.Models.Point", b =>
                {
                    b.HasOne("CompanyApp.Models.Employee", "Employee")
                        .WithMany("Point")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("CompanyApp.Models.Employee", "ReceivedFrom")
                        .WithMany()
                        .HasForeignKey("ReceivedFromId");
                });
#pragma warning restore 612, 618
        }
    }
}
