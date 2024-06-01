namespace HotelContracts.ViewModels
{
    public class ReportLunchesViewModel
    {
        public int Id { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public double RoomPrice { get; set; }
        public string ConferenceName { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public string LunchName { get; set; } = string.Empty;
        public double LunchPrice { get; set; }
    }
}
