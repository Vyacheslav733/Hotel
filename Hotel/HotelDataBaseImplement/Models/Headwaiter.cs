using HotelContracts.BindingModels;
using HotelContracts.ViewModels;
using HotelDataModels.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDataBaseImplement.Models
{
    public class Headwaiter : IHeadwaiterModel
    {
        public int Id { get; private set; }

        [Required]
        public string HeadwaiterSurname { get; set; } = string.Empty;

        [Required]
        public string HeadwaiterName { get; set; } = string.Empty;

        [Required]
        public string HeadwaiterPatronymic { get; set; } = string.Empty;

        [Required]
        public string HeadwaiterLogin { get; set; } = string.Empty;

        [Required]
        public string HeadwaiterPassword { get; set; } = string.Empty;

        [Required]
        public string HeadwaiterEmail { get; set; } = string.Empty;

        [Required]
        public string HeadwaiterPhoneNumber { get; set; } = string.Empty;

        [ForeignKey("HeadwaiterId")]
        public virtual List<Room> Rooms { get; set; } = new();

        [ForeignKey("HeadwaiterId")]
        public virtual List<Lunch> Lunches { get; set; } = new();

        [ForeignKey("HeadwaiterId")]
        public virtual List<ConferenceBooking> ConferenceBookings { get; set; } = new();

        public static Headwaiter? Create(HeadwaiterBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new Headwaiter()
            {
                Id = model.Id,
                HeadwaiterSurname = model.HeadwaiterSurname,
                HeadwaiterName = model.HeadwaiterName,
                HeadwaiterPatronymic = model.HeadwaiterPatronymic,
                HeadwaiterLogin = model.HeadwaiterLogin,
                HeadwaiterPassword = model.HeadwaiterPassword,
                HeadwaiterEmail = model.HeadwaiterEmail,
                HeadwaiterPhoneNumber = model.HeadwaiterPhoneNumber
            };
        }

        public static Headwaiter Create(HeadwaiterViewModel model)
        {
            return new Headwaiter
            {
                Id = model.Id,
                HeadwaiterSurname = model.HeadwaiterSurname,
                HeadwaiterName = model.HeadwaiterName,
                HeadwaiterPatronymic = model.HeadwaiterPatronymic,
                HeadwaiterLogin = model.HeadwaiterLogin,
                HeadwaiterPassword = model.HeadwaiterPassword,
                HeadwaiterEmail = model.HeadwaiterEmail,
                HeadwaiterPhoneNumber = model.HeadwaiterPhoneNumber
            };
        }

        public void Update(HeadwaiterBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            HeadwaiterSurname = model.HeadwaiterSurname;
            HeadwaiterName = model.HeadwaiterName;
            HeadwaiterPatronymic = model.HeadwaiterPatronymic;
            HeadwaiterLogin = model.HeadwaiterLogin;
            HeadwaiterPassword = model.HeadwaiterPassword;
            HeadwaiterEmail = model.HeadwaiterEmail;
            HeadwaiterPhoneNumber = model.HeadwaiterPhoneNumber;
        }

        public HeadwaiterViewModel GetViewModel => new()
        {
            Id = Id,
            HeadwaiterSurname = HeadwaiterSurname,
            HeadwaiterName = HeadwaiterName,
            HeadwaiterPatronymic = HeadwaiterPatronymic,
            HeadwaiterLogin = HeadwaiterLogin,
            HeadwaiterPassword = HeadwaiterPassword,
            HeadwaiterEmail = HeadwaiterEmail,
            HeadwaiterPhoneNumber = HeadwaiterPhoneNumber
        };
    }
}
