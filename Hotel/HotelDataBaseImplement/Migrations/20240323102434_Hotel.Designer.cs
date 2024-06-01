﻿// <auto-generated />
using System;
using HotelDataBaseImplement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelDataBaseImplement.Migrations
{
    [DbContext(typeof(HotelDataBase))]
    [Migration("20240323102434_Hotel")]
    partial class Hotel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotelDataBaseImplement.Models.Conference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConferenceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganiserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrganiserId");

                    b.ToTable("Conferences");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.ConferenceBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BookingDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ConferenceId")
                        .HasColumnType("int");

                    b.Property<int>("HeadwaiterId")
                        .HasColumnType("int");

                    b.Property<string>("NameHall")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ConferenceId");

                    b.HasIndex("HeadwaiterId");

                    b.ToTable("ConferenceBookings");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.ConferenceBookingLunch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ConferenceBookingId")
                        .HasColumnType("int");

                    b.Property<int>("LunchId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConferenceBookingId");

                    b.HasIndex("LunchId");

                    b.ToTable("ConferenceBookingLunches");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.ConferenceMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ConferenceId")
                        .HasColumnType("int");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConferenceId");

                    b.HasIndex("MemberId");

                    b.ToTable("ConferenceMembers");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Headwaiter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("HeadwaiterEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadwaiterLogin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadwaiterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadwaiterPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadwaiterPatronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadwaiterPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeadwaiterSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Headwaiters");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Lunch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("HeadwaiterId")
                        .HasColumnType("int");

                    b.Property<string>("LunchName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LunchPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("HeadwaiterId");

                    b.ToTable("Lunches");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.MealPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MealPlanName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MealPlanPrice")
                        .HasColumnType("float");

                    b.Property<int>("OrganiserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganiserId");

                    b.ToTable("MealPlans");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.MealPlanMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MealPlanId")
                        .HasColumnType("int");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MealPlanId");

                    b.HasIndex("MemberId");

                    b.ToTable("MealPlanMembers");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Member", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MemberName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberPatronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemberSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganiserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganiserId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Organiser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("OrganiserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganiserLogin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganiserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganiserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganiserPatronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganiserPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrganiserSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organisers");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("HeadwaiterId")
                        .HasColumnType("int");

                    b.Property<int>("MealPlanId")
                        .HasColumnType("int");

                    b.Property<string>("RoomFrame")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("RoomPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("HeadwaiterId");

                    b.HasIndex("MealPlanId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.RoomLunch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LunchId")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LunchId");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomLunches");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Conference", b =>
                {
                    b.HasOne("HotelDataBaseImplement.Models.Organiser", "Organiser")
                        .WithMany("Conferences")
                        .HasForeignKey("OrganiserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organiser");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.ConferenceBooking", b =>
                {
                    b.HasOne("HotelDataBaseImplement.Models.Conference", "Conference")
                        .WithMany("ConferenceBookings")
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelDataBaseImplement.Models.Headwaiter", "Headwaiter")
                        .WithMany("ConferenceBookings")
                        .HasForeignKey("HeadwaiterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conference");

                    b.Navigation("Headwaiter");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.ConferenceBookingLunch", b =>
                {
                    b.HasOne("HotelDataBaseImplement.Models.ConferenceBooking", "ConferenceBooking")
                        .WithMany("Lunches")
                        .HasForeignKey("ConferenceBookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelDataBaseImplement.Models.Lunch", "Lunch")
                        .WithMany("ConferenceBookingLunch")
                        .HasForeignKey("LunchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ConferenceBooking");

                    b.Navigation("Lunch");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.ConferenceMember", b =>
                {
                    b.HasOne("HotelDataBaseImplement.Models.Conference", "Conference")
                        .WithMany("Members")
                        .HasForeignKey("ConferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelDataBaseImplement.Models.Member", "Member")
                        .WithMany("ConferenceMembers")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Conference");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Lunch", b =>
                {
                    b.HasOne("HotelDataBaseImplement.Models.Headwaiter", "Headwaiter")
                        .WithMany("Lunches")
                        .HasForeignKey("HeadwaiterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Headwaiter");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.MealPlan", b =>
                {
                    b.HasOne("HotelDataBaseImplement.Models.Organiser", "Organiser")
                        .WithMany("MealPlans")
                        .HasForeignKey("OrganiserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organiser");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.MealPlanMember", b =>
                {
                    b.HasOne("HotelDataBaseImplement.Models.MealPlan", "MealPlan")
                        .WithMany("Members")
                        .HasForeignKey("MealPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelDataBaseImplement.Models.Member", "Member")
                        .WithMany("MealPlanMember")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MealPlan");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Member", b =>
                {
                    b.HasOne("HotelDataBaseImplement.Models.Organiser", "Organiser")
                        .WithMany("Members")
                        .HasForeignKey("OrganiserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organiser");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Room", b =>
                {
                    b.HasOne("HotelDataBaseImplement.Models.Headwaiter", "Headwaiter")
                        .WithMany("Rooms")
                        .HasForeignKey("HeadwaiterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelDataBaseImplement.Models.MealPlan", "MealPlan")
                        .WithMany("Rooms")
                        .HasForeignKey("MealPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Headwaiter");

                    b.Navigation("MealPlan");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.RoomLunch", b =>
                {
                    b.HasOne("HotelDataBaseImplement.Models.Lunch", "Lunch")
                        .WithMany("RoomLunches")
                        .HasForeignKey("LunchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HotelDataBaseImplement.Models.Room", "Room")
                        .WithMany("Lunches")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lunch");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Conference", b =>
                {
                    b.Navigation("ConferenceBookings");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.ConferenceBooking", b =>
                {
                    b.Navigation("Lunches");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Headwaiter", b =>
                {
                    b.Navigation("ConferenceBookings");

                    b.Navigation("Lunches");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Lunch", b =>
                {
                    b.Navigation("ConferenceBookingLunch");

                    b.Navigation("RoomLunches");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.MealPlan", b =>
                {
                    b.Navigation("Members");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Member", b =>
                {
                    b.Navigation("ConferenceMembers");

                    b.Navigation("MealPlanMember");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Organiser", b =>
                {
                    b.Navigation("Conferences");

                    b.Navigation("MealPlans");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("HotelDataBaseImplement.Models.Room", b =>
                {
                    b.Navigation("Lunches");
                });
#pragma warning restore 612, 618
        }
    }
}