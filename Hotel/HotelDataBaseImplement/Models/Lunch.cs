using HotelContracts.BindingModels;
using HotelContracts.ViewModels;
using HotelDataModels.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDataBaseImplement.Models
{
    public class Lunch : ILunchModel
    {
        public int Id { get; set; }
        public int HeadwaiterId { get; set; }

        [Required]
        public string LunchName { get; set; } = string.Empty;

        [Required]  
        public double LunchPrice { get; set; }

        public virtual Headwaiter Headwaiter { get; set; }

        [ForeignKey("LunchId")]
        public virtual List<RoomLunch> RoomLunches { get; set; } = new();

        [ForeignKey("LunchId")]
        public virtual List<ConferenceBookingLunch> ConferenceBookingLunch { get; set; } = new();

        public static Lunch? Create(LunchBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Lunch()
            {
                Id = model.Id,
                LunchName = model.LunchName,
                LunchPrice = model.LunchPrice,
                HeadwaiterId = model.HeadwaiterId
            };
        }

        public static Lunch Create(LunchViewModel model)
        {
            return new Lunch
            {
                Id = model.Id,
                LunchName = model.LunchName,
                LunchPrice = model.LunchPrice,
                HeadwaiterId = model.HeadwaiterId
            };
        }

        public void Update(LunchBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            LunchName = model.LunchName;
            LunchPrice = model.LunchPrice;
            HeadwaiterId = model.HeadwaiterId;
        }

        public LunchViewModel GetViewModel => new()
        {
            Id = Id,
            LunchName = LunchName,
            LunchPrice = LunchPrice,
            HeadwaiterId = HeadwaiterId
        };
    }
}
