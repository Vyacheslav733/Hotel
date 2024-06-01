using System.ComponentModel.DataAnnotations;

namespace HotelDataBaseImplement.Models
{
    public class RoomLunch
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int LunchId { get; set; }

        [Required]
        public virtual Room Room { get; set; }
        public virtual Lunch Lunch { get; set; }
    }
}
