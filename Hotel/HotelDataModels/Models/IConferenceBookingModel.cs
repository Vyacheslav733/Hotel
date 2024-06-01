namespace HotelDataModels.Models
{
    public interface IConferenceBookingModel : IId
    {
        int? ConferenceId { get; }
        int HeadwaiterId { get; }
        DateTime? BookingDate { get; }
        string NameHall { get; }
        public Dictionary<int, ILunchModel> ConferenceBookingLunches { get; }
    }
}