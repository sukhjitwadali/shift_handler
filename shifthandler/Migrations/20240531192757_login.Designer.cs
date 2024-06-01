﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using shifthandler.Data;

#nullable disable

namespace shifthandler.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240531192757_login")]
    partial class login
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("shifthandler.Models.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("ConfirmationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("ConfirmationGuid")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("InvitationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ShiftId")
                        .HasColumnType("int");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShiftId");

                    b.HasIndex("WorkerId");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("shifthandler.Models.Shifts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Location")
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Rate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Task")
                        .HasColumnType("longtext");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("time(6)");

                    b.HasKey("Id");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("shifthandler.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("shifthandler.Models.Invitation", b =>
                {
                    b.HasOne("shifthandler.Models.Shifts", "Shift")
                        .WithMany()
                        .HasForeignKey("ShiftId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("shifthandler.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Shift");

                    b.Navigation("Worker");
                });
#pragma warning restore 612, 618
        }
    }
}
