using HotelContracts.BindingModels;
using HotelContracts.ViewModels;
using HotelDataModels.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDataBaseImplement.Models
{
    public class ConferenceBooking : IConferenceBookingModel
    {
        public int? ConferenceId { get; private set; }
        public int HeadwaiterId { get; private set; }
        public int Id { get; private set; }
        public string NameHall { get; set; } = string.Empty;
        public DateTime? BookingDate { get; set; }
        public virtual Headwaiter Headwaiter { get; set; }
        public virtual Conference? Conference { get; set; }

        [ForeignKey("ConferenceBookingId")]
        public virtual List<ConferenceBookingLunch> Lunches { get; set; }

        private Dictionary<int, ILunchModel> _conferenceBookingLunches = null;

        [NotMapped]
        public Dictionary<int, ILunchModel> ConferenceBookingLunches
        {
            get
            {
                if (_conferenceBookingLunches == null)
                {
					_conferenceBookingLunches = Lunches.ToDictionary(x => x.LunchId, x => (x.Lunch as ILunchModel));
				}
                return _conferenceBookingLunches;
            }
        }

        public static ConferenceBooking Create(HotelDataBase context, ConferenceBookingBindingModel model)
        {
            return new ConferenceBooking()
            {
                Id = model.Id,
                HeadwaiterId = model.HeadwaiterId,
                NameHall = model.NameHall,
                BookingDate = model.BookingDate,
                Lunches = model.ConferenceBookingLunches.Select(x => new ConferenceBookingLunch
                {
                    Lunch = context.Lunches.First(y => y.Id == x.Key),
                }).ToList()
            };
        }

        public void Update(ConferenceBookingBindingModel model)
        {
            ConferenceId = model.ConferenceId;
            NameHall = model.NameHall;
            BookingDate = model.BookingDate;
        }

        public ConferenceBookingViewModel GetViewModel => new()
        {
            Id = Id,
            ConferenceId = ConferenceId,
            HeadwaiterId = HeadwaiterId,
            NameHall = NameHall,
            BookingDate = BookingDate,
            ConferenceBookingLunches = ConferenceBookingLunches,
            ConfName = Conference?.ConferenceName
        };

        public void UpdateLunches(HotelDataBase context, ConferenceBookingBindingModel model)
        {
            var conferenceBookingLunches = context.ConferenceBookingLunches.Where(rec => rec.ConferenceBookingId == model.Id).ToList();

            if (conferenceBookingLunches != null && conferenceBookingLunches.Count > 0)
            {
                context.ConferenceBookingLunches.RemoveRange(conferenceBookingLunches.Where(rec => !model.ConferenceBookingLunches.ContainsKey(rec.LunchId)));
                context.SaveChanges();

                foreach (var updateLunch in conferenceBookingLunches)
                {
                    model.ConferenceBookingLunches.Remove(updateLunch.LunchId);
                }
                context.SaveChanges();
            }

            var conferenceBooking = context.ConferenceBookings.First(x => x.Id == Id);

            foreach (var cm in model.ConferenceBookingLunches)
            {
                context.ConferenceBookingLunches.Add(new ConferenceBookingLunch
                {
                    ConferenceBooking = conferenceBooking,
                    Lunch = context.Lunches.First(x => x.Id == cm.Key)
                });
                context.SaveChanges();
            }
            _conferenceBookingLunches = null;
        }
    }
}
