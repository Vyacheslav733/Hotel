using HotelDataModels.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace HotelContracts.ViewModels
{
    public class ConferenceBookingViewModel : IConferenceBookingModel
    {
        public int? ConferenceId { get; set; }
        public int HeadwaiterId { get; set; }
        public int Id { get; set; }

        [DisplayName("Дата начала бронирования")]
        public DateTime? BookingDate { get; set; }

        public string ConfName { get; set; } = string.Empty;
        public string NameHall { get; set; } = string.Empty;

        public Dictionary<int, ILunchModel> ConferenceBookingLunches { get; set; }
    }
}