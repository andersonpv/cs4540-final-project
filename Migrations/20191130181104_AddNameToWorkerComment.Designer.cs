﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using cs4540_final_project.Data;

namespace cs4540_final_project.Migrations
{
    [DbContext(typeof(WorkerContext))]
    [Migration("20191130181104_AddNameToWorkerComment")]
    partial class AddNameToWorkerComment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("IdentityUser");
                });

            modelBuilder.Entity("cs4540_final_project.Models.DaySchedule", b =>
                {
                    b.Property<int>("DayScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Eleven");

                    b.Property<bool>("ElevenThirty");

                    b.Property<bool>("Four");

                    b.Property<bool>("FourThirty");

                    b.Property<bool>("Nine");

                    b.Property<bool>("NineThirty");

                    b.Property<bool>("One");

                    b.Property<bool>("OneThirty");

                    b.Property<bool>("Ten");

                    b.Property<bool>("TenThirty");

                    b.Property<bool>("Three");

                    b.Property<bool>("ThreeThirty");

                    b.Property<bool>("Twelve");

                    b.Property<bool>("TwelveThirty");

                    b.Property<bool>("Two");

                    b.Property<bool>("TwoThirty");

                    b.Property<int?>("WorkerID");

                    b.Property<DateTime>("dateTime");

                    b.HasKey("DayScheduleID");

                    b.HasIndex("WorkerID");

                    b.ToTable("DaySchedule");
                });

            modelBuilder.Entity("cs4540_final_project.Models.Worker", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Job");

                    b.Property<string>("Name");

                    b.Property<string>("Services");

                    b.Property<string>("UserId");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("Worker");
                });

            modelBuilder.Entity("cs4540_final_project.Models.WorkerComment", b =>
                {
                    b.Property<int>("WorkerCommentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<DateTime>("LastUpdated");

                    b.Property<int>("StarRating");

                    b.Property<int>("WorkerID");

                    b.Property<string>("name");

                    b.HasKey("WorkerCommentID");

                    b.HasIndex("WorkerID");

                    b.ToTable("WorkerComment");
                });

            modelBuilder.Entity("cs4540_final_project.Models.DaySchedule", b =>
                {
                    b.HasOne("cs4540_final_project.Models.Worker")
                        .WithMany("Schedule")
                        .HasForeignKey("WorkerID");
                });

            modelBuilder.Entity("cs4540_final_project.Models.Worker", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("cs4540_final_project.Models.WorkerComment", b =>
                {
                    b.HasOne("cs4540_final_project.Models.Worker", "Worker")
                        .WithMany("Reviews")
                        .HasForeignKey("WorkerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
