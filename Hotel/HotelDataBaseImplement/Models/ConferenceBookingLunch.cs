using System.ComponentModel.DataAnnotations;

namespace HotelDataBaseImplement.Models
{
    public class ConferenceBookingLunch
    {
        public int Id { get; set; }
        public int ConferenceBookingId { get; set; }
        public int LunchId { get; set; }

        [Required]
        public virtual ConferenceBooking ConferenceBooking { get; set; }
        public virtual Lunch Lunch { get; set; }
    }
}
