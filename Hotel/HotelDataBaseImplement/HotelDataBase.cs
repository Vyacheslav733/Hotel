using HotelDataBaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HotelDataBaseImplement
{
    public class HotelDataBase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-7DB3VEN\SQLEXPRESS;Initial Catalog=HotelDataBase;Integrated Security=True;MultipleActiveResultSets=True;;TrustServerCertificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }
        // DESKTOP-7DB3VEN 
        // LAPTOP-M2G96S06
        public virtual DbSet<Conference> Conferences { set; get; }
        public virtual DbSet<MealPlan> MealPlans { set; get; }
        public virtual DbSet<Member> Members { set; get; }
        public virtual DbSet<Organiser> Organisers { set; get; }
        public virtual DbSet<ConferenceMember> ConferenceMembers { set; get; }
        public virtual DbSet<MealPlanMember> MealPlanMembers { set; get; }
        public virtual DbSet<ConferenceBooking> ConferenceBookings { set; get; }
        public virtual DbSet<Lunch> Lunches { set; get; }
        public virtual DbSet<ConferenceBookingLunch> ConferenceBookingLunches { set; get; }
        public virtual DbSet<Room> Rooms { set; get; }
        public virtual DbSet<RoomLunch> RoomLunches { set; get; }
        public virtual DbSet<Headwaiter> Headwaiters { set; get; }
    }
}